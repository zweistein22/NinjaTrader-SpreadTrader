// Decompiled with JetBrains decompiler
// Type: NinjaTrader.Core.Globals
// Assembly: NinjaTrader.Core, Version=8.0.9.0, Culture=neutral
// MVID: B655B0D7-AD5E-4644-8151-C22FBEFA884B
// Assembly location: C:\Program Files (x86)\NinjaTrader 8\bin\NinjaTrader.Core.dll

using NinjaTrader.Cbi;
using NinjaTrader.Data;
using NinjaTrader.Gui;
//using NinjaTrader.NinjaScript;
//using SharpDX.Direct2D1;
//using SharpDX.WIC;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Threading;
using System.Xml.Linq;

namespace NinjaTrader.Core
{
  public partial class Globals
  {
    private static string activeWorkspace;
   /// private static Dictionary<string, AddOnAuthorization> addOnRegistry;
    private static bool applicationStartingDone;
    //private static readonly AssemblyRegistry assemblyRegistry;
    //private static AtiOptions atiOptions;
    //private static readonly Collection<NinjaTrader.Cbi.ConnectOptions> connectOptions;
    private static string[] customSubDirs;
   // private static readonly SharpDX.Direct2D1.Factory d2DFactory;
    //private static readonly SharpDX.DirectWrite.Factory directWriteFactory;
    private static Dispatcher[] dispatchers;
    private static FileSystemWatcher fileTypeWatcher;
    private static bool inBackup;
    private static bool isApplicationStarting;
    private static bool isInfragisticsMAExceptionTraced;
    private static bool logOnFailedPfConfigDownload;
    private static string longTimeMillisecondPattern;
    private static string mailPasswordDefault;
    private static string mailServerDefault;
    private static int mailPortDefault;
    private static Thread mailThread;
    private static string mailUserDefault;
    private static Dispatcher mainThreadDispatcher;
    //private static MarketDataOptions marketDataOptions;
    private static Mutex mutex;
    private static int nextDispatcher;
    private static XDocument pfConfig;
    private static XDocument pfConfigBackup;
    //private static StrategiesOptions strategiesOptions;
    private static readonly List<string> soundErrors;
    private static readonly List<string> soundQueue;
    private static Thread soundThread;
    private static readonly object syncAtiOptions;
    private static readonly object syncDisconnectAll;
    private static readonly object syncMailJobs;
    private static readonly object syncMarketDataOptions;
    private static readonly object syncStrategiesOptions;
    private static readonly object syncUIThreads;
    private static readonly object syncUI;
    private static System.Timers.Timer timer;
    private static readonly Dictionary<string, TimeZoneInfo> timeZoneInfos;
    private static XDocument ui;
    //private static readonly ImagingFactory wicImagingFactory;
    internal const int AppServerPort = 31656;
    internal static object[] SyncConnectOptions;
    public static readonly Collection<Window> AllWindows;
    public static readonly TimeZoneInfo Cst;
    public static readonly TimeZoneInfo CstNdst;
    public static bool DoBackupOnClose;
    public static readonly TimeZoneInfo Est;
    public static int LinkGenerationGlobal;
    public static readonly string SalesMailAddress;
    public static readonly string SupportMailAddress;
    public static object[] SyncAdapterCrashFile;
    public static object[] SyncBackup;
  //  public static Func<Instrument, int, OrderAction, OrderType, double, double, object, bool> ConfirmOrderPlacementCallback;
    public static string[] TemplateSubfolders;
    /// <summary>
    /// The set of characters that are unreserved in RFC 2396 but are NOT unreserved in RFC 3986.
    /// </summary>
    private static readonly string[] uriRfc3986CharsToEscape;
  //  private static AdminOptions adminOptions;
    private static XDocument config;
    private static CultureInfo currentCultureClone;
    private static GeneralOptions generalOptions;
    private static DateTime lastTimeDownloadRemoteConfig;
    private static string[] localizedOrderActions;
    private static string[] localizedOrderStates;
    private static string[] localizedOrderTypes;
    private static string[] localizedTimeInForces;
    private static bool logOnFailedNTConfigDownload;
    private static XDocument ntConfig;
    private static XDocument ntConfigBackup;
    private static int ntConfigFilePollingSeconds;
  //  private static ServerOptions serverOptions;
    private static readonly object syncAdminOptions;
    private static readonly object syncGeneralOptions;
    private static readonly object syncGlobals;
    private static readonly object syncLocalization;
    private static readonly object syncServerOptions;
    private static readonly object syncTradingOptions;
    private static readonly object syncUserDataDir;
 //   private static TradingOptions tradingOptions;
    private static string userDataDir;
    public static readonly DateTime ContinuousContractExpiry;
    internal static List<object[]> MailQueue;
    public static readonly DateTime MaxDate;
    public static readonly DateTime MinDate;

