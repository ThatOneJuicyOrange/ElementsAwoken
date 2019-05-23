using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Ancients
{
    public class AncientShard : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 14;
            item.value = Item.sellPrice(0, 2, 50, 0);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Shard");
        }
    }
}
