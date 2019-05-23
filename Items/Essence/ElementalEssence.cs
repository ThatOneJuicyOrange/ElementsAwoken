using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Essence
{
    public class ElementalEssence : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 3;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Essence of the Elements");
            Tooltip.SetDefault("The essence of Terraria");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(10, 18));
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DesertEssence", 1);
            recipe.AddIngredient(null, "FireEssence", 1);
            recipe.AddIngredient(null, "SkyEssence", 1);
            recipe.AddIngredient(null, "FrostEssence", 1);
            recipe.AddIngredient(null, "WaterEssence", 1);
            recipe.AddIngredient(null, "VoidEssence", 1);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
