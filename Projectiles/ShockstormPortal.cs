using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ShockstormPortal : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 100;
            projectile.height = 100;
            projectile.magic = true;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.alpha = 255;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shockstorm");
            Main.projFrames[projectile.type] = 4;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 2)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                    projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.localAI[0]++;
            if (projectile.localAI[0] < 300f)
            {
                projectile.alpha -= 5;
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
            }
            else
            {
                projectile.alpha += 5;
                if (projectile.alpha >= 255)
                {
                    projectile.Kill();
                }
            }
            int num3;
            if (projectile.alpha < 150 && projectile.ai[0] < 180f)
            {
                for (int num849 = 0; num849 < 1; num849 = num3 + 1)
                {
                    float num850 = (float)Main.rand.NextDouble() * 1f - 0.5f;
                    if (num850 < -0.5f)
                    {
                        num850 = -0.5f;
                    }
                    if (num850 > 0.5f)
                    {
                        num850 = 0.5f;
                    }
                    Vector2 value37 = new Vector2((float)(-(float)projectile.width) * 0.2f * projectile.scale, 0f).RotatedBy((double)(num850 * 6.28318548f), default(Vector2)).RotatedBy((double)projectile.velocity.ToRotation(), default(Vector2));
                    int num851 = Dust.NewDust(projectile.Center - Vector2.One * 5f, 10, 10, 226, -projectile.velocity.X / 3f, -projectile.velocity.Y / 3f, 150, Color.Transparent, 0.7f);
                    Main.dust[num851].position = projectile.Center + value37;
                    Main.dust[num851].velocity = Vector2.Normalize(Main.dust[num851].position - projectile.Center) * 2f;
                    Main.dust[num851].noGravity = true;
                    num3 = num849;
                }
                for (int num852 = 0; num852 < 1; num852 = num3 + 1)
                {
                    float num853 = (float)Main.rand.NextDouble() * 1f - 0.5f;
                    if (num853 < -0.5f)
                    {
                        num853 = -0.5f;
                    }
                    if (num853 > 0.5f)
                    {
                        num853 = 0.5f;
                    }
                    Vector2 value38 = new Vector2((float)(-(float)projectile.width) * 0.6f * projectile.scale, 0f).RotatedBy((double)(num853 * 6.28318548f), default(Vector2)).RotatedBy((double)projectile.velocity.ToRotation(), default(Vector2));
                    int num854 = Dust.NewDust(projectile.Center - Vector2.One * 5f, 10, 10, 226, -projectile.velocity.X / 3f, -projectile.velocity.Y / 3f, 150, Color.Transparent, 0.7f);
                    Main.dust[num854].velocity = Vector2.Zero;
                    Main.dust[num854].position = projectile.Center + value38;
                    Main.dust[num854].noGravity = true;
                    num3 = num852;
                }
            }

            projectile.localAI[1]--;
            float max = 1000f;
            for (int i = 0; i < 200; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(projectile.Center, nPC.Center) <= max)
                {
                    if (projectile.localAI[1] <= 0)
                    {
                        Vector2 vector94 = nPC.Center - projectile.Center;
                        float ai = (float)Main.rand.Next(100);
                        Vector2 vector95 = Vector2.Normalize(vector94.RotatedByRandom(0.78539818525314331)) * 9f;
                        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector95.X, vector95.Y, mod.ProjectileType("LightningArc"), (int)projectile.ai[1], 0f, Main.myPlayer, vector94.ToRotation(), ai);
                        Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 122);

                        projectile.localAI[1] = Main.rand.Next(15, 60);
                    }
                }
            }
        }   
    }
}