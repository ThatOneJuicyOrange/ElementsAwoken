using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable.VoidStone
{
    public class VoidClock : ModItem
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
            item.createTile = mod.TileType("VoidClock");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Clock");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("IronBar", 3);
            recipe.AddIngredient(ItemID.Glass, 6);
            recipe.AddIngredient(null, "VoidBrick", 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
