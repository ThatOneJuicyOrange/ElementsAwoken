using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.TheGuardian
{
    public class GuardianTargeter : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft = 100;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Guardian");
        }
        float speed = 4f;
        public override void AI()
        {
            for (int i = 0; i < 8; i++)
            {
                Vector2 position = projectile.Center + Main.rand.NextVector2Circular(projectile.width * 0.5f, projectile.height * 0.5f);
                Dust dust = Dust.NewDustPerfect(position, 6, Vector2.Zero);
                dust.noGravity = true;
            }
            Player P = Main.player[(int)projectile.ai[1]];
            if (!P.active)
            {
                projectile.Kill();
            }
            double angle = Math.Atan2(P.Center.Y - projectile.Center.Y, P.Center.X - projectile.Center.X);
            if (Vector2.Distance(P.Center, projectile.Center) > 100)
            {
                speed = 7.5f;
            }
            projectile.velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * speed;
        }
    }
}