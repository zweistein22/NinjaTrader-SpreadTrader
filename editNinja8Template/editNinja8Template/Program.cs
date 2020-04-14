using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zweistein;


namespace editNinja8Template
{
    class Program
    {
        static void Main(string[] args)
        {

            string strInstrument = "NQ 12-17 Globex";
            string B_strInstrument = "ES 12-17";
            int A = 4;
            int B = 3;
            double APriceDisplayMultiplier = 1.01;
            double BPriceDisplayMultiplier = 1.02;
            string pricestring = "Price=";
           
            NinjaTrader.Data.BarsPeriod bp = new NinjaTrader.Data.BarsPeriod();
            bp.BarsPeriodType = NinjaTrader.Data.BarsPeriodType.Minute;
            bp.BarsPeriodTypeName = NinjaTrader.Data.BarsPeriodType.Minute.ToString();
            bp.BaseBarsPeriodValue = 1;
            bp.Value = 15;
            bp.Value2 = 1;
            bp.MarketDataType = NinjaTrader.Data.MarketDataType.Last;
            bp.PointAndFigurePriceType = NinjaTrader.Data.PointAndFigurePriceType.Close;
            bp.ReversalType = NinjaTrader.Data.ReversalType.Tick;

            string current= TemplateExtensions.SpreadChartTemplate(strInstrument, B_strInstrument, A, B, APriceDisplayMultiplier, BPriceDisplayMultiplier, pricestring,bp);

        }

    }
}
