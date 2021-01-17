using ElementsAwoken.Tiles.Quicksand;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable.Tiles
{
    public class QuicksandHallowItem : QuicksandItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.createTile = ModContent.TileType<QuicksandHallow>();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pearl Quicksand");
            Tooltip.SetDefault("");
        }
    }
}
