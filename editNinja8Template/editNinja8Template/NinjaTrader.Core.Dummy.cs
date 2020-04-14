using System.Collections.Generic;
using System;



namespace NinjaTrader.Data
{
    public class BarsPeriod
    {
        public BarsPeriod() { }
        public BarsPeriodType BarsPeriodType { get; set; }
        public int BarsPeriodTypeSerialize
        {
            get
            {
                return (int)this.BarsPeriodType;
            }
            set
            {
                this.BarsPeriodType = (BarsPeriodType)value;
            }
        }

        public string BarsPeriodTypeName
        {
            get; set;
            
        }


        public BarsPeriodType BaseBarsPeriodType { get; set; }

        public int BaseBarsPeriodValue { get; set; }


        public MarketDataType MarketDataType { get; set; }

        public PointAndFigurePriceType PointAndFigurePriceType { get; set; }

        public ReversalType ReversalType { get; set; }

       

        public int Value { get; set; }

        public int Value2 { get; set; }

    }
}
namespace NinjaTrader.Data
{

    public enum ReversalType
    {
        Percent,
        Tick,
    }
    public enum PointAndFigurePriceType
    {
        Close,
        HighsAndLows,
    }
    public enum MarketDataType
    {
        Ask,
        Bid,
        Last,
        DailyHigh,
        DailyLow,
        DailyVolume,
        LastClose,
        Opening,
        OpenInterest,
        Settlement,
        Unknown,
    }

    public enum BarsPeriodType
    {
        Tick,
        Volume,
        Range,
        Second,
        Minute,
        Day,
        Week,
        Month,
        Year,
        HeikenAshi,
        Kagi,
        Renko,
        PointAndFigure,
        LineBreak,
    }
}

namespace NinjaTrader {


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
    namespace Cbi {

        public enum LogLevel
        {
            Information,
            Warning,
            Error,
            Alert
        }

        public class LogEventArgs
        {
            public static void ProcessEventArgs(LogEventArgs lea){


            }

            public LogEventArgs(string s, LogLevel l)
            {

            }
            


        }
        
        public enum Exchange {
            Amex,
            Nasdaq,
            Arca,
            Box,
            Island,
            Nybot,
            Phlx,
            Swx,
            Nyse,
            EurexSW,
            Idem,
            Xetra,
            Eurex,
            Globex,
            Cme,
            Nymex
        }

        public enum InstrumentType
        {
            Stock,
            Future,
            Option,
        }

        public class MasterInstrument
        {
            string name;
            Cbi.Exchange exchange;
            Cbi.InstrumentType instrumenttype;
            public MasterInstrument(string n,Cbi.Exchange ex)
            {
                name = n;
                exchange = ex;
                instrumenttype = Cbi.InstrumentType.Future;

            }
            public string Name
            {
                get { return name; }

            }
            public string Description
            {
                get { return name; }

            }
            public Cbi.Exchange Exchange
            {
                get { return exchange; }

            }
            public Cbi.InstrumentType InstrumentType
            {

                get { return instrumenttype; }
            }

        }

        public class Instrument
        {
            Instrument GetInstrument(string instrumentName,bool create)
            {
               return null;
            }
          

        }
        public class InstrumentList
        {

            List<Instrument> instruments = new List<Instrument>();
            static public InstrumentList GetObject(string s)
            {
                InstrumentList Il = new InstrumentList(s);
                return Il;
               
            }

            InstrumentList(string list)
            {
                instruments.Add(new Instrument());

            }

            public List<Instrument> Instruments
            {
                get { return instruments; }
            }





        }
    }
}