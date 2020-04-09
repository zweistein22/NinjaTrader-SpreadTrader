using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using NinjaTrader.Cbi;
using NinjaTrader.Data;
using NinjaTrader.Gui.Tools;
using NinjaTrader.NinjaScript;

namespace Zweistein
{
	
	
	
	public static class ExecutionHelpers {
		
		
	public static int CompareTime(NinjaTrader.Cbi.Execution a, NinjaTrader.Cbi.Execution b){
		return a.Time.CompareTo(b.Time);
	}
   }
	
	
	
    static public class NinjaTraderExtension
    {

       
        public static MethodInfo mi = null;
        
        public static void ConfigHook(StrategyBase b)
        {
         //   if (mi != null) mi.Invoke(b, new object[] { BarsPeriodType.Minute, 5 });

        }

        static NinjaTrader.Gui.ControlCenter cc = null;


        static NinjaTrader.Gui.NinjaScript.StrategiesGrid grdStrategies = null;
		
		static Infragistics.Windows.DataPresenter.XamDataGrid xgrdStrategies=null;
		
		
		public static Infragistics.Windows.DataPresenter.XamDataGrid StrategiesNTGrid(){
			
			
			 if (xgrdStrategies != null) return xgrdStrategies;

            NinjaTrader.Gui.ControlCenter _cc = ControlCenter();
            _cc.Dispatcher.Invoke((Action)(() =>
            {
                var v2 = _cc.FindFirst("StrategiesGridTabItem");
                if (v2 != null)
                {
                    System.Windows.Controls.TabItem ti = v2 as System.Windows.Controls.TabItem;
                    var a  = ti.Content as NinjaTrader.Gui.NinjaScript.StrategiesGrid;
					FieldInfo fi=a.GetType().GetField("grdStrategies",BindingFlags.NonPublic|BindingFlags.Instance);
					var b=fi.GetValue(a);
					xgrdStrategies = b as Infragistics.Windows.DataPresenter.XamDataGrid;
					
					//if(fi!=null) 
					

                }
            }));
            return xgrdStrategies;
			
			
			
		}
		
		
		
		 public static NinjaTrader.Gui.Tools.NTMenuItem StrategiesGridDeleteMenuItem()
        {

          
			//NTMenuItem mnuRemove;
			NinjaTrader.Gui.Tools.NTMenuItem mnu=null;
            NinjaTrader.Gui.ControlCenter _cc = ControlCenter();
            _cc.Dispatcher.Invoke((Action)(() =>
            {
                var v2 = _cc.FindFirst("StrategiesGridTabItem");
                if (v2 != null)
                {
                    System.Windows.Controls.TabItem ti = v2 as System.Windows.Controls.TabItem;
                    grdStrategies = ti.Content as NinjaTrader.Gui.NinjaScript.StrategiesGrid;
					 var a  = grdStrategies;
					FieldInfo fi=a.GetType().GetField("grdStrategies",BindingFlags.NonPublic|BindingFlags.Instance);
					var b=fi.GetValue(a);
					mnu=b as NTMenuItem;

                }
            }));
            return mnu;
        }
		
		
        public static NinjaTrader.Gui.NinjaScript.StrategiesGrid StrategiesGrid()
        {

            if (grdStrategies != null) return grdStrategies;

            NinjaTrader.Gui.ControlCenter _cc = ControlCenter();
            _cc.Dispatcher.Invoke((Action)(() =>
            {
                var v2 = _cc.FindFirst("StrategiesGridTabItem");
                if (v2 != null)
                {
                    System.Windows.Controls.TabItem ti = v2 as System.Windows.Controls.TabItem;
                    grdStrategies = ti.Content as NinjaTrader.Gui.NinjaScript.StrategiesGrid;

                }
            }));
            return grdStrategies;
        }


