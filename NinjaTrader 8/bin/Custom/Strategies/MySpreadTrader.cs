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
using System.Xml.Serialization;
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
using System.Runtime.Serialization;
#endregion

using Zweistein.Spreads;

namespace Zweistein.Users.Custom {
	
	public enum SpreadExitHandling {
        None,
		_2ProfitTargets,
		TrailingStopSimple
	}
	
		[Serializable()]	
    	public class TrailingStopSimple:IExitHandling,ISerializable
        {
            #region Variables
            double minPnl=0;
			DateTime created;
            double stoplossperunit;
            #endregion
            public TrailingStopSimple(double _stoplossperunit){
				stoplossperunit=_stoplossperunit;
				created=DateTime.Now;
				minPnl=-stoplossperunit;
            }
            #region Serialize
            public TrailingStopSimple(SerializationInfo info, StreamingContext ctxt){
				 StopLossPerUnit = (double)info.GetValue("StopLossPerUnit", typeof(double));
				 MinPnl = (double)info.GetValue("MinPnl", typeof(double));
				Created=(DateTime) info.GetValue("Created",typeof(DateTime));
			}
			public  void GetObjectData(SerializationInfo info, StreamingContext ctxt){
				info.AddValue("StopLossPerUnit", StopLossPerUnit);
				info.AddValue("MinPnl", MinPnl);
				info.AddValue("Created",Created);
            }
            #endregion
            public TargetExitQuantity ExitNow(SingleSpread spread){
				if(DateTime.Now<created.AddMinutes(2)) return TargetExitQuantity.None;
				// let the trade breath for the first 2 minutes and therefore  do never exit within that time
								
				double pnl=spread.PnlPerUnit();
				
				if(stoplossperunit!=0 && pnl<=-1*stoplossperunit) {
					return TargetExitQuantity.All;
					// this is the case of a  hard stop  
				}
				
				if(pnl<=minPnl) {
					return TargetExitQuantity.All;
					// trailing stopp hit 
				}
				if(pnl-stoplossperunit>minPnl){
					//adjust trailing
					minPnl=pnl-stoplossperunit;
					if(spread.StrategyBase().TraceOrders) spread.StrategyBase().Print("MinPnl:"+minPnl.ToString());
					
				}
				NinjaTrader.NinjaScript.Strategies.Strategy s=spread.StrategyBase() as NinjaTrader.NinjaScript.Strategies.Strategy;
				NinjaTrader.NinjaScript.Strategies.SpreadExitStrategy S = s as NinjaTrader.NinjaScript.Strategies.SpreadExitStrategy;
				var spreadpriceseries=s.Spread(S.Closes[0],S.PriceString,spread.Lots1,S.strLeg2Instrument,spread.Lots2,S.Leg1PriceDisplayMultiplier,S.Leg2PriceDisplayMultiplier);
				// use all Indicators on the spread price series!!!
				// and applying SMA to it
				double smoothed=s.SMA(spreadpriceseries.SpreadValue,3)[0];
						
				return TargetExitQuantity.None;
			}
			
			public override string ToString(){
				return this.GetType().Name+"(StopLossPerUnit{"+stoplossperunit.ToString()+"},MinPnl{"+minPnl.ToString()+"},Created{"+created.ToString()+"})";
			}
			#region Properties
			
			public double StopLossPerUnit {
				get {return stoplossperunit;}
				set {stoplossperunit=value;}
			}
			
			public double MinPnl {
				get {return minPnl;}
				set {minPnl=value;}
			}
			public DateTime Created {
				get {return created;}
				set {created=value;}
				
			}
			#endregion
			
			
		}
	
		[Serializable()]
	    public class _2ProfitTargets:IExitHandling,ISerializable
        {

            #region Variables
            double stoplossperunit;
		    double profit1perunit;
		    double profit2perunit;
		    TargetExitQuantity target1exitquantity;
		    TargetExitQuantity target2exitquantity;
		    bool profit1perunitreached=false;
		    bool profit2perunitreached=false;
            #endregion
            #region Serialize
           
