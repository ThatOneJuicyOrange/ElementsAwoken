using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Other
{
    public class BrokenHeroWhip : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 8;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Broken Hero Whip");
        }
    }
}
