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
using NinjaTrader.NinjaScript.DrawingTools;
#endregion

//This namespace holds Indicators in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Indicators
{
	public class Spread : Indicator
	{
		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description									= @"Enter the description for your new custom Indicator here.";
				Name										= "Spread";
				Calculate									= Calculate.OnPriceChange;
				IsOverlay									= false;
				DisplayInDataBox							= true;
				DrawOnPricePanel							= true;
				DrawHorizontalGridLines						= true;
				DrawVerticalGridLines						= true;
				PaintPriceMarkers							= true;
				ScaleJustification							= NinjaTrader.Gui.Chart.ScaleJustification.Right;
				//Disable this property if your indicator requires custom values that cumulate with each new market data event. 
				//See Help Guide for additional information.
				IsSuspendedWhileInactive					= true;
				PriceString="Spread=";
				A					= 1;
				B					= 1;
				APriceDisplayMultiplier=1.0;
				BPriceDisplayMultiplier=1.0;
			
				AddPlot(Brushes.Goldenrod,"SpreadValue");
			}
			else if (State == State.Configure)
			{
				AddDataSeries(B_strInstrument,BarsPeriod.BarsPeriodType,BarsPeriod.Value);
			}
		}

		
		public override string DisplayName
		{
 			 get { 
                
                string instr=PriceString;
                if (APriceDisplayMultiplier != 1)
                {
                    instr += APriceDisplayMultiplier.ToString();
                    if (A != 1) instr += "*";
                    else instr += " ";
                }
               

                if (A!=1) instr+=A.ToString()+" ";	
				if(this.Instruments!=null && this.Instruments.Length>1 && !string.IsNullOrEmpty(this.Instruments[0].FullName)){
					instr+=this.Instruments[0].FullName;
				}
				else {
					instr= "Spread(" + A.ToString() + "," + APriceDisplayMultiplier.ToString() + "," + B.ToString() + "," + B_strInstrument + "," + BPriceDisplayMultiplier.ToString() + "," + PriceString + ")";
				}
				if(this.Instruments.Length>1){
					instr+=" - ";
                    if (BPriceDisplayMultiplier != 1)
                    {
                        instr += BPriceDisplayMultiplier.ToString();
                        if (B != 1) instr += "*";
                        else instr += " ";
                    }
                    if (B!=1) instr+=B.ToString()+" ";	 
					if(this.Instruments[1]!=null  && !string.IsNullOrEmpty(this.Instruments[1].FullName)){
						instr+=this.Instruments[1].FullName;
					}
				 }
				
		 	    return instr;
		     }
		}
		
		protected override void OnBarUpdate()
		{
			//Add your custom indicator logic here.
			int a=CurrentBars[0];
			int b=CurrentBars[1];
			
			if(a<0 || b<0) return;
			//DateTime ta=Times[0][a];
			//DateTime tb=Times[1][b];
			
			SpreadValue[0] = APriceDisplayMultiplier*A * Closes[0][0]- BPriceDisplayMultiplier*B * Closes[1][0];
		}

		#region Properties
		
		[Browsable(false)]
		[XmlIgnore()]
		public Series<double> SpreadValue
		{
			get { return Values[0]; }
		}
		
		[NinjaScriptProperty]
		[Display( Name = "Price string", GroupName = "Parameters", Order = 0)]
		public string PriceString {
			get; set;
		}
		[NinjaScriptProperty]
		[Range(1, int.MaxValue)]
		[Display(Name="A", Order=1, GroupName="Parameters")]
		public int A
		{ get; set; }
		
		
		[NinjaScriptProperty]
	    [Display( Name = "Instrument B", GroupName = "Parameters", Order = 2)]
		public string B_strInstrument {get;set;}
		

		[NinjaScriptProperty]
		[Range(1, int.MaxValue)]
		[Display(Name="B", GroupName="Parameters", Order=3)]
		public int B
		{ get; set; }
		
			
	
		[NinjaScriptProperty]
		[Display( Name = "APriceDisplayMultiplier", GroupName = "Parameters", Order = 4)]
		public double APriceDisplayMultiplier
        {
            get ;set;
        }
		[NinjaScriptProperty]
		[Display( Name = "BPriceDisplayMultiplier", GroupName = "Parameters", Order = 5)]
		public double BPriceDisplayMultiplier
        {
            get ;set;
        }
		
				
		#endregion

	}
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private Spread[] cacheSpread;
		public Spread Spread(string priceString, int a, string b_strInstrument, int b, double aPriceDisplayMultiplier, double bPriceDisplayMultiplier)
		{
			return Spread(Input, priceString, a, b_strInstrument, b, aPriceDisplayMultiplier, bPriceDisplayMultiplier);
		}

		public Spread Spread(ISeries<double> input, string priceString, int a, string b_strInstrument, int b, double aPriceDisplayMultiplier, double bPriceDisplayMultiplier)
		{
			if (cacheSpread != null)
				for (int idx = 0; idx < cacheSpread.Length; idx++)
					if (cacheSpread[idx] != null && cacheSpread[idx].PriceString == priceString && cacheSpread[idx].A == a && cacheSpread[idx].B_strInstrument == b_strInstrument && cacheSpread[idx].B == b && cacheSpread[idx].APriceDisplayMultiplier == aPriceDisplayMultiplier && cacheSpread[idx].BPriceDisplayMultiplier == bPriceDisplayMultiplier && cacheSpread[idx].EqualsInput(input))
						return cacheSpread[idx];
			return CacheIndicator<Spread>(new Spread(){ PriceString = priceString, A = a, B_strInstrument = b_strInstrument, B = b, APriceDisplayMultiplier = aPriceDisplayMultiplier, BPriceDisplayMultiplier = bPriceDisplayMultiplier }, input, ref cacheSpread);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.Spread Spread(string priceString, int a, string b_strInstrument, int b, double aPriceDisplayMultiplier, double bPriceDisplayMultiplier)
		{
			return indicator.Spread(Input, priceString, a, b_strInstrument, b, aPriceDisplayMultiplier, bPriceDisplayMultiplier);
		}

		public Indicators.Spread Spread(ISeries<double> input , string priceString, int a, string b_strInstrument, int b, double aPriceDisplayMultiplier, double bPriceDisplayMultiplier)
		{
			return indicator.Spread(input, priceString, a, b_strInstrument, b, aPriceDisplayMultiplier, bPriceDisplayMultiplier);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.Spread Spread(string priceString, int a, string b_strInstrument, int b, double aPriceDisplayMultiplier, double bPriceDisplayMultiplier)
		{
			return indicator.Spread(Input, priceString, a, b_strInstrument, b, aPriceDisplayMultiplier, bPriceDisplayMultiplier);
		}

		public Indicators.Spread Spread(ISeries<double> input , string priceString, int a, string b_strInstrument, int b, double aPriceDisplayMultiplier, double bPriceDisplayMultiplier)
		{
			return indicator.Spread(input, priceString, a, b_strInstrument, b, aPriceDisplayMultiplier, bPriceDisplayMultiplier);
		}
	}
}

#endregion
