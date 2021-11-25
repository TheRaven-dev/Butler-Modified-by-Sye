using Butler__Modified_by_Sye_.Hook;
using robotManager.Helpful;
using System;
using System.Collections.Generic;
using System.Globalization;
using wManager.Plugin;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

public class Main : IPlugin
{
	private static string butlerVersion = "1.3";
	private static string ButlerPrefix = "[Butler]: ";
	private Boolean ButlerLaunched = false;
	private static string[] itemStatConstants = null;
	public static ItemInfo EquippedItem1 = null;
	public static ItemInfo EquippedItem2 = null;
	private static Dictionary<string, float> itemValues = new Dictionary<string, float>();
	private static Dictionary<int, int> itemValueOverrides = new Dictionary<int, int>();


	public void Initialize()
	{
		ButlerLaunched = true;
		AutoRollHook.Start();
		ButlerSettings.Load();
		ButlerGlobalSettings.Load();
		ButlerBlackListSettings.Load();
		Logging.Write(ButlerPrefix + "Butler version " + butlerVersion + " is loaded and ready");
		itemStatConstants = getItemStatConstants();

		getitemValueOverrides();
	}


	public static void checkThisItem(ItemInfo item)
	{
		if (item.itemMinLevel <= ObjectManager.Me.Level)
		{
			float itemValue = getItemValue(item);
			if (itemValue != 0)
			{
				if (!ButlerBlackListSettings.CurrentSetting.blackItems.Contains(item.itemLink))
				{
					string equipLoc = translateLoc(item.itemEquipLoc);
					loadWeareable(equipLoc);
					bool equipThisItem = false;
					float wert1 = 0; float wert2 = 0;
					if (EquippedItem1 != null) { wert1 = getItemValue(item); } else { wert1 = -1; }
					if (EquippedItem2 != null) { wert2 = getItemValue(item); } else { wert2 = -1; }
					if (wert2 > wert1)
					{
						wert1 = wert2;
						EquippedItem1 = EquippedItem2;
					}
					if (itemValue > wert1) { equipThisItem = true; }
					if (equipThisItem)
					{
						AutoLootAPI.RollOnLoot(item.ItemRollID, AutoRollTypes.Need);
					}
				}
			}
			AutoLootAPI.RollOnLoot(item.ItemRollID, AutoRollTypes.Pass);
		}
	}

	private static float getItemValue(ItemInfo item)
	{
		float itemValue = 0;
		if (item != null)
		{
			itemValue = getItemOverride(item);
			if (itemValue == -1701)
			{
				itemValue = 0;
				string itemLink = item.itemLink;
				if (!itemValues.TryGetValue(itemLink, out itemValue))
				{
					string itemStats = getItemStats(item);
					itemValue = getItemStatValue(itemStats);
					itemValues.Add(itemLink, itemValue);
					Logging.WriteDebug(ButlerPrefix + "acknowledging item \"" + item.itemName + "\" id " + item.itemEntry + " with a value of " + itemValue);
				}
			}
		}
		return itemValue;
	}

	private static int getItemOverride(ItemInfo item)
	{
		int itemValue = -1701;
		if (item != null)
		{
			int itemEntry = item.itemEntry;
			if (!itemValueOverrides.TryGetValue(itemEntry, out itemValue))
			{
				itemValue = -1701;
			}
		}
		return itemValue;
	}

	private static void getitemValueOverrides()
	{
		int itemEntry = 0; int itemValue = 0; int converted = 0;
		string[] KeyPair = ButlerGlobalSettings.CurrentSetting.ItemValueOverrides.Split(',');
		foreach (string Key in KeyPair)
		{
			itemEntry = 0; itemValue = 0; converted = 0;
			string[] KeyValue = Key.Split(':');
			if (int.TryParse(KeyValue[0], out itemEntry)) { converted = converted + 1; };
			if (int.TryParse(KeyValue[1], out itemValue)) { converted = converted + 1; };
			if (converted == 2) { itemValueOverrides.Add(itemEntry, itemValue); }
		}
	}

	private static List<ItemInfo> getBagItems()
	{
		List<ItemInfo> getiteminfo = new List<ItemInfo>();
        foreach (var item in Bag.GetBagItem())
        {
			var newLink = new ItemInfo(item.GetItemInfo.ItemLink);
			getiteminfo.Add(newLink);
		}
		return getiteminfo;
	}

	private static List<ItemInfo> getEquippedItems()
	{
		List<ItemInfo> items = new List<ItemInfo>();
        for (int i = 0; i < 18; i++)
        {
			var Link = Lua.LuaDoString<String>($"return GetInventoryItemLink('player', {i});".Replace("'", "\""));
			if(!string.IsNullOrWhiteSpace(Link))
            {
				items.Add(new ItemInfo(Link));
			}
		}
		return items;
	}

