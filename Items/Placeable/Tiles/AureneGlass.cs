using ElementsAwoken.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable.Tiles
{
    public class AureneGlass : ModItem
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
            item.useTime = 10;
            item.useStyle = 1;
            item.createTile = ModContent.TileType<AureneGlassTile>();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aurene Glass");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Glass, 5);
            recipe.AddIngredient(ItemID.CrystalShard, 3);
            recipe.AddTile(TileID.Hellforge);
            recipe.SetResult(this,5);
            recipe.AddRecipe();
        }
    }
}