    public static string ActiveWorkspace
    {
      get
      {
        return Globals.activeWorkspace;
      }
      [MethodImpl(MethodImplOptions.NoInlining)] set
      {
      }
    }

    public static event EventHandler ActiveWorkspaceChanged
    {
      [MethodImpl(MethodImplOptions.NoInlining)] add
      {
      }
      [MethodImpl(MethodImplOptions.NoInlining)] remove
      {
      }
    }

    public static string AdapterCrashFileName { get; set; }

        /*
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static Dictionary<string, AddOnAuthorization> AddOnRegistry
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (Dictionary<string, AddOnAuthorization>) null;
      }
      [MethodImpl(MethodImplOptions.NoInlining)] set
      {
      }
    }
    */
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static bool ApplicationExit()
    {
      return false;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static bool ApplicationStart(Action<string> splashMessageCallback, Action<string> errorCallback, Func<string, int> userFeedbackCallbackYesNoAll)
    {
      return false;
    }
    /*
    public static AssemblyRegistry AssemblyRegistry
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (AssemblyRegistry) null;
      }
    }

    public static AtiOptions AtiOptions
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (AtiOptions) null;
      }
    }
    */
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void BackupWithConfigSettings(Action callback)
    {
    }

    public static string BuyPlatformURL
    {
      get
      {
        return "http://www.ninjatrader.com/BuyPlatform";
      }
    }

    public static string CompanyName
    {
      get
      {
        return "NinjaTrader, LLC";
      }
    }
    /*
    public static Collection<NinjaTrader.Cbi.ConnectOptions> ConnectOptions
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (Collection<NinjaTrader.Cbi.ConnectOptions>) null;
      }
    }
    */
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void CopyAndInstallAssemblies(string dir, bool isFirstAttempt, string assemblyToLoad, Collection<string> installedAssembliesToLeave)
    {
    }

    /*
    public static Func<ILoadingDialog> CreateLoadingDialog { get; set; }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static bool CreateNT8Mutex()
    {
      return false;
    }
    */
    /*
    public static Func<string, IProgress> CreateProgressWindow { get; set; }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void CreateSubDirs(string file)
    {
    }
    */
    public static string[] CustomSubDirs
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (string[]) null;
      }
    }
    /*
    [CLSCompliant(false)]
    public static SharpDX.Direct2D1.Factory D2DFactory
    {
      get
      {
        return Globals.d2DFactory;
      }
    }

    [CLSCompliant(false)]
    public static SharpDX.DirectWrite.Factory DirectWriteFactory
    {
      get
      {
        return Globals.directWriteFactory;
      }
    }
    
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void DecompressArchiveToTmpDir(string archiveName, string tmpDir, bool fromRestore, Collection<string> allFiles, IProgress progress)
    {
    }
    */
    [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string Decrypt(string text, string key)
    {
      return (string) null;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static string DecryptLegacy(string text, string decrKey)
    {
      return (string) null;
    }

    public static void DeleteFile(string path)
    {
      //NativeMethods.DeleteFileOperation(path);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T Deserialize<T>(string xml)
    {
      return default (T);
    }
/*
    [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void DoBackup(XDocument info, string archiveName, bool hadCompileError, bool backupConfigurationFiles, bool backupDatabase, bool backupHistoricalChartData, bool backupLogAndTraceFiles, bool backupMarketReplay, bool backupNinjaScriptFiles, bool backupTemplates, bool backupWorkspaces, IProgress progress, Action<string> messageCallback, Action callback)
    {
    }
    
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void DoRestore(string archiveName, int entitiesToRestore, IProgress progress, Action<string> messageCallback, Action<bool> callback)
    {
    }
    */
    [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string Encrypt(string text, string key)
    {
      return (string) null;
    }

    /// <summary>
    /// <para>Returns a result verifying that the token we received is valid. </para>
    /// <para>https://developers.facebook.com/docs/facebook-login/manually-build-a-login-flow#confirm </para>
    /// </summary>
    /// <param name="oauth_token"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task<string> ExchangeFacebookTokenAsync(string oauth_token)
    {
      return (Task<string>) null;
    }

    
    /// <summary>Formats a value to USD currency value.</summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string FormatCurrency(double value)
    {
            return "USD";
    //  return Globals.FormatCurrency(value, NinjaTrader.Cbi.Currency.UsDollar);
    }

    /// <summary>
    /// Formats a value to the position's account denomination value if it exists, or to the current culture selected in Tools -&gt; Options -&gt; General if does not exist
    /// </summary>
    /// <param name="value"></param>
    /// <param name="position"></param>
    /// <returns></returns>
   /* [MethodImpl(MethodImplOptions.NoInlining)]
    public static string FormatCurrency(double value, Position position)
    {
      return (string) null;
    }
    
    /// <summary>
    /// Format a value using a strategy account's Denomination property
    /// </summary>
    /// <param name="value"></param>
    /// <param name="strategy"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string FormatCurrency(double value, StrategyBase strategy)
    {
      return (string) null;
    }
    */
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string[] GetAddOnsInDll(string dllFile)
    {
      return (string[]) null;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string[] GetAddOnsInZip(string zipFile)
    {
      return (string[]) null;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int GetExpiryYear(int yearInDecade)
    {
      return 0;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string GetIncrementedCounterName(Collection<string> existingNames, string newName)
    {
      return (string) null;
    }

    /*[MethodImpl(MethodImplOptions.NoInlining)]
    public static int GetLotSize(Account account, Instrument instrument)
    {
      return 0;
    }
    */
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static TimeZoneInfo GetTimeZoneInfo(Exchange exchange)
    {
      return (TimeZoneInfo) null;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string GetTwitterSignature(string url, string method, OrderedDictionary parameters)
    {
      return (string) null;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string GetTwitterSignature(string url, string method, string secret, OrderedDictionary parameters)
    {
      return (string) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static DateTime GetValidTime(TimeZoneInfo timeZoneInfo, DateTime time, bool resolveAmbigous = true)
    {
      return new DateTime();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static List<string> GetWrapperFiles(string path)
    {
      return (List<string>) null;
    }

    private Globals()
    {
    }

    public static string HelpGuideUrl
    {
      get
      {
        return "http://www.ninjatrader.com/support/helpGuides/nt8/";
      }
    }

    public static string InstallDir
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (string) null;
      }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static bool IsImportVersionCheckOk(string zipFilename)
    {
      return false;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static bool IsValidFileName(string name)
    {
      return false;
    }

    public static Assembly LoadFrom3rdParty(string file)
    {
      return Assembly.UnsafeLoadFrom(file);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string ToLocalizedObject(BarsPeriodType value, CultureInfo cultureInfo = null)
    {
      return (string) null;
    }

    public static string LongTimeMillisecondPattern
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (string) null;
      }
    }

    public static Dispatcher MainThreadDispatcher
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (Dispatcher) null;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static string MailPasswordDefault
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (string) null;
      }
    }

    internal static int MailPortDefault
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return 0;
      }
    }

    internal static string MailServerDefault
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (string) null;
      }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void MailThreadProc()
    {
    }

    internal static string MailUserDefault
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (string) null;
      }
    }
/*
    public static MarketDataOptions MarketDataOptions
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (MarketDataOptions) null;
      }
    }
    */
    public static Action<string> InformationMessageCallback { get; set; }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void OnFileTypeWatcherChanged(object sender, FileSystemEventArgs e)
    {
    }

    /*[MethodImpl(MethodImplOptions.NoInlining)]
    private static void OnFileTypeWatcherChangedNow(string archiveName, Action<string> messageCallback, Func<StartupInfoType, string, bool> userFeedbackCallback)
    {
    }
    */
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void OnUnhandledApplicationException(Exception exception)
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void OnUnhandledApplicationException(object sender, UnhandledExceptionEventArgs e)
    {
    }

    public static string OpenLiveAccountURL
    {
      get
      {
        return "http://www.ninjatrader.com/OpenAccount#!OpenAccount_Futures";
      }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static long ParseQuantity(string txt)
    {
      return 0;
    }

    public static XDocument PFConfig
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (XDocument) null;
      }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void PFConfigReset()
    {
    }

    /*[MethodImpl(MethodImplOptions.NoInlining)]
    public static void PlaySound(SoundType soundType, Account account = null)
    {
    }
    */
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void PlaySound(string file)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static string PrimaryServerFtpDir
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (string) null;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static string PrimaryServerFtpPwd
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (string) null;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static string PrimaryServerFtpUser
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (string) null;
      }
    }

    public static Dispatcher RandomDispatcher
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (Dispatcher) null;
      }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void RemoveBasenameSubfiles(Collection<string> dllBasenamesToRemove)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static string ScriptServer
    {
      get
      {
        return "www.ninjatrader-support2.com";
      }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void SendMail(string to, string cc, string subject, string text, string[] attachmentPaths)
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void SendMailNow(string to, string cc, string subject, string body, string[] attachmentPaths)
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void SendMailToSupportAsync(string from, string to, string subject, string body)
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string Serialize<T>(T data)
    {
      return (string) null;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void SetFileTimestamp(string filename, DateTime newTime)
    {
    }

    /*public static StrategiesOptions StrategiesOptions
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (StrategiesOptions) null;
      }
    }
    */
    public static Collection<string> SupportedLanguages { get; }

    public static Dictionary<string, TimeZoneInfo> TimeZoneInfoDictionary
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (Dictionary<string, TimeZoneInfo>) null;
      }
    }

