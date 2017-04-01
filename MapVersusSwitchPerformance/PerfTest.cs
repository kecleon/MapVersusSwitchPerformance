using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibKRelay.Data;
using LibKRelay;
using LibKRelay.Messages;
using System.Diagnostics;

namespace MapVersusSwitchPerformance
{
	public class PerfTest
	{
		public static Random ran = new Random(1337);

		public static void Main(string[] args)
		{
			Console.WriteLine("Started");
			GameObject go = new GameObject();
			Console.WriteLine("Created Object");

			Stopwatch sw = new Stopwatch();
			sw.Start();
			List<Status> stats = new List<Status>();
			for (int i = 0; i < 200000; i++)
			{
				stats.Add(GenStatus());
			}
			sw.Stop();
			Console.WriteLine("Generated " + stats.Count + " Statuses in " + sw.ElapsedMilliseconds + "ms");

			sw.Restart();
			foreach (Status s in stats)
			{
				go.ParseStatusSwitch(s);
			}
			sw.Stop();
			Console.WriteLine("Switch finished in " + sw.ElapsedMilliseconds + "ms");
			sw.Restart();
			foreach (Status s in stats)
			{
				go.ParseStatusDelegate(s);
			}
			sw.Stop();
			Console.WriteLine("Delegate finished in " + sw.ElapsedMilliseconds + "ms");

			Console.ReadLine();
		}

		public static Status GenStatus()
		{
			Status st = new Status();
			st.ObjectId = ran.Next();
			st.Position = new Location(ran.NextDouble(), ran.NextDouble());
			st.Stats = new Dictionary<StatType, StatData>();
			int adder = 0;
			for (int x = 0; x < ran.Next(0, 93); x++)
			{
				adder = 0;
				byte id = (byte)x;
				do
				{
					id = (byte)(x + adder++);
				} while (((id < 23) && (id > 25)) && !st.Stats.ContainsKey((StatType)id));

				StatData sd = new StatData((StatType)id, ran.Next());
				byte[] bytes = new byte[30];
				ran.NextBytes(bytes);
				sd.StringValue = Encoding.ASCII.GetString(bytes);
				st.Stats.Add(sd.Id, sd);
			}
			return st;
		}
	}

	public class GameObject
	{
		public int ObjectId;
		public Location Position;

		public int MaximumHP;
		public int HP;
		public int Size;
		public int MaximumMP;
		public int MP;
		public int NextLevelExperience;
		public int Experience;
		public int Level;
		public int Inventory0;
		public int Inventory1;
		public int Inventory2;
		public int Inventory3;
		public int Inventory4;
		public int Inventory5;
		public int Inventory6;
		public int Inventory7;
		public int Inventory8;
		public int Inventory9;
		public int Inventory10;
		public int Inventory11;
		public int Attack;
		public int Defense;
		public int Speed;
		public int Vitality;
		public int Wisdom;
		public int Dexterity;
		public int Effects;
		public int Stars;
		public string Name;
		public int Texture1;
		public int Texture2;
		public int MerchandiseType;
		public int Credits;
		public int MerchandisePrice;
		public int PortalUsable;
		public string AccountId;
		public int AccountFame;
		public int MerchandiseCurrency;
		public int ObjectConnection;
		public int MerchandiseRemainingCount;
		public int MerchandiseRemainingMinutes;
		public int MerchandiseDiscount;
		public int MerchandiseRankRequirement;
		public int HealthBonus;
		public int ManaBonus;
		public int AttackBonus;
		public int DefenseBonus;
		public int SpeedBonus;
		public int VitalityBonus;
		public int WisdomBonus;
		public int DexterityBonus;
		public string OwnerAccountId;
		public int RankRequired;
		public int NameChosen;
		public int CharacterFame;
		public int CharacterFameGoal;
		public int Glowing;
		public int SinkLevel;
		public int AltTextureIndex;
		public string GuildName;
		public int GuildRank;
		public int OxygenBar;
		public int XpBoosterActive;
		public int XpBoostTime;
		public int LootDropBoostTime;
		public int LootTierBoostTime;
		public int HealthPotionCount;
		public int MagicPotionCount;
		public int Backpack0;
		public int Backpack1;
		public int Backpack2;
		public int Backpack3;
		public int Backpack4;
		public int Backpack5;
		public int Backpack6;
		public int Backpack7;
		public int HasBackpack;
		public int Skin;
		public int PetInstanceId;
		public string PetName;
		public int PetType;
		public int PetRarity;
		public int PetMaximumLevel;
		public int PetFamily;
		public int PetPoints0;
		public int PetPoints1;
		public int PetPoints2;
		public int PetLevel0;
		public int PetLevel1;
		public int PetLevel2;
		public int PetAbilityType0;
		public int PetAbilityType1;
		public int PetAbilityType2;
		public int Effects2;
		public int FortuneTokens;

