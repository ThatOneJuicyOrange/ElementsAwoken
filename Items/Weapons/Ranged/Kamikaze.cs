using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Ranged
{
    public class Kamikaze : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26;

            item.damage = 62;
            item.knockBack = 3.5f;

            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 5;
            item.UseSound = SoundID.Item61;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 15, 0, 0);
            item.rare = 7;

            item.shootSpeed = 10f;
            item.shoot = ProjectileID.RocketI;
            item.useAmmo = AmmoID.Rocket;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kamikaze");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float pi = 0.314159274f;
            int numProjectiles = 3;
            Vector2 vector14 = new Vector2(speedX, speedY);
            vector14.Normalize();
            vector14 *= 40f;
            bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector14, 0, 0);
            for (int num123 = 0; num123 < numProjectiles; num123++)
            {
                float num124 = (float)num123 - ((float)numProjectiles - 1f) / 2f;
                Vector2 vector15 = vector14.RotatedBy((double)(pi * num124), default(Vector2));
                if (!flag11)
                {
                    vector15 -= vector14;
                }
                int num125 = Projectile.NewProjectile(vector2.X + vector15.X, vector2.Y + vector15.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0.0f, 0.0f);
                Main.projectile[num125].noDropItem = true;
            }
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, -2);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteShotbow, 1);
            recipe.AddIngredient(ItemID.RocketLauncher, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
