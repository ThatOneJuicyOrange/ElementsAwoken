using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ExlitureEye : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.penetrate = 1;

            projectile.minion = true;
            projectile.tileCollide = true;
            projectile.friendly = true;

            projectile.timeLeft = 600;

            Main.projFrames[projectile.type] = 2;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eye");
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 12)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 1)
                    projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            for (int num121 = 0; num121 < 3; num121++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 5)];
                dust.velocity *= 0.6f;
                dust.scale *= 0.6f;
                dust.noGravity = true;
            }

            float centerY = projectile.Center.X;
            float centerX = projectile.Center.Y;
            float num = 400f;
            bool home = false;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].CanBeChasedBy(projectile, false) && Collision.CanHit(projectile.Center, 1, 1, Main.npc[i].Center, 1, 1))
                {
                    float num1 = Main.npc[i].position.X + (float)(Main.npc[i].width / 2);
                    float num2 = Main.npc[i].position.Y + (float)(Main.npc[i].height / 2);
                    float num3 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num1) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num2);
                    if (num3 < num)
                    {
                        num = num3;
                        centerY = num1;
                        centerX = num2;
                        home = true;
                    }
                }
            }
            if (home)
            {
                float speed = 3f;
                Vector2 vector35 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                float num4 = centerY - vector35.X;
                float num5 = centerX - vector35.Y;
                float num6 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
                num6 = speed / num6;
                num4 *= num6;
                num5 *= num6;
                projectile.velocity.X = (projectile.velocity.X * 20f + num4) / 21f;
                projectile.velocity.Y = (projectile.velocity.Y * 20f + num5) / 21f;
                return;
            }

        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 5;
        }
        public override void Kill(int timeLeft)
        {
            Gore gore = Main.gore[Gore.NewGore(projectile.position, projectile.velocity, mod.GetGoreSlot("Gores/ExlitureEye1"))];
            Gore gore2 = Main.gore[Gore.NewGore(projectile.position, projectile.velocity, mod.GetGoreSlot("Gores/ExlitureEye2"))];
            gore.timeLeft = 60;
            gore2.timeLeft = 60;
        }
    }
}