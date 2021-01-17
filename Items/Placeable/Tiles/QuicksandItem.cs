using ElementsAwoken.Tiles.Quicksand;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable.Tiles
{
    public class QuicksandItem : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;

            item.useAnimation = 15;
            item.useTime = 15;
            item.useStyle = 1;

            item.createTile = ModContent.TileType<Quicksand>();
            item.GetGlobalItem<ItemsGlobal>().quicksandItem = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Quicksand");
            Tooltip.SetDefault("");
        }
    }
}
