using robotManager.Helpful;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using wManager.Wow.ObjectManager;





public class ButlerGlobalSettings : Settings
{
	public ButlerGlobalSettings()
	{
		ItemValueOverrides = "2586:0,11508:0,12064:0";
		pulseDelay = 3000;
	}

	public static ButlerGlobalSettings CurrentSetting { get; set; }

	public bool Save()
	{
		try 
		{ 
			return Save(AdviserFilePathAndName("Butler", "Globals")); 
		}
		catch (Exception e) 
		{ 
			Logging.WriteDebug("Butler failed to save global settings because of: " + e);
			return false; 
		}
	}

	public static bool Load()
	{
		try
		{
			if (File.Exists(AdviserFilePathAndName("Butler", "Globals")))
			{
				CurrentSetting = Load<ButlerGlobalSettings>(AdviserFilePathAndName("Butler", "Globals"));
				return true;
			}
			CurrentSetting = new ButlerGlobalSettings();
		}
		catch (Exception e)
		{
			Logging.WriteDebug("Butler failed to load global settings because of: " + e);
		}
		return false;
	}

	[Setting]
	[Category("Global Settings")]
	[DisplayName("Item value overrides")]
	[Description("Comma-separated ItemID:value pairs to fix the item value calculation - 2586:0 would never, 2586:99999999 would everytime equip my gamemaster robe")]
	public string ItemValueOverrides { get; set; }

	[Setting]
	[Category("Global Settings")]
	[DisplayName("Pulse delay")]
	[Description("Time in milliseconds between butler pulses (3000 by default)")]
	public int pulseDelay { get; set; }
}


public class ButlerSettings : Settings
{
	public ButlerSettings()
	{
		DestroyGray = false;
		EquipGray = true;
		EquipWhite = true;
		EquipGreen = true;
		EquipBlue = true;
		EquipEpic = false;
		EquipLegendary = false;
		replaceFood = true;
		replaceDrink = true;
		multiAGILITY = 100;
		multiARMOR_PENETRATION_RATING = 100;
		multiATTACK_POWER = 100;
		multiBLOCK_RATING = 100;
		multiBLOCK_VALUE = 100;
		multiCRIT_MELEE_RATING = 100;
		multiCRIT_RANGED_RATING = 100;
		multiCRIT_RATING = 100;
		multiCRIT_SPELL_RATING = 100;
		multiDAMAGE_PER_SECOND = 5;
		multiDEFENSE_SKILL_RATING = 100;
		multiDODGE_RATING = 100;
		multiEXPERTISE_RATING = 100;
		multiFERAL_ATTACK_POWER = 100;
		multiHASTE_MELEE_RATING = 100;
		multiHASTE_RANGED_RATING = 100;
		multiHASTE_RATING = 100;
		multiHASTE_SPELL_RATING = 100;
		multiHEALTH = 100;
		multiHEALTH_REGENERATION = 100;
		multiHIT_MELEE_RATING = 100;
		multiHIT_RANGED_RATING = 100;
		multiHIT_RATING = 100;
		multiHIT_SPELL_RATING = 100;
		multiHIT_TAKEN_RATING = 100;
		multiHIT_TAKEN_SPELL_RATING = 100;
		multiHIT_TAKEN_MELEE_RATING = 100;
		multiHIT_TAKEN_RANGED_RATING = 100;
		multiINTELLECT = 100;
		multiMANA = 100;
		multiMANA_REGENERATION = 100;
		multiMELEE_ATTACK_POWER = 100;
		multiPARRY_RATING = 100;
		multiRANGED_ATTACK_POWER = 100;
		multiRESILIENCE_RATING = 100;
		multiSPELL_DAMAGE_DONE = 100;
		multiSPELL_HEALING_DONE = 100;
		multiSPELL_POWER = 100;
		multiSPELL_PENETRATION = 100;
		multiSPIRIT = 100;
		multiSTAMINA = 100;
		multiSTRENGTH = 100;
	}

	public static ButlerSettings CurrentSetting { get; set; }

	public bool Save()
	{
		try 
		{ 
			return Save(AdviserFilePathAndName("Butler", ObjectManager.Me.Name + "." + wManager.Wow.Helpers.Usefuls.RealmName));
		}
		catch (Exception e)
		{ 
			Logging.WriteDebug("Butler failed to save settings because of: " + e); 
			return false; 
		}
	}

