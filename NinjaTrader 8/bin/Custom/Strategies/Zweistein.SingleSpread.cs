#region Using declarations
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Data;
using NinjaTrader.Gui.Chart;
using NinjaTrader.NinjaScript;
using Zweistein;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Zweistein.Spreads;
#endregion


namespace ibKastl.Helper
{
   public static class BinaryFormatterHelper
   {
      public static T Read<T>(MemoryStream stream, Assembly currentAssembly)
      {
         T retunValue;
         try
         {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Binder = new SearchAssembliesBinder(currentAssembly,true);            
            retunValue = (T)binaryFormatter.Deserialize(stream);
         }
         finally
         {
           // fileStream.Close();
         }

         return retunValue;
      }

      public static void Write<T>(T obj, MemoryStream stream)
      {
         BinaryFormatter formatter = new BinaryFormatter();
         try
         {
            formatter.Serialize(stream, obj);
         }
         finally
         {
           // stream.Close();
         }
      }
   }
   sealed class SearchAssembliesBinder : SerializationBinder
   {
      private readonly bool _searchInDlls;
      private readonly Assembly _currentAssembly;

      public SearchAssembliesBinder(Assembly currentAssembly, bool searchInDlls)
      {
         _currentAssembly = currentAssembly;
         _searchInDlls = searchInDlls;
      }

      public override Type BindToType(string assemblyName, string typeName)
      {
         List<AssemblyName> assemblyNames = new List<AssemblyName>();
         assemblyNames.Add(_currentAssembly.GetName()); // EXE

         if (_searchInDlls)
         {
		  	string[] keys = NinjaTrader.Core.Globals.AssemblyRegistry.Keys;
		    foreach(string k in keys ){
			// Zweistein.Diagnostics.Trace(k);
			 Assembly A = NinjaTrader.Core.Globals.AssemblyRegistry[k];
			 assemblyNames.Add(A.GetName());
	     	}
		 }
         foreach (AssemblyName an in assemblyNames)
         {
	        var typeToDeserialize = GetTypeToDeserialize(typeName, an);
            if (typeToDeserialize != null)
            {
               return typeToDeserialize; // found
            }
			//Zweistein.Diagnostics.Trace("typeToDeserialize("+ typeName+  ") = null:" + an.FullName);
			
         }

         return null; // not found
      }

      private static Type GetTypeToDeserialize(string typeName, AssemblyName an)
      {
         string fullTypeName = string.Format("{0}, {1}", typeName, an.FullName);
         var typeToDeserialize = Type.GetType(fullTypeName);
         return typeToDeserialize;
      }
   }

}


public enum PNLCalculatedBy {
		Last,
		Book
	}
public enum TargetExitQuantity
    {
        None,
        OneThird,
        Half,
        ThreeQuarters,
        All
    }
public interface IExitHandling
    {
        TargetExitQuantity ExitNow(Zweistein.Spreads.SingleSpread spread);
    }



namespace Zweistein.Spreads
{
    public enum LegEntryType
    {
        Market = 0,
        JoinBidOrAsk,
       // Payup1Tick
    }
    public enum LegExitType
    {
        Market = 0,
       JoinBidOrAsk,
       // Payup1Tick,
    }
	
	public class HandledOrder {
		NinjaTrader.Cbi.Order iorder;
		bool checkinflight;
		NinjaTrader.NinjaScript.StrategyBase strategybase;
		
		public void setStrategyBase(NinjaTrader.NinjaScript.StrategyBase nb){
			strategybase=nb;
		}
		
		public NinjaTrader.Cbi.Order Order {
			get {return iorder;}
			set {iorder=value;}
		}
		public bool CheckInflight {
		 get {return checkinflight;}
		 set {checkinflight=value;}
		}
		public void Cancel(bool _checkinflight){
			if(iorder==null) return;
			strategybase.CancelOrder(iorder);
			checkinflight=_checkinflight;
		}
		
		public void Zero(){
			if(strategybase.TraceOrders) {
				string tmp="";
				if(iorder!=null) tmp+=iorder.Name+":";
				tmp+="iorder==null";
				strategybase.Print(tmp);
			}
			iorder=null;
			checkinflight=false;
		}
		
    	public HandledOrder(NinjaTrader.NinjaScript.StrategyBase _strategybase){
			strategybase=_strategybase;	
		}

