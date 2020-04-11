using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tech.Materials
{
    public class Microcontroller : ModItem
    {
        // T6
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 0, 80, 0);
            item.rare = 7;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Controller");
        }
         public override void AddRecipes()
         {
             ModRecipe recipe = new ModRecipe(mod);
             recipe.AddIngredient(ItemID.ChlorophyteBar, 1);
             recipe.AddIngredient(null, "CopperWire", 2);
             recipe.AddIngredient(null, "GoldWire", 3);
             recipe.AddIngredient(null, "RoyalScale", 3);
             recipe.AddTile(TileID.MythrilAnvil);
             recipe.SetResult(this);
             recipe.AddRecipe();
         }
    }
}
