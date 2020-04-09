using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NinjaTrader.Gui
{
 public class DisplayAttributeExtensions:System.Object
    {
        public enum DisplayAttributeValue
        {
             Description,
             GroupName,
             Name,
             Prompt,
             ShortName,
             Unset
        }

    }


}

namespace NinjaTrader.Cbi
{
    public enum MarketPosition
    {
        Flat = 0,
        Long = 1,
        Short = -1
    }

}

namespace NinjaTrader.NinjaScript
{
    
    namespace Strategies
    {

        public class SpreadExitStrategy
        {

            public SpreadExitStrategy()
            {
                Left = -1;
                Top = -1;

            }
            public void Close_Click(object sender, System.EventArgs e) { }

            public void Reverse_Click(object sender, System.EventArgs e) { }

            public void GoShort_Click(object sender, System.EventArgs e) { }

            public void GoLong_Click(object sender, System.EventArgs e) { }

            [Display(Name = "Top", GroupName = "RuntimeEditable")]
            public double Left { get; set; }

            [Display(Name ="Top",GroupName ="RuntimeEditable")]
            public double Top { get; set; }
            public NinjaTrader.Cbi.MarketPosition MarketPosition {get; set;}
        }


    }
    public class StrategyBase
    {

        public StrategyBase()
        {

        }

    }

    




}