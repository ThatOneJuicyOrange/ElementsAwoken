using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Materials
{
    public class CInfinityCrys : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 10;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cracked Infinity Crystal");
            Tooltip.SetDefault("Unimaginable power surges from this very crystal in your hands\nIt is up to you to decide what to do with it...");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "InfinityCrys", 1);
            recipe.AddIngredient(null, "NeutronFragment", 1);
            recipe.AddTile(null, "CrystalCracker");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
