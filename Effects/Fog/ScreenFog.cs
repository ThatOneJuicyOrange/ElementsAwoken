using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Effects.Fog
{
    public class ScreenFog
    {
        public float fogOffsetX = 0;
        public float fogOffsetY = 0;
        public float fadeOpacity = 0f;
		public bool backgroundFog = false;
        private Vector2 prevScreenPos = Vector2.Zero;
		public ScreenFog(bool bg)
		{
			backgroundFog = bg;
		}

        public void Update(Texture2D texture, int dir, bool criteria)
        {
            if (Main.netMode == 2 || Main.dedServ) return; //BEGONE SERVER HEATHENS! UPDATE ONLY CLIENTSIDE!
            if (prevScreenPos == Vector2.Zero) prevScreenPos = Main.screenPosition;
            Player player = Main.player[Main.myPlayer];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            //bool enableFog = ElementsAwoken.encounter == 2 || (MyWorld.plateauWeather == 1 && MyWorld.plateauWeatherTime > 0 && modPlayer.zonePlateau);

            fogOffsetX += 1;
           // float mult = 1;
            if (Main.hasFocus)
            {
                //if (player.wet || player.lavaWet) mult = 0.5f;
                //if (player.honeyWet) mult = 0.25f;
                //fogOffsetX += -player.velocity.X * mult * dir;
                //fogOffsetY += -player.velocity.Y * mult;

                fogOffsetX += prevScreenPos.X - Main.screenPosition.X;
                fogOffsetY += prevScreenPos.Y - Main.screenPosition.Y;
            }
            if (fogOffsetX >= texture.Width || fogOffsetX <= -texture.Width) fogOffsetX = 0;
            if (fogOffsetY >= texture.Height || fogOffsetY <= -texture.Height) fogOffsetY = 0;

            float maxOpacity = 0.5f;
            if (criteria)
            {
                fadeOpacity += 0.005f;
                if (fadeOpacity > maxOpacity) fadeOpacity = maxOpacity;
            }
            else
            {
                fadeOpacity -= 0.005f;
                if (fadeOpacity < 0f) fadeOpacity = 0f;
            }
            prevScreenPos = Main.screenPosition;
        }

        public void Draw(Texture2D texture, int dir, Color defaultColor, bool setSB = false, float fogAlpha = 0.4f)
        {
            
            if (fadeOpacity == 0f) return; //don't draw if no fog
            if (setSB) Main.spriteBatch.Begin();

            //Color bgColor = GetAlpha(defaultColor, 0.2f * fadeOpacity);
            Color fogColor = GetAlpha(defaultColor, fogAlpha * fadeOpacity);

            //ensure we cover the whole screen first
            // Main.spriteBatch.Draw(texture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), null, bgColor, 0f, Vector2.Zero, SpriteEffects.None, 0f);	

            //overlap a little so you cannot see edges
            int minX = -texture.Width;
            int minY = -texture.Height;
            int maxX = Main.screenWidth + texture.Width;
            int maxY = Main.screenHeight + texture.Height;

            for (int i = minX; i < maxX; i += texture.Width)
            {
                for (int j = minY; j < maxY; j += texture.Height)
                {
                    Main.spriteBatch.Draw(texture, new Rectangle(i + (int)fogOffsetX * dir, j + (int)fogOffsetY, texture.Width, texture.Height), null, fogColor, 0f, Vector2.Zero, SpriteEffects.None, 0f);
                }
            }
            if (setSB) Main.spriteBatch.End();



            /*


            if (fadeOpacity == 0f) return; //don't draw if no fog
            if (!setSB) Main.spriteBatch.End();
            Main.spriteBatch.Begin();

            //Color bgColor = GetAlpha(defaultColor, 0.2f * fadeOpacity);
            Color fogColor = GetAlpha(defaultColor, fogAlpha * fadeOpacity);
            //ensure we cover the whole screen first
            // Main.spriteBatch.Draw(texture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), null, bgColor, 0f, Vector2.Zero, SpriteEffects.None, 0f);	

            //overlap a little so you cannot see edges
            int minX = -texture.Width;
            int minY = -texture.Height;
            int maxX = Main.screenWidth + texture.Width;
            int maxY = Main.screenHeight + texture.Height;

            for (int i = minX; i < maxX; i += texture.Width)
            {
                for (int j = minY; j < maxY; j += texture.Height)
                {
                    Main.spriteBatch.Draw(texture, new Rectangle(i + (int)fogOffsetX * dir, j + (int)fogOffsetY, texture.Width, texture.Height), null, fogColor, 0f, Vector2.Zero, SpriteEffects.None, 0f);
                }
            }

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(default, bsSubtract);
            Texture2D bloomTex = ModContent.GetTexture("ElementsAwoken/Extra/Bloom");
            Main.spriteBatch.Draw(bloomTex, Main.LocalPlayer.Center - Main.screenPosition, bloomTex.Frame(), Color.White, 0, bloomTex.Size() / 2, 2f, 0, 0);
            
            Main.spriteBatch.End();
            if (!setSB) Main.spriteBatch.Begin();*/
        }
        public readonly static BlendState
    bsSubtract = new BlendState
    {
        ColorSourceBlend = Blend.SourceAlpha,
        ColorDestinationBlend = Blend.One,
        ColorBlendFunction = BlendFunction.ReverseSubtract,
        AlphaSourceBlend = Blend.SourceAlpha,
        AlphaDestinationBlend = Blend.One,
        AlphaBlendFunction = BlendFunction.ReverseSubtract
    };
        public Color GetAlpha(Color newColor, float alph)
		{
			int alpha = 255 - (int)(255 * alph);
			float alphaDiff = (float)(255 - alpha) / 255f;
			int newR = (int)((float)newColor.R * alphaDiff);
			int newG = (int)((float)newColor.G * alphaDiff);
			int newB = (int)((float)newColor.B * alphaDiff);
			int newA = (int)newColor.A - alpha;
			if (newA < 0) newA = 0;
			if (newA > 255) newA = 255;		
			return new Color(newR, newG, newB, newA);
		}		
    }
}
