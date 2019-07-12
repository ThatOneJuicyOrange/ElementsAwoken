using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Wasteland
{
    public class AcidAuraBase : ModProjectile
    {  	
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;

            projectile.penetrate = -1;
            projectile.timeLeft = 1200;
            projectile.tileCollide = false;
            projectile.hostile = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acid Aura");
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override void AI()
        {
            projectile.velocity = Vector2.Zero;
            for (int i = 0; i < Main.player.Length; i++)
            {
                Player player = Main.player[i];
                if (Vector2.Distance(player.Center, projectile.Center) < 60)
                {
                    player.AddBuff(mod.BuffType("AcidBurn"), 180);
                }
            }
            Lighting.AddLight(projectile.Center, 0.35f, 0.5f, 0.24f);
            if (Main.rand.Next(7) == 0)
            {
                Vector2 position = projectile.Center + Main.rand.NextVector2Circular(50, 50);
                Dust dust = Dust.NewDustPerfect(position, 74, Vector2.Zero, 150);
                dust.noGravity = true;
            }
            projectile.localAI[0]++;
            if (projectile.localAI[0] > 900)
            {
                projectile.alpha += 7;
                if (projectile.alpha >= 255)
                {
                    projectile.Kill();
                }
            }
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawPos = projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY);
            Texture2D auraTex = mod.GetTexture("Projectiles/NPCProj/Wasteland/AcidAura");
            spriteBatch.Draw(auraTex, drawPos - new Vector2(auraTex.Width / 2, auraTex.Height / 2), null, projectile.GetAlpha(lightColor) * 0.3f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            Texture2D outlineTex = mod.GetTexture("Projectiles/NPCProj/Wasteland/AcidAuraOutline");
            spriteBatch.Draw(outlineTex, drawPos - new Vector2(outlineTex.Width / 2, outlineTex.Height / 2), null, projectile.GetAlpha(lightColor), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
        public override void Kill(int timeLeft)
        {
            int numDusts = 20;
            for (int i = 0; i < numDusts; i++)
            {
                Vector2 position = (Vector2.Normalize(Vector2.One) * new Vector2((float)projectile.width / 2f, (float)projectile.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + projectile.Center;
                Vector2 velocity = position - projectile.Center;
                int dust = Dust.NewDust(position + velocity, 0, 0, 74, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].noLight = true;
                Main.dust[dust].velocity = Vector2.Normalize(velocity) * 3f;
            }
        }
    }
}