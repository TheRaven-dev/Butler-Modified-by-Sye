using Butler__Modified_by_Sye_.Hook.Helpers.cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ItemInfo(String ItemLink,  Int32 RollID = 0)
        {
            var GetContext = ItemParse.GetItemInfo(ItemLink);
            this.ItemRollID = RollID;
            this.itemName = GetContext[0];
            this.itemLink = ItemLink;
            this.itemRarity = GetContext[2];
            this.itemLevel = int.Parse(GetContext[3]);
            this.itemMinLevel = int.Parse(GetContext[4]);
            this.itemType = GetContext[5];
            this.itemSubType = GetContext[6].Replace(" ", "");
            this.itemStackCount = int.Parse(GetContext[7]);
            this.itemEquipLoc = GetContext[8];
            this.itemTexture = GetContext[9];
            this.itemSellPrice = int.Parse(GetContext[10]);
            this.itemEntry = int.Parse(this.itemLink.Substring(itemLink.IndexOf(":") + 1).Split(':')[0]);
        }
    }
}
