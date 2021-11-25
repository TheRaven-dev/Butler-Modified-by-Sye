using robotManager.Helpful;
using System;
using System.Collections.Generic;
using wManager.Wow.Helpers;

namespace Butler__Modified_by_Sye_.Hook
{
    public class AutoRollHook
    {
        public static void Start()
        {
            EventsLuaWithArgs.OnEventsLuaStringWithArgs += Attach;
        }

        public static void Stop()
        {
            EventsLuaWithArgs.OnEventsLuaStringWithArgs -= Attach;
        }

        private static void Attach(String Event, List<string> Args)
        {
            if (Event.ToString() == "START_LOOT_ROLL")
            {
                if (Int32.TryParse(Args[0].ToString(), out int num))
                {
                    var item = new ItemInfo(AutoLootAPI.GetLootRollItemLink(num), num);
                    Logging.Write(AutoLootAPI.GetLootRollItemLink(num));
                    Main.checkThisItem(item);
                }
            }
            if (Event.ToString() == "CONFIRM_LOOT_ROLL")
            {
                //AutoLootAPI.ConfirmLootRoll(GetRollInfo.Item1, GetRollInfo.Item2);
            }
        }
    }
}
