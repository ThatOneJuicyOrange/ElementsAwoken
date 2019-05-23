using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Mortemite
{
    public class MortemiteDust : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.buyPrice(0, 1, 0, 0);
            item.rare = 9;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mortemite Dust");
            Tooltip.SetDefault("It sucks the warmth out of you");
        }

    }
}
