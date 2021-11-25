using Butler__Modified_by_Sye_;
using Butler__Modified_by_Sye_.Hook;
using robotManager.Helpful;
using System;
using wManager.Plugin;

public class Main : IPlugin
{
	public static string butlerVersion = "1.3";
	public static string ButlerPrefix = "[Butler]: ";
	public Boolean ButlerLaunched = false;
	public static string[] itemStatConstants = null;
	


	public void Initialize()
	{
		ButlerLaunched = true;
		AutoRollHook.Start();
		ButlerSettings.Load();
		ButlerGlobalSettings.Load();
		Logging.Write(ButlerPrefix + "Butler version " + butlerVersion + " is loaded and ready");
		itemStatConstants = ItemHelpers.getItemStatConstants();
		ItemHelpers.getitemValueOverrides();
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
	}
}