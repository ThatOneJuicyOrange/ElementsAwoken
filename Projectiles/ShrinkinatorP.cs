using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.NPCs;

namespace ElementsAwoken.Projectiles
{
    public class ShrinkinatorP : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.ranged = true;

            projectile.penetrate = 1;
            projectile.timeLeft = 200;

            projectile.light = 0.5f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shrinkinator");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            if (Main.rand.Next(3) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 58);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1f;
                Main.dust[dust].velocity *= 0.1f;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            NPCsGLOBAL modNPC = target.GetGlobalNPC<NPCsGLOBAL>(mod);
            if (!modNPC.shrunk)
            {
                modNPC.shrinking = true;
            }
        }
    }
}