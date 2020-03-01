using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class DubstepPulse : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public int dustType = 60;
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;

            projectile.friendly = true;
            projectile.ranged = true;

            projectile.penetrate = 1;
            projectile.timeLeft = 200;
            projectile.tileCollide = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dubstep Pulse");
        }
        public override void AI()
        {
            if (projectile.localAI[0] == 0)
            {
                dustType = Main.rand.Next(219, 224); // Main.rand.Next(59, 64)
                projectile.localAI[0]++;
            }
            projectile.localAI[1]++;
            if (projectile.localAI[1] % 5 == 0) // every 5 ticks
            {
                // make dust in an expanding circle
                int numDusts = 20;
                for (int i = 0; i < numDusts; i++)
                {
                    Vector2 position = (Vector2.Normalize(projectile.velocity) * new Vector2((float)projectile.width / 2f, (float)projectile.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + projectile.Center;
                    Vector2 velocity = position - projectile.Center;
                    int dust = Dust.NewDust(position + velocity, 0, 0, dustType, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].noLight = true;
                    Main.dust[dust].velocity = Vector2.Normalize(velocity) * 3f;
                }
            }
        }
    }
}