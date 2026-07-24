using System;
using System.Net.Http;
using System.Threading.Tasks;
using WpfApp1.Model;

namespace RustTweaker.Model
{
	// Token: 0x02000044 RID: 68
	public static class Statistic
	{
		// Token: 0x0600027C RID: 636 RVA: 0x0000D3C4 File Offset: 0x0000B7C4
		public static async Task<string> GetStatistics(string target)
		{
			SecureHttp secureHttp = new SecureHttp();
			HttpClient client = secureHttp.GetClient();
			var anon = new
			{
				query = Statistic.queryString,
				variables = new
				{
					id = target
				}
			};
			string text = P4258EBF.AFA7138A.M6233B19[330](anon);
			HttpResponseMessage httpResponseMessage = await P4258EBF.AFA7138A.M6233B19[583](client, "/graphql", P4258EBF.AFA7138A.M6233B19[321](text, P4258EBF.AFA7138A.M6233B19[204](), "application/json"));
			HttpResponseMessage httpResponseMessage2 = httpResponseMessage;
			P4258EBF.AFA7138A.M6233B19[538](httpResponseMessage2);
			return await P4258EBF.AFA7138A.M6233B19[623](P4258EBF.AFA7138A.M6233B19[405](httpResponseMessage2));
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000D408 File Offset: 0x0000B808
		public static async Task<string> GetFreindList(string target, int _offset)
		{
			SecureHttp secureHttp = new SecureHttp();
			HttpClient client = secureHttp.GetClient();
			var anon = new
			{
				query = Statistic.queryGetFriendsList,
				variables = new
				{
					id = target,
					offset = _offset
				}
			};
			string text = P4258EBF.AFA7138A.M6233B19[330](anon);
			HttpResponseMessage httpResponseMessage = await P4258EBF.AFA7138A.M6233B19[583](client, "/graphql", P4258EBF.AFA7138A.M6233B19[321](text, P4258EBF.AFA7138A.M6233B19[204](), "application/json"));
			HttpResponseMessage httpResponseMessage2 = httpResponseMessage;
			P4258EBF.AFA7138A.M6233B19[538](httpResponseMessage2);
			return await P4258EBF.AFA7138A.M6233B19[623](P4258EBF.AFA7138A.M6233B19[405](httpResponseMessage2));
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000D454 File Offset: 0x0000B854
		public static async Task<string> GetSimpleUserInfo(string target)
		{
			SecureHttp secureHttp = new SecureHttp();
			HttpClient client = secureHttp.GetClient();
			var anon = new
			{
				query = Statistic.querySimpleUserInfo,
				variables = new
				{
					id = target
				}
			};
			string text = P4258EBF.AFA7138A.M6233B19[330](anon);
			HttpResponseMessage httpResponseMessage = await P4258EBF.AFA7138A.M6233B19[583](client, "/graphql", P4258EBF.AFA7138A.M6233B19[321](text, P4258EBF.AFA7138A.M6233B19[204](), "application/json"));
			HttpResponseMessage httpResponseMessage2 = httpResponseMessage;
			P4258EBF.AFA7138A.M6233B19[538](httpResponseMessage2);
			return await P4258EBF.AFA7138A.M6233B19[623](P4258EBF.AFA7138A.M6233B19[405](httpResponseMessage2));
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000D498 File Offset: 0x0000B898
		public static async Task<string> GetLeaderboard(string statType, int limit = 5)
		{
			SecureHttp secureHttp = new SecureHttp();
			HttpClient client = secureHttp.GetClient();
			var anon = new
			{
				query = Statistic.queryLeaderboard,
				variables = new { statType, limit }
			};
			string text = P4258EBF.AFA7138A.M6233B19[330](anon);
			HttpResponseMessage httpResponseMessage = await P4258EBF.AFA7138A.M6233B19[583](client, "/graphql", P4258EBF.AFA7138A.M6233B19[321](text, P4258EBF.AFA7138A.M6233B19[204](), "application/json"));
			HttpResponseMessage httpResponseMessage2 = httpResponseMessage;
			P4258EBF.AFA7138A.M6233B19[538](httpResponseMessage2);
			return await P4258EBF.AFA7138A.M6233B19[623](P4258EBF.AFA7138A.M6233B19[405](httpResponseMessage2));
		}

		// Token: 0x040000C7 RID: 199
		private static string queryString = "query GetPlayerStatic($id: ID!) {\r\n  player(steamId: $id) {\r\n    steamid: steamId\r\n    personaname: nickname\r\n    avatar: avatarUrl\r\n    miniProfileBackground {\r\n      movieWebM\r\n    }\r\n    communityVisibilityState\r\n    rusttweaker_user: isRustTweakerUser\r\n    timecreated: accountCreated\r\n    accountbans: bans {\r\n      steamBans {\r\n        communityBanned\r\n        vacBanned\r\n        economyBan\r\n      }\r\n      bans {\r\n        status: status\r\n        server: server\r\n        date: date\r\n        reason: reason\r\n        expiredAt: expiredAt,\r\n        isVerified\r\n      }\r\n    }\r\n    steamlevel: steamLevel\r\n    friends_count: friendsCount {\r\n      value\r\n      userPosition\r\n      maxPosition\r\n    }\r\n    rust_inventory_cost: rustInventoryCost {\r\n      value\r\n      userPosition\r\n      maxPosition\r\n    }\r\n    rust_hours: rustHours {\r\n      value\r\n      userPosition\r\n      maxPosition\r\n    }\r\n    rust_hours_history: rustHoursHistory {\r\n      date\r\n      value\r\n    }\r\n    total_2week: total2Weeks\r\n    last_games: lastGames {\r\n      appid: appId\r\n      name: name\r\n      img: imageUrl\r\n      playtime_2weeks: playtime2Weeks\r\n    }\r\n    friends(limit: 0) {\r\n      friends {\r\n        steamId\r\n      }\r\n    }\r\n    personastate: personaState\r\n    currentGame: currentGame {\r\n      currentGameId\r\n      currentGameName\r\n    }\r\n    dataSource {\r\n      source\r\n      recordedAt\r\n    }\r\n    statistic: stats {\r\n      combat {\r\n        kills {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        deaths {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        headshots {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        headshotPercentage {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        kd {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        kdHistory {\r\n          date\r\n          value\r\n        }\r\n        maxUserKd {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        minUserKd {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n      }\r\n      shooting {\r\n        accuracy {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        minUserAccuracy {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        maxUserAccuracy {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        accuracyHistory {\r\n          date\r\n          value\r\n        }\r\n        shotsFired {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        shotsHit {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n      }\r\n      deaths {\r\n        suicide {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        fall {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        selfInflicted {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        byWolf {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        byBear {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        other {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n      }\r\n      injuries {\r\n        wounded {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        woundedHealed {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        woundedAssisted {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n      }\r\n      npcKills {\r\n        wolves {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        scientists {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        boars {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        bears {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        horses {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        stags {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        chickens {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n      }\r\n      resources {\r\n        wood {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        stone {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        metalOre {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        cloth {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        animalFat {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        lowGradeFuel {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        scrap {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n      }\r\n      builds {\r\n        blockPlaced {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        blockUpdated {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        wiresLink {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        pipesLink {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n        hosesLink {\r\n          value\r\n          userPosition\r\n          maxPosition\r\n        }\r\n      }\r\n    }\r\n  }\r\n}";

		// Token: 0x040000C8 RID: 200
		private static string queryLeaderboard = "query topUsersByStatistic($statType: StatType!, $limit: Int!) {\r\n  topUsersByStatistic(statType: $statType, limit: $limit) {\r\n    steamid\r\n    nickname\r\n    position\r\n    value\r\n    miniProfileBackground {\r\n      movieWebM\r\n    }\r\n    avatarUrl\r\n  }\r\n}";

		// Token: 0x040000C9 RID: 201
		private static string querySimpleUserInfo = "query getSimpleInfo($id: ID!) {\r\n  player(steamId: $id) {\r\n    nickname\r\n    avatarUrl\r\n    miniProfileBackground {\r\n      movieWebM\r\n    }\r\n    steamId\r\n    steamLevel\r\n  }\r\n}";

		// Token: 0x040000CA RID: 202
		private static string queryGetFriendsList = "query getFriends($id: ID!, $offset: Int) {\r\n  player(steamId: $id) {\r\n    friends(limit: 8, offset: $offset) {\r\n      totalCount\r\n      friends {\r\n        steamId\r\n        nickname\r\n        avatarUrl\r\n        isRustTweakerUser\r\n        miniProfileBackground {\r\n          movieWebM\r\n        }\r\n      }\r\n    }\r\n  }\r\n}";
	}
}
