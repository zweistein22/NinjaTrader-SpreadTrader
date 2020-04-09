#region Using declarations
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Documents;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using NinjaTrader.Cbi;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.SuperDom;
using NinjaTrader.Gui.Tools;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript.Indicators;
using NinjaTrader.NinjaScript.DrawingTools;
using Zweistein.Spreads;
using Zweistein;
using System.Diagnostics;
using System.Reflection;
#endregion


namespace Zweistein
{
	public class SpreadExitStrategyPtr2Members {

        StrategyBase _strategybase;

        MethodInfo  golongclick;
        MethodInfo goshortclick;
        MethodInfo reverseclick;
        MethodInfo closeclick;

        MethodInfo  ieh;

        public void GoLong_Click(object sender, RoutedEventArgs e) {
            golongclick = _strategybase.GetType().GetMethod("GoLong_Click");
            golongclick.Invoke(_strategybase, new object[] { sender, e });
        }
		public void  GoShort_Click(object sender, RoutedEventArgs e){
            MethodInfo goshortclick = _strategybase.GetType().GetMethod("GoShort_Click");
            goshortclick.Invoke(_strategybase, new object[] { sender, e });
        }
		public void Reverse_Click(object sender, RoutedEventArgs e){
            MethodInfo reverseclick = _strategybase.GetType().GetMethod("Reverse_Click");
            reverseclick.Invoke(_strategybase, new object[] { sender, e });
        }
        public void Close_Click(object sender, RoutedEventArgs e){
            MethodInfo closeclick = _strategybase.GetType().GetMethod("Close_Click");
            closeclick.Invoke(_strategybase, new object[] { sender, e });
        }

        public string QuantityRatio;
		public double Leg1PriceDisplayMultiplier;
		public double Leg2PriceDisplayMultiplier;
		public string PriceString;
		public string strLeg2Instrument;
		

		public SpreadExitStrategyPtr2Members(StrategyBase strategybase){
            _strategybase = strategybase;
            PropertyInfo pi=_strategybase.GetType().GetProperty("QuantityRatio");
            QuantityRatio = (string) pi.GetValue(_strategybase);

            pi = _strategybase.GetType().GetProperty("Leg1PriceDisplayMultiplier");
            Leg1PriceDisplayMultiplier = (double)pi.GetValue(_strategybase);

            pi = _strategybase.GetType().GetProperty("Leg2PriceDisplayMultiplier");
            Leg2PriceDisplayMultiplier = (double)pi.GetValue(_strategybase);

            pi = _strategybase.GetType().GetProperty("PriceString");
            PriceString = (string)pi.GetValue(_strategybase);

            pi = _strategybase.GetType().GetProperty("strLeg2Instrument");
            strLeg2Instrument = (string)pi.GetValue(_strategybase);
            
           
           
           
          

        }
		
		public  object IExitHandlingInstance(){
            MethodInfo ieh = _strategybase.GetType().GetMethod("IExitHandlingInstance");
            return ieh.Invoke(_strategybase, null);
		
		}
		
		
	}
	
    public partial class SpreadTraderWindow : Window
    {
		
		delegate SpreadTraderWindow stwDelegate();
		NinjaTrader.Cbi.Currency basecurrency; 
		string currencySymbol;
		bool currencyIsPrefix;
		
		public void setCurrency(NinjaTrader.Cbi.Currency currency){
			basecurrency=currency;
			currencySymbol=Zweistein.MoneyString.Symbol(basecurrency);
			currencyIsPrefix=Zweistein.MoneyString.IsPrefix(basecurrency);
		}
		
		public NinjaTrader.Cbi.Currency getCurrency(){
			return basecurrency;
		}
		