	private static string translateLoc(string equipLoc)
	{
		string adjustedLoc = "";
		if (equipLoc == "INVTYPE_AMMO") { adjustedLoc = "0"; }
		if (equipLoc == "INVTYPE_HEAD") { adjustedLoc = "1"; }
		if (equipLoc == "INVTYPE_NECK") { adjustedLoc = "2"; }
		if (equipLoc == "INVTYPE_SHOULDER") { adjustedLoc = "3"; }
		if (equipLoc == "INVTYPE_BODY") { adjustedLoc = "4"; }
		if (equipLoc == "INVTYPE_CHEST") { adjustedLoc = "5"; }
		if (equipLoc == "INVTYPE_ROBE") { adjustedLoc = "5"; }
		if (equipLoc == "INVTYPE_WAIST") { adjustedLoc = "6"; }
		if (equipLoc == "INVTYPE_LEGS") { adjustedLoc = "7"; }
		if (equipLoc == "INVTYPE_FEET") { adjustedLoc = "8"; }
		if (equipLoc == "INVTYPE_WRIST") { adjustedLoc = "9"; }
		if (equipLoc == "INVTYPE_HAND") { adjustedLoc = "10"; }
		if (equipLoc == "INVTYPE_FINGER") { adjustedLoc = "11"; }
		if (equipLoc == "INVTYPE_TRINKET") { adjustedLoc = "13"; }
		if (equipLoc == "INVTYPE_CLOAK") { adjustedLoc = "15"; }
		if (equipLoc == "INVTYPE_WEAPON") { adjustedLoc = "16"; }
		if (equipLoc == "INVTYPE_SHIELD") { adjustedLoc = "17"; }
		if (equipLoc == "INVTYPE_2HWEAPON") { adjustedLoc = "16"; }
		if (equipLoc == "INVTYPE_WEAPONMAINHAND") { adjustedLoc = "16"; }
		if (equipLoc == "INVTYPE_WEAPONOFFHAND") { adjustedLoc = "17"; }
		if (equipLoc == "INVTYPE_HOLDABLE") { adjustedLoc = "17"; }
		if (equipLoc == "INVTYPE_RANGED") { adjustedLoc = "18"; }
		if (equipLoc == "INVTYPE_THROWN") { adjustedLoc = "18"; }
		if (equipLoc == "INVTYPE_RANGEDRIGHT") { adjustedLoc = "18"; }
		if (equipLoc == "INVTYPE_RELIC") { adjustedLoc = "18"; }
		if (equipLoc == "INVTYPE_TABARD") { adjustedLoc = "19"; }
		if (equipLoc == "INVTYPE_BAG") { adjustedLoc = "20"; }
		if (equipLoc == "INVTYPE_QUIVER") { adjustedLoc = "20"; }
		return adjustedLoc;
	}

	private static void loadWeareable(string equipLoc)
	{
		bool loadFirst = true;
		EquippedItem1 = null;
		EquippedItem2 = null;
		foreach (ItemInfo item in getEquippedItems())
		{
			if (equipLoc == translateLoc(item.itemEquipLoc))
			{
				if (loadFirst)
				{
					EquippedItem1 = new ItemInfo(item.itemLink);
					loadFirst = false;
				}
				else
				{
					EquippedItem2 = new ItemInfo(item.itemLink); 
				}
			}
		}
	}

	private static bool itemIsEquipped(ItemInfo _item)
	{
		bool isEquipped = false;
		foreach (ItemInfo item in Main.getEquippedItems())
		{ 
			if (_item.itemLink == item.itemLink) 
			{ 
				isEquipped = true; 
			} 
		}
		return isEquipped;
	}

