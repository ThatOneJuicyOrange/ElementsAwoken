using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Blightfire
{
    public class Blightfire : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 11;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blightfire");
            Tooltip.SetDefault("Everything burns if you get it hot enough.\nMade by throwing concerntrated pyroplasm and putrid ore into lava");
        }
    }
}
