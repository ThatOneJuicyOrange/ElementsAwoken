using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;

namespace ElementsAwoken.Items.Elements.Sky
{
    public class Aurora : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 52;
            item.ranged = true;
            item.width = 38;
            item.height = 18;
            item.useTime = 22;
            item.useAnimation = 22;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = Item.buyPrice(0, 25, 0, 0);
            item.rare = 6;
            item.shoot = 10;
            item.useAmmo = 97;
            item.shootSpeed = 18f;
            item.UseSound = SoundID.Item41;
            item.autoReuse = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aurora");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SkyEssence", 6);
            recipe.AddIngredient(ItemID.Cloud, 25);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
