using System;
using System.Collections.Generic;
using ElementsAwoken.Projectiles.Explosions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class AeroflakP : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;

            projectile.friendly = true;
            projectile.ranged = true;

            projectile.penetrate = -1;
            projectile.timeLeft = 600;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Star");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (projectile.localAI[0] == 0)
            {
                projectile.ai[0] = Main.rand.NextFloat((float)Math.PI * 2);
                projectile.localAI[0]++;
            }
            projectile.ai[0] += 0.02f;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.noGravity && Vector2.Distance(nPC.Center, projectile.Center) < 100 && nPC.CanBeChasedBy(projectile, false) && Collision.CanHitLine(projectile.Center, 1, 1, nPC.Center, 1, 1))
                {
                    Explosion();
                    int numDusts = 40;
                    for (int k = 0; k < numDusts; k++)
                    {
                        Vector2 position = (Vector2.Normalize(projectile.velocity) * new Vector2((float)projectile.width / 2f, (float)projectile.height) * 0.75f * 0.5f).RotatedBy((double)((float)(k - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + projectile.Center;
                        Vector2 velocity = position - projectile.Center;
                        int dust = Dust.NewDust(position + velocity, 0, 0, 229, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].noLight = true;
                        Main.dust[dust].velocity = Vector2.Normalize(velocity) * 9f;
                    }
                    projectile.Kill();
                    break;
                }
            }
            if (projectile.oldPos[projectile.oldPos.Length - 1] == projectile.position && projectile.ai[1] != 0) projectile.Kill();
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.ai[1] = 1;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.ai[1] = 1;
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.ai[1] != 0) return false;
            return base.CanHitNPC(target);
        }
        public override bool ShouldUpdatePosition()
        {
            if (projectile.ai[1] != 0) return false;
            return base.ShouldUpdatePosition();
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                if (projectile.oldPos[k] == projectile.position && projectile.ai[1] != 0) continue;
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);

                float scale = 1 - ((float)k / (float)projectile.oldPos.Length);
                Color color = Color.Lerp(Color.White, new Color(85, 44, 156), (float)k / (float)projectile.oldPos.Length) * scale;

                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, scale, SpriteEffects.None, 0f);
            }

            Texture2D star = ModContent.GetTexture("ElementsAwoken/Projectiles/AeroflakHead");
            Vector2 starOrigin = new Vector2(star.Width * 0.5f, star.Height * 0.5f);
            Vector2 starPos = projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY);
            if (projectile.ai[1] == 0) Main.spriteBatch.Draw(star, starPos, null, Color.White, projectile.ai[0], starOrigin, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }

        private void Explosion()
        {
            Projectile exp = Main.projectile[Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<AeroflakExplosion>(), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f)];
            Main.PlaySound(SoundID.Item62, (int)projectile.position.X, (int)projectile.position.Y);
            int num = ModContent.GetInstance<Config>().lowDust ? 10 : 20;
            for (int i = 0; i < num; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f)];
                dust.velocity *= 3f;
            }
            int num2 = ModContent.GetInstance<Config>().lowDust ? 5 : 10;
            for (int i = 0; i < num2; i++)
            {
                int dustID = 229;
                Dust dust = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dustID, 0f, 0f, 100, default(Color), 2.5f)];
                dust.noGravity = true;
                dust.velocity *= 5f;
                int dustID2 = 229;
                dust = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dustID2, 0f, 0f, 100, default(Color), 1.5f)];
                dust.velocity *= 6f;
            }
            int num373 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore85 = Main.gore[num373];
            gore85.velocity.X = gore85.velocity.X + 1f;
            Gore gore86 = Main.gore[num373];
            gore86.velocity.Y = gore86.velocity.Y + 1f;
            num373 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore87 = Main.gore[num373];
            gore87.velocity.X = gore87.velocity.X - 1f;
            Gore gore88 = Main.gore[num373];
            gore88.velocity.Y = gore88.velocity.Y + 1f;
            num373 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore89 = Main.gore[num373];
            gore89.velocity.X = gore89.velocity.X + 1f;
            Gore gore90 = Main.gore[num373];
            gore90.velocity.Y = gore90.velocity.Y - 1f;
            num373 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore91 = Main.gore[num373];
            gore91.velocity.X = gore91.velocity.X - 1f;
            Gore gore92 = Main.gore[num373];
            gore92.velocity.Y = gore92.velocity.Y - 1f;
        }
    }
}