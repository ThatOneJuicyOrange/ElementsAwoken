using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ElementsAwoken.Items.BossDrops.zVanilla
{
    public class SpinalSplayer : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26;

            item.damage = 46;
            item.knockBack = 2;

            item.useTime = 42;
            item.useAnimation = 42;
            item.useStyle = 5;
            item.UseSound = SoundID.Item61;

            item.ranged = true;
            item.autoReuse = true;
            item.noMelee = true;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 6;

            item.shoot = 10;
            item.shootSpeed = 25f;
            item.useAmmo = AmmoID.Bullet;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spinal Splayer");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (Main.rand.Next(8) == 0)
            {
                int numberProjectiles = Main.rand.Next(3, 6);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("SpinalEye"), damage, knockBack, player.whoAmI);
                }
                Main.PlaySound(4, (int)position.X, (int)position.Y, 13);
            }
            else
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("Spine"), damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}
