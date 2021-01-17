using ElementsAwoken.Tiles.Quicksand;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable.Tiles
{
    public class QuicksandCorruptItem : QuicksandItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.createTile = ModContent.TileType<QuicksandCorrupt>();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ebon Quicksand");
            Tooltip.SetDefault("");
        }
    }
}
