using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace ElementsAwoken.Encounters.Fog
{
    public class ScreenFog
    {
		public int fogOffsetX = 0;
		public float fadeOpacity = 0f;
		public bool backgroundFog = false;

		public ScreenFog(bool bg)
		{
			backgroundFog = bg;
		}

        public void Update(Texture2D texture)
        {
			if(Main.netMode == 2 || Main.dedServ) return; //BEGONE SERVER HEATHENS! UPDATE ONLY CLIENTSIDE!

			Player player = Main.player[Main.myPlayer];
            bool enableFog = MyWorld.encounter2;
			
			fogOffsetX += 1;
			if(fogOffsetX >= texture.Width) fogOffsetX = 0;
            float maxOpacity = 0.5f;
			if(enableFog)
			{
				fadeOpacity += 0.005f;
				if(fadeOpacity > maxOpacity) fadeOpacity = maxOpacity;
			}else
			{
				fadeOpacity -= 0.005f;
				if(fadeOpacity < 0f) fadeOpacity = 0f;
			}
        }

        public void Draw(Texture2D texture, bool dir, Color defaultColor, bool setSB = false)
        {
			if(fadeOpacity == 0f) return; //don't draw if no fog
            if(setSB) Main.spriteBatch.Begin();
			
			//Color bgColor = GetAlpha(defaultColor, 0.2f * fadeOpacity);
			Color fogColor = GetAlpha(defaultColor, 0.4f * fadeOpacity);
			
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
                    Main.spriteBatch.Draw(texture, new Rectangle(i + (dir ? -fogOffsetX : fogOffsetX), j, texture.Width, texture.Height), null, fogColor, 0f, Vector2.Zero, SpriteEffects.None, 0f);
                }
            }
            if(setSB) Main.spriteBatch.End();
        }

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