		public void OnOrderUpdate(NinjaTrader.Cbi.Order order){
			if(iorder==null) return;
			if(order.OrderId!=iorder.OrderId) return;
			if(order.OrderState==OrderState.Rejected){
				if (strategybase.TraceOrders) strategybase.Print(order.Name+":Order.Rejected");
			}
			if (order.OrderState == OrderState.Cancelled)
            {
             	
				if (strategybase.TraceOrders){
				    strategybase.Print(order.Name+": Order Cancelled");
                	if(checkinflight) strategybase.Print(order.Name+": checkinflight end");
				}
				checkinflight=false;
				iorder=null;
			}
			
		}
		
		public bool Inflight(NinjaTrader.Cbi.Execution execution){
			if(checkinflight){
					if(strategybase.TraceOrders) strategybase.Print("WARNING: inflight execution:"+execution.MarketPosition.ToString()+ " "+execution.Quantity.ToString()+" "+execution.Instrument.FullName);
					strategybase.Print("Inflight execution:"+execution.MarketPosition.ToString()+ " "+execution.Quantity.ToString()+" "+execution.Instrument.FullName+ ",LogLevel.Warning");
					if(execution.Order.Filled==execution.Order.Quantity){
						if(strategybase.TraceOrders) strategybase.Print("end inflight");
						checkinflight=false;
					}
					return true;
				}
			return false;
		}
	}
	
	public partial class SingleSpread 
    {
		
	
   		int _toexitX=0;
		int _toexitY=0;
		int _exitedX=0;
		int _exitedY=0;
        
		 NinjaTrader.Cbi.Currency basecurrency;
		
		IExitHandling iexithandling;
        int units = 1;

        int lots1;
        int lots2;
	
		bool exitinprogress=false;
		
        int positionX = 0;
        int positionY = 0;

        PNLCalculatedBy pnlby = PNLCalculatedBy.Last;

        double totalprice0 = 0;
        double totalprice1 = 0;
		
		decimal convfactor0=1;
		decimal convfactor1=1;
		
		Guid guid=Guid.Empty;
        MarketPosition mleg1 = MarketPosition.Flat;
		NinjaTrader.NinjaScript.StrategyBase strategybase;
       
		List<NinjaTrader.Cbi.Execution> inflight=new List<NinjaTrader.Cbi.Execution>();
	
		HandledOrder[] entry;
		HandledOrder[] exit;
		
