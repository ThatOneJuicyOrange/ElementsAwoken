using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Melee
{
    public class Ghostbrand : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 58;
            item.height = 58;

            item.damage = 75;

            item.useTime = 17;
            item.useAnimation = 17;
            item.useStyle = 1;

            item.useTurn = true;
            item.melee = true;
            item.autoReuse = true;
 
            item.knockBack = 7;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 9;
            item.UseSound = SoundID.Item103;

            item.shoot = mod.ProjectileType("PoltercastP");
            item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ghostbrand");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            //innacurate fire
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(7));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Ectoplasm, 20);
            recipe.AddIngredient(ItemID.Keybrand, 1);
            recipe.AddIngredient(null, "RoyalScale", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
