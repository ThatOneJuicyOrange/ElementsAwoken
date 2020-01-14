using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class GustStrikeCloud : ModProjectile
    {
        public int shootTimer = 0;
        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;

            projectile.penetrate = -1;

            projectile.melee = true;
            projectile.friendly = true;

            projectile.timeLeft = 300;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gust Strike");
            Main.projFrames[projectile.type] = 4;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.ai[1]++;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.alpha > 0) return false;
            return base.CanHitNPC(target);
        }
        public override void AI()
        {
            if (projectile.localAI[0] == 0)
            {
                shootTimer = Main.rand.Next(20, 180);
                projectile.localAI[0]++;
            }
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            projectile.ai[0] += (float)(Math.PI / 10);
            for (int j = 0; j < 2; j++)
            {
                int dustLength = ModContent.GetInstance<Config>().lowDust ? 1 : 3;
                for (int i = 0; i < dustLength; i++)
                {
                    float Y = ((float)Math.Sin(projectile.ai[0]) * 5) * (j == 0 ? 1 : -1) + (j == 0 ? 1 : -1) * 10;
                    Vector2 dustPos = new Vector2(Y, 0);
                    dustPos = dustPos.RotatedBy((double)projectile.rotation, default(Vector2));

                    Dust dust = Main.dust[Dust.NewDust(projectile.Center + dustPos - Vector2.One * 5f, 2, 2, 31)];
                    dust.velocity = Vector2.Zero;
                    dust.position -= projectile.velocity / dustLength * (float)i;
                    dust.noGravity = true;
                    dust.alpha = projectile.alpha;
                }
            }
            shootTimer--;
            if (shootTimer <= 0)
            {
                for (int l = 0; l < 200; l++)
                {
                    NPC nPC = Main.npc[l];
                    if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Collision.CanHit(projectile.position, projectile.width, projectile.height, nPC.position, nPC.width, nPC.height) && Vector2.Distance(projectile.Center, nPC.Center) <= 300)
                    {
                        float projSpeed = 8f; //modify the speed the projectile are shot.  Lower number = slower projectile.
                        float speedX = nPC.Center.X - projectile.Center.X;
                        float speedY = nPC.Center.Y - projectile.Center.Y;
                        float num406 = (float)Math.Sqrt((double)(speedX * speedX + speedY * speedY));
                        num406 = projSpeed / num406;
                        speedX *= num406;
                        speedY *= num406;

                        Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 60, 1, -0.2f);
                        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, speedX, speedY, mod.ProjectileType("GustStrikeLightning"), projectile.damage / 2, 0f, projectile.owner);
                        shootTimer = Main.rand.Next(20, 180);
                        return;
                    }
                }
            }
            if (projectile.ai[1] != 0)
            {
                projectile.alpha += 7;
                if (projectile.alpha >= 255) projectile.Kill();
            }
        }
    
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 6)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                    projectile.frame = 0;
            }

            Texture2D tex = Main.projectileTexture[projectile.type];
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                Rectangle rectangle = new Rectangle(0, (tex.Height / Main.projFrames[projectile.type]) * projectile.frame, tex.Width, tex.Height / Main.projFrames[projectile.type]);
                sb.Draw(tex, drawPos, rectangle, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }

            return true;
        }
    }
}