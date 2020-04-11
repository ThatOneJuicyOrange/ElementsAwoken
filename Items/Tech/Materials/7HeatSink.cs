using ElementsAwoken.Items.BossDrops.Azana;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Tech.Materials
{
    public class HeatSink : ModItem
    {
        // T7

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 10;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heat Sink");
        }
         public override void AddRecipes()
         {
             ModRecipe recipe = new ModRecipe(mod);
             recipe.AddIngredient(ItemID.LunarBar, 5);
            recipe.AddIngredient(ItemID.CopperBar, 2);
            recipe.AddTile(TileID.LunarCraftingStation);
             recipe.SetResult(this, 2);
             recipe.AddRecipe();
         }
    }
}