		public Delegate[] StatMap = new Delegate[98];
		public delegate void DelSetStatMaximumHP(StatData sd);
		public delegate void DelSetStatHP(StatData sd);
		public delegate void DelSetStatSize(StatData sd);
		public delegate void DelSetStatMaximumMP(StatData sd);
		public delegate void DelSetStatMP(StatData sd);
		public delegate void DelSetStatNextLevelExperience(StatData sd);
		public delegate void DelSetStatExperience(StatData sd);
		public delegate void DelSetStatLevel(StatData sd);
		public delegate void DelSetStatInventory0(StatData sd);
		public delegate void DelSetStatInventory1(StatData sd);
		public delegate void DelSetStatInventory2(StatData sd);
		public delegate void DelSetStatInventory3(StatData sd);
		public delegate void DelSetStatInventory4(StatData sd);
		public delegate void DelSetStatInventory5(StatData sd);
		public delegate void DelSetStatInventory6(StatData sd);
		public delegate void DelSetStatInventory7(StatData sd);
		public delegate void DelSetStatInventory8(StatData sd);
		public delegate void DelSetStatInventory9(StatData sd);
		public delegate void DelSetStatInventory10(StatData sd);
		public delegate void DelSetStatInventory11(StatData sd);
		public delegate void DelSetStatAttack(StatData sd);
		public delegate void DelSetStatDefense(StatData sd);
		public delegate void DelSetStatSpeed(StatData sd);
		public delegate void DelSetStatVitality(StatData sd);
		public delegate void DelSetStatWisdom(StatData sd);
		public delegate void DelSetStatDexterity(StatData sd);
		public delegate void DelSetStatEffects(StatData sd);
		public delegate void DelSetStatStars(StatData sd);
		public delegate void DelSetStatName(StatData sd);
		public delegate void DelSetStatTexture1(StatData sd);
		public delegate void DelSetStatTexture2(StatData sd);
		public delegate void DelSetStatMerchandiseType(StatData sd);
		public delegate void DelSetStatCredits(StatData sd);
		public delegate void DelSetStatMerchandisePrice(StatData sd);
		public delegate void DelSetStatPortalUsable(StatData sd);
		public delegate void DelSetStatAccountId(StatData sd);
		public delegate void DelSetStatAccountFame(StatData sd);
		public delegate void DelSetStatMerchandiseCurrency(StatData sd);
		public delegate void DelSetStatObjectConnection(StatData sd);
		public delegate void DelSetStatMerchandiseRemainingCount(StatData sd);
		public delegate void DelSetStatMerchandiseRemainingMinutes(StatData sd);
		public delegate void DelSetStatMerchandiseDiscount(StatData sd);
		public delegate void DelSetStatMerchandiseRankRequirement(StatData sd);
		public delegate void DelSetStatHealthBonus(StatData sd);
		public delegate void DelSetStatManaBonus(StatData sd);
		public delegate void DelSetStatAttackBonus(StatData sd);
		public delegate void DelSetStatDefenseBonus(StatData sd);
		public delegate void DelSetStatSpeedBonus(StatData sd);
		public delegate void DelSetStatVitalityBonus(StatData sd);
		public delegate void DelSetStatWisdomBonus(StatData sd);
		public delegate void DelSetStatDexterityBonus(StatData sd);
		public delegate void DelSetStatOwnerAccountId(StatData sd);
		public delegate void DelSetStatRankRequired(StatData sd);
		public delegate void DelSetStatNameChosen(StatData sd);
		public delegate void DelSetStatCharacterFame(StatData sd);
		public delegate void DelSetStatCharacterFameGoal(StatData sd);
		public delegate void DelSetStatGlowing(StatData sd);
		public delegate void DelSetStatSinkLevel(StatData sd);
		public delegate void DelSetStatAltTextureIndex(StatData sd);
		public delegate void DelSetStatGuildName(StatData sd);
		public delegate void DelSetStatGuildRank(StatData sd);
		public delegate void DelSetStatOxygenBar(StatData sd);
		public delegate void DelSetStatXpBoosterActive(StatData sd);
		public delegate void DelSetStatXpBoostTime(StatData sd);
		public delegate void DelSetStatLootDropBoostTime(StatData sd);
		public delegate void DelSetStatLootTierBoostTime(StatData sd);
		public delegate void DelSetStatHealthPotionCount(StatData sd);
		public delegate void DelSetStatMagicPotionCount(StatData sd);
		public delegate void DelSetStatBackpack0(StatData sd);
		public delegate void DelSetStatBackpack1(StatData sd);
		public delegate void DelSetStatBackpack2(StatData sd);
		public delegate void DelSetStatBackpack3(StatData sd);
		public delegate void DelSetStatBackpack4(StatData sd);
		public delegate void DelSetStatBackpack5(StatData sd);
		public delegate void DelSetStatBackpack6(StatData sd);
		public delegate void DelSetStatBackpack7(StatData sd);
		public delegate void DelSetStatHasBackpack(StatData sd);
		public delegate void DelSetStatSkin(StatData sd);
		public delegate void DelSetStatPetInstanceId(StatData sd);
		public delegate void DelSetStatPetName(StatData sd);
		public delegate void DelSetStatPetType(StatData sd);
		public delegate void DelSetStatPetRarity(StatData sd);
		public delegate void DelSetStatPetMaximumLevel(StatData sd);
		public delegate void DelSetStatPetFamily(StatData sd);
		public delegate void DelSetStatPetPoints0(StatData sd);
		public delegate void DelSetStatPetPoints1(StatData sd);
		public delegate void DelSetStatPetPoints2(StatData sd);
		public delegate void DelSetStatPetLevel0(StatData sd);
		public delegate void DelSetStatPetLevel1(StatData sd);
		public delegate void DelSetStatPetLevel2(StatData sd);
		public delegate void DelSetStatPetAbilityType0(StatData sd);
		public delegate void DelSetStatPetAbilityType1(StatData sd);
		public delegate void DelSetStatPetAbilityType2(StatData sd);
		public delegate void DelSetStatEffects2(StatData sd);
		public delegate void DelSetStatFortuneTokens(StatData sd);
		public delegate void DelNOP(StatData sd);

