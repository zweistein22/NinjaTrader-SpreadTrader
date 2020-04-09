// Decompiled with JetBrains decompiler
// Type: NinjaTrader.Core.GeneralOptions
// Assembly: NinjaTrader.Core, Version=8.0.9.0, Culture=neutral
// MVID: B655B0D7-AD5E-4644-8151-C22FBEFA884B
// Assembly location: C:\Program Files (x86)\NinjaTrader 8\bin\NinjaTrader.Core.dll

using NinjaTrader.Gui;
//using NinjaTrader.NinjaScript.ShareServices;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace NinjaTrader.Core
{
  //[TypeConverter(typeof (GeneralOptionsConverter))]
  //[CategoryOrder(typeof (Resource), "GuiPreferences", 1)]
  //[CategoryOrder(typeof (Resource), "GuiGeneralOptionsSounds", 2)]
  //[CategoryOrder(typeof (Resource), "GuiGeneralOptionsMail", 3)]
  //[CategoryOrder(typeof (Resource), "GuiAppServer", 4)]
  [CategoryDefaultExpanded(true)]
  public sealed class GeneralOptions : ICloneable
  {
  //  private SupportedLanguage language;
   // private Collection<ShareService> shareServices;
    private TimeZoneInfo timeZoneInfo;
    private TimeZoneInfo timeZoneInfoPersisted;

   // [Display(GroupName = "GuiPreferences", Name = "GuiGeneralOptionsConfirmWindowClose", Order = 0, ResourceType = typeof (Resource))]
    public bool ConfirmWindowClose { get; set; }

    [Browsable(false)]
    public string CurrentUICultureSerializable
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (string) null;
      }
      [MethodImpl(MethodImplOptions.NoInlining)] set
      {
      }
    }

  //  [Display(GroupName = "GuiPreferences", Name = "GuiGeneralOptionsIsDrawingObjectsGlobalAcrossWorkspaces", Order = 10, ResourceType = typeof (Resource))]
    public bool IsGlobalDrawingObjectsAcrossWorkspaces { get; set; }

    [Browsable(false)]
    public bool HasSuperDomRoyaltiesWarned { get; set; }

//    [Display(GroupName = "GuiPreferences", Name = "GuiGeneralOptionsGlobalLinkButton", Order = 11, ResourceType = typeof (Resource))]
    public bool IsGlobalLinkButtonEnabled { get; set; }

    [Browsable(false)]
    public string MailFrom { get; set; }

//    [Display(GroupName = "GuiGeneralOptionsSounds", Name = "GuiGeneralOptionsSoundPlayConsecutively", Order = 9, ResourceType = typeof (Resource))]
    public bool SoundPlayConsecutively { get; set; }
