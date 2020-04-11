using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Radia
{
    public class Radia : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 2,50, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radia");
            Tooltip.SetDefault("");
        }
    }
}
