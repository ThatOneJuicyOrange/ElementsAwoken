using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class VoidHeal : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.alpha = 60;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft = 200;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heal");
        }
        public override void AI()
        {
            int num508 = (int)projectile.ai[0];
            float num509 = 4f;
            Vector2 vector33 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
            float num510 = Main.player[num508].Center.X - vector33.X;
            float num511 = Main.player[num508].Center.Y - vector33.Y;
            float num512 = (float)Math.Sqrt((double)(num510 * num510 + num511 * num511));
            float num513 = num512;
            if (num512 < 50f && projectile.position.X < Main.player[num508].position.X + (float)Main.player[num508].width && projectile.position.X + (float)projectile.width > Main.player[num508].position.X && projectile.position.Y < Main.player[num508].position.Y + (float)Main.player[num508].height && projectile.position.Y + (float)projectile.height > Main.player[num508].position.Y)
            {
                if (projectile.owner == Main.myPlayer && !Main.player[Main.myPlayer].moonLeech)
                {
                    int num514 = (int)projectile.ai[1];
                    Main.player[num508].HealEffect(num514, false);
                    Player player = Main.player[num508];
                    player.statLife += num514;
                    if (Main.player[num508].statLife > Main.player[num508].statLifeMax2)
                    {
                        Main.player[num508].statLife = Main.player[num508].statLifeMax2;
                    }
                    NetMessage.SendData(66, -1, -1, null, num508, (float)num514, 0f, 0f, 0, 0, 0);
                }
                projectile.Kill();
            }
            num512 = num509 / num512;
            num510 *= num512;
            num511 *= num512;
            projectile.velocity.X = (projectile.velocity.X * 15f + num510) / 16f;
            projectile.velocity.Y = (projectile.velocity.Y * 15f + num511) / 16f;
            {
                for (int num519 = 0; num519 < 5; num519 += 1)
                {
                    float num520 = projectile.velocity.X * 0.2f * (float)num519;
                    float num521 = (0f - projectile.velocity.Y * 0.2f) * (float)num519;
                    Vector2 position144 = new Vector2(projectile.position.X, projectile.position.Y);
                    int width117 = projectile.width;
                    int height117 = projectile.height;
                    int num522 = Dust.NewDust(position144, width117, height117, 127, 0f, 0f, 100, default(Color), 1.3f);
                    Main.dust[num522].noGravity = true;
                    Main.dust[num522].velocity *= 0f;
                    Main.dust[num522].position.X -= num520;
                    Main.dust[num522].position.Y -= num521;
                }
            }
        }
    }
}