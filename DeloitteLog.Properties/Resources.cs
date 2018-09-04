using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace DeloitteLog.Properties
{
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [DebuggerNonUserCode]
    [CompilerGenerated]
    public class Resources
    {
        private static ResourceManager resourceMan;
        private static CultureInfo resourceCulture;

        internal Resources()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (DeloitteLog.Properties.Resources.resourceMan == null)
                    DeloitteLog.Properties.Resources.resourceMan = new ResourceManager("DeloitteLog.Properties.Resources", typeof(DeloitteLog.Properties.Resources).Assembly);
                return DeloitteLog.Properties.Resources.resourceMan;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get
            {
                return DeloitteLog.Properties.Resources.resourceCulture;
            }
            set
            {
                DeloitteLog.Properties.Resources.resourceCulture = value;
            }
        }

        public static string TimeoutMSException
        {
            get
            {
                return DeloitteLog.Properties.Resources.ResourceManager.GetString(nameof(TimeoutMSException), DeloitteLog.Properties.Resources.resourceCulture);
            }
        }
    }
}