    /// <summary>
    /// Converts the provided time to the time zone selected in Tools -&gt; Options -&gt; General
    /// </summary>
    /// <param name="localTime"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static DateTime ToApplicationTime(DateTime localTime)
    {
      return new DateTime();
    }

    /// <summary>
    /// Converts the provided time from the provided time zone to the time zone selected in Tools -&gt; Options -&gt; General
    /// </summary>
    /// <param name="localTime"></param>
    /// <param name="sourceTimeZone"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static DateTime ToApplicationTime(DateTime localTime, TimeZoneInfo sourceTimeZone)
    {
      return new DateTime();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string ToFileName(string name)
    {
      return (string) null;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void UnzipInfoFile(string archiveName, string tmpInfoDir)
    {
    }

    public static XDocument UI
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (XDocument) null;
      }
      [MethodImpl(MethodImplOptions.NoInlining)] set
      {
      }
    }

    /// <summary>
    /// Converts a string to its RFC3986-compliant escaped representation as appropriate for use in URI string or other encoding requirements.
    /// </summary>
    /// <param name="value">The string to be encoded</param>
    /// <returns>The encoded string</returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string UrlEncode(string value)
    {
      return (string) null;
    }

    //public static Func<StartupInfoType, string, bool> UserFeedbackCallback { get; set; }

