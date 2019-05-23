using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Puff
{
    public class Puffball : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.buyPrice(0, 0, 1, 0);
            item.rare = 0;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Puffball");
            Tooltip.SetDefault("It is unbelievably soft");
        }

    }
}
