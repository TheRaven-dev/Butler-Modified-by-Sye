using System;
using wManager.Wow.Helpers;

namespace Butler__Modified_by_Sye_.Hook
{
    public class AutoLootAPI
    {
        public static String GetLootRollItemLink(Int32 RollID)
        {
            return Lua.LuaDoString<String>($"return GetLootRollItemLink({RollID})");
        }

        public static void RollOnLoot(int RollID, AutoRollTypes Type)
        {
            Lua.LuaDoString($"RollOnLoot({RollID},{(int)Type})");
        }
        public static void ConfirmLootRoll(int RollID, AutoRollTypes Type)
        {
            Lua.LuaDoString($"ConfirmLootRoll({RollID},{(int)Type})");
        }
    }
}