		public GameObject()
		{
			DelSetStatMaximumHP delSetStatMaximumHP = SetStatMaximumHP;
			StatMap[0] = delSetStatMaximumHP;
			DelSetStatHP delSetStatHP = SetStatHP;
			StatMap[1] = delSetStatHP;
			DelSetStatSize delSetStatSize = SetStatSize;
			StatMap[2] = delSetStatSize;
			DelSetStatMaximumMP delSetStatMaximumMP = SetStatMaximumMP;
			StatMap[3] = delSetStatMaximumMP;
			DelSetStatMP delSetStatMP = SetStatMP;
			StatMap[4] = delSetStatMP;
			DelSetStatNextLevelExperience delSetStatNextLevelExperience = SetStatNextLevelExperience;
			StatMap[5] = delSetStatNextLevelExperience;
			DelSetStatExperience delSetStatExperience = SetStatExperience;
			StatMap[6] = delSetStatExperience;
			DelSetStatLevel delSetStatLevel = SetStatLevel;
			StatMap[7] = delSetStatLevel;
			DelSetStatInventory0 delSetStatInventory0 = SetStatInventory0;
			StatMap[8] = delSetStatInventory0;
			DelSetStatInventory1 delSetStatInventory1 = SetStatInventory1;
			StatMap[9] = delSetStatInventory1;
			DelSetStatInventory2 delSetStatInventory2 = SetStatInventory2;
			StatMap[10] = delSetStatInventory2;
			DelSetStatInventory3 delSetStatInventory3 = SetStatInventory3;
			StatMap[11] = delSetStatInventory3;
			DelSetStatInventory4 delSetStatInventory4 = SetStatInventory4;
			StatMap[12] = delSetStatInventory4;
			DelSetStatInventory5 delSetStatInventory5 = SetStatInventory5;
			StatMap[13] = delSetStatInventory5;
			DelSetStatInventory6 delSetStatInventory6 = SetStatInventory6;
			StatMap[14] = delSetStatInventory6;
			DelSetStatInventory7 delSetStatInventory7 = SetStatInventory7;
			StatMap[15] = delSetStatInventory7;
			DelSetStatInventory8 delSetStatInventory8 = SetStatInventory8;
			StatMap[16] = delSetStatInventory8;
			DelSetStatInventory9 delSetStatInventory9 = SetStatInventory9;
			StatMap[17] = delSetStatInventory9;
			DelSetStatInventory10 delSetStatInventory10 = SetStatInventory10;
			StatMap[18] = delSetStatInventory10;
			DelSetStatInventory11 delSetStatInventory11 = SetStatInventory11;
			StatMap[19] = delSetStatInventory11;
			DelSetStatAttack delSetStatAttack = SetStatAttack;
			StatMap[20] = delSetStatAttack;
			DelSetStatDefense delSetStatDefense = SetStatDefense;
			StatMap[21] = delSetStatDefense;
			DelSetStatSpeed delSetStatSpeed = SetStatSpeed;
			StatMap[22] = delSetStatSpeed;
			DelSetStatVitality delSetStatVitality = SetStatVitality;
			DelNOP delNOP = NOP;
			StatMap[23] = delNOP;
			StatMap[24] = delNOP;
			StatMap[25] = delNOP;
			StatMap[26] = delSetStatVitality;
			DelSetStatWisdom delSetStatWisdom = SetStatWisdom;
			StatMap[27] = delSetStatWisdom;
			DelSetStatDexterity delSetStatDexterity = SetStatDexterity;
			StatMap[28] = delSetStatDexterity;
			DelSetStatEffects delSetStatEffects = SetStatEffects;
			StatMap[29] = delSetStatEffects;
			DelSetStatStars delSetStatStars = SetStatStars;
			StatMap[30] = delSetStatStars;
			DelSetStatName delSetStatName = SetStatName;
			StatMap[31] = delSetStatName;
			DelSetStatTexture1 delSetStatTexture1 = SetStatTexture1;
			StatMap[32] = delSetStatTexture1;
			DelSetStatTexture2 delSetStatTexture2 = SetStatTexture2;
			StatMap[33] = delSetStatTexture2;
			DelSetStatMerchandiseType delSetStatMerchandiseType = SetStatMerchandiseType;
			StatMap[34] = delSetStatMerchandiseType;
			DelSetStatCredits delSetStatCredits = SetStatCredits;
			StatMap[35] = delSetStatCredits;
			DelSetStatMerchandisePrice delSetStatMerchandisePrice = SetStatMerchandisePrice;
			StatMap[36] = delSetStatMerchandisePrice;
			DelSetStatPortalUsable delSetStatPortalUsable = SetStatPortalUsable;
			StatMap[37] = delSetStatPortalUsable;
			DelSetStatAccountId delSetStatAccountId = SetStatAccountId;
			StatMap[38] = delSetStatAccountId;
			DelSetStatAccountFame delSetStatAccountFame = SetStatAccountFame;
			StatMap[39] = delSetStatAccountFame;
			DelSetStatMerchandiseCurrency delSetStatMerchandiseCurrency = SetStatMerchandiseCurrency;
			StatMap[40] = delSetStatMerchandiseCurrency;
			DelSetStatObjectConnection delSetStatObjectConnection = SetStatObjectConnection;
			StatMap[41] = delSetStatObjectConnection;
			DelSetStatMerchandiseRemainingCount delSetStatMerchandiseRemainingCount = SetStatMerchandiseRemainingCount;
			StatMap[42] = delSetStatMerchandiseRemainingCount;
			DelSetStatMerchandiseRemainingMinutes delSetStatMerchandiseRemainingMinutes = SetStatMerchandiseRemainingMinutes;
			StatMap[43] = delSetStatMerchandiseRemainingMinutes;
			DelSetStatMerchandiseDiscount delSetStatMerchandiseDiscount = SetStatMerchandiseDiscount;
			StatMap[44] = delSetStatMerchandiseDiscount;
			DelSetStatMerchandiseRankRequirement delSetStatMerchandiseRankRequirement = SetStatMerchandiseRankRequirement;
			StatMap[45] = delSetStatMerchandiseRankRequirement;
			DelSetStatHealthBonus delSetStatHealthBonus = SetStatHealthBonus;
			StatMap[46] = delSetStatHealthBonus;
			DelSetStatManaBonus delSetStatManaBonus = SetStatManaBonus;
			StatMap[47] = delSetStatManaBonus;
			DelSetStatAttackBonus delSetStatAttackBonus = SetStatAttackBonus;
			StatMap[48] = delSetStatAttackBonus;
			DelSetStatDefenseBonus delSetStatDefenseBonus = SetStatDefenseBonus;
			StatMap[49] = delSetStatDefenseBonus;
			DelSetStatSpeedBonus delSetStatSpeedBonus = SetStatSpeedBonus;
			StatMap[50] = delSetStatSpeedBonus;
			DelSetStatVitalityBonus delSetStatVitalityBonus = SetStatVitalityBonus;
			StatMap[51] = delSetStatVitalityBonus;
			DelSetStatWisdomBonus delSetStatWisdomBonus = SetStatWisdomBonus;
			StatMap[52] = delSetStatWisdomBonus;
			DelSetStatDexterityBonus delSetStatDexterityBonus = SetStatDexterityBonus;
			StatMap[53] = delSetStatDexterityBonus;
			DelSetStatOwnerAccountId delSetStatOwnerAccountId = SetStatOwnerAccountId;
			StatMap[54] = delSetStatOwnerAccountId;
			DelSetStatRankRequired delSetStatRankRequired = SetStatRankRequired;
			StatMap[55] = delSetStatRankRequired;
			DelSetStatNameChosen delSetStatNameChosen = SetStatNameChosen;
			StatMap[56] = delSetStatNameChosen;
			DelSetStatCharacterFame delSetStatCharacterFame = SetStatCharacterFame;
			StatMap[57] = delSetStatCharacterFame;
			DelSetStatCharacterFameGoal delSetStatCharacterFameGoal = SetStatCharacterFameGoal;
			StatMap[58] = delSetStatCharacterFameGoal;
			DelSetStatGlowing delSetStatGlowing = SetStatGlowing;
			StatMap[59] = delSetStatGlowing;
			DelSetStatSinkLevel delSetStatSinkLevel = SetStatSinkLevel;
			StatMap[60] = delSetStatSinkLevel;
			DelSetStatAltTextureIndex delSetStatAltTextureIndex = SetStatAltTextureIndex;
			StatMap[61] = delSetStatAltTextureIndex;
			DelSetStatGuildName delSetStatGuildName = SetStatGuildName;
			StatMap[62] = delSetStatGuildName;
			DelSetStatGuildRank delSetStatGuildRank = SetStatGuildRank;
			StatMap[63] = delSetStatGuildRank;
			DelSetStatOxygenBar delSetStatOxygenBar = SetStatOxygenBar;
			StatMap[64] = delSetStatOxygenBar;
			DelSetStatXpBoosterActive delSetStatXpBoosterActive = SetStatXpBoosterActive;
			StatMap[65] = delSetStatXpBoosterActive;
			DelSetStatXpBoostTime delSetStatXpBoostTime = SetStatXpBoostTime;
			StatMap[66] = delSetStatXpBoostTime;
			DelSetStatLootDropBoostTime delSetStatLootDropBoostTime = SetStatLootDropBoostTime;
			StatMap[67] = delSetStatLootDropBoostTime;
			DelSetStatLootTierBoostTime delSetStatLootTierBoostTime = SetStatLootTierBoostTime;
			StatMap[68] = delSetStatLootTierBoostTime;
			DelSetStatHealthPotionCount delSetStatHealthPotionCount = SetStatHealthPotionCount;
			StatMap[69] = delSetStatHealthPotionCount;
			DelSetStatMagicPotionCount delSetStatMagicPotionCount = SetStatMagicPotionCount;
			StatMap[70] = delSetStatMagicPotionCount;
			DelSetStatBackpack0 delSetStatBackpack0 = SetStatBackpack0;
			StatMap[71] = delSetStatBackpack0;
			DelSetStatBackpack1 delSetStatBackpack1 = SetStatBackpack1;
			StatMap[72] = delSetStatBackpack1;
			DelSetStatBackpack2 delSetStatBackpack2 = SetStatBackpack2;
			StatMap[73] = delSetStatBackpack2;
			DelSetStatBackpack3 delSetStatBackpack3 = SetStatBackpack3;
			StatMap[74] = delSetStatBackpack3;
			DelSetStatBackpack4 delSetStatBackpack4 = SetStatBackpack4;
			StatMap[75] = delSetStatBackpack4;
			DelSetStatBackpack5 delSetStatBackpack5 = SetStatBackpack5;
			StatMap[76] = delSetStatBackpack5;
			DelSetStatBackpack6 delSetStatBackpack6 = SetStatBackpack6;
			StatMap[77] = delSetStatBackpack6;
			DelSetStatBackpack7 delSetStatBackpack7 = SetStatBackpack7;
			StatMap[78] = delSetStatBackpack7;
			DelSetStatHasBackpack delSetStatHasBackpack = SetStatHasBackpack;
			StatMap[79] = delSetStatHasBackpack;
			DelSetStatSkin delSetStatSkin = SetStatSkin;
			StatMap[80] = delSetStatSkin;
			DelSetStatPetInstanceId delSetStatPetInstanceId = SetStatPetInstanceId;
			StatMap[81] = delSetStatPetInstanceId;
			DelSetStatPetName delSetStatPetName = SetStatPetName;
			StatMap[82] = delSetStatPetName;
			DelSetStatPetType delSetStatPetType = SetStatPetType;
			StatMap[83] = delSetStatPetType;
			DelSetStatPetRarity delSetStatPetRarity = SetStatPetRarity;
			StatMap[84] = delSetStatPetRarity;
			DelSetStatPetMaximumLevel delSetStatPetMaximumLevel = SetStatPetMaximumLevel;
			StatMap[85] = delSetStatPetMaximumLevel;
			DelSetStatPetFamily delSetStatPetFamily = SetStatPetFamily;
			StatMap[86] = delSetStatPetFamily;
			DelSetStatPetPoints0 delSetStatPetPoints0 = SetStatPetPoints0;
			StatMap[87] = delSetStatPetPoints0;
			DelSetStatPetPoints1 delSetStatPetPoints1 = SetStatPetPoints1;
			StatMap[88] = delSetStatPetPoints1;
			DelSetStatPetPoints2 delSetStatPetPoints2 = SetStatPetPoints2;
			StatMap[89] = delSetStatPetPoints2;
			DelSetStatPetLevel0 delSetStatPetLevel0 = SetStatPetLevel0;
			StatMap[90] = delSetStatPetLevel0;
			DelSetStatPetLevel1 delSetStatPetLevel1 = SetStatPetLevel1;
			StatMap[91] = delSetStatPetLevel1;
			DelSetStatPetLevel2 delSetStatPetLevel2 = SetStatPetLevel2;
			StatMap[92] = delSetStatPetLevel2;
			DelSetStatPetAbilityType0 delSetStatPetAbilityType0 = SetStatPetAbilityType0;
			StatMap[93] = delSetStatPetAbilityType0;
			DelSetStatPetAbilityType1 delSetStatPetAbilityType1 = SetStatPetAbilityType1;
			StatMap[94] = delSetStatPetAbilityType1;
			DelSetStatPetAbilityType2 delSetStatPetAbilityType2 = SetStatPetAbilityType2;
			StatMap[95] = delSetStatPetAbilityType2;
			DelSetStatEffects2 delSetStatEffects2 = SetStatEffects2;
			StatMap[96] = delSetStatEffects2;
			DelSetStatFortuneTokens delSetStatFortuneTokens = SetStatFortuneTokens;
			StatMap[97] = delSetStatFortuneTokens;
		}

