using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Fire
{
    public class FireStorm : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.ClockworkAssaultRifle);
            item.shootSpeed *= 0.75f;
            item.damage = 8;
            item.shoot = 10;
            item.useAmmo = 97;
            item.value = Item.buyPrice(0, 7, 0, 0);
            item.rare = 4;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Storm");
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FireEssence", 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddIngredient(ItemID.PhoenixBlaster);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
