using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Frost
{
    public class FrostPulse : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 60;
            item.ranged = true;
            item.width = 62;
            item.height = 28;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 7;
            item.UseSound = SoundID.Item92;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("FrostPulse");
            item.shootSpeed = 20f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost Pulse Rifle");
            Tooltip.SetDefault("Fires an icy pulse");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FrostEssence", 7);
            recipe.AddRecipeGroup("ElementsAwoken:IceGroup", 5);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
