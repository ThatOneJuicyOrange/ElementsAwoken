using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using ReLogic.Utilities;
using ElementsAwoken.Items.Materials;
using Terraria.Graphics.Shaders;

namespace ElementsAwoken.Projectiles.Other
{
    public class InfinityCrystalGuide : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;

            projectile.tileCollide = false;

            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.timeLeft = 60000;
            projectile.scale = 0.05f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Guide");
        }
        public override bool CanDamage()
        {
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) // to centre the spin
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (projectile.spriteDirection == -1) spriteEffects = SpriteEffects.FlipHorizontally;
            Texture2D crys = Main.itemTexture[ItemType<InfinityCrys>()];
            Texture2D frag = Main.itemTexture[ItemType<NeutronFragment>()];
            Texture2D crac = Main.itemTexture[ItemType<CInfinityCrys>()];
            Vector2 drawPos = projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY);
            float itemScale = 0.8f;
            if (projectile.ai[0] == 0)
            {
                Texture2D tex = ElementsAwoken.AADeathBall;
                if (projectile.localAI[1] == 1) spriteBatch.Draw(crac, drawPos, null, Color.White * (1f - (float)projectile.alpha / 255f), projectile.rotation, crac.Size() / 2, itemScale * 0.8f, spriteEffects, 0f);
                 spriteBatch.Draw(tex, drawPos, null, Color.White, 0, tex.Size() / 2, projectile.scale * 0.15f, SpriteEffects.None, 0.0f);

            }
            else
            {
                spriteBatch.Draw(crys, drawPos + new Vector2(projectile.ai[0], 0), null, Color.White * (1f - (float)projectile.alpha / 255f), projectile.rotation, crys.Size() / 2, itemScale, spriteEffects, 0f);
                spriteBatch.Draw(frag, drawPos - new Vector2(projectile.ai[0], 0), null, Color.White * (1f - (float)projectile.alpha / 255f), projectile.rotation, frag.Size() / 2, itemScale, spriteEffects, 0f);
            }
            return false;
        }
        public override void AI()
        {
            int desiredAlpha = 70;
            if (projectile.ai[1] == 0)
            {
                projectile.ai[0] = -30;
            }
            projectile.ai[1]++;
            if (projectile.ai[1] < 240)
            {
                if (projectile.alpha > desiredAlpha) projectile.alpha -= 10;
                if (projectile.alpha < desiredAlpha) projectile.alpha = 70;
            }
            else
            {
                projectile.alpha += 10;
                if (projectile.alpha >= 255) projectile.Kill();
            }
            if (projectile.alpha == desiredAlpha)
            {
                if (projectile.ai[0] < 0)
                {
                    projectile.localAI[0] += 0.01f;
                    projectile.ai[0] += projectile.localAI[0];
                }
                else projectile.ai[0] = 0;
            }
            if (projectile.ai[0] > -6)
            {
                if (projectile.localAI[1] == 0)
                {
                    if (projectile.scale < 1f) projectile.scale += 0.03f;
                    else projectile.localAI[1] = 1;
                }
                else
                {
                    if (projectile.scale > 0f) projectile.scale -= 0.05f;
                    else projectile.scale = 0;
                }
            }
        }
    }
}