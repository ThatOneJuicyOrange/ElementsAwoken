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
    public class InfinityCrystalSpawner : ModProjectile
    {
        private int duration = 60;
        private int riseTime = 15;

        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;

            projectile.penetrate = -1;
            projectile.timeLeft = 60000;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ability");
        }
        public override bool CanDamage()
        {
            return false;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) // to centre the spin
        {
            spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
            Texture2D texture = Main.npcTexture[(int)projectile.ai[0]];
            int frameHeight = texture.Height / Main.npcFrameCount[(int)projectile.ai[0]];

            var shader = GameShaders.Misc["ElementsAwoken:SwirlSprite"];
            shader.UseOpacity(0);
            if (projectile.ai[1] > 15f)
            {
                shader.UseSaturation(Main.npcFrameCount[(int)projectile.ai[0]]);
                shader.Shader.Parameters["uRadius"].SetValue(frameHeight / 2);
                shader.UseOpacity(((projectile.ai[1] - 15f) / ((float)duration - (float)riseTime)) * 1);
                shader.UseColor(lightColor);
            }
            shader.Apply(null);

            SpriteEffects spriteEffects = SpriteEffects.None;
            if (projectile.spriteDirection == -1) spriteEffects = SpriteEffects.FlipHorizontally;
            int startY = frameHeight * projectile.frame;
            Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, frameHeight);

            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), sourceRectangle, lightColor, projectile.rotation, new Vector2(texture.Width / 2, frameHeight / 2), projectile.scale, spriteEffects, 0f);



            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

           
            return false;
        }
        public override void AI()
        {
            Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, Main.rand.NextBool() ? DustID.PinkFlame : 135, projectile.velocity.X * 2f, projectile.velocity.Y * 2f, 0, default(Color), 1f)];
            dust.noGravity = true;
            projectile.ai[1]++;
            if (projectile.ai[1] < riseTime)
            {
                projectile.velocity.Y = -5;
            }
            else
            {
                projectile.velocity.Y *= 0.95f;
                if (projectile.ai[1] > riseTime * 4.5f) projectile.scale -= 1 / ((float)duration - (float)riseTime * 1.5f);
            }
            if (projectile.scale <= 0)
            {
                projectile.Kill();
                for (int p = 1; p <= 2; p++)
                {
                    float strength = p * 3f;
                    int numDusts = p * 20;
                    ProjectileUtils.OutwardsCircleDust(projectile, p == 1 ? DustID.PinkFlame : 135, numDusts, strength, randomiseVel: true);
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            int item = Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, ItemType<InfinityCrys>());
            NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
            Main.PlaySound(SoundID.Item67, projectile.position);
        }
    }
}