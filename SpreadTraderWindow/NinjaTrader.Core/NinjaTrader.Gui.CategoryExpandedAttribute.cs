// Decompiled with JetBrains decompiler
// Type: NinjaTrader.Gui.CategoryExpandedAttribute
// Assembly: NinjaTrader.Core, Version=8.0.9.0, Culture=neutral
// MVID: B655B0D7-AD5E-4644-8151-C22FBEFA884B
// Assembly location: C:\Program Files (x86)\NinjaTrader 8\bin\NinjaTrader.Core.dll

using System;
using System.Runtime.CompilerServices;

namespace NinjaTrader.Gui
{
  /// <summary>
  /// dmh - Controls the default expanded state of a category in the case of categories being displayed w/ expanders
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
  public sealed class CategoryExpandedAttribute : Attribute
  {
    private readonly string categoryName;
    private readonly Type resourceType;

    public string Category
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (string) null;
      }
    }

    public bool Expanded { get; private set; }

    public Type ResourceType
    {
      get
      {
        return this.resourceType;
      }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public CategoryExpandedAttribute(Type resourceType, string category, bool expanded)
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public CategoryExpandedAttribute(string category, bool expanded)
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    static CategoryExpandedAttribute()
    {
     // \u003CAgileDotNetRTPro\u003E.Initialize();
     // \u003CAgileDotNetRTPro\u003E.PostInitialize();
    }
  }
}