		public static void StrategyUpdateTickets(StrategyBase strategy,SpreadTraderWindow stw,Queue<SingleSpread> spreads,double displayedprice,string strratio,LegEntryType leg1entrytype,LegEntryType leg2entrytype,object ieh){
			List<SpreadOrderTicket> ticketstoremove=new List<SpreadOrderTicket>();
			    lock (stw.tickets)
                {
                    if (stw.tickets.Count > 0)
                    {
                        
                            foreach (SpreadOrderTicket ticket in stw.tickets)
                            {
                                if (ticket.EntryConditionSatisfied(displayedprice))
                                {
                                    Guid newguid=Guid.NewGuid();
									ticket.Guid=newguid;
									ticketstoremove.Add(ticket);
									ClickhandlerArgs A = new ClickhandlerArgs(ticket.ClickedButton, ticket.Units,newguid);
									Zweistein.SpreadTraderWindow.StrategyHandleClickedButton(strategy,A,spreads,strratio,leg1entrytype,leg2entrytype,ieh);
		
                                   // clickcustomhandler(A);
								}
                            }
                    }
                }
				if(ticketstoremove.Count>0) {
					foreach(SpreadOrderTicket t in ticketstoremove) {	
					stw.Dispatcher.Invoke(new Action(()=> stw.DeleteTicket(t)));
					}
				}
		}
		public static double StrategyUpdateSpreads(Queue<SingleSpread> spreads){
			double realizedpnl=0;
			
			lock(spreads){
					for(int i=0;i<spreads.Count;i++) {
						SingleSpread s = spreads.Peek();	
						if(s.Finished()) {
							realizedpnl+=s.RealizedPnl();
							spreads.Dequeue();
							s.Dispose();
							s=null;
						}
					}
			}
			
			return realizedpnl;
		}
		
		public static void StrategyHandleClickedButton(StrategyBase strategy,ClickhandlerArgs cha,Queue<SingleSpread> spreads,string strratio,LegEntryType leg1entrytype,LegEntryType leg2entrytype,object ieh){
			
			int _units=cha.Units;
			 //  bool b= mut.WaitOne(new TimeSpan(0,0,3));
             // if (b == false) throw new TimeoutException("clickcustomhandler mutex timeout");
			switch(cha.ClickedButton){
				
				case ClickedButton.Close:
				lock(spreads){
									
					foreach(SingleSpread spread in spreads){
						spread.Exit(LegExitType.Market,LegExitType.Market);
					}
				}
				break;
				
				case ClickedButton.Reverse:
				lock(spreads){
					
					foreach(SingleSpread s in spreads){
							s.Reverse();	
						}
					
					
				}
				break;
					
				case ClickedButton.GoLong:
					lock(spreads){
					
					foreach(SingleSpread s in spreads){
						if(s.MarketPosition!=MarketPosition.Long) {
							Zweistein.NinjaTraderLog.Process("GoLong: already Short","",LogLevel.Error,LogCategories.User);
							if(strategy.TraceOrders) strategy.Print("GoLong: already Short");	
							return;
						}
					}
					//strategy.Account.Currency
					SingleSpread spread=new SingleSpread(MarketPosition.Long,_units,strratio,strategy);
					
					spread.Entry(leg1entrytype,leg2entrytype);
					IExitHandling eh= ieh as IExitHandling;
					spread.setIExitHandling(eh);
						if(cha.Guid!=null) spread.Guid=cha.Guid;
					
					spreads.Enqueue(spread);
					
				
					
				}
				break;
				
				case ClickedButton.GoShort:
				lock(spreads){
					foreach(SingleSpread s in spreads){
						if(s.MarketPosition!=MarketPosition.Short) {
							Zweistein.NinjaTraderLog.Process("GoShort: already Long","",	LogLevel.Error,LogCategories.User);
                                if (strategy.TraceOrders) strategy.Print("GoShort: already Long");	
							return;
						}
					}
					SingleSpread spread=new SingleSpread(MarketPosition.Short,_units,strratio,strategy);
					spread.Entry(leg1entrytype,leg2entrytype);
					IExitHandling eh= ieh as IExitHandling;
					spread.setIExitHandling(eh);
					if(cha.Guid!=null) spread.Guid=cha.Guid;
					spreads.Enqueue(spread);
					
				}
				break;
			}
			
		}
		