/*
    [PropertyEditor("NinjaTrader.Gui.Tools.FilePathPicker", Filter = "WAV Files (*.wav)|*.wav")]
    [Display(GroupName = "GuiGeneralOptionsSounds", Name = "GuiGeneralOptionsSoundAnnouncement", Order = 10, ResourceType = typeof (Resource))]
    public string SoundAnnouncement { get; set; }

    [PropertyEditor("NinjaTrader.Gui.Tools.FilePathPicker", Filter = "WAV Files (*.wav)|*.wav")]
    [Display(GroupName = "GuiGeneralOptionsSounds", Name = "GuiGeneralOptionsSoundAutoBreakEven", Order = 20, ResourceType = typeof (Resource))]
    public string SoundAutoBreakeven { get; set; }

    [PropertyEditor("NinjaTrader.Gui.Tools.FilePathPicker", Filter = "WAV Files (*.wav)|*.wav")]
    [Display(GroupName = "GuiGeneralOptionsSounds", Name = "GuiGeneralOptionsSoundAutoChase", Order = 30, ResourceType = typeof (Resource))]
    public string SoundAutoChase { get; set; }

    [PropertyEditor("NinjaTrader.Gui.Tools.FilePathPicker", Filter = "WAV Files (*.wav)|*.wav")]
    [Display(GroupName = "GuiGeneralOptionsSounds", Name = "GuiGeneralOptionsSoundAutoTrail", Order = 40, ResourceType = typeof (Resource))]
    public string SoundAutoTrail { get; set; }

    [PropertyEditor("NinjaTrader.Gui.Tools.FilePathPicker", Filter = "WAV Files (*.wav)|*.wav")]
    [Display(GroupName = "GuiGeneralOptionsSounds", Name = "GuiGeneralOptionsSoundCompiledSuccessfully", Order = 45, ResourceType = typeof (Resource))]
    public string SoundCompiledSuccessfully { get; set; }

    [Display(GroupName = "GuiGeneralOptionsSounds", Name = "GuiGeneralOptionsSoundConnected", Order = 50, ResourceType = typeof (Resource))]
    [PropertyEditor("NinjaTrader.Gui.Tools.FilePathPicker", Filter = "WAV Files (*.wav)|*.wav")]
    public string SoundConnected { get; set; }

    [PropertyEditor("NinjaTrader.Gui.Tools.FilePathPicker", Filter = "WAV Files (*.wav)|*.wav")]
    [Display(GroupName = "GuiGeneralOptionsSounds", Name = "GuiGeneralOptionsSoundConnectionLost", Order = 60, ResourceType = typeof (Resource))]
    public string SoundConnectionLost { get; set; }

    [Display(GroupName = "GuiGeneralOptionsSounds", Name = "GuiGeneralOptionsSoundOrderCanceled", Order = 70, ResourceType = typeof (Resource))]
    [PropertyEditor("NinjaTrader.Gui.Tools.FilePathPicker", Filter = "WAV Files (*.wav)|*.wav")]
    public string SoundOrderCanceled { get; set; }

    [PropertyEditor("NinjaTrader.Gui.Tools.FilePathPicker", Filter = "WAV Files (*.wav)|*.wav")]
    [Display(GroupName = "GuiGeneralOptionsSounds", Name = "GuiGeneralOptionsSoundOrderFilled", Order = 80, ResourceType = typeof (Resource))]
    public string SoundOrderFilled { get; set; }

    [Display(GroupName = "GuiGeneralOptionsSounds", Name = "GuiGeneralOptionsSoundOrderPending", Order = 90, ResourceType = typeof (Resource))]
    [PropertyEditor("NinjaTrader.Gui.Tools.FilePathPicker", Filter = "WAV Files (*.wav)|*.wav")]
    public string SoundOrderPending { get; set; }

    [Display(GroupName = "GuiGeneralOptionsSounds", Name = "GuiGeneralOptionsSoundReversing", Order = 100, ResourceType = typeof (Resource))]
    [PropertyEditor("NinjaTrader.Gui.Tools.FilePathPicker", Filter = "WAV Files (*.wav)|*.wav")]
    public string SoundReversing { get; set; }

    [PropertyEditor("NinjaTrader.Gui.Tools.FilePathPicker", Filter = "WAV Files (*.wav)|*.wav")]
    [Display(GroupName = "GuiGeneralOptionsSounds", Name = "GuiGeneralOptionsSoundStopFilled", Order = 110, ResourceType = typeof (Resource))]
    public string SoundStopFilled { get; set; }

    [PropertyEditor("NinjaTrader.Gui.Tools.FilePathPicker", Filter = "WAV Files (*.wav)|*.wav")]
    [Display(GroupName = "GuiGeneralOptionsSounds", Name = "GuiGeneralOptionsSoundTargetFilled", Order = 120, ResourceType = typeof (Resource))]
    public string SoundTargetFilled { get; set; }

    [Display(GroupName = "GuiPreferences", Name = "GuiLanguage", Order = 30, ResourceType = typeof (Resource))]
    public SupportedLanguage Language
    {
      get
      {
        return this.language;
      }
      [MethodImpl(MethodImplOptions.NoInlining)] set
      {
      }
    }
    */
    [Browsable(false)]
    public bool KinetickGlobexNonProFees { get; set; }

    [Browsable(false)]
    public double LastSuperDomRoyalty { get; set; }

    [Browsable(false)]
    public int LogsLoaded { get; set; }

    [Browsable(false)]
    public string Name { get; set; }

    /*[Display(GroupName = "GuiPreferences", Name = "GuiGeneralOptionsPerformanceMetrics", Order = 5, Prompt = "GuiGeneralOptionsPerformanceMetricsPrompt", ResourceType = typeof (Resource))]
    [PropertyEditor("NinjaTrader.Gui.Tools.PerformanceMetricsEditor")]
    public Collection<string> PerformanceMetrics { get; set; }
    */
    [Browsable(false)]
    public bool PromptLanguageWarningForBasicEntry { get; set; }

    /*[PropertyEditor("NinjaTrader.Gui.Tools.ShareServiceEditor")]
    [Display(GroupName = "GuiPreferences", Name = "GuiGeneralOptionsSharingConnections", Order = 35, Prompt = "GuiGeneralOptionsShareServicesPrompt", ResourceType = typeof (Resource))]
    [XmlIgnore]
    public Collection<ShareService> ShareServices
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (Collection<ShareService>) null;
      }
      set
      {
        this.shareServices = value;
      }
    }
    */
  //  [Display(GroupName = "GuiPreferences", Name = "GuiGeneralOptionsShowToolTips", Order = 40, ResourceType = typeof (Resource))]
    public bool ShowToolTips { get; set; }
