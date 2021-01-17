using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class MiningChargeP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;

            projectile.friendly = true;
            projectile.thrown = true;

            projectile.penetrate = 1;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mining Charge");
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
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), sourceRectangle, drawColor, projectile.rotation, texture.Size() / 2 + new Vector2(3, 3), projectile.scale, spriteEffects, 0f);
            return false;
        }
        public override void AI()
        {
            projectile.rotation += projectile.velocity.X * 0.05f;
            if (projectile.ai[0] == 0) projectile.velocity.Y += 0.16f;
            projectile.ai[1]++;
            int flicker = 45;
            if (projectile.ai[1] > flicker) Lighting.AddLight(projectile.Center, 0.3f, 0.2f, 0.1f);
            if (projectile.ai[1] > flicker * 2) projectile.ai[1] = 0;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.velocity *= 0;
            projectile.ai[0] = 1;
            return false;
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, new int[] { 6 }, projectile.damage, "thrown");
            for (int i = 0; i < 5; i++)
            {
                Vector2 perturbedSpeed = new Vector2(4f, 4f).RotatedByRandom(MathHelper.ToRadians(360));
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<MiningChargeSpark>(), (int)(projectile.damage * 0.33f), 0f, 0);
            }
        }
    }
}