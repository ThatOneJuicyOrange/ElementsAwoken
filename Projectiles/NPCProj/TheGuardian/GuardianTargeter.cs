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
            projectile.width = 62;
            projectile.height = 62;

            projectile.hostile = true;
            projectile.tileCollide = false;

            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
            projectile.scale = 0.7f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Guardian");
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.9f, 0.2f, 0.4f);
            projectile.rotation += 0.05f;
            if (projectile.alpha > 0)  projectile.alpha -= 255 / 30;

            int innerCircle = 18;
            Vector2 position = projectile.Center + Main.rand.NextVector2Circular(innerCircle * 0.5f, innerCircle * 0.5f);
            Dust dust = Dust.NewDustPerfect(position, 6, Vector2.Zero);
            dust.noGravity = true;
            dust.velocity = projectile.velocity * -0.2f;

            Player P = Main.player[(int)projectile.ai[1]];
            if (!P.active)projectile.Kill();

            float speed = MathHelper.Lerp(4f,7.5f, Vector2.Distance(P.Center, projectile.Center) / 200);
            float speedScale = Main.expertMode ? 1.5f : 1;
            if (MyWorld.awakenedMode) speedScale = 2f;
            speed *= speedScale;
            double angle = Math.Atan2(P.Center.Y - projectile.Center.Y, P.Center.X - projectile.Center.X);
            projectile.velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * speed;
        }
    }
}