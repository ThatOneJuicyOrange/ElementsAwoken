using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.TheGuardian
{
    public class GuardianPortal : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;

            projectile.penetrate = -1;
            projectile.timeLeft = 600;

        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Temples Vortex");
        }
        public override bool CanHitPlayer(Player target)
        {
            if (projectile.alpha >= 60) return false;
            return base.CanHitPlayer(target);
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.9f, 0.2f, 0.4f);

            if (Main.rand.NextBool(2))
            {
                Vector2 position = projectile.Center + Main.rand.NextVector2Circular(projectile.width * 0.5f, projectile.height * 0.5f);
                Dust dust = Dust.NewDustPerfect(position, 6, Vector2.Zero);
                dust.noGravity = true;
            }
            if (projectile.ai[0] == 0)
            {
                int swirlCount = 5;
                for (int l = 0; l < swirlCount; l++)
                {
                    int distance = 360 / swirlCount;
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("GuardianPortalSwirl"), projectile.damage, projectile.knockBack, 0, l * distance, projectile.whoAmI);
                }
                projectile.ai[0]++;
            }
            if (projectile.timeLeft <= 60)
            {
                projectile.alpha += 255 / 60;
                if (projectile.alpha >= 255) projectile.Kill();
            }
            projectile.ai[1]++;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, Main.projectileTexture[projectile.type].Height * 0.5f);
            Vector2 drawPos = projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY);
            Color color = projectile.GetAlpha(lightColor);
            sb.Draw(Main.projectileTexture[projectile.type], drawPos, null, color * 0.5f, projectile.rotation, drawOrigin, projectile.scale * ((1+ (float)Math.Sin(projectile.ai[1] / 6)) * 0.75f), SpriteEffects.None, 0f);
            return true;
        }
    }
}