		public void ParseStatusDelegate(Status status)
		{
			ObjectId = status.ObjectId;
			Position = status.Position;

			foreach (var stato in status.Stats)
			{
				byte id = (byte)stato.Key;
				StatData stat = stato.Value;

				StatMap[id].Method.Invoke(this, new object[] { stat }); //TODO: figure out why id is sometimes 23
			}
		}

		public void ParseStatusSwitch(Status status)
		{
			ObjectId = status.ObjectId;
			Position = status.Position;

			foreach (var stato in status.Stats)
			{
				byte id = (byte)stato.Key;
				StatData stat = stato.Value;
				switch (id)
				{
					case 0: MaximumHP = stat.IntValue; break;
					case 1: HP = stat.IntValue; break;
					case 2: Size = stat.IntValue; break;
					case 3: MaximumMP = stat.IntValue; break;
					case 4: MP = stat.IntValue; break;
					case 5: NextLevelExperience = stat.IntValue; break;
					case 6: Experience = stat.IntValue; break;
					case 7: Level = stat.IntValue; break;
					case 8: Inventory0 = stat.IntValue; break;
					case 9: Inventory1 = stat.IntValue; break;
					case 10: Inventory2 = stat.IntValue; break;
					case 11: Inventory3 = stat.IntValue; break;
					case 12: Inventory4 = stat.IntValue; break;
					case 13: Inventory5 = stat.IntValue; break;
					case 14: Inventory6 = stat.IntValue; break;
					case 15: Inventory7 = stat.IntValue; break;
					case 16: Inventory8 = stat.IntValue; break;
					case 17: Inventory9 = stat.IntValue; break;
					case 18: Inventory10 = stat.IntValue; break;
					case 19: Inventory11 = stat.IntValue; break;
					case 20: Attack = stat.IntValue; break;
					case 21: Defense = stat.IntValue; break;
					case 22: Speed = stat.IntValue; break;
					case 26: Vitality = stat.IntValue; break;
					case 27: Wisdom = stat.IntValue; break;
					case 28: Dexterity = stat.IntValue; break;
					case 29: Effects = stat.IntValue; break;
					case 30: Stars = stat.IntValue; break;
					case 31: Name = stat.StringValue; break;
					case 32: Texture1 = stat.IntValue; break;
					case 33: Texture2 = stat.IntValue; break;
					case 34: MerchandiseType = stat.IntValue; break;
					case 35: Credits = stat.IntValue; break;
					case 36: MerchandisePrice = stat.IntValue; break;
					case 37: PortalUsable = stat.IntValue; break;
					case 38: AccountId = stat.StringValue; break;
					case 39: AccountFame = stat.IntValue; break;
					case 40: MerchandiseCurrency = stat.IntValue; break;
					case 41: ObjectConnection = stat.IntValue; break;
					case 42: MerchandiseRemainingCount = stat.IntValue; break;
					case 43: MerchandiseRemainingMinutes = stat.IntValue; break;
					case 44: MerchandiseDiscount = stat.IntValue; break;
					case 45: MerchandiseRankRequirement = stat.IntValue; break;
					case 46: HealthBonus = stat.IntValue; break;
					case 47: ManaBonus = stat.IntValue; break;
					case 48: AttackBonus = stat.IntValue; break;
					case 49: DefenseBonus = stat.IntValue; break;
					case 50: SpeedBonus = stat.IntValue; break;
					case 51: VitalityBonus = stat.IntValue; break;
					case 52: WisdomBonus = stat.IntValue; break;
					case 53: DexterityBonus = stat.IntValue; break;
					case 54: OwnerAccountId = stat.StringValue; break;
					case 55: RankRequired = stat.IntValue; break;
					case 56: NameChosen = stat.IntValue; break;
					case 57: CharacterFame = stat.IntValue; break;
					case 58: CharacterFameGoal = stat.IntValue; break;
					case 59: Glowing = stat.IntValue; break;
					case 60: SinkLevel = stat.IntValue; break;
					case 61: AltTextureIndex = stat.IntValue; break;
					case 62: GuildName = stat.StringValue; break;
					case 63: GuildRank = stat.IntValue; break;
					case 64: OxygenBar = stat.IntValue; break;
					case 65: XpBoosterActive = stat.IntValue; break;
					case 66: XpBoostTime = stat.IntValue; break;
					case 67: LootDropBoostTime = stat.IntValue; break;
					case 68: LootTierBoostTime = stat.IntValue; break;
					case 69: HealthPotionCount = stat.IntValue; break;
					case 70: MagicPotionCount = stat.IntValue; break;
					case 71: Backpack0 = stat.IntValue; break;
					case 72: Backpack1 = stat.IntValue; break;
					case 73: Backpack2 = stat.IntValue; break;
					case 74: Backpack3 = stat.IntValue; break;
					case 75: Backpack4 = stat.IntValue; break;
					case 76: Backpack5 = stat.IntValue; break;
					case 77: Backpack6 = stat.IntValue; break;
					case 78: Backpack7 = stat.IntValue; break;
					case 79: HasBackpack = stat.IntValue; break;
					case 80: Skin = stat.IntValue; break;
					case 81: PetInstanceId = stat.IntValue; break;
					case 82: PetName = stat.StringValue; break;
					case 83: PetType = stat.IntValue; break;
					case 84: PetRarity = stat.IntValue; break;
					case 85: PetMaximumLevel = stat.IntValue; break;
					case 86: PetFamily = stat.IntValue; break;
					case 87: PetPoints0 = stat.IntValue; break;
					case 88: PetPoints1 = stat.IntValue; break;
					case 89: PetPoints2 = stat.IntValue; break;
					case 90: PetLevel0 = stat.IntValue; break;
					case 91: PetLevel1 = stat.IntValue; break;
					case 92: PetLevel2 = stat.IntValue; break;
					case 93: PetAbilityType0 = stat.IntValue; break;
					case 94: PetAbilityType1 = stat.IntValue; break;
					case 95: PetAbilityType2 = stat.IntValue; break;
					case 96: Effects2 = stat.IntValue; break;
					case 97: FortuneTokens = stat.IntValue; break;
				}
			}
		}