	private static float getItemStatValue(string itemStats)
	{
		float itemWert = 0;
		if (itemStats.Length > 1)
		{
			string[] statsArray = itemStats.Split(';');
			foreach (string itemStat in statsArray)
			{
				string[] statArray = itemStat.Split(':');
				string statKey = statArray[0];
				string statValue = statArray[1];
				float statValueFloat = 0;
				float multiplier = 1;
				statValueFloat = Convert.ToSingle(statValue, CultureInfo.InvariantCulture);
				int index = -1;
				for (var i = 0; i < itemStatConstants.Length; i++) { if (itemStatConstants[i] == statKey) { index = i; }; }
				if (index == 0) { multiplier = ButlerSettings.CurrentSetting.multiAGILITY; };
				if (index == 1) { multiplier = ButlerSettings.CurrentSetting.multiARMOR_PENETRATION_RATING; };
				if (index == 2) { multiplier = ButlerSettings.CurrentSetting.multiATTACK_POWER; };
				if (index == 3) { multiplier = ButlerSettings.CurrentSetting.multiBLOCK_RATING; };
				if (index == 4) { multiplier = ButlerSettings.CurrentSetting.multiBLOCK_VALUE; };
				if (index == 5) { multiplier = ButlerSettings.CurrentSetting.multiCRIT_MELEE_RATING; };
				if (index == 6) { multiplier = ButlerSettings.CurrentSetting.multiCRIT_RANGED_RATING; };
				if (index == 7) { multiplier = ButlerSettings.CurrentSetting.multiCRIT_RATING; };
				if (index == 8) { multiplier = ButlerSettings.CurrentSetting.multiCRIT_SPELL_RATING; };
				if (index == 9) { multiplier = ButlerSettings.CurrentSetting.multiDAMAGE_PER_SECOND; };
				if (index == 10) { multiplier = ButlerSettings.CurrentSetting.multiDEFENSE_SKILL_RATING; };
				if (index == 11) { multiplier = ButlerSettings.CurrentSetting.multiDODGE_RATING; };
				if (index == 12) { multiplier = ButlerSettings.CurrentSetting.multiEXPERTISE_RATING; };
				if (index == 13) { multiplier = ButlerSettings.CurrentSetting.multiFERAL_ATTACK_POWER; };
				if (index == 14) { multiplier = ButlerSettings.CurrentSetting.multiHASTE_MELEE_RATING; };
				if (index == 15) { multiplier = ButlerSettings.CurrentSetting.multiHASTE_RANGED_RATING; };
				if (index == 16) { multiplier = ButlerSettings.CurrentSetting.multiHASTE_RATING; };
				if (index == 17) { multiplier = ButlerSettings.CurrentSetting.multiHASTE_SPELL_RATING; };
				if (index == 18) { multiplier = ButlerSettings.CurrentSetting.multiHEALTH; };
				if (index == 19) { multiplier = ButlerSettings.CurrentSetting.multiHEALTH_REGENERATION; };
				if (index == 20) { multiplier = ButlerSettings.CurrentSetting.multiHIT_MELEE_RATING; };
				if (index == 21) { multiplier = ButlerSettings.CurrentSetting.multiHIT_RANGED_RATING; };
				if (index == 22) { multiplier = ButlerSettings.CurrentSetting.multiHIT_RATING; };
				if (index == 23) { multiplier = ButlerSettings.CurrentSetting.multiHIT_SPELL_RATING; };
				if (index == 24) { multiplier = ButlerSettings.CurrentSetting.multiHIT_TAKEN_RATING; };
				if (index == 25) { multiplier = ButlerSettings.CurrentSetting.multiHIT_TAKEN_SPELL_RATING; };
				if (index == 26) { multiplier = ButlerSettings.CurrentSetting.multiHIT_TAKEN_MELEE_RATING; };
				if (index == 27) { multiplier = ButlerSettings.CurrentSetting.multiHIT_TAKEN_RANGED_RATING; };
				if (index == 28) { multiplier = ButlerSettings.CurrentSetting.multiINTELLECT; };
				if (index == 29) { multiplier = ButlerSettings.CurrentSetting.multiMANA; };
				if (index == 30) { multiplier = ButlerSettings.CurrentSetting.multiMANA_REGENERATION; };
				if (index == 31) { multiplier = ButlerSettings.CurrentSetting.multiMELEE_ATTACK_POWER; };
				if (index == 32) { multiplier = ButlerSettings.CurrentSetting.multiPARRY_RATING; };
				if (index == 33) { multiplier = ButlerSettings.CurrentSetting.multiRANGED_ATTACK_POWER; };
				if (index == 34) { multiplier = ButlerSettings.CurrentSetting.multiRESILIENCE_RATING; };
				if (index == 35) { multiplier = ButlerSettings.CurrentSetting.multiSPELL_DAMAGE_DONE; };
				if (index == 36) { multiplier = ButlerSettings.CurrentSetting.multiSPELL_HEALING_DONE; };
				if (index == 37) { multiplier = ButlerSettings.CurrentSetting.multiSPELL_POWER; };
				if (index == 38) { multiplier = ButlerSettings.CurrentSetting.multiSPELL_PENETRATION; };
				if (index == 39) { multiplier = ButlerSettings.CurrentSetting.multiSPIRIT; };
				if (index == 40) { multiplier = ButlerSettings.CurrentSetting.multiSTAMINA; };
				if (index == 41) { multiplier = ButlerSettings.CurrentSetting.multiSTRENGTH; };
				itemWert = itemWert + statValueFloat * multiplier;
			}
		}
		return itemWert;
	}

	private static string getItemStats(ItemInfo item)
	{
		string itemStats = Lua.LuaDoString("istats=GetItemStats(\"" + item.itemLink + "\") stats4butler=\"\" for stat, value in pairs(istats) do stats4butler=stats4butler ..  _G[stat] .. \":\" .. value .. \";\" end", "stats4butler");
		if (itemStats.Length > 1) { itemStats = itemStats.Substring(0, itemStats.Length - 1); };
		return itemStats;
	}

