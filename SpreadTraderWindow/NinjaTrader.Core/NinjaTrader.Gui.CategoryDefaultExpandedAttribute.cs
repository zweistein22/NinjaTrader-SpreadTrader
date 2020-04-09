// Decompiled with JetBrains decompiler
// Type: NinjaTrader.Gui.CategoryDefaultExpandedAttribute
// Assembly: NinjaTrader.Core, Version=8.0.9.0, Culture=neutral
// MVID: B655B0D7-AD5E-4644-8151-C22FBEFA884B
// Assembly location: C:\Program Files (x86)\NinjaTrader 8\bin\NinjaTrader.Core.dll

using System;
using System.Runtime.CompilerServices;

namespace NinjaTrader.Gui
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
  public class CategoryDefaultExpandedAttribute : Attribute
  {
    public bool Expanded { get; private set; }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public CategoryDefaultExpandedAttribute(bool expanded)
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    static CategoryDefaultExpandedAttribute()
    {
    //  \u003CAgileDotNetRTPro\u003E.Initialize();
    //  \u003CAgileDotNetRTPro\u003E.PostInitialize();
    }
  }
}
