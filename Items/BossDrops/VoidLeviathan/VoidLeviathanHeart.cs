using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ElementsAwoken.Items.BossDrops.VoidLeviathan
{
    public class VoidLeviathanHeart : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.value = Item.sellPrice(0, 10, 0, 0);
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Leviathan Heart");
            Tooltip.SetDefault("It glows with power");
        }
    }
}