        public static void AddStrategyToGrid(StrategyBase strategy)
        {

            NinjaTrader.Gui.NinjaScript.StrategiesGrid _grd=  StrategiesGrid();
            
            _grd.Dispatcher.Invoke((Action)(() => {
               
                MethodInfo strategyadd = grdStrategies.GetType().GetMethod("StrategyAdd", BindingFlags.NonPublic | BindingFlags.Static);

                strategyadd.Invoke(grdStrategies, new object[] { strategy });

               

            }));


        }
		
		
		 public static void DisableStrategy(StrategyBase strategy)
        {

            NinjaTrader.Gui.NinjaScript.StrategiesGrid _grd=  StrategiesGrid();

            _grd.Dispatcher.InvokeAsync((Action)(() => {
               
                MethodInfo strategydisable = grdStrategies.GetType().GetMethod("StrategyDisable", BindingFlags.NonPublic | BindingFlags.Static);

                if(strategydisable != null) {
                    strategydisable.Invoke(grdStrategies, new object[] { strategy });
				}

               

            }));


        }
		
		
		
		 public static NinjaTrader.Gui.Chart.BarsDialog BarsDialog()
        {
            NinjaTrader.Gui.Chart.BarsDialog cc=null;

            foreach (var v in NinjaTrader.Core.Globals.AllWindows)
            {

                v.Dispatcher.Invoke((Action)(() =>
                {
                    if (v.GetType() == typeof(NinjaTrader.Gui.Chart.BarsDialog)) cc = v as NinjaTrader.Gui.Chart.BarsDialog;

                }));
            }
            return cc;
        }
		
		
		
         public static NinjaTrader.Gui.ControlCenter ControlCenter()
        {
            if (cc != null) return cc;
    
            foreach (var v in NinjaTrader.Core.Globals.AllWindows)
            {

                v.Dispatcher.Invoke((Action)(() =>
                {
                    if (v.GetType() == typeof(NinjaTrader.Gui.ControlCenter)) cc = v as NinjaTrader.Gui.ControlCenter;

                }));
            }
            return cc;
        }

    public static void LogError(string msg)
        {

            Zweistein.Diagnostics.Trace(msg);
            //Zweistein.NinjaTraderLog.Process(msg,"", NinjaTrader.Cbi.LogLevel.Error, NinjaTrader.Cbi.LogCategories.Default);


        }
        
        static public double MarketPriceLast( this NinjaTrader.Cbi.Instrument instrument)
    {

		if(instrument!=null && instrument.HasSeenMarketData) {
			if(instrument.MarketData!=null) {
			 //Print(instrument.FullName+":" + instrument.MarketData.Last.Price.ToString());
			return	instrument.MarketData.Last.Price;
			}
		}
		 return 0.0;
    }
	
	static public Exchange RelatedCashMarket( this NinjaTrader.Cbi.Instrument I){
			if(I.FullName.Contains("ES ")) return Exchange.Nyse;
			if(I.FullName.Contains("FDAX ")) return Exchange.Xetra;
			return I.Exchange;	
	}
	
	static public TimeSpan BarsTimeSpan( this NinjaTrader.Data.Bars bars)
    {
		switch(bars.BarsPeriod.BarsPeriodType){
			
			case NinjaTrader.Data.BarsPeriodType.Day:
					return new TimeSpan(bars.BarsPeriod.Value,0,0,0);
			case NinjaTrader.Data.BarsPeriodType.Minute:
					return new TimeSpan(0,bars.BarsPeriod.Value,0);
			case NinjaTrader.Data.BarsPeriodType.Month:
					return new TimeSpan(31*bars.BarsPeriod.Value,0,0,0);
			case NinjaTrader.Data.BarsPeriodType.Second:
					return new TimeSpan(0,0,bars.BarsPeriod.Value);
			case NinjaTrader.Data.BarsPeriodType.Week:
					return new TimeSpan(7*bars.BarsPeriod.Value,0,0,0);
			case NinjaTrader.Data.BarsPeriodType.Year:
					return new TimeSpan(365*bars.BarsPeriod.Value,0,0,0);
			default:
				return new TimeSpan();
		}
	}	
	}
}
