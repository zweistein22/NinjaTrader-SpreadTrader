// Decompiled with JetBrains decompiler
// Type: NinjaTrader.Gui.DisplayAttributeExtensions
// Assembly: NinjaTrader.Core, Version=8.0.9.0, Culture=neutral
// MVID: B655B0D7-AD5E-4644-8151-C22FBEFA884B
// Assembly location: C:\Program Files (x86)\NinjaTrader 8\bin\NinjaTrader.Core.dll

using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace NinjaTrader.Gui
{
  public static class DisplayAttributeExtensions
  {
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string GetValueSafe(this DisplayAttribute displayAttribute, DisplayAttributeExtensions.DisplayAttributeValue valueType)
    {
      return (string) null;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    static DisplayAttributeExtensions()
    {
     // \u003CAgileDotNetRTPro\u003E.Initialize();
     // \u003CAgileDotNetRTPro\u003E.PostInitialize();
    }

    public enum DisplayAttributeValue
    {
      Unset,
      Name,
      ShortName,
      GroupName,
      Description,
      Prompt,
    }
  }
}
