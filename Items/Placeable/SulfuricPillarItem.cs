using ElementsAwoken.Tiles.VolcanicPlateau.Objects;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable
{
    public class SulfuricPillarItem : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.maxStack = 999;

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;

            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;

            item.createTile = ModContent.TileType<SulfuricPillar>();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sulfuric Pillar");
        }
    }
}
