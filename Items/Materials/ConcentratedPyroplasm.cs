using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Materials
{
    public class ConcentratedPyroplasm : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 10;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Concentrated Pyroplasm");
            Tooltip.SetDefault("Used to craft Blightfire");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(9, 8));
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Pyroplasm", 2);
            recipe.AddIngredient(ItemID.LunarOre, 1);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
