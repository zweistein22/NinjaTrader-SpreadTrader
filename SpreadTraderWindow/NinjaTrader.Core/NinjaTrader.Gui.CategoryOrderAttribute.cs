// Decompiled with JetBrains decompiler
// Type: NinjaTrader.Gui.CategoryOrderAttribute
// Assembly: NinjaTrader.Core, Version=8.0.9.0, Culture=neutral
// MVID: B655B0D7-AD5E-4644-8151-C22FBEFA884B
// Assembly location: C:\Program Files (x86)\NinjaTrader 8\bin\NinjaTrader.Core.dll

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NinjaTrader.Gui
{
  /// <summary>
  /// Determines the sequence in which a NinjaScript object's Display.GroupName categories are arranged in relation to other categories in the UI.   The default behavior will display each GroupName of an object in alphabetical order, however this behavior can be changed by defining the CategoryOrder attribute before the object's declaration.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
  [EditorBrowsable(EditorBrowsableState.Always)]
  public sealed class CategoryOrderAttribute : Attribute
  {
    private string categoryName;
    private readonly Type resourceType;

    /// <summary>A string identifying the GroupName to be categorize</summary>
    [EditorBrowsable(EditorBrowsableState.Always)]
    public string Category
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (string) null;
      }
      private set
      {
        this.categoryName = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:NinjaTrader.Gui.CategoryOrderAttribute" /> class.
    /// </summary>
    /// <param name="category"></param>
    /// <param name="order">The order.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public CategoryOrderAttribute(string category, int order)
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public CategoryOrderAttribute(Type resourceType, string category, int order)
    {
    }

    /// <summary>An int determining the sequence the Category displays</summary>
    /// <value>The order.</value>
    [EditorBrowsable(EditorBrowsableState.Always)]
    public int Order { get; private set; }

    public Type ResourceType
    {
      get
      {
        return this.resourceType;
      }
    }

    public override object TypeId
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        return (object) null;
      }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    static CategoryOrderAttribute()
    {
    //  \u003CAgileDotNetRTPro\u003E.Initialize();
     // \u003CAgileDotNetRTPro\u003E.PostInitialize();
    }
  }
}
