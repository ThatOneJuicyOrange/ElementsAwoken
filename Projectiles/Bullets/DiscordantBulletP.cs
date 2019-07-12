using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Bullets
{
    public class DiscordantBulletP : ModProjectile
    {
        public bool teleported = false;
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;

            projectile.aiStyle = 1;

            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ranged = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;

            projectile.penetrate = 1;

            projectile.timeLeft = 600;

            projectile.extraUpdates = 1;

            aiType = ProjectileID.Bullet;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discordant Bullet");

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.6f, 0.1f, 0.3f);

            float max = 200f;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (!teleported && nPC.active && !nPC.friendly && !nPC.dontTakeDamage && Vector2.Distance(projectile.Center, nPC.Center) <= max)
                {
                    TeleDust();

                    projectile.tileCollide = false;

                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    Vector2 offset = new Vector2((float)Math.Sin(angle) * max, (float)Math.Cos(angle) * max);// unit circle yay
                    projectile.Center = nPC.Center + offset;

                    Vector2 toTarget = new Vector2(nPC.Center.X - projectile.Center.X, nPC.Center.Y - projectile.Center.Y);
                    toTarget.Normalize();
                    projectile.velocity = toTarget * 20f;

                    TeleDust();

                    teleported = true;
                }
            }
        }
        private void TeleDust()
        {
            float num1 = 16f;
            int num2 = 0;
            while ((float)num2 < num1)
            {
                Vector2 vector = Vector2.UnitX * 0f;
                vector += -Vector2.UnitY.RotatedBy((double)((float)num2 * (6.28318548f / num1)), default(Vector2)) * new Vector2(1f, 4f);
                vector = vector.RotatedBy((double)projectile.velocity.ToRotation(), default(Vector2));
                int dust = Dust.NewDust(projectile.Center, 0, 0, 127, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust].scale = 1f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = projectile.Center + vector;
                Main.dust[dust].velocity = projectile.velocity * 0f + vector.SafeNormalize(Vector2.UnitY) * 1f;
                num2++;
            }
        }
    }
}