    public static Action<string, string, string, string, string> VendorLicenseNotification { get; set; }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static string VendorCode { get; set; }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static bool VerifyFileSizes(string pathCustom, string pathVendor)
    {
      return false;
    }

    public static string Website
    {
      get
      {
        return "www.ninjatrader.com";
      }
    }

    /*[CLSCompliant(false)]
    public static ImagingFactory WicImagingFactory
    {
      get
      {
        return Globals.wicImagingFactory;
      }
    }
    
    public static AdminOptions AdminOptions
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (AdminOptions) null;
      }
    }
    */
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int CheckPasswordPolicy(string userName, string password)
    {
      return 0;
    }

    public static XDocument Config
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (XDocument) null;
      }
      set
      {
        Globals.config = value;
      }
    }

    /// <summary>
    /// Format a value to a culturally-appropriate string using the provided currency. 1234.56 and USDollar currency returns $1,234.56.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="currency"></param>
    /// <returns></returns>
    /*[MethodImpl(MethodImplOptions.NoInlining)]
    public static string FormatCurrency(double value, NinjaTrader.Cbi.Currency currency)
    {
      return (string) null;
    }
    */
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string FormatQuantity(long quantity, bool round)
    {
      return (string) null;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string FormatYValue(double value)
    {
      return (string) null;
    }

    public static GeneralOptions GeneralOptions
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (GeneralOptions) null;
      }
    }
    
    /*[MethodImpl(MethodImplOptions.NoInlining)]
    public static Tuple<string, int> GetConnectPoint(ConnectionType connectionType, bool useWebSocket = false, string system = null)
    {
      return (Tuple<string, int>) null;
    }
    
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string GetCurrencySymbol(NinjaTrader.Cbi.Currency currency)
    {
      return (string) null;
    }
    */
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string GetTickFormatString(double tickSize)
    {
      return (string) null;
    }

    public static bool IsAdapterAsProcess { get; set; }

    public static bool IsApplicationExiting { get; set; }

    internal static bool IsOffline
    {
      get
      {
        return false;
      }
    }

    public static bool IsInSafeMode { get; set; }

    /*[MethodImpl(MethodImplOptions.NoInlining)]
    public static void File2ZipArchive(ZipArchive zipArchive, string entryName, string fileName, bool isBackup = false)
    {
    }
    */

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static object FromLocalizedObject(Type type, string txt, CultureInfo currentUICulture = null)
    {
      return (object) null;
    }

    public static string MachineName
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (string) null;
      }
    }

    public static XDocument NTConfig
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (XDocument) null;
      }
    }

    public static DateTime Now
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return new DateTime();
      }
    }

    internal static string PrimaryServer
    {
      get
      {
        return "license.ninjatrader.com";
      }
    }

    public static string ProductName
    {
      get
      {
        return "NinjaTrader";
      }
    }

    internal static string SecondaryServer
    {
      get
      {
        return "license2.ninjatrader.com";
      }
    }
    /*
    public static ServerOptions ServerOptions
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (ServerOptions) null;
      }
    }
    */
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string ToLocalizedObject(object value, CultureInfo cultureInfo = null)
    {
      return (string) null;
    }

    /*[MethodImpl(MethodImplOptions.NoInlining)]
    public static string ToLocalizedObject(OrderAction orderAction)
    {
      return (string) null;
    }
    */
    /*
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string ToLocalizedObject(OrderState orderState)
    {
      return (string) null;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string ToLocalizedObject(OrderType orderType)
    {
      return (string) null;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string ToLocalizedObject(TimeInForce timeInForce)
    {
      return (string) null;
    }
    
    public static TraceListener TraceListener { get; set; }

    public static TradingOptions TradingOptions
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (TradingOptions) null;
      }
    }
    */

    /// <summary>
    /// Returns the full path of the NinjaTrader 8 folder found in the user's 'Documents' folder.
    /// </summary>
    /// 
    /*
    public static string UserDataDir
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (string) null;
      }
    }
        */

    static Globals()
    {
      //\u003CAgileDotNetRTPro\u003E.Initialize();
      //\u003CAgileDotNetRTPro\u003E.PostInitialize();
      Globals.activeWorkspace = string.Empty;
     // Globals.addOnRegistry = (Dictionary<string, AddOnAuthorization>) null;
      Globals.applicationStartingDone = false;
    //  Globals.assemblyRegistry = new AssemblyRegistry();
   //   Globals.connectOptions = new Collection<NinjaTrader.Cbi.ConnectOptions>();
      Globals.customSubDirs = (string[]) null;
  //    Globals.d2DFactory = new SharpDX.Direct2D1.Factory(SharpDX.Direct2D1.FactoryType.MultiThreaded, DebugLevel.None);
    //  Globals.directWriteFactory = new SharpDX.DirectWrite.Factory(SharpDX.DirectWrite.FactoryType.Shared);
      Globals.inBackup = false;
      Globals.isInfragisticsMAExceptionTraced = false;
      Globals.logOnFailedPfConfigDownload = true;
      Globals.longTimeMillisecondPattern = (string) null;
      Globals.mailPortDefault = 587;
      Globals.nextDispatcher = -1;
      Globals.soundErrors = new List<string>();
      Globals.soundQueue = new List<string>();
      Globals.syncAtiOptions = new object();
      Globals.syncDisconnectAll = new object();
      Globals.syncMailJobs = new object();
      Globals.syncMarketDataOptions = new object();
      Globals.syncStrategiesOptions = new object();
      Globals.syncUIThreads = new object();
      Globals.syncUI = new object();
      Globals.timeZoneInfos = new Dictionary<string, TimeZoneInfo>();
      //Globals.wicImagingFactory = new ImagingFactory();
      Globals.SyncConnectOptions = new object[0];
      Globals.AllWindows = new Collection<Window>();
      Globals.Cst = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
      Globals.CstNdst = TimeZoneInfo.CreateCustomTimeZone("CSTNDST", new TimeSpan(-6, 0, 0), "Central Standard Time (no daylight saving)", "Central Standard Time (no daylight saving)");
      Globals.DoBackupOnClose = false;
      Globals.Est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
      Globals.LinkGenerationGlobal = 0;
      Globals.SalesMailAddress = "platformsales@ninjatrader.com";
      Globals.SupportMailAddress = "platformsupport@ninjatrader.com";
      Globals.SyncAdapterCrashFile = new object[0];
      Globals.SyncBackup = new object[0];
      Globals.SupportedLanguages = new Collection<string>()
      {
        "zh-Hant",
        "de-DE",
        "en-US",
        "es-ES",
        "ru-RU",
        "pt-PT",
        "fr-FR"
      };
      Globals.TemplateSubfolders = new string[22]
      {
        "AlertAction",
        "AtmStrategy",
        "Chart",
        "ChartSeries",
        "ChartStyle",
        "Commission",
        "DrawingTool",
        "Fee",
        "GlobalDrawingObject",
        "HotListAnalyzer",
        "Indicator",
        "MarketAnalyzer",
        "MarketDataEntitlement",
        "Risk",
        "Skins",
        "StopStrategy",
        "Strategy",
        "StrategyAnalyzer",
        "StrategyWizardAction",
        "StrategyWizardCondition",
        "SuperDom",
        "TradingHours"
      };
      Globals.uriRfc3986CharsToEscape = new string[5]
      {
        "!",
        "*",
        "'",
        "(",
        ")"
      };
      Globals.currentCultureClone = (CultureInfo) null;
      Globals.lastTimeDownloadRemoteConfig = Globals.MinDate;
      Globals.logOnFailedNTConfigDownload = true;
      Globals.ntConfigFilePollingSeconds = 900;
      Globals.syncAdminOptions = new object();
      Globals.syncGeneralOptions = new object();
      Globals.syncGlobals = new object();
      Globals.syncLocalization = new object();
      Globals.syncServerOptions = new object();
      Globals.syncTradingOptions = new object();
      Globals.syncUserDataDir = new object();
      Globals.ContinuousContractExpiry = new DateTime(1900, 1, 1);
      Globals.MailQueue = new List<object[]>();
      Globals.MaxDate = new DateTime(2099, 12, 1);
      Globals.MinDate = new DateTime(1800, 1, 1);
    }

    private class FacebookAuthJsonResult
    {
      public string access_token { get; set; }

      public string token_type { get; set; }

      [MethodImpl(MethodImplOptions.NoInlining)]
      static FacebookAuthJsonResult()
      {
   //     \u003CAgileDotNetRTPro\u003E.Initialize();
     //   \u003CAgileDotNetRTPro\u003E.PostInitialize();
      }
    }
  }
}
