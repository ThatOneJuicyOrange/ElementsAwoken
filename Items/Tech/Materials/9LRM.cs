using ElementsAwoken.Items.BossDrops.Azana;
using ElementsAwoken.Tiles.Crafting;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Tech.Materials
{
    public class LRM : ModItem
    {
        // T9

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Laser Refraction Matrix");
        }
         public override void AddRecipes()
         {
             ModRecipe recipe = new ModRecipe(mod);
             recipe.AddIngredient(ItemID.Glass, 10);
            recipe.AddIngredient(ItemType<DiscordantBar>(), 8);
            recipe.AddIngredient(ItemType<GoldWire>(), 6);
            recipe.AddTile(TileType<ChaoticCrucible>());
            recipe.SetResult(this);
             recipe.AddRecipe();
         }
    }
}
