using System;
using System.Collections.Generic;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class RadiantKatanaStar : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
            projectile.scale *= 0.7f;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiant Star");
        }
        public override void AI()
        {
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 15f)
            {
                ProjectileUtils.Home(projectile, 15f);
            }
            if (projectile.oldPos[projectile.oldPos.Length - 1] == projectile.position && projectile.ai[1] != 0) projectile.Kill();
        }
        public override void Kill(int timeLeft)
        {

        }
        private void Explosion()
        {
            Vector2 spinningpoint = new Vector2(0f, -3f).RotatedByRandom(3.1415927410125732);
            float num71 = 24f;
            Vector2 value = new Vector2(1.05f, 1f);
            float num74;
            for (float num72 = 0f; num72 < num71; num72 = num74 + 1f)
            {
                int num73 = Dust.NewDust(projectile.Center, 0, 0, DustID.PinkFlame, 0f, 0f, 0, Color.Transparent, 1f);
                Main.dust[num73].position = projectile.Center;
                Main.dust[num73].velocity = spinningpoint.RotatedBy((double)(6.28318548f * num72 / num71), default(Vector2)) * value * (0.8f + Main.rand.NextFloat() * 0.4f) * 2f;
                Main.dust[num73].color = Color.SkyBlue;
                Main.dust[num73].noGravity = true;
                Dust dust = Main.dust[num73];
                dust.scale += 0.5f + Main.rand.NextFloat();
                num74 = num72;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.ai[1] = 1;
            Explosion();
                }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.ai[1] = 1;
            Explosion();
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
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (projectile.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Vector2 vector11 = new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2), (float)(Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type] / 2));

            Texture2D texture = Main.projectileTexture[projectile.type];
            Vector2 vector40 = projectile.Center - Main.screenPosition;
            vector40 -= new Vector2((float)texture.Width, (float)(texture.Height / Main.projFrames[projectile.type])) * projectile.scale / 2f;
            vector40 += vector11 * projectile.scale + new Vector2(0f, projectile.gfxOffY);
            float num147 = 1f / (float)projectile.oldPos.Length * 1.1f;
            int num148 = projectile.oldPos.Length - 1;
            while ((float)num148 >= 0f)
            {
                if (projectile.oldPos[num148] != projectile.position)
                {
                    float num149 = (float)(projectile.oldPos.Length - num148) / (float)projectile.oldPos.Length;
                    Color color35 = Color.White;
                    color35 *= 1f - num147 * (float)num148 / 1f;
                    color35.A = (byte)((float)color35.A * (1f - num149));
                    Main.spriteBatch.Draw(texture, vector40 + projectile.oldPos[num148] - projectile.position, null, color35, projectile.oldRot[num148], vector11, projectile.scale * MathHelper.Lerp(0.8f, 0.3f, num149), spriteEffects, 0f);
                }
                num148--;
            }
            texture = mod.GetTexture("Projectiles/RadiantStar");
            if (projectile.ai[1] == 0) Main.spriteBatch.Draw(texture, vector40, null, Color.White, 0f, texture.Size() / 2f, projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}