		public void NOP(StatData sd)
		{

		}

		public void SetStatMaximumHP(StatData sd)
		{
			MaximumHP = sd.IntValue;
		}

		public void SetStatHP(StatData sd)
		{
			HP = sd.IntValue;
		}

		public void SetStatSize(StatData sd)
		{
			Size = sd.IntValue;
		}

		public void SetStatMaximumMP(StatData sd)
		{
			MaximumMP = sd.IntValue;
		}

		public void SetStatMP(StatData sd)
		{
			MP = sd.IntValue;
		}

		public void SetStatNextLevelExperience(StatData sd)
		{
			NextLevelExperience = sd.IntValue;
		}

		public void SetStatExperience(StatData sd)
		{
			Experience = sd.IntValue;
		}

		public void SetStatLevel(StatData sd)
		{
			Level = sd.IntValue;
		}

		public void SetStatInventory0(StatData sd)
		{
			Inventory0 = sd.IntValue;
		}

		public void SetStatInventory1(StatData sd)
		{
			Inventory1 = sd.IntValue;
		}

		public void SetStatInventory2(StatData sd)
		{
			Inventory2 = sd.IntValue;
		}

		public void SetStatInventory3(StatData sd)
		{
			Inventory3 = sd.IntValue;
		}

		public void SetStatInventory4(StatData sd)
		{
			Inventory4 = sd.IntValue;
		}

