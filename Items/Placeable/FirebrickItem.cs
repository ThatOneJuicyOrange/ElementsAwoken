using ElementsAwoken.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable
{
    public class FirebrickItem : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 11;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 0;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = ModContent.TileType<Firebrick>();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Firebrick");
            Tooltip.SetDefault("");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FireEssence", 1);
            recipe.AddIngredient(ItemID.GrayBrick, 50);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }
    }
}