	private static string[] getItemStatConstants()
	{
		string luacommand = "stats4butler=ITEM_MOD_AGILITY_SHORT .. \";\".. " +
								"ITEM_MOD_ARMOR_PENETRATION_RATING_SHORT .. \";\".. " +
								"ITEM_MOD_ATTACK_POWER_SHORT .. \";\".. " +
								"ITEM_MOD_BLOCK_RATING_SHORT .. \";\".. " +
								"ITEM_MOD_BLOCK_VALUE_SHORT .. \";\".. " +
								"ITEM_MOD_CRIT_MELEE_RATING_SHORT .. \";\".. " +
								"ITEM_MOD_CRIT_RANGED_RATING_SHORT .. \";\".. " +
								"ITEM_MOD_CRIT_RATING_SHORT .. \";\".. " +
								"ITEM_MOD_CRIT_SPELL_RATING_SHORT .. \";\".. " +
								"ITEM_MOD_DAMAGE_PER_SECOND_SHORT .. \";\".. " +
								"ITEM_MOD_DEFENSE_SKILL_RATING_SHORT .. \";\".. " +
								"ITEM_MOD_DODGE_RATING_SHORT .. \";\".. " +
								"ITEM_MOD_EXPERTISE_RATING_SHORT .. \";\".. " +
								"ITEM_MOD_FERAL_ATTACK_POWER_SHORT .. \";\".. " +
								"ITEM_MOD_HASTE_MELEE_RATING_SHORT .. \";\".. " +
								"ITEM_MOD_HASTE_RANGED_RATING_SHORT .. \";\".. " +
								"ITEM_MOD_HASTE_RATING_SHORT .. \";\".. " +
								"ITEM_MOD_HASTE_SPELL_RATING_SHORT .. \";\".. " +
								"ITEM_MOD_HEALTH_SHORT .. \";\".. " +
								"ITEM_MOD_HEALTH_REGENERATION_SHORT .. \";\".. " +
								"ITEM_MOD_HIT_MELEE_RATING_SHORT .. \";\".. " +
								"ITEM_MOD_HIT_RANGED_RATING_SHORT .. \";\".. " +
								"ITEM_MOD_HIT_RATING_SHORT .. \";\".. " +
								"ITEM_MOD_HIT_SPELL_RATING_SHORT .. \";\".. " +
								"ITEM_MOD_HIT_TAKEN_RATING_SHORT .. \";\".. " +
								"ITEM_MOD_HIT_TAKEN_SPELL_RATING_SHORT .. \";\".. " +
								"ITEM_MOD_HIT_TAKEN_MELEE_RATING_SHORT .. \";\".. " +
								"ITEM_MOD_HIT_TAKEN_RANGED_RATING_SHORT .. \";\".. " +
								"ITEM_MOD_INTELLECT_SHORT .. \";\".. " +
								"ITEM_MOD_MANA_SHORT .. \";\".. " +
								"ITEM_MOD_MANA_REGENERATION_SHORT .. \";\".. " +
								"ITEM_MOD_MELEE_ATTACK_POWER_SHORT .. \";\".. " +
								"ITEM_MOD_PARRY_RATING_SHORT .. \";\".. " +
								"ITEM_MOD_RANGED_ATTACK_POWER_SHORT .. \";\".. " +
								"ITEM_MOD_RESILIENCE_RATING_SHORT .. \";\".. " +
								"ITEM_MOD_SPELL_DAMAGE_DONE_SHORT .. \";\".. " +
								"ITEM_MOD_SPELL_HEALING_DONE_SHORT .. \";\".. " +
								"ITEM_MOD_SPELL_POWER_SHORT .. \";\".. " +
								"ITEM_MOD_SPELL_PENETRATION_SHORT .. \";\".. " +
								"ITEM_MOD_SPIRIT_SHORT .. \";\".. " +
								"ITEM_MOD_STAMINA_SHORT .. \";\".. " +
								"ITEM_MOD_STRENGTH_SHORT";
		string statConstants = Lua.LuaDoString(luacommand, "stats4butler");
		string[] statConstantsArray = statConstants.Split(';');
		return statConstantsArray;
	}

	public void Dispose()
	{
		ButlerLaunched = false;
	}

	public void Settings()
	{
		ButlerSettings.Load();
		ButlerSettings.CurrentSetting.ToForm();
		ButlerSettings.CurrentSetting.Save();
		ButlerGlobalSettings.Load();
		ButlerGlobalSettings.CurrentSetting.ToForm();
		ButlerGlobalSettings.CurrentSetting.Save();
		ButlerBlackListSettings.Load();
		ButlerBlackListSettings.CurrentSetting.Save();
	}
}