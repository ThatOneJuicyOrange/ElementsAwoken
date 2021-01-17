using ElementsAwoken.Tiles.VolcanicPlateau;
using Terraria.ModLoader;
using Terraria.ID;

namespace ElementsAwoken.Items.Placeable.Tiles.Plateau
{
    public class SulfuricBricksItem : ModItem
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

            item.createTile = ModContent.TileType<SulfuricBricks>();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sulfuric Bricks");
            Tooltip.SetDefault("");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<SulfuricSlateItem>(), 2);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