		public static double StrategySetPositionAndEntryLabels(SpreadTraderWindow stw,Queue<SingleSpread> spreads){
			    double netunits=0;
				double avgentryprice=0;
            lock (spreads)
            {
                foreach (SingleSpread s in spreads)
                {
                    int n = s.NUnits;
                    int sign = 0;
                    if (s.MarketPosition == MarketPosition.Long) sign = 1;
                    if (s.MarketPosition == MarketPosition.Short) sign = -1;
                    if (sign * n != 0 && (sign * n + netunits)!=0)
                    {
                        double old = avgentryprice * netunits;
                        double newp = s.EntryPricePerUnit(stw.ptr2members.Leg1PriceDisplayMultiplier, stw.ptr2members.Leg2PriceDisplayMultiplier) * n;
                        avgentryprice = (old + newp) / (sign*n + netunits);
                    }
                    netunits += sign*n;

                }
            }
				string txt="";
				if(netunits==0){ txt="FLAT";}
				//this.form1.ENTRY.Text="ENTRY";
				if(netunits>0) txt="LONG "+netunits.ToString();
				if(netunits<0) txt="SHORT "+(-netunits).ToString();
				//is.form1.Position.TextBackColor(txt,netunits,ChartControl);
			//	if(TraceOrders) Print("SetPositionAndEntryLabels: "+txt+" ,"+avgentryprice.ToString());
				if(stw!=null) stw.Dispatcher.Invoke(new Action(()=> stw.SetPositionTextAndValue(txt,avgentryprice)));	
				return netunits;
			
		}
		
		public static void StrategySetDefaults(StrategyBase _strategy){
			
			_strategy.EntriesPerDirection = 1;
			_strategy.DefaultQuantity=1;
			_strategy.EntryHandling = EntryHandling.AllEntries;
            _strategy.IsExitOnSessionCloseStrategy = false;
            _strategy.ExitOnSessionCloseSeconds = 30;
			_strategy.StartBehavior = StartBehavior.ImmediatelySubmit;
            _strategy.TimeInForce = TimeInForce.Gtc;
			_strategy.StopTargetHandling = StopTargetHandling.PerEntryExecution;
			_strategy.SetOrderQuantity=SetOrderQuantity.DefaultQuantity;
            _strategy.IsFillLimitOnTouch = false;
            _strategy.OrderFillResolution = OrderFillResolution.Standard;
            _strategy.Slippage = 0;
			_strategy.TradingHoursSerializable="Default 24 x 7";
			_strategy.MaximumBarsLookBack = MaximumBarsLookBack.TwoHundredFiftySix;
            _strategy.TraceOrders = false;
            _strategy.RealtimeErrorHandling = RealtimeErrorHandling.StopCancelClose;
            _strategy.IncludeTradeHistoryInBacktest = false;
            
                // Disable this property for performance gains in Strategy Analyzer optimizations
                // See the Help Guide for additional information
            _strategy.IsInstantiatedOnEachOptimizationIteration = false;
			_strategy.IsUnmanaged=true;
			
		}
		
