using System;
using System.ComponentModel;
using NinjaTrader.Cbi;

namespace NinjaTrader
{

    namespace Core
    {
        public class Globals
        {

            public static string UserDataDir
            {
                get
                {
                    return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NinjaTrader 8\\";
                }

            }
        }


    }



}

namespace NinjaTrader.Cbi
{
    public class Account
    {


        public string Name
        {
            get { return "Sim101"; }

        }
    }




}
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

        public class SpreadExitStrategy:StrategyBase
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


            public double Left { get; set; }

            [Browsable(false)]
            public double Top { get; set; }

            [Category("RuntimeEditable")]
            public double Top0 { get; set; }

            [Description("Please enter your full name")]
            public double Top1 { get; set; }

            [Category("RuntimeEditable")]
            public double Top2 { get; set; }
            [Category("RuntimeEditable")]
            public double Top3 { get; set; }
            [Category("RuntimeEditable")]
            public double Top4 { get; set; }
            [Category("RuntimeEditable")]
            public double Top5 { get; set; }
            [Category("RuntimeEditable")]
            public double Top6 { get; set; }
            [Category("RuntimeEditable")]
            public double Top7 { get; set; }
            [Category("RuntimeEditable")]
            public double Top8 { get; set; }

            [Category("RuntimeEditable")]
            public double Top9 { get; set; }
            [Category("RuntimeEditable")]
            public double Top10 { get; set; }

            public NinjaTrader.Cbi.MarketPosition MarketPosition {get; set;}
        }


    }
    public class StrategyBase
    {

        public StrategyBase()
        {
        }

        public Account Account   {
                get
            {
                NinjaTrader.Cbi.Account a = new Account();
                return a;
            }
         }
        

    }

    




}