        public SingleSpread(ref Queue<SingleSpread> transferfrom,NinjaTrader.Cbi.Currency currency)
        {
            int tmpunits = 0;
            Zweistein.FractionString f = null;
			
			SetBaseCurrency(currency);
			
            List<SingleSpread> toremove = new List<SingleSpread>();
            foreach (SingleSpread s in transferfrom)
            {
                if (s.exit[0].Order != null || s.exit[1].Order != null) continue;
                if (s.mleg1 == NinjaTrader.Cbi.MarketPosition.Long)
                {
                    if (s.positionX != s.lots1) continue;
                    if (-1 * s.positionY != s.lots2) continue;
                }
                if (s.mleg1 == NinjaTrader.Cbi.MarketPosition.Short)
                {
                    if (-1 * s.positionX != s.lots1) continue;
                    if (s.positionY != s.lots2) continue;
                }
                if (s.mleg1 == NinjaTrader.Cbi.MarketPosition.Flat) continue;
                if (s.MarketPosition == NinjaTrader.Cbi.MarketPosition.Long) tmpunits += s.NUnits;
                if (s.MarketPosition == NinjaTrader.Cbi.MarketPosition.Short) tmpunits -= s.NUnits;
                totalprice0 += (double) Zweistein.CurrencyConversion.Cube.Convert((decimal)s.totalprice0,Zweistein.CurrencyConversion.ISO(s.BaseCurrency),Zweistein.CurrencyConversion.ISO(basecurrency));
                totalprice1 += (double) Zweistein.CurrencyConversion.Cube.Convert((decimal)s.totalprice1,Zweistein.CurrencyConversion.ISO(s.BaseCurrency),Zweistein.CurrencyConversion.ISO(basecurrency));

                positionX += s.positionX;
                positionY += s.positionY;
                if (s.units > 0)
                {
                    int k1 = s.lots1 / s.units;
                    int k2 = s.lots2 / s.units;
                    if (f == null)
                    {
                        f = new Zweistein.FractionString(k1.ToString() + ":" + k2.ToString(), ':');
                        strategybase = s.strategybase;
                        
                    }
                }
                toremove.Add(s);
            }




            if (f == null) throw new ArgumentNullException("SingleSpread(ref Queue<SingleSpread> spreads) , spreads is empty collection");
            if (tmpunits > 0) this.mleg1 = NinjaTrader.Cbi.MarketPosition.Long;
            else { if (tmpunits < 0) this.mleg1 = NinjaTrader.Cbi.MarketPosition.Short; }

            if (tmpunits == 0) throw new ArgumentNullException("SingleSpread(ref Queue<SingleSpread> spreads) , tmpunits == 0");
            units = Math.Abs(tmpunits);
            lots1 = (int)f.Nominator * units;
            lots2 = (int)f.Denominator * units;
            if (lots1 == 0 || lots2 == 0) throw new ArgumentNullException("SingleSpread , incomplete spread defintion: lots1 or lots2 == 0");

            foreach (SingleSpread r in toremove)
            {
                for (int i = 0; i < transferfrom.Count; i++)
                {
                    SingleSpread q = transferfrom.Dequeue();
                    if (q == r) break;
                    transferfrom.Enqueue(q);
                }
            }

        }
		
		
		public void SetBaseCurrency(NinjaTrader.Cbi.Currency currency){

            Zweistein.NinjaTraderLog.Process("SetBaseCurrency:", "old:" + basecurrency.ToString() + " new:" + currency.ToString(), LogLevel.Information, LogCategories.Strategy);
			basecurrency=currency;
			decimal oldconvfactor0=convfactor0;
			decimal oldconvfactor1=convfactor1;
			
			convfactor0=Zweistein.CurrencyConversion.Cube.Convert((decimal) 1.0,
					Zweistein.CurrencyConversion.ISO(strategybase.Instruments[0].MasterInstrument.Currency),
							Zweistein.CurrencyConversion.ISO(basecurrency));
			
			
			convfactor1=Zweistein.CurrencyConversion.Cube.Convert((decimal) 1.0,
					Zweistein.CurrencyConversion.ISO(strategybase.Instruments[1].MasterInstrument.Currency),
							Zweistein.CurrencyConversion.ISO(basecurrency));
			
			totalprice0*=(double) (convfactor0/oldconvfactor0);
			totalprice1*=(double) (convfactor1/oldconvfactor1);
			
			
		}
        public SingleSpread(MarketPosition leg1, int _units, string strratio, NinjaTrader.NinjaScript.StrategyBase _strategybase)
        {
            mleg1 = leg1;
		    strategybase = _strategybase;
            units = _units;
		    Zweistein.FractionString f = new Zweistein.FractionString(strratio, ':');
			lots1 = (int)f.Nominator * units;
            lots2 = (int)f.Denominator * units;
            if (lots1 == 0 || lots2 == 0) throw new ArgumentNullException("SingleSpread , incomplete spread defintion: lots1 or lots2 == 0");
					
			SetBaseCurrency(_strategybase.Account.Denomination);
			
						
			
			entry=new HandledOrder[]{new HandledOrder(strategybase),new HandledOrder(strategybase)};
			exit=new HandledOrder[]{new HandledOrder(strategybase),new HandledOrder(strategybase)};
			
		
        }
        public void Dispose() { }

        public TargetExitQuantity ExitNow()
        {
               if (iexithandling != null) return iexithandling.ExitNow(this);
                return TargetExitQuantity.None;
          
        }

		public Guid Guid {
			get {return guid;}
			set { guid=value;}
		}
		
        public int Units(TargetExitQuantity teq)
        {

            if (teq == TargetExitQuantity.Half) return Math.Max(1, (int)(units / 2));
            if (teq == TargetExitQuantity.None) return 0;
            if (teq == TargetExitQuantity.OneThird) return Math.Max(1, (int)(units / 3));
            if (teq == TargetExitQuantity.ThreeQuarters) return Math.Max(1, (int)(3 * units / 4));
            return units;

        }