		public static  void UpdateForm(SpreadTraderWindow stw,Queue<SingleSpread> spreads,double realizedpnl,string pricestring,double displaymarketprice)
        {
			double unrealizedpnl=0;
            try
            {
                lock (spreads)
                {
                    foreach (SingleSpread s in spreads)
                    {
                        int n = s.NUnits;
                        unrealizedpnl += s.PnlPerUnit() * n;
                    }
                }
            }
            catch(Exception ex)
            {

            }
				
				//Print("unrealized="+unrealizedpnl.ToString());
				if(stw!=null) stw.Dispatcher.Invoke(new Action(()=>stw.SetUnrealizedPNL(unrealizedpnl)));
				List<double> pnls=new List<double>();
				List<string> pnlxunit=new List<string>();
            lock (spreads)
            {
                foreach (SingleSpread s in spreads)
                {
                    string status = s.OnBarUpdateStatus();
                    double d = s.PnlPerUnit();
                    if (!string.IsNullOrEmpty(status)) continue;
                    pnls.Add(d);
                    status = " x Units(" + s.NUnits.ToString() + ") " + s.MarketPosition.ToString();
                    pnlxunit.Add(status);
                }
            }
				if(stw!=null) stw.Dispatcher.Invoke(new Action(()=>stw.SetDetailsPnl(realizedpnl,pricestring,displaymarketprice,pnls,pnlxunit)));
 
			
		}
		
		
		public static  void StrategyTerminated(SpreadTraderWindow stw,Queue<SingleSpread> spreads,List<SpreadOrderTicket> Tickets)
        {
			try {
                foreach (SingleSpread spread in spreads) spread.setIExitHandling(spread.getIExitHandling()); // update iexithandling params prior to serialize
               	if(spreads.Count>0) {
					var stacktrace = new StackTrace(false);
		            int i = 0;
		            foreach (StackFrame f in stacktrace.GetFrames())
		            {
		                bool found = false;
		                if (f.GetMethod().ToString().Contains("FlattenEverything()")) found = true;
		                if (found)
		                {
		                    spreads.Clear();
		                    break;
		                }
		                i++;
		                if (i > 5) break;
		            }
				}
				}
				catch(Exception ex){
					Zweistein.Diagnostics.Trace(ex.Message);
				}
				if(stw!=null) {
					
					stw.Dispatcher.Invoke(new Action(()=>{
					if(stw.tickets!=null) {
						lock(stw.tickets){
							Tickets.Clear();
							foreach(SpreadOrderTicket sot in stw.tickets){
							Tickets.Add(sot);
							}
						}
					}
					}));
					//stw.Destroy();
					stw.Dispatcher.Invoke(new Action(()=>stw.Destroy()));
					stw=null;
				}
				
			
		}
		public static void StrategyTransition(SpreadTraderWindow stw,Queue<SingleSpread> spreads,List<SpreadOrderTicket> Tickets)
        {
			stw.Dispatcher.Invoke(new Action(() => stw.FillGrid(Tickets)));
					foreach(SingleSpread s in spreads) {
					s.RebuildPositions(stw._strategy);
				}
			
		}
		
        public static SpreadTraderWindow StrategyDataLoaded(StrategyBase _strategybase,double Left,double Top,Queue<SingleSpread> spreads,string DisplayName)
        {

            SpreadTraderWindow stw = null;
			
						
			System.Reflection.Assembly ass = System.Reflection.Assembly.GetAssembly(typeof(SpreadTraderWindow));
          //  System.Reflection.Assembly ass = _strategybase.GetType().Assembly;

		
            //stw = SpreadTraderWindow.CreateFromBaml(ass);
			
			stw=(SpreadTraderWindow) NinjaTrader.Core.Globals.RandomDispatcher.Invoke((stwDelegate) delegate(){
                return (stw=SpreadTraderWindow.CreateFromBaml(ass));
            } ) ;
            
			//stw.setCurrency(Zweistein.MoneyString.FromString(System.Globalization.RegionInfo.CurrentRegion.ISOCurrencySymbol));
			if(stw!=null) stw.Dispatcher.Invoke(new Action(()=> stw.setCurrency(_strategybase.Account.Denomination)));
					
            if (Left != -1) if (stw != null) stw.Dispatcher.Invoke(new Action(() => stw.Left = Left));
            if (Top != -1) if (stw != null) stw.Dispatcher.Invoke(new Action(() => stw.Top = Top));
            if (stw != null) stw.Dispatcher.Invoke(new Action(()=> stw.fromStrategy(_strategybase)));
			foreach(SingleSpread s in spreads) s.setStrategyBase(_strategybase);
					
			if(stw!=null) stw.Dispatcher.Invoke(new Action(()=> stw.Show()));
			if(stw!=null) stw.Dispatcher.Invoke(new Action(()=> stw.Title=DisplayName));
			
            return stw;
        }

    }

}

