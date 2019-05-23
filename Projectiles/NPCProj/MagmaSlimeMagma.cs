using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj
{
    public class MagmaSlimeMagma : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.hostile = true;
            projectile.alpha = 255;
            projectile.aiStyle = 1;
            projectile.penetrate = -1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Slime Spike");
        }
        public override void AI()
        {
            for (int l = 0; l < 5; l++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 6)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / 6f * (float)l;
                dust.noGravity = true;
                dust.scale = 1f;
            }
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 5f)
            {
                projectile.ai[0] = 5f;
                projectile.velocity.Y += 0.1f;
            }
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.OnFire, 80, false);
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 6, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}