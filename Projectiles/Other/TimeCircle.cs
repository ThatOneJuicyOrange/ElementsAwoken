using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.Other
{
    public class TimeCircle : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 60000;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Time Circle");
            Main.projFrames[projectile.type] = 3;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            if (Main.dayTime) projectile.frame = 0;
            else projectile.frame = 1;
            return true;
        }
        public override bool CanDamage()
        {
            return false;
        }
        public override void AI()
        {
            Player parent = Main.player[(int)projectile.ai[0]];
            MyPlayer modPlayer = parent.GetModPlayer<MyPlayer>();

            projectile.ai[1] += 2f;
            int amount = 30;
            if (modPlayer.timeAbilityTimer > 180) amount = 120;
            else if (modPlayer.timeAbilityTimer > 60) amount = 60;

            float dayDuration = 54000;
            float nightDuration = 32400;

            int fadeDuration = (int)(1.5f * amount * 60);
            if (Main.dayTime) projectile.ai[1] = (float)(((float)Main.time / dayDuration) * 180);
            else projectile.ai[1] = (float)(((float)Main.time / nightDuration) * 180);
            if (Main.time <= fadeDuration) projectile.alpha = (int)MathHelper.Lerp(255, 0, MathHelper.Clamp((float)Main.time / fadeDuration, 0, 1));
            else if (Main.dayTime) projectile.alpha = (int)MathHelper.Lerp(0, 255, MathHelper.Clamp((float)(Main.time - (dayDuration - fadeDuration)) / fadeDuration, 0, 1));
            else projectile.alpha = (int)MathHelper.Lerp(0, 255, MathHelper.Clamp((float)(Main.time - (nightDuration - fadeDuration)) / fadeDuration, 0, 1));
            int distance = 60;
            double rad = projectile.ai[1] * (Math.PI / 180); // angle to radians
            projectile.Center = parent.Center - new Vector2((int)(Math.Cos(rad) * distance), (int)(Math.Sin(rad) * distance));

            int dustType = 127;
            if (!Main.dayTime) dustType = 15;

            int dustDist = distance;
            for (int i = 0; i < 5; i++)
            {
                double angle = Main.rand.NextFloat(-0.5f,0.5f) * Math.PI;
                Vector2 offset = new Vector2((float)Math.Sin(angle) * dustDist, (float)Math.Cos(angle) * dustDist);
                Dust dust = Main.dust[Dust.NewDust(parent.Center - offset - new Vector2(4, 4), 0, 0, dustType)];
                dust.noGravity = true;
                dust.velocity *= 0.1f;
            }
            for (int k = 0; k < 10; k++)
            {
                Vector2 left = new Vector2(parent.Center.X - 90, parent.Center.Y);
                Vector2 right = new Vector2(parent.Center.X + 90, parent.Center.Y);
                Dust d = Main.dust[Dust.NewDust(left + (right - left) * Main.rand.NextFloat() - new Vector2(4, 4), 0, 0, dustType)];
                d.noGravity = true;
                d.velocity *= 0.04f;
                d.scale *= 0.8f;
            }
            if (!ElementsAwoken.timeA.Current || !parent.active) projectile.Kill();
        }
    }
}