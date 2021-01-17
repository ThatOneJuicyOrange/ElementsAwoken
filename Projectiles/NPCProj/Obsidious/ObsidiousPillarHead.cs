using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Obsidious
{
    public class ObsidiousPillarHead : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blazing Glory");
        }
        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 48;

            projectile.tileCollide = false;
            projectile.hostile = true;
            projectile.alpha = 255;
            projectile.penetrate = -1;
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
        {
            drawCacheProjsBehindNPCsAndTiles.Add(index);
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (projectile.ai[0] == 0f)
            {
                projectile.alpha -= 90;
                if (projectile.alpha <= 100)
                {
                    projectile.alpha = 0;
                    projectile.ai[0] = 1f;
                    if (projectile.ai[1] == 0f)
                    {
                        projectile.ai[1] += 1f;
                        projectile.position += projectile.velocity * 1f;
                    }
                }
            }
            else
            {
                if (projectile.alpha < 170 && projectile.alpha + 5 >= 170)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 127, projectile.velocity.X * 0.025f, projectile.velocity.Y * 0.025f)];
                        dust.noGravity = true;
                        dust.velocity *= 0.5f;
                    }
                }
                projectile.ai[0]++;
                if (projectile.ai[0] > 120)
                {
                    projectile.alpha += 15;
                    if (projectile.alpha >= 255)
                    {
                        projectile.Kill();
                        return;
                    }
                }
            }
            projectile.localAI[0]++;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            Vector2 pos = projectile.Center;
            spriteBatch.Draw(tex, pos - Main.screenPosition, null, Color.White * (1f - (float)projectile.alpha / 255f), projectile.rotation, tex.Size() / 2, projectile.scale, SpriteEffects.None, 0f);
            int numGloweys = 6;
            for (int l = 0; l < numGloweys; l++)
            {
                float distance = 3.14f / numGloweys;
                //pos += new Vector2(2 * (float)Math.Sin(projectile.localAI[0] / 5 + (float)l * distance), 0).RotatedBy(projectile.rotation);

                float scalePulse = (float)((1 + Math.Sin(projectile.localAI[0] / 9 + (float)l * distance)) / 2) * 1.3f;
                Color color = new Color(100, 100, 100, 0) * (1f - (float)projectile.alpha / 255f);
                spriteBatch.Draw(tex, pos - Main.screenPosition, null, color, projectile.rotation, tex.Size() / 2, new Vector2(scalePulse, projectile.scale), SpriteEffects.None, 0f);
            }
            return false;
        }
    }
}