using Butler__Modified_by_Sye_.Hook.Helpers.cs;
using System;

namespace Butler__Modified_by_Sye_.Hook
{
    public class ItemInfo
    {
        public String itemName { get; set; }
        public String itemLink { get; set; }
        public String itemRarity { get; set; }
        public Int32 itemLevel { get; set; }
        public Int32 itemMinLevel { get; set; }
        public String itemType { get; set; }
        public String itemSubType { get; set; }
        public Int32 itemStackCount { get; set; }
        public String itemEquipLoc { get; set; }
        public String itemTexture { get; set; }
        public Int32 itemSellPrice { get; set; }
        public Int32 itemEntry { get; set; }
        public float ItemWeight { get; set; }
        public Int32 ItemRollID { get; set; }

        public ItemInfo() { }
        public ItemInfo(String ItemLink, Int32 RollID = 0)
        {
            var GetContext = ItemParse.GetItemInfo(ItemLink);
            ItemRollID = RollID;
            itemName = GetContext[0];
            itemLink = ItemLink;
            itemRarity = GetContext[2];
            itemLevel = int.Parse(GetContext[3]);
            itemMinLevel = int.Parse(GetContext[4]);
            itemType = GetContext[5];
            itemSubType = GetContext[6].Replace(" ", "");
            itemStackCount = int.Parse(GetContext[7]);
            itemEquipLoc = GetContext[8];
            itemTexture = GetContext[9];
            itemSellPrice = int.Parse(GetContext[10]);
            itemEntry = int.Parse(itemLink.Substring(itemLink.IndexOf(":") + 1).Split(':')[0]);
        }
    }
}
