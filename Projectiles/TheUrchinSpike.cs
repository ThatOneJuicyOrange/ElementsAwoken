using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class TheUrchinSpike : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 100;
            projectile.light = 1f;
            projectile.ignoreWater = true;
            projectile.knockBack = 10;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("TheUrchinSpike");
        }
        public override void AI()
        {
            int dust = Dust.NewDust(projectile.Center, projectile.width, projectile.height, 111);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;

            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 2.355f;
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 27);
            int numberProjectiles = 4;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 value15 = new Vector2((float)Main.rand.Next(-7, 7), (float)Main.rand.Next(-7, 7));
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, value15.X, value15.Y, mod.ProjectileType("TheUrchinUrchin"), projectile.damage / 2, 2f, projectile.owner, 0f, 0f);
            }
        }
    }
}