		public _2ProfitTargets(SerializationInfo info, StreamingContext ctxt){
			StopLossPerUnit = (double)info.GetValue("StopLossPerUnit", typeof(double));
			Profit1PerUnit =  (double)info.GetValue("Profit1PerUnit", typeof(double));
			Profit2PerUnit	= (double)info.GetValue("Profit2PerUnit", typeof(double));
			Target1ExitQuantity	= (TargetExitQuantity)info.GetValue("Target1ExitQuantity", typeof(TargetExitQuantity));
			Target2ExitQuantity	= (TargetExitQuantity)info.GetValue("Target2ExitQuantity", typeof(TargetExitQuantity));
			Profit1perUnitReached= (bool)info.GetValue("Profit1perUnitReached", typeof(bool));
			Profit2perUnitReached= (bool)info.GetValue("Profit2perUnitReached", typeof(bool));
		}
		public  void GetObjectData(SerializationInfo info, StreamingContext ctxt){
			info.AddValue("StopLossPerUnit", StopLossPerUnit);
			info.AddValue("Profit1PerUnit",Profit1PerUnit);
			info.AddValue("Profit2PerUnit",Profit2PerUnit);
			info.AddValue("Target1ExitQuantity",Target1ExitQuantity);
			info.AddValue("Target2ExitQuantity",Target2ExitQuantity);
			info.AddValue("Profit1perUnitReached",Profit1perUnitReached);
			info.AddValue("Profit2perUnitReached",Profit2perUnitReached);
        }
            #endregion


        public _2ProfitTargets(double _stoplossperunit,double _profit1perunit,double _profit2perunit,TargetExitQuantity _target1exitquantity,TargetExitQuantity _target2exitquantity){
			stoplossperunit=_stoplossperunit;
			profit1perunit=_profit1perunit;
			profit2perunit=_profit2perunit;
			target1exitquantity=_target1exitquantity;
			target2exitquantity=_target2exitquantity;
		}
		

		public override string ToString(){
				return this.GetType().Name+"(StopLossPerUnit{"+stoplossperunit.ToString()+"},Profit1PerUnit{"+ profit1perunit.ToString()+"},Profit2PerUnit{"+profit2perunit.ToString()
				+"},Target1ExitQuantity{"+target1exitquantity.ToString()+"},Target2ExitQuantity{"+target2exitquantity.ToString()
				+"},Profit1perUnitReached{"+profit1perunitreached.ToString()+"},Profit2perUnitReached{"+profit2perunitreached.ToString()+"})";
		}
		
		public TargetExitQuantity ExitNow(SingleSpread spread){
			
			double pnl=spread.PnlPerUnit();
			if(stoplossperunit!=0 && pnl<=-1*stoplossperunit) {
				return TargetExitQuantity.All;
			}
			if(profit1perunitreached==false && profit1perunit!=0 && pnl>=profit1perunit) {
			  	profit1perunitreached=true;
				return target1exitquantity;
			}
			if(profit2perunitreached==false && profit2perunit!=0 && pnl>=profit2perunit) {
				profit2perunitreached=true;
				return target2exitquantity;
				
			}
		
			StrategyBase sb=spread.StrategyBase() ;
			NinjaTrader.NinjaScript.Strategies.MySpreadTrader S = (NinjaTrader.NinjaScript.Strategies.MySpreadTrader) sb;
			NinjaTrader.NinjaScript.Strategies.Strategy s=S as NinjaTrader.NinjaScript.Strategies.Strategy;
			//  variables S and s are always the same object sb which is our strategy
			//  the nt8 code editor intellisense logic does not show inherited  class members, but only direct members
				
			// example of using spread prices
			var spreadpriceseries=s.Spread(S.Closes[0],S.PriceString,spread.Lots1,S.strLeg2Instrument,spread.Lots2,S.Leg1PriceDisplayMultiplier,S.Leg2PriceDisplayMultiplier);
			// and applying SMA to it
			double smoothed=s.SMA(spreadpriceseries.SpreadValue,3)[0];
			//S.Print(" SMA="+smoothed.ToString());
		
			return TargetExitQuantity.None;
		}
		
		#region Properties
		 [Display( Name = "StopLossPerUnit", Description = "", GroupName = "RuntimeEditable", Order = 3)]
			public double StopLossPerUnit {
			get {return stoplossperunit;}
			set {stoplossperunit=value;}
		}

