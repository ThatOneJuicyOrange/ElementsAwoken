using ElementsAwoken.Tiles.VolcanicPlateau;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable.Tiles.Plateau
{
    public class MalignantFleshItem : ModItem
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

            item.createTile = ModContent.TileType<MalignantFlesh>();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Malignant Flesh");
            Tooltip.SetDefault("");
        }
    }
}
