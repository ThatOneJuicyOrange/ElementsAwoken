using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Materials.Flowers
{
    public class VoidBulb : ModItem
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
            DisplayName.SetDefault("Void Bulb");
            Tooltip.SetDefault("'An anomalous glasslike bulb; it appears to exist in multiple states at once'");
        }

    }
}