/*
    [Display(GroupName = "GuiPreferences", Name = "GuiSkin", Order = 50, ResourceType = typeof (Resource))]
    [TypeConverter(typeof (SkinConverter))]
    public string Skin { get; set; }
    */
    [Browsable(false)]
    public double SuperDomRoyaltiesWarningThreshold { get; set; }

    [Browsable(false)]
    [XmlIgnore]
    public bool TabRespositioningImmediate { get; set; }

    [XmlIgnore]
    [Browsable(false)]
    public TimeZoneInfo TimeZoneInfo
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (TimeZoneInfo) null;
      }
      set
      {
        this.timeZoneInfo = value;
      }
    }
/*
    [XmlIgnore]
    [Display(GroupName = "GuiPreferences", Name = "GuiGeneralOptionsTimeZone", Order = 60, ResourceType = typeof (Resource))]
    [TypeConverter(typeof (TimeZoneInfoConverter))]
    public TimeZoneInfo TimeZoneInfoPersisted
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (TimeZoneInfo) null;
      }
      set
      {
        this.timeZoneInfoPersisted = value;
      }
    }
    */
    [Browsable(false)]
    public string TimeZoneInfoSerializable
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (string) null;
      }
      [MethodImpl(MethodImplOptions.NoInlining)] set
      {
      }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public GeneralOptions()
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public object Clone()
    {
      return (object) null;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void CopyTo(GeneralOptions options)
    {
    }

    [Browsable(false)]
    [XmlIgnore]
    public CultureInfo CurrentCulture { get; set; }

    [XmlIgnore]
    [Browsable(false)]
    public CultureInfo CurrentUICulture { get; set; }

    [Browsable(false)]
    public int LogsMaintainedDays { get; set; }

  //  [Display(GroupName = "GuiPreferences", Name = "GuiGeneralOptionsMailAlertMsgs", Order = 6, ResourceType = typeof (Resource))]
    public string MailAlertMessagesTo { get; set; }

    [MethodImpl(MethodImplOptions.NoInlining)]
    static GeneralOptions()
    {
   //   \u003CAgileDotNetRTPro\u003E.Initialize();
    //  \u003CAgileDotNetRTPro\u003E.PostInitialize();
    }
  }
}
