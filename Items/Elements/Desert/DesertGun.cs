using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;

namespace ElementsAwoken.Items.Elements.Desert
{
    public class DesertGun : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 18;

            item.damage = 36;
            item.knockBack = 5;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.autoReuse = false;

            item.useTime = 28;
            item.useAnimation = 28;
            item.useStyle = 5;
            item.UseSound = SoundID.Item11;

            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 3;

            item.shoot = 10;
            item.useAmmo = 97;
            item.shootSpeed = 8f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desert Eagle");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DesertEssence", 4);
            recipe.AddRecipeGroup("ElementsAwoken:SandGroup", 25);
            recipe.AddRecipeGroup("ElementsAwoken:SandstoneGroup", 10);
            recipe.AddIngredient(ItemID.FlintlockPistol);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
