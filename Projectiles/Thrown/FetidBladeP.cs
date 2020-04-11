using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class FetidBladeP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;

            projectile.thrown = true;
            projectile.friendly = true;

            projectile.penetrate = -1;
            projectile.timeLeft = 600;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Energy Fork");
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (projectile.ai[1] == 0)
            {
                Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
                for (int k = 0; k < projectile.oldPos.Length; k++)
                {
                    Texture2D tailTex = mod.GetTexture("Projectiles/Thrown/FetidBladeTrail");
                    Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);

                    float scale = 1 - ((float)k / (float)projectile.oldPos.Length);
                    Color color = Color.Lerp(Color.White, new Color(0, 150, 88), (float)k / (float)projectile.oldPos.Length) * scale;

                    spriteBatch.Draw(tailTex, drawPos, null, color, projectile.rotation, drawOrigin, scale, SpriteEffects.None, 0f);
                }
            }
            return true;
        } 
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (projectile.timeLeft < 120)
            {
                projectile.alpha += 255 / 120;
                if (projectile.alpha >= 255) projectile.Kill();
            }
            if (projectile.ai[1] != 0)
            {
                projectile.tileCollide = false;
                NPC stick = Main.npc[(int)projectile.ai[0]];
                if (stick.active)
                {
                    projectile.Center = stick.Center - projectile.velocity * 2f;
                    projectile.gfxOffY = stick.gfxOffY;
                }
                else projectile.Kill();
            }
            else
            {
                if (Main.rand.NextBool(3))
                {
                    int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 46, 0, 0, 150);
                    Main.dust[dust].velocity *= 0.1f;
                    Main.dust[dust].scale *= 1.5f;
                    Main.dust[dust].noGravity = true;
                }
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.ai[1] == 1) return false;
            return base.CanHitNPC(target);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            int numAttached = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj.active && proj.type == projectile.type && proj.alpha < 100 && proj.ai[1] != 0 && proj.ai[0] == target.whoAmI)
                {
                    numAttached++;
                }
            }
            if (numAttached < 3)
            {
                projectile.ai[0] = target.whoAmI;
                projectile.ai[1] = 1;
                projectile.velocity = (target.Center - projectile.Center) * 0.75f;
                projectile.netUpdate = true;
            }
            else projectile.Kill();
        }
    }
}