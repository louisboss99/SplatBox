using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace GeckoMapTester.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (GeckoMapTester.Properties.Resources.resourceMan == null)
          GeckoMapTester.Properties.Resources.resourceMan = new ResourceManager("GeckoMapTester.Properties.Resources", typeof (GeckoMapTester.Properties.Resources).Assembly);
        return GeckoMapTester.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return GeckoMapTester.Properties.Resources.resourceCulture;
      }
      set
      {
        GeckoMapTester.Properties.Resources.resourceCulture = value;
      }
    }

    internal Resources()
    {
    }
  }
}
