using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Other
{
    public class FleshClump : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.buyPrice(0, 0, 1, 0);
            item.rare = 2;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flesh Clump");
        }

    }
}