		public double  EntryPricePerUnit(double leg1pricemultiplier,double leg2pricemultiplier){
            decimal a = (decimal)(totalprice0 / strategybase.Instruments[0].MasterInstrument.PointValue) / convfactor0/lots1;
            decimal b= (decimal)(totalprice1 / strategybase.Instruments[1].MasterInstrument.PointValue) / convfactor1/lots2;
            decimal d = (decimal) leg1pricemultiplier* lots1* a + (decimal)leg2pricemultiplier * lots2*b;
           	if(units==0) return 0;
			return (double) ( d/units);
			
		}
        public void OnOrderUpdate(Order order)
        {
			
			entry[0].OnOrderUpdate(order);
			entry[1].OnOrderUpdate(order);
			exit[0].OnOrderUpdate(order);
			exit[1].OnOrderUpdate(order);
        }

       
        public bool IsOurExecution(NinjaTrader.Cbi.Execution execution)
        {
            if (entry[0].Order != null && execution.Order.OrderId == entry[0].Order.OrderId) return true;
            if (entry[1].Order != null && execution.Order.OrderId == entry[1].Order.OrderId) return true;
            if (exit[0].Order != null && execution.Order.OrderId == exit[0].Order.OrderId) return true;
            if (exit[1].Order != null && execution.Order.OrderId == exit[1].Order.OrderId) return true;
            return false;
        }
        public void OnExecution(NinjaTrader.Cbi.Execution execution)
        {
          	
			if(execution==null) return;
			if (execution.Order == null) return;
			int sign = (execution.MarketPosition == MarketPosition.Long) ? 1 : -1;
			if(entry[0].Order!=null && execution.Order.OrderId == entry[0].Order.OrderId)
            {
				
				if(entry[0].Inflight(execution)) {
					inflight.Add(execution);
					// we assume that an inflight execution will never come as a collection of
					// partial executions. Only the first partial will stay inflight, a second
					// partial would be cancelled
					return;
				}
				positionX += sign * execution.Quantity;
                totalprice0 += (double)convfactor0 * execution.Price * sign * execution.Quantity * strategybase.Instruments[0].MasterInstrument.PointValue;
				var commission=(double)convfactor0 *execution.Commission;
				totalprice0+=commission;
					
				int lsign=(mleg1==MarketPosition.Long)?+1:-1;
			
					if(strategybase.State==State.Realtime) CollectTokens(execution);
				
				if(lots1*lsign==positionX) 	entry[0].Zero();
			    
				
            }
            if (entry[1].Order != null && execution.Order.OrderId == entry[1].Order.OrderId)
            {
				
				
				if(entry[1].Inflight(execution)) {
					inflight.Add(execution);
					return;
				}
              	positionY += sign * execution.Quantity;
                totalprice1 += (double)convfactor1 *execution.Price * sign * execution.Quantity*strategybase.Instruments[1].MasterInstrument.PointValue;
				var commission=(double)convfactor1 *execution.Commission;
				totalprice1+=commission;
		
				
				int lsign=(mleg1==MarketPosition.Short)?+1:-1;
				
				if(strategybase.State==State.Realtime) CollectTokens(execution);
				
				if(lots2*lsign==positionY) entry[1].Zero();
            }
            if (exit[0].Order != null && execution.Order.OrderId == exit[0].Order.OrderId)
            {
             	
				if(exit[0].Inflight(execution)) {
					inflight.Add(execution);
					return;
				}
				positionX += sign * execution.Quantity;
                totalprice0 += (double)convfactor0* execution.Price * sign * execution.Quantity*strategybase.Instruments[0].MasterInstrument.PointValue;
				var commission=(double)convfactor0 *execution.Commission;
				totalprice0+=commission;
				_exitedX+=sign * execution.Quantity;
				
				if(strategybase.State==State.Realtime) CollectTokens(execution);
            }
            if (exit[1].Order != null && execution.Order.OrderId == exit[1].Order.OrderId)
            {
               exitinprogress=false;
				if(exit[1].Inflight(execution)) {
					inflight.Add(execution);
					return;
				}
				positionY += sign * execution.Quantity;
                totalprice1 += (double) convfactor1* execution.Price * sign * execution.Quantity*strategybase.Instruments[1].MasterInstrument.PointValue;
				var commission=(double)convfactor1 *execution.Commission;
				totalprice1+=commission;
				_exitedY+=sign * execution.Quantity;
				
				if(strategybase.State==State.Realtime) CollectTokens(execution);
            }

            if (exit[0].Order != null && execution.Order.OrderId == exit[0].Order.OrderId)
            {
             	if(_exitedX==-1*_toexitX) 	exit[0].Zero();
				
			
            }

            if (exit[1].Order != null && execution.Order.OrderId== exit[1].Order.OrderId)
            {
            	if(_exitedY==-1*_toexitY) 	exit[1].Zero();
				
              
            }
			if(exit[0].Order==null && exit[1].Order==null) exitinprogress=false;

            if (strategybase.TraceOrders) {
				//strategybase.Print(execution.ToString());
                string tmp = "Spread.OnExecution: Leg1(" + positionX.ToString() + "),Leg2(" + positionY.ToString() + ")";
				strategybase.Print(tmp);
                Zweistein.NinjaTraderLog.Process(tmp, "", LogLevel.Information, LogCategories.Strategy);

            }
		
			

        }

