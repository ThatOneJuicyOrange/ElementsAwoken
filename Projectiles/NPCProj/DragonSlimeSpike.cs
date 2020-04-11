using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj
{
    public class DragonSlimeSpike : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.hostile = true;
            projectile.aiStyle = 1;
            projectile.penetrate = -1;
            projectile.scale *= 0.8f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Slime Spike");
        }
        public override void AI()
        {
            if (projectile.ai[0] % 3 == 0)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position + new Vector2(0, 12).RotatedBy(projectile.rotation), projectile.width, projectile.height, 6, 0f, 0f, 50, new Color(255, 136, 78, 150), 1.2f)];
                dust.velocity *= 0.3f;
                dust.velocity -= projectile.velocity * 0.3f;
                dust.noGravity = true;
                dust.fadeIn = 0.8f;
            }
            if (projectile.ai[1] == 0f)
            {
                projectile.ai[1] = 1f;
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 17);
            }
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 5f)
            {
                projectile.ai[0] = 5f;
                projectile.velocity.Y = projectile.velocity.Y + 0.15f;
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