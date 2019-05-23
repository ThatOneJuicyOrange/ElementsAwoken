using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj.Permafrost
{
    public class PermafrostBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Permafrost");
            Main.projFrames[projectile.type] = 2;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            if (projectile.ai[0] == 0)
            {
                projectile.frame = 0;
            }
            else
            {
                projectile.frame = 1;
            }
            return true;
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            for (int i = 0; i < 3; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 135)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / 6f * (float)i;
                dust.noGravity = true;
                dust.scale = 1f;
            }
            // if it comes from the orbital (ai[1] = 1) then dont make it big
            if (projectile.localAI[0] == 0 && projectile.ai[1] == 0)
            {
                if (Main.expertMode && Main.rand.Next(6) == 0)
                {
                    projectile.ai[0] = 1f;
                }
                projectile.localAI[0]++;
            }
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            if (projectile.ai[0] == 1)
            {
                player.AddBuff(BuffID.Frozen, 30, false);
            }
            else
            {
                player.AddBuff(BuffID.Frostburn, 90, false);
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 135, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}