		public void SetStatInventory5(StatData sd)
		{
			Inventory5 = sd.IntValue;
		}

		public void SetStatInventory6(StatData sd)
		{
			Inventory6 = sd.IntValue;
		}

		public void SetStatInventory7(StatData sd)
		{
			Inventory7 = sd.IntValue;
		}

		public void SetStatInventory8(StatData sd)
		{
			Inventory8 = sd.IntValue;
		}

		public void SetStatInventory9(StatData sd)
		{
			Inventory9 = sd.IntValue;
		}

		public void SetStatInventory10(StatData sd)
		{
			Inventory10 = sd.IntValue;
		}

		public void SetStatInventory11(StatData sd)
		{
			Inventory11 = sd.IntValue;
		}

		public void SetStatAttack(StatData sd)
		{
			Attack = sd.IntValue;
		}

		public void SetStatDefense(StatData sd)
		{
			Defense = sd.IntValue;
		}

		public void SetStatSpeed(StatData sd)
		{
			Speed = sd.IntValue;
		}

		public void SetStatVitality(StatData sd)
		{
			Vitality = sd.IntValue;
		}

		public void SetStatWisdom(StatData sd)
		{
			Wisdom = sd.IntValue;
		}

		public void SetStatDexterity(StatData sd)
		{
			Dexterity = sd.IntValue;
		}

