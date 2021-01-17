using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Obsidious
{
    public class ObsidiousGravitySwitcher : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grav Switcher");
        }
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;

            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.penetrate = -1;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
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
            projectile.ai[0]++;
            int num = 180;
            if (projectile.ai[0] == num)
            {

            }
            else if (projectile.ai[0] > num)
            {

                projectile.alpha += 10;
                if (projectile.alpha >= 255) projectile.Kill();
            }
            else
            {
                projectile.alpha -= 5;
                if (projectile.alpha < 90) projectile.alpha = 90;
            }
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(default, BlendState.Additive);

            Texture2D tex = ModContent.GetTexture("ElementsAwoken/Extra/Beam");

            float alphaScale = 1f - (float)projectile.alpha / 255f;
            Vector2 center = projectile.Center - Main.screenPosition;
            SpriteEffects spriteeffects = SpriteEffects.None;
            if (projectile.ai[1] == 1)
            {
                center.Y += tex.Height * alphaScale;
                spriteeffects = SpriteEffects.FlipVertically;
            }
            if (Main.LocalPlayer.gravDir == -1)
            { 
                center.Y -= (projectile.Center.Y - Main.LocalPlayer.Center.Y) * 2;
                spriteeffects = projectile.ai[1] == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically;
                center.Y += tex.Height * alphaScale;
            } 
            spriteBatch.Draw(tex, center, null, new Color(211, 111, 214) * alphaScale * 0.7f, 0, new Vector2(tex.Width / 2, tex.Height), new Vector2(20f, alphaScale), spriteeffects, 0f);

            spriteBatch.End();
            spriteBatch.Begin();
        }
    }
}