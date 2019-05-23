using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Stellarium
{
    public class Stellorite : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.buyPrice(0, 5, 0, 0);
            item.rare = 9;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stellorite");
            Tooltip.SetDefault("It harnesses the stellar energies");
        }
    }
}
