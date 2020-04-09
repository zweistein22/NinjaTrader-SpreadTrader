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
#endregion

namespace NinjaTrader.NinjaScript.Strategies
{
    public partial class SpreadExitStrategy : Strategy //NinjaTrader.Gui.NinjaScript.StrategyRenderBase
    {
        [NinjaScriptProperty]
        [Display(Name = "Price string", Description = "", GroupName = "Parameters", Order = 0)]
        public string PriceString
        {
            get { return pricestring; }
            set { pricestring = value; }

        }
        [NinjaScriptProperty]
        [Display(Name = "Leg1PriceDisplayMultiplier", Description = "", GroupName = "Parameters", Order = 1)]
        public double Leg1PriceDisplayMultiplier
        {
            get { return leg1pricemultiplier; }
            set
            {
                leg1pricemultiplier = value;
            }
        }
        [NinjaScriptProperty]
        [Display(Name = "Leg2PriceDisplayMultiplier", Description = "", GroupName = "Parameters", Order = 2)]
        public double Leg2PriceDisplayMultiplier
        {
            get { return leg2pricemultiplier; }
            set
            {
                leg2pricemultiplier = value;
            }
        }
        [NinjaScriptProperty]
        [Display(Name = "Leg1 Entry Type", Description = "", GroupName = "RuntimeEditable", Order = 10)]
        public LegEntryType Leg1EntryType
        {
            get { return leg1entrytype; }
            set
            {
                leg1entrytype = value;
            }
        }
        [NinjaScriptProperty]
        [Display(Name = "Leg2 Entry Type", Description = "", GroupName = "RuntimeEditable", Order = 11)]
        public LegEntryType Leg2EntryType
        {
            get { return leg2entrytype; }
            set
            {
                leg2entrytype = value;
            }
        }
        [NinjaScriptProperty]
        [Display(Name = "Leg1 Exit Type", Description = "", GroupName = "RuntimeEditable", Order = 12)]
        public LegExitType Leg1ExitType
        {
            get { return leg1exittype; }
            set
            {
                leg1exittype = value;
            }
        }
        [NinjaScriptProperty]
        [Display(Name = "Leg2 Exit Type", Description = "", GroupName = "RuntimeEditable", Order = 13)]
        public LegExitType Leg2ExitType
        {
            get { return leg2exittype; }
            set
            {
                leg2exittype = value;
            }
        }
        [NinjaScriptProperty]
        [Display(Name = "Units", Description = "", GroupName = "RuntimeEditable", Order = 0)]
        public int Units { get; set; }



        [NinjaScriptProperty]
        [Display(Name = "Quantity Ratio", Description = "Leg 1 : Leg 2", GroupName = "Data Series", Order = 0)]
        public string QuantityRatio
        {
            get
            {
                Zweistein.FractionString f = new Zweistein.FractionString(strratio, ':');
                strratio = f.String;
                leg1Unit = (int)f.Nominator;
                leg2Unit = (int)f.Denominator;
                return strratio;
            }
            set
            {
                if (strratio.CompareTo(value) == 0) return;
                Zweistein.FractionString f = new Zweistein.FractionString(value, ':');
                strratio = f.String;
                leg1Unit = (int)f.Nominator;
                leg2Unit = (int)f.Denominator;
            }
        }

        [NinjaScriptProperty]
        [Browsable(false)]
        public string strLeg2Instrument { get; set; }
        [EditorBrowsable(EditorBrowsableState.Always)]
        [XmlIgnore]
        //[Display(GroupName = "NinjaScriptDataSeries", Name = "Instrument 2", Order = 100, ResourceType = typeof (Resource))]
        [Display(Name = "Instrument 2", Description = "Leg 2 o Spread", GroupName = "Data Series", Order = 0)]
        public NinjaTrader.Cbi.Instrument Leg2Instrument
        {
            get { return leg2instrument; }
            set
            {
                leg2instrument = value;
                if (value != null) strLeg2Instrument = leg2instrument.FullName;
            }
        }

        [Browsable(false)]
        public new StartBehavior StartBehavior { get; set; }

        [Browsable(false)]
        public new bool IsExitOnSessionCloseStrategy { get; set; }

        [Browsable(false)]
        public new int ExitOnSessionCloseSeconds { get; set; }
        [Browsable(false)]
        public new StopTargetHandling StopTargetHandling { get; set; }

        [Browsable(false)]
        public new EntryHandling EntryHandling { get; set; }
        [Browsable(false)]
        public new int EntriesPerDirection { get; set; }

        [Browsable(false)]
        public new SetOrderQuantity SetOrderQuantity { get; set; }

        [Browsable(false)]
        public new TimeInForce TimeInForce { get; set; }

        [Browsable(false)]
        public new int DefaultQuantity { get; set; }


        [Browsable(false)]
        public new bool IsFillLimitOnTouch { get; set; }
        //  [Browsable(false)]   //ERROR IN EDITSTRATEGY
        // public new OrderFillResolution  OrderFillResolution {get;set;}//ERROR IN EDITSTRATEGY

        // [Browsable(false)]
        // public new int Slippage{get;set;}
        /*
		//  [Browsable(false)]	//ERROR IN ENABLESTRATEGY
		 public new int BarsRequiredToTrade {
			 get {return 0;}
			 set {  int dummy = 0;
				//  base.BarsRequiredToTrade=0;
			 }
		 }		//ERROR IN ENABLESTRATEGY
		 */

        [Browsable(false)]
        public string FirstLoadUTC { get; set; }



        [Browsable(false)]
        public SingleSpread[] Spreads
        {
            get { return spreads.ToArray(); }
            set
            {
                spreads.Clear();
                foreach (SingleSpread s in value)
                {
                    spreads.Enqueue(s);
                }
            }
        }



        [Browsable(false)]
        public double Left { get; set; }
        [Browsable(false)]
        public double Top { get; set; }


        [Browsable(false)]
        public List<SpreadOrderTicket> Tickets
        {
            get { return _tickets; }
            set { _tickets = value; }
        }
        [Browsable(false)]
        public double Realizedpnl
        {
            get { return realizedpnl; }
            set { realizedpnl = value; }
        }

    }
}