        public void Entry(LegEntryType et1,LegEntryType et2)
        {
            if (entry[0].Order != null || entry[1].Order != null) return;
            if (exit[0].Order != null || exit[1].Order != null) return;
			
            if (mleg1 == MarketPosition.Flat) return;

            double limitleg1 = 0;
            double limitleg2 = 0;

            if (et1 == LegEntryType.Market) limitleg1 = 0;
            if (et2 == LegEntryType.Market) limitleg2 = 0;

            OrderAction[] o = { OrderAction.Buy, OrderAction.SellShort };
            string[] o_name = { "Buy_X", "SellShort_Y" };

            if (mleg1 == MarketPosition.Long)
            {

                if (et1 == LegEntryType.JoinBidOrAsk) limitleg1 = strategybase.GetCurrentBid(0);
                if (et2 == LegEntryType.JoinBidOrAsk) limitleg2 = strategybase.GetCurrentAsk(1);

            }

            if (mleg1 == MarketPosition.Short)
            {
                o = new OrderAction[] { OrderAction.SellShort, OrderAction.Buy };
                o_name = new String[] { "SellShort_X", "Buy_Y" };

                if (et1 == LegEntryType.JoinBidOrAsk) limitleg1 = strategybase.GetCurrentAsk(0);
                if (et2 == LegEntryType.JoinBidOrAsk) limitleg2 = strategybase.GetCurrentBid(1);
            }

            if (strategybase.TraceOrders) strategybase.Print(strategybase.Time[0].ToString() + " Entry:" + strategybase.Instruments[0].FullName + " " + o_name[0] + "; " + strategybase.Instruments[1].FullName + " " + o_name[1]);


           if(lots1!=0) entry[0].Order = strategybase.SubmitOrderUnmanaged(0, o[0], (limitleg1 == 0) ? OrderType.Market : OrderType.Limit, lots1, limitleg1, 0, "", o_name[0]);
           if(lots2!=0) entry[1].Order = strategybase.SubmitOrderUnmanaged(1, o[1], (limitleg2 == 0) ? OrderType.Market : OrderType.Limit, lots2, limitleg2, 0, "", o_name[1]);
        }

        public void Reverse()
        {
            if (strategybase.TraceOrders)
            {
                strategybase.Print("Reverse, old: Leg1(" + positionX.ToString() + "),Leg2(" + positionY.ToString() + ")");
                if (positionX == 0 || positionY == 0)
                {
                    strategybase.Print("ERROR: SimpleSpread.Reverse() no position in leg1 or leg2");
					return;
                }
            }
            int tmp1=lots1*2;
            int tmp2=lots2*2;

			int c1=0;
			int c2=0;
            // this will correct for unfilled orders
            if (mleg1 == NinjaTrader.Cbi.MarketPosition.Long)
            {
                c1= (lots1 - positionX);
                c2= (lots2 + positionY);
            }

            if (mleg1 == NinjaTrader.Cbi.MarketPosition.Short)
            {
                c1= (lots1 + positionX);
                c2= (lots2 - positionY);
            }
			
			//c1=c2=0; // disable unfilled order correction
			
			
                if (c1!= 0 || c2!=0){
                    string f = "SingleSpread.Reverse():";
                    string msg= "unfilled orders:leg1("+c1.ToString()+"),leg2("+c2.ToString()+")";
                    Zweistein.NinjaTraderLog.Process(f, msg, LogLevel.Error, LogCategories.User);
                     if (strategybase.TraceOrders) strategybase.Print("ERROR in " + f + msg);
					return;
           		 }
			
			tmp1-=c1;
			tmp2-=c2;
			

            ExitLots(tmp1,tmp2, LegExitType.Market, LegExitType.Market);
            if (mleg1 == NinjaTrader.Cbi.MarketPosition.Long)
            {
                mleg1 = NinjaTrader.Cbi.MarketPosition.Short;
            }
            else
            {
                if (mleg1 == NinjaTrader.Cbi.MarketPosition.Short) mleg1 = NinjaTrader.Cbi.MarketPosition.Long;
            }

        }

        public void PartialExit(int units2exit, LegExitType et1,LegExitType et2)
        {
            if (entry[0].CheckInflight) return;
            if (entry[1].CheckInflight) return;
            if (exit[0].CheckInflight) return;
            if (exit[0].CheckInflight) return;
            if (exitinprogress) return;

            if (strategybase.TraceOrders) strategybase.Print("PartialExit, " + units2exit.ToString() + " units2exit");

            if (units2exit == 0) return;
            if (units2exit > units)
            {
                string tmp = "units2exit(" + units2exit.ToString() + ") can only exit " + units.ToString() + " units";
                string f = "SingleSpread.PartialExit:";
                Zweistein.NinjaTraderLog.Process(f, tmp, LogLevel.Error, LogCategories.Strategy);
                if (strategybase.TraceOrders) strategybase.Print("ERROR " + f + tmp);
                return;
            }


            exitinprogress = true;
            //FixInFlight();
            int tmp1 = lots1*units2exit;
            int tmp2 = lots2*units2exit;


            // this will correct for unfilled orders
            if (mleg1 == NinjaTrader.Cbi.MarketPosition.Long)
            {
                tmp1 -= (lots1 - positionX);
                tmp2 -= (lots2 + positionY);
            }

            if (mleg1 == NinjaTrader.Cbi.MarketPosition.Short)
            {
                tmp1 -= (lots1 + positionX);
                tmp2 -= (lots2 - positionY);
            }
            ExitLots(tmp1, tmp2, et1, et2);
        //    units -= units2exit;
            lots1 = tmp1;
            lots2 = tmp2;

        }

