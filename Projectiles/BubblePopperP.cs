using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    class BubblePopperP : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.ranged = true;
            projectile.alpha = 255;
            projectile.penetrate = 1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bubble Popper");
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 1, 1, 1);

            projectile.localAI[1]++;
            if (projectile.localAI[1] > 4)
            {
                for (int num121 = 0; num121 < 6; num121++)
                {
                    Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 111)];
                    dust.velocity = Vector2.Zero;
                    dust.position -= projectile.velocity / 6f * (float)num121;
                    dust.noGravity = true;
                    dust.scale = 1f;
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            int numberProjectiles = 1;
            for (int i = 0; i < numberProjectiles; i++)
            {
                int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4), ProjectileID.FlaironBubble, projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                Main.projectile[proj].melee = false;
                Main.projectile[proj].ranged = true;
            }
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            int numberProjectiles = 1;
            for (int i = 0; i < numberProjectiles; i++)
            {
                int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4), ProjectileID.FlaironBubble, projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                Main.projectile[proj].melee = false;
                Main.projectile[proj].ranged = true;
            }
        }
    }
}