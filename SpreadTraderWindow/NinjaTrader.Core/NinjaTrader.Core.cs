using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Runtime.CompilerServices;


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

namespace NinjaTrader
{


    namespace Core
    {
        public partial class Globals
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
    namespace Cbi
    {

        public enum LogLevel
        {
            Information,
            Warning,
            Error,
            Alert
        }

        public class LogEventArgs
        {
            public static void ProcessEventArgs(LogEventArgs lea)
            {


            }

            public LogEventArgs(string s, LogLevel l)
            {

            }



        }

        public enum Exchange
        {
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
            public MasterInstrument(string n, Cbi.Exchange ex)
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
            Instrument GetInstrument(string instrumentName, bool create)
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



namespace NinjaTrader.Gui
{
    /// <summary>
    /// Controls the Browsable state of the category with corresponding properties.
    /// Supports the "*" (All) wildcard determining whether all the categories within the given class should be Browsable.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class BrowsableCategoryAttribute : Attribute
    {
        private readonly Type resourceType;
        private string categoryName;
        /// <summary>
        /// Determines a wildcard for all categories to be affected.
        /// </summary>
        public const string All = "*";

        /// <summary>Gets the name of the category.</summary>
        /// <value>The name of the category.</value>
        public string CategoryName
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return (string)null;
            }
            private set
            {
                this.categoryName = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether category is browsable.
        /// </summary>
        /// <value><c>true</c> if category should be displayed at run time; otherwise, <c>false</c>.</value>
        public bool Browsable { get; private set; }

        public Type ResourceType
        {
            get
            {
                return this.resourceType;
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public BrowsableCategoryAttribute(Type resourceType, string categoryName, bool browsable)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public BrowsableCategoryAttribute(Type resourceType, string categoryName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:NinjaTrader.Gui.BrowsableCategoryAttribute" /> class.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        /// <param name="browsable">if set to <c>true</c> the category is browsable.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public BrowsableCategoryAttribute(string categoryName, bool browsable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:NinjaTrader.Gui.BrowsableCategoryAttribute" /> class.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        public BrowsableCategoryAttribute(string categoryName)
          : this(categoryName, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:NinjaTrader.Gui.BrowsableCategoryAttribute" /> class.
        /// </summary>
        /// <param name="browsable">if set to <c>true</c> all categories are browsable; otherwise hidden</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public BrowsableCategoryAttribute(bool browsable)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static BrowsableCategoryAttribute()
        {
      //\u003CAgileDotNetRTPro\u003E.Initialize();
      //\u003CAgileDotNetRTPro\u003E.PostInitialize();
        }
    }
}

namespace NinjaTrader.Gui
{
    /// <summary>
    /// Controls Browsable state of the property without having access to property declaration or inherited property.
    /// Supports a "*" (All) wildcard determining whether all the properties within the given class should be Browsable.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
    public sealed class BrowsablePropertyAttribute : Attribute
    {
        /// <summary>
        /// Determines a wildcard for all properties to be affected.
        /// </summary>
        public const string All = "*";

        /// <summary>Gets the name of the property.</summary>
        /// <value>The name of the property.</value>
        public string PropertyName { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether property is browsable.
        /// </summary>
        /// <value><c>true</c> if property should be displayed at run time; otherwise, <c>false</c>.</value>
        public bool Browsable { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:NinjaTrader.Gui.BrowsablePropertyAttribute" /> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="browsable">if set to <c>true</c> the property is browsable.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public BrowsablePropertyAttribute(string propertyName, bool browsable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:NinjaTrader.Gui.BrowsablePropertyAttribute" /> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public BrowsablePropertyAttribute(string propertyName)
          : this(propertyName, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:NinjaTrader.Gui.BrowsablePropertyAttribute" /> class.
        /// </summary>
        /// <param name="browsable">if set to <c>true</c> all public properties are browsable; otherwise hidden.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public BrowsablePropertyAttribute(bool browsable)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static BrowsablePropertyAttribute()
        {
    //  \u003CAgileDotNetRTPro\u003E.Initialize();
    //  \u003CAgileDotNetRTPro\u003E.PostInitialize();
        }
    }
}