        public void Exit(LegExitType et1, LegExitType et2)
        {
			if(entry[0].CheckInflight) return;
			if(entry[1].CheckInflight) return;
			if(exit[0].CheckInflight) return;
			if(exit[0].CheckInflight) return;
			if(exitinprogress) return;
			exitinprogress=true;
			//FixInFlight();
            int tmp1 = lots1;
            int tmp2 = lots2;
            // this will correct for unfilled orders
            if (mleg1 == NinjaTrader.Cbi.MarketPosition.Long)
            {
                tmp1 -= (lots1 - positionX);
                tmp2 -= (lots2 + positionY);
            }

            if (mleg1 == NinjaTrader.Cbi.MarketPosition.Short)
            {
                tmp1 -= (lots1 + positionX);
                tmp2 -= (lots2 - positionY);
            }
            ExitLots(tmp1, tmp2, et1, et2);
        }

        public void ExitLots(int _lots1, int _lots2, LegExitType et1, LegExitType et2)
        {
			
            if (mleg1 == NinjaTrader.Cbi.MarketPosition.Long){
					_toexitX=_lots1;
					_toexitY=-_lots2;
            }
            if (mleg1 == NinjaTrader.Cbi.MarketPosition.Short){
				
					_toexitX=-_lots1;
					_toexitY=_lots2;
            }
            entry[0].Cancel(!exitinprogress);
			entry[1].Cancel(!exitinprogress);
			
            exit[0].Cancel(true);
			exit[1].Cancel(true);
            
			double limitleg1 = 0;
            double limitleg2 = 0;

            OrderAction[] o = { OrderAction.Sell, OrderAction.BuyToCover };
            string[] o_name = { "Sell_X", "BuyToCover_Y" };

            if (mleg1 == MarketPosition.Long)
            {

                if (et1 == LegExitType.JoinBidOrAsk) {
					if(_lots1>0)  limitleg1 = strategybase.GetCurrentAsk(0);
					if(_lots1<0)  limitleg1 = strategybase.GetCurrentBid(0);
				}
                if (et2 == LegExitType.JoinBidOrAsk) {
					if(_lots2>0) limitleg2 = strategybase.GetCurrentBid(1);
					if(_lots2<0) limitleg2 = strategybase.GetCurrentAsk(1);
					
				}

            }

            if (mleg1 == MarketPosition.Short)
            {
                o = new OrderAction[] { OrderAction.Buy, OrderAction.SellShort };
                o_name = new String[] { "BuyToCover_X", "Sell_Y" };

                if (et1 == LegExitType.JoinBidOrAsk) {
					
				 if(_lots1>0) 	limitleg1 = strategybase.GetCurrentBid(0);
				 if(_lots1<0) 	limitleg1 = strategybase.GetCurrentAsk(0);
				}
                if (et2 == LegExitType.JoinBidOrAsk) {
					
					if(_lots2>0)	limitleg2 = strategybase.GetCurrentAsk(1);
					if(_lots2<0)	limitleg2 = strategybase.GetCurrentBid(1);
				}

            }


            if (strategybase.TraceOrders)
            {
                string tmp = strategybase.Time[0].ToString();
                if (positionX != 0) tmp += " Exit:" + strategybase.Instruments[0].FullName + " " + o_name[0];
                if (positionY != 0) tmp += "; " + strategybase.Instruments[1].FullName + " " + o_name[1];
                if (positionX != 0 || positionY != 0) strategybase.Print(tmp);

            }
		
            if (_lots1 > 0)
            {
            	_exitedX=0;
				exit[0].Order = strategybase.SubmitOrderUnmanaged(0, o[0], (limitleg1 == 0) ? OrderType.Market : OrderType.Limit, _lots1, limitleg1, 0, "", o_name[0]);
            }
			else if(_lots1 < 0){
				if (strategybase.TraceOrders) strategybase.Print("WARNING: Compensating overfill Leg1("+ (-1*_lots1).ToString()+")");
				exit[0].Order = strategybase.SubmitOrderUnmanaged(0, o[1], (limitleg1 == 0) ? OrderType.Market : OrderType.Limit, -_lots1, limitleg1, 0, "", o_name[0]);

			}
			
            if (_lots2 > 0)
            {
               _exitedY=0;
				exit[1].Order = strategybase.SubmitOrderUnmanaged(1, o[1], (limitleg2 == 0) ? OrderType.Market : OrderType.Limit, _lots2, limitleg2, 0, "", o_name[1]);

            }
			else if(_lots2 < 0){
				if (strategybase.TraceOrders) strategybase.Print("WARNING: Compensating overfill Leg2("+ (-1*_lots2).ToString()+")");
				exit[1].Order = strategybase.SubmitOrderUnmanaged(1, o[0], (limitleg2 == 0) ? OrderType.Market : OrderType.Limit, -_lots2, limitleg2, 0, "", o_name[1]);

				
			}


        }
		