		public void SetStatEffects(StatData sd)
		{
			Effects = sd.IntValue;
		}

		public void SetStatStars(StatData sd)
		{
			Stars = sd.IntValue;
		}

		public void SetStatName(StatData sd)
		{
			Name = sd.StringValue;
		}

		public void SetStatTexture1(StatData sd)
		{
			Texture1 = sd.IntValue;
		}

		public void SetStatTexture2(StatData sd)
		{
			Texture2 = sd.IntValue;
		}

		public void SetStatMerchandiseType(StatData sd)
		{
			MerchandiseType = sd.IntValue;
		}

		public void SetStatCredits(StatData sd)
		{
			Credits = sd.IntValue;
		}

		public void SetStatMerchandisePrice(StatData sd)
		{
			MerchandisePrice = sd.IntValue;
		}

		public void SetStatPortalUsable(StatData sd)
		{
			PortalUsable = sd.IntValue;
		}

		public void SetStatAccountId(StatData sd)
		{
			AccountId = sd.StringValue;
		}

		public void SetStatAccountFame(StatData sd)
		{
			AccountFame = sd.IntValue;
		}

		public void SetStatMerchandiseCurrency(StatData sd)
		{
			MerchandiseCurrency = sd.IntValue;
		}

		public void SetStatObjectConnection(StatData sd)
		{
			ObjectConnection = sd.IntValue;
		}

		public void SetStatMerchandiseRemainingCount(StatData sd)
		{
			MerchandiseRemainingCount = sd.IntValue;
		}

		public void SetStatMerchandiseRemainingMinutes(StatData sd)
		{
			MerchandiseRemainingMinutes = sd.IntValue;
		}

		public void SetStatMerchandiseDiscount(StatData sd)
		{
			MerchandiseDiscount = sd.IntValue;
		}

		public void SetStatMerchandiseRankRequirement(StatData sd)
		{
			MerchandiseRankRequirement = sd.IntValue;
		}

		public void SetStatHealthBonus(StatData sd)
		{
			HealthBonus = sd.IntValue;
		}

		public void SetStatManaBonus(StatData sd)
		{
			ManaBonus = sd.IntValue;
		}

		public void SetStatAttackBonus(StatData sd)
		{
			AttackBonus = sd.IntValue;
		}

		public void SetStatDefenseBonus(StatData sd)
		{
			DefenseBonus = sd.IntValue;
		}

		public void SetStatSpeedBonus(StatData sd)
		{
			SpeedBonus = sd.IntValue;
		}

		public void SetStatVitalityBonus(StatData sd)
		{
			VitalityBonus = sd.IntValue;
		}

