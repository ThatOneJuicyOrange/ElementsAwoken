using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class FundementalStrike : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 42;
            projectile.height = 42;

            projectile.penetrate = -1;

            projectile.melee = true;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.tileCollide = false;

            projectile.timeLeft = 300;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Fundementals");
            Main.projFrames[projectile.type] = 4;
        }
        public override void AI()
        {
            if (projectile.ai[0] == 0 && projectile.localAI[0] == 0)
            {
                projectile.penetrate = 1;
                projectile.tileCollide = true;
                projectile.localAI[0] = 1;
            }
            if (projectile.ai[1] != 0)
            {
                projectile.alpha += 15;
                if (projectile.alpha >= 255) projectile.Kill();
            }

            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, GetDustID());
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;
        }
        private int GetDustID()
        {
            switch (Main.rand.Next(4))
            {
                case 0:
                    return mod.DustType("AncientRed");
                case 1:
                    return mod.DustType("AncientGreen");
                case 2:
                    return mod.DustType("AncientBlue");
                case 3:
                    return mod.DustType("AncientPink");
                default:
                    return mod.DustType("AncientRed");
            }
        }
        public override void Kill(int timeLeft)
        {
            if (projectile.ai[0] != 1)
            {
                ProjectileUtils.Explosion(projectile, new int[] { mod.DustType("AncientRed"), mod.DustType("AncientGreen"), mod.DustType("AncientBlue"), mod.DustType("AncientPink") }, damageType: "melee");

                for (int i = 0; i < 4; i++)
                {
                    Vector2 speed = new Vector2((float)Main.rand.Next(-9, 9), (float)Main.rand.Next(-9, 9));
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, speed.X, speed.Y, mod.ProjectileType("AncientShard"), (int)(projectile.damage * 0.75f), projectile.knockBack, projectile.owner, 0f, 0f);
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[0] == 1)
            {
                projectile.ai[1]++;
            }
            else
            {
                float damageMult = projectile.damage > 1000 ? 0.4f : 0.75f;
                for (int i = 0; i < 2; i++)
                {                
                    Projectile.NewProjectile(projectile.Center.X + 500 * (i % 2 == 0 ? -1 : 1), projectile.Center.Y, 22f * (i % 2 == 0 ? 1 : -1), 0f, mod.ProjectileType("FundementalStrike"), (int)(projectile.damage * damageMult), projectile.knockBack, projectile.owner, 1f, 0f);
                }
                target.immune[projectile.owner] = 5;
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
       /*public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                sb.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }*/
    }
}