		  public string Status()
        {

                if (positionX == 0 && positionY == 0) return "Flat";
                string status = string.Empty;
                int yettofillX = 0;
                int yettofillY = 0;
				if(exitinprogress) status+="Exit in progress ";
                if (mleg1 == NinjaTrader.Cbi.MarketPosition.Long)
                {
                    yettofillX = lots1 - positionX;
                    yettofillY = lots2 + positionY;
                }
                if (mleg1 == NinjaTrader.Cbi.MarketPosition.Short)
                {
                    yettofillX = lots1 + positionX;
                    yettofillY = lots2 - positionY;

                }
                if (yettofillX != 0) status += "Leg1 unfilled(" + yettofillX.ToString() + ")";
                if (yettofillY != 0)
                {
                    if (!string.IsNullOrEmpty(status)) status += ", ";
                    status += "Leg2 unfilled(" + yettofillY.ToString() + ")";
                }
			//	if(strategybase!=null && strategybase.TraceOrders) {
			//		if(status.Contains("unfilled"))	strategybase.Print(status);
			//	}
                return status;
        
        }

		public string OnBarUpdateStatus()
        {
            
                int units2exit = Units(ExitNow());
                if (units2exit > 0) PartialExit(units2exit, LegExitType.Market, LegExitType.Market);
                return Status();
        }
		   public double RealizedPnl()
        {
            return -totalprice0 - totalprice1;

        }
        public double PnlPerUnit()
        {
         
                double marketvalue = 0;
				      marketvalue =(double)convfactor0*strategybase.Instruments[0].MasterInstrument.PointValue* positionX * MarketPrice0(positionX)
							+(double)convfactor1*strategybase.Instruments[1].MasterInstrument.PointValue* positionY * MarketPrice1(positionY);
                return (marketvalue - totalprice0 - totalprice1) / units;
        
        }


        public double MarketPrice0(int net)
        {
            if (net == 0 || pnlby == PNLCalculatedBy.Last) return strategybase.Closes[0][0];
#if LEVEL2_DATA

			if(net>0)	return biddepth[0].AvgFillPrice(-net);
			if(net<0) return askdepth[0].AvgFillPrice(-net);
#endif
            return 0;

        }

        public double MarketPrice1(int net)
        {
            if (net == 0 || pnlby == PNLCalculatedBy.Last) return strategybase.Closes[1][0];
#if LEVEL2_DATA
	
			if(net>0)	return askdepth[1].AvgFillPrice(net);
			if(net<0) return biddepth[1].AvgFillPrice(net);
#endif
            return 0;
        }

        protected void FixInFlight(){
			if(inflight.Count>0){
					foreach(NinjaTrader.Cbi.Execution iexec in inflight){
						int i=-1;
						if(strategybase.Instruments[0]==iexec.Instrument) i=0;
						if(strategybase.Instruments[1]==iexec.Instrument) i=1;
						OrderAction action=OrderAction.Sell;
						if(iexec.MarketPosition==MarketPosition.Short) action=OrderAction.BuyToCover;
						if(i==0 || i==1){ 
							strategybase.SubmitOrderUnmanaged(i,action,OrderType.Market,iexec.Quantity,0,0,"","inflight remove");
							//strategybase.Log("Inflight remove: "+action.ToString()+" "+iexec.Quantity.ToString()+" "+strategybase.Instruments[i].FullName,LogLevel.Warning);
							strategybase.Print("WARNING: Inflight remove: "+action.ToString()+" "+iexec.Quantity.ToString()+" "+strategybase.Instruments[i].FullName);

							}
						
					}
					inflight.Clear();
					
				}
			
		}
		
