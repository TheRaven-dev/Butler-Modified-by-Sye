using System;
using wManager.Wow.Helpers;

namespace Butler__Modified_by_Sye_.Hook.Helpers.cs
{
    public class ItemParse
    {
        public static String[] GetItemInfo(String ItemLink)
        {
            return Lua.LuaDoString<String>(string.Format(@"local ItemInfo = {{GetItemInfo('{0}')}};
                                                          return ItemInfo[1]..'^' .. ItemInfo[2]..'^' .. ItemInfo[3]..'^'  .. ItemInfo[4]..'^' 
                                                        .. ItemInfo[5]..'^' .. ItemInfo[6]..'^' ..ItemInfo[7]..'^' .. ItemInfo[8]..'^' 
                                                        .. ItemInfo[9]..'^' .. ItemInfo[10]..'^' .. ItemInfo[11];", ItemLink.Split('|')[2])).Split('^');
        }
    }
}
