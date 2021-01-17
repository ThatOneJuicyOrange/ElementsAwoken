using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Materials
{
    public class WingsClip : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 0, 10, 0);
            item.rare = 4;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wings Clip");
            Tooltip.SetDefault("A useful hook that allows wings to be attached to boots");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofFlight, 10);
            recipe.AddRecipeGroup("ElementsAwoken:CobaltBar", 6);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
