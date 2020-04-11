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
    public class RadiantStarHoming : ModProjectile
    {
        int numCanHit = 6;
        public override string Texture { get { return "ElementsAwoken/Projectiles/RadiantStar"; } }
        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 38;

            projectile.friendly = true;
            projectile.melee = true;
            projectile.tileCollide = false;

            projectile.penetrate = -1;
            projectile.timeLeft = 300;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiant Star");
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            if (projectile.ai[1] < numCanHit)
            {
                Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
                for (int k = 0; k < projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                    float alpha = (1 - projectile.alpha / 255) - ((float)k / (float)projectile.oldPos.Length);
                    float scale = 1 - ((float)k / (float)projectile.oldPos.Length);
                    Color color = Color.Lerp(Color.White, new Color(127, 3, 252), (float)k / (float)projectile.oldPos.Length) * alpha;
                    sb.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale * scale, SpriteEffects.None, 0f);
                }
            }
            return true;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.ai[1] >= numCanHit) return false;
            return base.CanHitNPC(target);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 5;
            projectile.ai[1]++;
        }
        public override void AI()
        {
            if (projectile.ai[1] < numCanHit)
            {
                if (projectile.timeLeft <= 60 || (CountProjectiles() > 9 && HasLeastTimeleft()))
                {
                    projectile.ai[1] = numCanHit;
                }
                projectile.rotation += 0.05f;
                projectile.ai[0] += 1f;
                if (projectile.ai[0] >= 15f)
                {
                    ProjectileUtils.Home(projectile, 8f, 800f);
                }
                projectile.velocity *= 0.98f;
                ProjectileUtils.PushOtherEntities(projectile);
            }
            else
            {
                projectile.velocity *= 0.9f;
                ProjectileUtils.FadeOut(projectile, 255 / 60);
                Vector2 oldCenter = projectile.Center;
                projectile.scale *= 0.98f;
                projectile.Center = oldCenter;
            }
        }
        private bool HasLeastTimeleft()
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == projectile.type && projectile.timeLeft > Main.projectile[i].timeLeft && Main.projectile[i].ai[1] < numCanHit) return false;
            }
            return true;
        }
        private int CountProjectiles()
        {
            int num = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == projectile.type && Main.projectile[i].owner == projectile.owner && Main.projectile[i].ai[1] < numCanHit) num++;
            }
            return num;
        }
    }
}