	public static bool Load()
	{
		try
		{
			if (File.Exists(AdviserFilePathAndName("Butler", ObjectManager.Me.Name + "." + wManager.Wow.Helpers.Usefuls.RealmName)))
			{
				CurrentSetting = Load<ButlerSettings>(AdviserFilePathAndName("Butler", ObjectManager.Me.Name + "." + wManager.Wow.Helpers.Usefuls.RealmName));
				return true;
			}
			CurrentSetting = new ButlerSettings();
		}
		catch (Exception e)
		{
			Logging.WriteDebug("Butler failed to load settings because of: " + e);
		}
		return false;
	}

	[Setting]
	[Category("Common Settings")]
	[DisplayName("Destroy poor")]
	[Description("Destroy poor (gray) items in inventory")]
	public bool DestroyGray { get; set; }

	[Setting]
	[Category("Common Settings")]
	[DisplayName("Equip poor")]
	[Description("Equip poor (gray) items on pickup")]
	public bool EquipGray { get; set; }

	[Setting]
	[Category("Common Settings")]
	[DisplayName("Equip common")]
	[Description("Equip common (white) items on pickup")]
	public bool EquipWhite { get; set; }

	[Setting]
	[Category("Common Settings")]
	[DisplayName("Equip uncommon")]
	[Description("Equip uncommon (green) items on pickup")]
	public bool EquipGreen { get; set; }

	[Setting]
	[Category("Common Settings")]
	[DisplayName("Equip rare")]
	[Description("Equip rare (blue) items on pickup")]
	public bool EquipBlue { get; set; }

	[Setting]
	[Category("Common Settings")]
	[DisplayName("Equip epic")]
	[Description("Equip epic (purple) items on pickup")]
	public bool EquipEpic { get; set; }

	[Setting]
	[Category("Common Settings")]
	[DisplayName("Equip legendary")]
	[Description("Equip legendary items on pickup")]
	public bool EquipLegendary { get; set; }

	[Setting]
	[Category("Item stats multiplier BASIC")]
	[DisplayName("AGILITY")]
	[Description("Multiplier for Agility")]
	public float multiAGILITY { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("ARMOR_PENETRATION_RATING")]
	[Description("Multiplier for Armor Penetration")]
	public float multiARMOR_PENETRATION_RATING { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("ATTACK_POWER")]
	[Description("Multiplier for Attack Power")]
	public float multiATTACK_POWER { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("BLOCK_RATING")]
	[Description("Multiplier for Block rating")]
	public float multiBLOCK_RATING { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("BLOCK_VALUE")]
	[Description("Multiplier for Block value")]
	public float multiBLOCK_VALUE { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("CRIT_MELEE_RATING")]
	[Description("Multiplier for Crit (melee)")]
	public float multiCRIT_MELEE_RATING { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("CRIT_RANGED_RATING")]
	[Description("Multiplier for Crit (ranged)")]
	public float multiCRIT_RANGED_RATING { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("CRIT_RATING")]
	[Description("Multiplier for Crit")]
	public float multiCRIT_RATING { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("CRIT_SPELL_RATING")]
	[Description("Multiplier for Crit (spell)")]
	public float multiCRIT_SPELL_RATING { get; set; }

	[Setting]
	[Category("Item stats multiplier BASIC")]
	[DisplayName("DAMAGE_PER_SECOND")]
	[Description("Multiplier for DPS")]
	public float multiDAMAGE_PER_SECOND { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("DEFENSE_SKILL_RATING")]
	[Description("Multiplier for Defense")]
	public float multiDEFENSE_SKILL_RATING { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("DODGE_RATING")]
	[Description("Multiplier for Dodge")]
	public float multiDODGE_RATING { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("EXPERTISE_RATING")]
	[Description("Multiplier for Expertise")]
	public float multiEXPERTISE_RATING { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("FERAL_ATTACK_POWER")]
	[Description("Multiplier for Feral Attack Power")]
	public float multiFERAL_ATTACK_POWER { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("HASTE_MELEE_RATING")]
	[Description("Multiplier for Haste (melee)")]
	public float multiHASTE_MELEE_RATING { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("HASTE_RANGED_RATING")]
	[Description("Multiplier for Haste (ranged)")]
	public float multiHASTE_RANGED_RATING { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("HASTE_RATING")]
	[Description("Multiplier for Haste")]
	public float multiHASTE_RATING { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("HASTE_SPELL_RATING")]
	[Description("Multiplier for Haste (spell)")]
	public float multiHASTE_SPELL_RATING { get; set; }

	[Setting]
	[Category("Item stats multiplier BASIC")]
	[DisplayName("HEALTH")]
	[Description("Multiplier for Health")]
	public float multiHEALTH { get; set; }

