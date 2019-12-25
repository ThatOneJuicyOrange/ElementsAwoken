using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.YukkiKun
{
    public class GelticArrow : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 8;
            item.height = 8;

            item.damage = 7;

            item.consumable = true;
            item.ranged = true;

            item.maxStack = 999;
            item.knockBack = 1.5f;
            item.value = 10;
            item.rare = 1;

            item.shoot = mod.ProjectileType("GelticArrow");
            item.shootSpeed = 13f;
            item.ammo = AmmoID.Arrow;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Geltic Arrow");
            Tooltip.SetDefault("Has a chance to inflict poison or slow\nYukki-Kun's donator item");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WoodenArrow, 10);
            recipe.AddIngredient(ItemID.Gel, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 10);
            recipe.AddRecipe();
        }
    }
}