			[Display( Name = "Profit1PerUnit", Description = "", GroupName = "RuntimeEditable", Order = 3)]
		public double Profit1PerUnit {
			get {return profit1perunit;}
			set {profit1perunit=value;}
		}
		
		public double Profit2PerUnit {
			get {return profit2perunit;}
			set {profit2perunit=value;}
		}
		
		public TargetExitQuantity Target1ExitQuantity {
			get {return target1exitquantity;}
			set {target1exitquantity=value;}
			
		}
		[Display( Name = "TargetExitQuantity", Description = "", GroupName = "RuntimeEditable", Order = 3)]
		public TargetExitQuantity Target2ExitQuantity {
			get {return target2exitquantity;}
			set {target2exitquantity=value;}
			
		}
		
		public bool Profit1perUnitReached {
			get {return profit1perunitreached; }
			set {profit1perunitreached=value;}
			
		}
		
		public bool Profit2perUnitReached {
			get {return profit2perunitreached; }
			set {profit2perunitreached=value;}
			
		}
		
		#endregion
	
	}
		
		
	
	
}
//This namespace holds Strategies in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Strategies
{
	public class MySpreadTrader : SpreadExitStrategy
	{
		
		double profit1perunit=0;
		double profit2perunit=0;
      	double stoplossperunit=0;

        static string iso = System.Globalization.RegionInfo.CurrentRegion.ISOCurrencySymbol;
        string profit1perunitstr="150 "+ iso;
        string profit2perunitstr = "800 " + iso;
      	string stoplossperunitstr="120 "+iso;
		
		TargetExitQuantity target1exitquantity=TargetExitQuantity.OneThird;
		TargetExitQuantity target2exitquantity=TargetExitQuantity.All;
		Zweistein.Users.Custom.SpreadExitHandling exithandling=Zweistein.Users.Custom.SpreadExitHandling.TrailingStopSimple;


        private Spread spreadprices;

        public override object IExitHandlingInstance(){
			if(Account==null)
            {
                Zweistein.NinjaTraderLog.Process("IExitHandlingInstance():", "account==null!", LogLevel.Error, LogCategories.Strategy);
                return null;
            }
				if(exithandling==Zweistein.Users.Custom.SpreadExitHandling._2ProfitTargets)
            {
                
                decimal sl_accountcurrency = Zweistein.CurrencyConversion.Cube.Convert((decimal)stoplossperunit, Zweistein.CurrencyConversion.ISO(Zweistein.MoneyString.FromString(stoplossperunitstr)), Zweistein.CurrencyConversion.ISO(Account.Denomination));
                decimal p1_accountcurrency = Zweistein.CurrencyConversion.Cube.Convert((decimal)profit1perunit, Zweistein.CurrencyConversion.ISO(Zweistein.MoneyString.FromString(profit1perunitstr)) , Zweistein.CurrencyConversion.ISO(Account.Denomination));
                decimal p2_accountcurrency = Zweistein.CurrencyConversion.Cube.Convert((decimal)profit2perunit, Zweistein.CurrencyConversion.ISO(Zweistein.MoneyString.FromString(profit2perunitstr)) , Zweistein.CurrencyConversion.ISO(Account.Denomination));

                return new Zweistein.Users.Custom._2ProfitTargets((double)sl_accountcurrency, (double)p1_accountcurrency, (double)p2_accountcurrency, target1exitquantity, target2exitquantity);

            }
            if (exithandling==Zweistein.Users.Custom.SpreadExitHandling.TrailingStopSimple)
            {
                decimal sl_accountcurrency = Zweistein.CurrencyConversion.Cube.Convert((decimal)stoplossperunit, Zweistein.CurrencyConversion.ISO(Zweistein.MoneyString.FromString(stoplossperunitstr)), Zweistein.CurrencyConversion.ISO(Account.Denomination));

                return new Zweistein.Users.Custom.TrailingStopSimple((double)sl_accountcurrency);
            }
            if (exithandling == Zweistein.Users.Custom.SpreadExitHandling.None)
            {
                return null;
            }
					
			return null;
		}
		
		
		public override object Clone(){
			MySpreadTrader ons = base.Clone() as MySpreadTrader;
			
			if(ons==null) {
				Log("MySpreadTrader.Clone() failed, duplicate versions in memory?Restart NinjaTrader to fix!", LogLevel.Error);	
				return null;
			}
			ons.Target1ExitQuantity=this.Target1ExitQuantity;
			ons.Target2ExitQuantity=this.Target2ExitQuantity;
			ons.Profit1PerUnit=this.Profit1PerUnit;
			ons.Profit2PerUnit=this.Profit2PerUnit;
			ons.StopplossPerUnit=this.StopplossPerUnit;
			
			
			ons.SpreadExitHandling=this.SpreadExitHandling;
			return ons;
		}
		
		protected override void OnStateChange()
		{
			base.OnStateChange();
			
			if (State == State.SetDefaults)
			{
				Description									= @"Enter the description for your new custom Strategy here.";
				Name										= "MySpreadTrader";
				IncludeTradeHistoryInBacktest = true;
				TraceOrders=false;
				
			}
			
            else if (State == State.DataLoaded)
            {
                Zweistein.FractionString f = new Zweistein.FractionString(QuantityRatio, ':');
                int lots1 = (int)f.Nominator * Units;
                int lots2 = (int)f.Denominator * Units;
               // spreadprices = Spread(Closes[0],PriceString, lots1, strLeg2Instrument,lots2, Leg1PriceDisplayMultiplier, Leg2PriceDisplayMultiplier);
                // we need this to avoid this NinjaTrader error: as we use Spread later on
                //"A hosted indicator tried to load additional data. All data must first be loaded by the hosting NinjaScript in its configure state"
            }
		}

        
              #region Properties

        [Display( Name = "ExitHandling", Description = "", GroupName = "RuntimeEditable", Order = 0)]
		public Zweistein.Users.Custom.SpreadExitHandling SpreadExitHandling {
			get {return exithandling;}
			set {exithandling=value;}
			
		}
		
		
	   [Display( Name = "Target1ExitQuantity", Description = "", GroupName = "Parameters", Order = 4)]
        public TargetExitQuantity Target1ExitQuantity
        {
            get {return target1exitquantity; }
            set { 
				target1exitquantity = value; 
			}
        }
		
		
	   [Display( Name = "Target2ExitQuantity", Description = "", GroupName = "Parameters", Order = 5)]
      public TargetExitQuantity Target2ExitQuantity
        {
            get {return target2exitquantity; }
            set { 
				target2exitquantity = value; 
			}
        }
		
		
		[XmlIgnore]
	   [Display( Name = "Profit1PerUnit", Description = "", GroupName = "RuntimeEditable", Order = 1)]
		public string Profit1PerUnit {
			get {
				Zweistein.MoneyString m = new Zweistein.MoneyString(profit1perunitstr);
				profit1perunitstr=m.String;
				profit1perunit=m.Amount;
				
				return profit1perunitstr;}
			set {
				Zweistein.MoneyString m = new Zweistein.MoneyString(value);
				profit1perunitstr=m.String;
				profit1perunit=m.Amount;
				//currency=m.Currency;
			}
			
		}
		[XmlIgnore]
	   [Display( Name = "Profit2PerUnit", Description = "", GroupName = "RuntimeEditable", Order = 2)]
         public string Profit2PerUnit
        {
            get {
				Zweistein.MoneyString m = new Zweistein.MoneyString(profit2perunitstr);
				profit2perunitstr=m.String;
				profit2perunit=m.Amount;
				
				return profit2perunitstr; }
            set { 
				Zweistein.MoneyString m = new Zweistein.MoneyString(value);
				profit2perunitstr=m.String;
				profit2perunit=m.Amount;
			}
        }
		[XmlIgnore]
       [Display( Name = "StopplossPerUnit", Description = "", GroupName = "RuntimeEditable", Order = 3)]
	    public string StopplossPerUnit
        {
            get {
				Zweistein.MoneyString m = new Zweistein.MoneyString(stoplossperunitstr);
				stoplossperunitstr=m.String;
				stoplossperunit = m.Amount; 
				return stoplossperunitstr;
				}
            set { 
				Zweistein.MoneyString m = new Zweistein.MoneyString(value);
				stoplossperunitstr=m.String;
				stoplossperunit = m.Amount; 
			}
        }
        #endregion
		

		
	}
}