        public bool Finished()
        {
         
				FixInFlight();
                if (positionX == 0 && positionY == 0 && entry[0].Order== entry[1].Order && exit[0].Order == exit[1].Order
                        && entry[0].Order == null && exit[0].Order == null) 
				{
					Leg1Tokens.Clear();
					Leg2Tokens.Clear();
					if(strategybase.TraceOrders) strategybase.Print("Spread.Finished");
					exitinprogress=false;
					return true;
					
				}

                return false;
          
        }
        
		 public void setStrategyBase(NinjaTrader.NinjaScript.StrategyBase s)
        {
            strategybase=s;
			SetBaseCurrency(s.Account.Denomination);
			
			 if (!string.IsNullOrEmpty(iehdata))
                {
                    try
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        MemoryStream memstr = new MemoryStream(Convert.FromBase64String(iehdata), true);
                    	iexithandling=ibKastl.Helper.BinaryFormatterHelper.Read<IExitHandling>(memstr, Assembly.GetExecutingAssembly());
                        Zweistein.NinjaTraderLog.Process("SingleSpread.setStrategyBase:", iexithandling.ToString(), NinjaTrader.Cbi.LogLevel.Warning, NinjaTrader.Cbi.LogCategories.Strategy);

                }
                catch (Exception e) {
						Zweistein.NinjaTraderLog.Process("SingleSpread.setStrategyBase:",e.Message, NinjaTrader.Cbi.LogLevel.Warning, NinjaTrader.Cbi.LogCategories.Strategy);
						if(strategybase.TraceOrders) strategybase.Print(e.Message+" SOURCE:"+e.Source);
					}
                }
			
			string tmp="iexithandling=";
			if(iexithandling==null) { 
				Zweistein.NinjaTraderLog.Process("SingleSpread.setStrategyBase:","iexithandling==null", NinjaTrader.Cbi.LogLevel.Warning, NinjaTrader.Cbi.LogCategories.Strategy);
			
				tmp+="null";
			}
			else tmp+=iexithandling.ToString();
			
			if(strategybase.TraceOrders) strategybase.Print(tmp);
			entry[0].setStrategyBase(s);
			entry[1].setStrategyBase(s);
			exit[0].setStrategyBase(s);
			exit[1].setStrategyBase(s);
        }

		public NinjaTrader.NinjaScript.StrategyBase StrategyBase()
        {
            return strategybase;
        }
		
		public IExitHandling getIExitHandling(){
			
			return 	iexithandling;
		}
       
         public void setIExitHandling(IExitHandling ieh)
        {
                iexithandling = ieh;
                if (ieh == null)
                {
                    iehdata = null;
                    return;
                }
				try {
					byte[] mem=new byte[32768];
                	MemoryStream memstr=new MemoryStream(mem,true);
                	ibKastl.Helper.BinaryFormatterHelper.Write(iexithandling,memstr);
                    //Zweistein.NinjaTraderLog.Process("setIExitHandling:", iexithandling.ToString(), NinjaTrader.Cbi.LogLevel.Warning, NinjaTrader.Cbi.LogCategories.Strategy);
                    byte[] used = new byte[(int)memstr.Position];
                    byte[] res=memstr.ToArray();
                    for (int i = 0; i < (int)memstr.Position; i++)
                    {
                        used[i]=res[i];
                    }
					iehdata=Convert.ToBase64String(used,Base64FormattingOptions.None);
				}
				catch(Exception e){
                    
					Zweistein.NinjaTraderLog.Process("setIExitHandling:",e.Message , NinjaTrader.Cbi.LogLevel.Warning, NinjaTrader.Cbi.LogCategories.Strategy);
                    if (strategybase.TraceOrders) strategybase.Print(e.Message + " " + e.Source);
            }
				
     
        }

       
 		#region properties
		
        public NinjaTrader.Cbi.MarketPosition MarketPosition
        {
            get { return mleg1; }
			set {mleg1=value;}
        }

        public int NUnits
        {
            get { return units; }
			set {units=value;}

        }
		
		public double Totalprice0 {
			get {return totalprice0;}
			set {totalprice0=value;}
		}
		
		public double Totalprice1 {
			get {return totalprice1;}
			set {totalprice1=value;}
		}
		
		public NinjaTrader.Cbi.Currency BaseCurrency {
			
				get {return basecurrency;}
				set {basecurrency=value;}
			
		}
		
		public decimal ConvFactor0 {
			
				get { return convfactor0;}
				set {convfactor0=value;}
		}
		
		public decimal ConvFactor1 {
			
				get { return convfactor1;}
				set {convfactor1=value;}
		}
		

     
		
		#endregion
    }




}