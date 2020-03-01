using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;
using System;

namespace ElementsAwoken.Items.ItemSets.Blightfire
{
    public class BlightedBuster : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 18;

            item.damage = 140;
            item.knockBack = 15;

            item.useTime = 24;
            item.useAnimation = 24;
            item.useStyle = 5;

            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 7, 50, 0);
            item.rare = 11;

            item.shoot = 10;
            item.shootSpeed = 16f;
            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item36;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blighted Buster");
            Tooltip.SetDefault("Turns normal bullets into a tight cluster of blighted bullets\nRight Click to shoot a wider spread");
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int angle = 3;
            if (player.altFunctionUse == 2)
            {
                angle = 20;
            }
            if (type == ProjectileID.Bullet)
            {
                type = mod.ProjectileType("BlightedBulletP");
            }
            int numberProjectiles = 4 + Main.rand.Next(2);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(angle));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }

            Vector2 toTarget = new Vector2(Main.MouseWorld.X - player.Center.X, Main.MouseWorld.Y - player.Center.Y);
            toTarget.Normalize();
            bool tooQuick = false;
            if (toTarget.X < 0 && player.velocity.X > 8f) tooQuick = true;
            if (toTarget.X > 0 && player.velocity.X < -8f) tooQuick = true;
            if (!tooQuick)
            {
                player.velocity -= toTarget * 3f;
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Blightfire", 10);
            recipe.AddIngredient(ItemID.LunarBar, 2);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
