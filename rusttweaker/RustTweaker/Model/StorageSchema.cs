using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RustTweaker.Model
{
	// Token: 0x02000045 RID: 69
	public class StorageSchema
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000D50D File Offset: 0x0000B90D
		// (set) Token: 0x06000282 RID: 642 RVA: 0x0000D515 File Offset: 0x0000B915
		[JsonProperty("favourites_command")]
		public List<string> FavouritesCommand { get; set; } = new List<string>();

		// Token: 0x06000283 RID: 643 RVA: 0x0015F194 File Offset: 0x0015C994
		public StorageSchema()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}
	}
}
