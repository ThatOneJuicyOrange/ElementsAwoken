using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tech.Materials
{
    public class Transformer : ModItem
    {
        // T7

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 11;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Transformer");
        }
         public override void AddRecipes()
         {
             ModRecipe recipe = new ModRecipe(mod);
             recipe.AddIngredient(ItemID.LunarBar, 5);
             recipe.AddIngredient(null, "VoiditeBar", 2);
             recipe.AddIngredient(null, "GoldWire", 3);
             recipe.AddIngredient(null, "CopperWire", 8);
             recipe.AddTile(TileID.LunarCraftingStation);
             recipe.SetResult(this);
             recipe.AddRecipe();
         }
    }
}