		public void SetStatWisdomBonus(StatData sd)
		{
			WisdomBonus = sd.IntValue;
		}

		public void SetStatDexterityBonus(StatData sd)
		{
			DexterityBonus = sd.IntValue;
		}

		public void SetStatOwnerAccountId(StatData sd)
		{
			OwnerAccountId = sd.StringValue;
		}

		public void SetStatRankRequired(StatData sd)
		{
			RankRequired = sd.IntValue;
		}

		public void SetStatNameChosen(StatData sd)
		{
			NameChosen = sd.IntValue;
		}

		public void SetStatCharacterFame(StatData sd)
		{
			CharacterFame = sd.IntValue;
		}

		public void SetStatCharacterFameGoal(StatData sd)
		{
			CharacterFameGoal = sd.IntValue;
		}

		public void SetStatGlowing(StatData sd)
		{
			Glowing = sd.IntValue;
		}

		public void SetStatSinkLevel(StatData sd)
		{
			SinkLevel = sd.IntValue;
		}

		public void SetStatAltTextureIndex(StatData sd)
		{
			AltTextureIndex = sd.IntValue;
		}

		public void SetStatGuildName(StatData sd)
		{
			GuildName = sd.StringValue;
		}

		public void SetStatGuildRank(StatData sd)
		{
			GuildRank = sd.IntValue;
		}

		public void SetStatOxygenBar(StatData sd)
		{
			OxygenBar = sd.IntValue;
		}

		public void SetStatXpBoosterActive(StatData sd)
		{
			XpBoosterActive = sd.IntValue;
		}

		public void SetStatXpBoostTime(StatData sd)
		{
			XpBoostTime = sd.IntValue;
		}

		public void SetStatLootDropBoostTime(StatData sd)
		{
			LootDropBoostTime = sd.IntValue;
		}

		public void SetStatLootTierBoostTime(StatData sd)
		{
			LootTierBoostTime = sd.IntValue;
		}

		public void SetStatHealthPotionCount(StatData sd)
		{
			HealthPotionCount = sd.IntValue;
		}

		public void SetStatMagicPotionCount(StatData sd)
		{
			MagicPotionCount = sd.IntValue;
		}

		public void SetStatBackpack0(StatData sd)
		{
			Backpack0 = sd.IntValue;
		}

		public void SetStatBackpack1(StatData sd)
		{
			Backpack1 = sd.IntValue;
		}

		public void SetStatBackpack2(StatData sd)
		{
			Backpack2 = sd.IntValue;
		}

		public void SetStatBackpack3(StatData sd)
		{
			Backpack3 = sd.IntValue;
		}

		public void SetStatBackpack4(StatData sd)
		{
			Backpack4 = sd.IntValue;
		}

		public void SetStatBackpack5(StatData sd)
		{
			Backpack5 = sd.IntValue;
		}

		public void SetStatBackpack6(StatData sd)
		{
			Backpack6 = sd.IntValue;
		}

		public void SetStatBackpack7(StatData sd)
		{
			Backpack7 = sd.IntValue;
		}

		public void SetStatHasBackpack(StatData sd)
		{
			HasBackpack = sd.IntValue;
		}

		public void SetStatSkin(StatData sd)
		{
			Skin = sd.IntValue;
		}

		public void SetStatPetInstanceId(StatData sd)
		{
			PetInstanceId = sd.IntValue;
		}

		public void SetStatPetName(StatData sd)
		{
			PetName = sd.StringValue;
		}

		public void SetStatPetType(StatData sd)
		{
			PetType = sd.IntValue;
		}

		public void SetStatPetRarity(StatData sd)
		{
			PetRarity = sd.IntValue;
		}

		public void SetStatPetMaximumLevel(StatData sd)
		{
			PetMaximumLevel = sd.IntValue;
		}

		public void SetStatPetFamily(StatData sd)
		{
			PetFamily = sd.IntValue;
		}

		public void SetStatPetPoints0(StatData sd)
		{
			PetPoints0 = sd.IntValue;
		}

		public void SetStatPetPoints1(StatData sd)
		{
			PetPoints1 = sd.IntValue;
		}

		public void SetStatPetPoints2(StatData sd)
		{
			PetPoints2 = sd.IntValue;
		}

		public void SetStatPetLevel0(StatData sd)
		{
			PetLevel0 = sd.IntValue;
		}

		public void SetStatPetLevel1(StatData sd)
		{
			PetLevel1 = sd.IntValue;
		}

		public void SetStatPetLevel2(StatData sd)
		{
			PetLevel2 = sd.IntValue;
		}

		public void SetStatPetAbilityType0(StatData sd)
		{
			PetAbilityType0 = sd.IntValue;
		}

		public void SetStatPetAbilityType1(StatData sd)
		{
			PetAbilityType1 = sd.IntValue;
		}

		public void SetStatPetAbilityType2(StatData sd)
		{
			PetAbilityType2 = sd.IntValue;
		}

		public void SetStatEffects2(StatData sd)
		{
			Effects2 = sd.IntValue;
		}

		public void SetStatFortuneTokens(StatData sd)
		{
			FortuneTokens = sd.IntValue;
		}
	}
}