	[Setting]
	[Category("Item stats multiplier BASIC")]
	[DisplayName("HEALTH_REGENERATION")]
	[Description("Multiplier for Health Regeneration (Hp5)")]
	public float multiHEALTH_REGENERATION { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("HIT_MELEE_RATING")]
	[Description("Multiplier for Hit (melee)")]
	public float multiHIT_MELEE_RATING { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("HIT_RANGED_RATING")]
	[Description("Multiplier for Hit (ranged)")]
	public float multiHIT_RANGED_RATING { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("HIT_RATING")]
	[Description("Multiplier for Hit")]
	public float multiHIT_RATING { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("HIT_SPELL_RATING")]
	[Description("Multiplier for Hit (spell)")]
	public float multiHIT_SPELL_RATING { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("HIT_TAKEN_RATING")]
	[Description("Multiplier for Miss")]
	public float multiHIT_TAKEN_RATING { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("HIT_TAKEN_SPELL_RATING")]
	[Description("Multiplier for Spell miss")]
	public float multiHIT_TAKEN_SPELL_RATING { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("HIT_TAKEN_MELEE_RATING")]
	[Description("Multiplier for Melee miss")]
	public float multiHIT_TAKEN_MELEE_RATING { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("HIT_TAKEN_RANGED_RATING")]
	[Description("Multiplier for Ranged miss")]
	public float multiHIT_TAKEN_RANGED_RATING { get; set; }

	[Setting]
	[Category("Item stats multiplier BASIC")]
	[DisplayName("INTELLECT")]
	[Description("Multiplier for Intellect")]
	public float multiINTELLECT { get; set; }

	[Setting]
	[Category("Item stats multiplier BASIC")]
	[DisplayName("MANA")]
	[Description("Multiplier for Mana")]
	public float multiMANA { get; set; }

	[Setting]
	[Category("Item stats multiplier BASIC")]
	[DisplayName("MANA_REGENERATION")]
	[Description("Multiplier for Mana Regeneration (Mp5)")]
	public float multiMANA_REGENERATION { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("MELEE_ATTACK_POWER")]
	[Description("Multiplier for Attack Power (melee)")]
	public float multiMELEE_ATTACK_POWER { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("PARRY_RATING")]
	[Description("Multiplier for Parry")]
	public float multiPARRY_RATING { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("RANGED_ATTACK_POWER")]
	[Description("Multiplier for Attack Power (ranged)")]
	public float multiRANGED_ATTACK_POWER { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("RESILIENCE_RATING")]
	[Description("Multiplier for Resilience")]
	public float multiRESILIENCE_RATING { get; set; }

	[Setting]
	[Category("Item stats multiplier BASIC")]
	[DisplayName("SPELL_DAMAGE_DONE")]
	[Description("Multiplier for Spellpower")]
	public float multiSPELL_DAMAGE_DONE { get; set; }

	[Setting]
	[Category("Item stats multiplier BASIC")]
	[DisplayName("SPELL_HEALING_DONE")]
	[Description("Multiplier for Healing")]
	public float multiSPELL_HEALING_DONE { get; set; }

	[Setting]
	[Category("Item stats multiplier BASIC")]
	[DisplayName("SPELL_POWER")]
	[Description("Multiplier for Spellpower")]
	public float multiSPELL_POWER { get; set; }

	[Setting]
	[Category("Item stats multiplier SECONDARY")]
	[DisplayName("SPELL_PENETRATION")]
	[Description("Multiplier for Penetration")]
	public float multiSPELL_PENETRATION { get; set; }

	[Setting]
	[Category("Item stats multiplier BASIC")]
	[DisplayName("SPIRIT")]
	[Description("Multiplier for Spirit")]
	public float multiSPIRIT { get; set; }

	[Setting]
	[Category("Item stats multiplier BASIC")]
	[DisplayName("STAMINA")]
	[Description("Multiplier for Stamina")]
	public float multiSTAMINA { get; set; }

	[Setting]
	[Category("Item stats multiplier BASIC")]
	[DisplayName("STRENGTH")]
	[Description("Multiplier for Strength")]
	public float multiSTRENGTH { get; set; }

	[Setting]
	[Category("Common Settings")]
	[DisplayName("Replace Food")]
	[Description("Replace Food from General Settings with known food you have most in inventory")]
	public bool replaceFood { get; set; }

	[Setting]
	[Category("Common Settings")]
	[DisplayName("Replace Drink")]
	[Description("Replace Manadrink from General Settings with known drink you have most in inventory")]
	public bool replaceDrink { get; set; }
}
