using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj.TheCelestial
{
    public class AstraShot : ModProjectile
    {
        public int type = 0;

        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
            projectile.light = 2f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astra");
            Main.projFrames[projectile.type] = 4;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frame = type;
            return true;
        }
        public override void AI()
        {
            if (projectile.localAI[0] == 0)
            {
                type = Main.rand.Next(3);
                projectile.localAI[0] = 1;
            }
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            Lighting.AddLight(projectile.Center, 0.0f, 0.2f, 0.4f);
            for (int i = 0; i < 5; i++)
            {
                int dustType = 6;
                switch (Main.rand.Next(4))
                {
                    case 0:
                        dustType = 6;
                        break;
                    case 1:
                        dustType = 197;
                        break;
                    case 2:
                        dustType = 229;
                        break;
                    case 3:
                        dustType = 242;
                        break;
                    default: break;
                }
                Dust dust = Main.dust[Dust.NewDust(projectile.Center, 4, 4, dustType)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / 6f * (float)i;
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }
    }
}