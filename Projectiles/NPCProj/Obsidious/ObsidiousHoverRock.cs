using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj.Obsidious
{
    public class ObsidiousHoverRock : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            projectile.width = 44;
            projectile.height = 44;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.penetrate = 1;
            projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Big Stone");
        }
        public override void AI()
        { 
            projectile.rotation += 0.05f;
            for (int i = 0; i < 2; i++)
            {
                int dust1 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.PinkFlame, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[dust1].noGravity = true;
                Main.dust[dust1].velocity *= 0f;
            }
            projectile.localAI[0]++;
            if (projectile.timeLeft < 90)
            {
                projectile.tileCollide = true;
                projectile.velocity.Y += 0.24f * -projectile.ai[0];
            }
            else
            {
                projectile.velocity *= 0.95f;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = ModContent.GetTexture("ElementsAwoken/Projectiles/NPCProj/Obsidious/ObsidiousHoverRockGlow");
            for (int l = 0; l < 3; l++)
            {
                float scalePulse = MathHelper.Lerp(0.9f, 1.2f, (float)((1 + Math.Sin(projectile.localAI[0] / 10 + l)) / 2));
                Color color = new Color(100, 100, 100, 0) * (1f - (float)projectile.alpha / 255f);
                spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, null, color, projectile.rotation, tex.Size() / 2, scalePulse, SpriteEffects.None, 0f);
            }
            spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center - Main.screenPosition, null, lightColor, projectile.rotation, Main.projectileTexture[projectile.type].Size() / 2, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.CreateDustRing(projectile, DustID.PinkFlame, 30, 30);
        }
    }
}