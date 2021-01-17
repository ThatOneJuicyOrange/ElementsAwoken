using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Materials.Flowers
{
    public class Toxithorn : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.buyPrice(0, 0, 0, 50);
            item.rare = 1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toxithorn");
            Tooltip.SetDefault("'A thorny plant that thrives in the most inhospitable of environments. A small prick is potentially lethal'");
        }

    }
}
