using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj
{
    public class InfernalScream : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;

            projectile.penetrate = -1;

            projectile.hostile = true;

            projectile.timeLeft = 360;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infernal Scream");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            projectile.ai[0] += 0.2f;
            if (projectile.ai[1] < 1) projectile.ai[1] += 1f / 60f;
            projectile.scale = MathHelper.Lerp(0.5f, 1f, (float)(Math.Sin(projectile.ai[0]) + 1) / 2) * projectile.ai[1];
            projectile.alpha = (int)MathHelper.Lerp(50, 200, (float)(Math.Sin(projectile.ai[0]) + 1) / 2);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) // to centre the spin
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (projectile.spriteDirection == -1) spriteEffects = SpriteEffects.FlipHorizontally;
            Texture2D texture = Main.projectileTexture[projectile.type];
            int frameHeight = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int startY = frameHeight * projectile.frame;
            Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);

            Color drawColor = projectile.GetAlpha(lightColor);
            Main.spriteBatch.Draw(texture, projectile.Center + new Vector2(4, 4) - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), sourceRectangle, drawColor, projectile.rotation, texture.Size() / 2, projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}