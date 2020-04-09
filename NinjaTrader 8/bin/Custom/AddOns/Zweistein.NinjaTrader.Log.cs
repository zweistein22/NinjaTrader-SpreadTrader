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
    static public class NinjaTraderLog
    {
        static public void Process(string s1,string s2,LogLevel ll,LogCategories lc)
        {
            NinjaTrader.Cbi.Log.Process(typeof(NinjaTrader.Custom.Resource), "Zweistein", new object[] { s1,s2 }, ll,lc);
        }

    }



}

