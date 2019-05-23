using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tech.Materials
{
    public class Capacitor : ModItem
    {
        // T3

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.buyPrice(0, 1, 0, 0);
            item.rare = 1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Capacitor");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("CopperBar", 4);
            recipe.AddIngredient(null, "CopperWire", 5);
            recipe.AddIngredient(ItemID.Bone, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        /* public override void AddRecipes()
         {
             ModRecipe recipe = new ModRecipe(mod);
             recipe.AddIngredient(ItemID.ChlorophyteBar, 1);
             recipe.AddIngredient(null, "CopperWire", 2);
             recipe.AddIngredient(null, "GoldWire", 3);
             recipe.AddIngredient(null, "RoyalScale", 3);
             recipe.AddTile(TileID.MythrilAnvil);
             recipe.SetResult(this);
             recipe.AddRecipe();
         }*/
    }
}
