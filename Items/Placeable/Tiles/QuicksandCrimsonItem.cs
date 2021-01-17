using ElementsAwoken.Tiles.Quicksand;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable.Tiles
{
    public class QuicksandCrimsonItem : QuicksandItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.createTile = ModContent.TileType<QuicksandCrimson>();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crimson Quicksand");
            Tooltip.SetDefault("");
        }
    }
}
