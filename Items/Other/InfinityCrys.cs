using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Other
{
    public class InfinityCrys : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.buyPrice(0, 5, 0, 0);
            item.rare = 6;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infinity Crystal");
            Tooltip.SetDefault("A cold crystal... What could it be used for?");
        }

    }
}
