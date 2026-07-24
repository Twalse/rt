using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace RustTweaker.Properties
{
	// Token: 0x0200001B RID: 27
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x0015A424 File Offset: 0x00157C24
		internal Resources()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00161324 File Offset: 0x0015EB24
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					ResourceManager resourceManager = P4258EBF.AFA7138A.M6233B19[377]("RustTweaker.Properties.Resources", P4258EBF.AFA7138A.M6233B19[91](P4258EBF.AFA7138A.M6233B19[22](typeof(Resources).TypeHandle)));
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00005DC5 File Offset: 0x000041C5
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00005DCC File Offset: 0x000041CC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x04000053 RID: 83
		private static ResourceManager resourceMan;

		// Token: 0x04000054 RID: 84
		private static CultureInfo resourceCulture;
	}
}
