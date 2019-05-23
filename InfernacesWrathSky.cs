using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace ElementsAwoken
{
    public class InfernacesWrathSky : CustomSky
    {
        private bool isActive;
        private float opacity;

        public override void Update(GameTime gameTime)
        {
            if (Main.gamePaused || !Main.hasFocus)
            {
                return;
            }
            if (isActive && opacity < 0.035f)
            {
                opacity += 0.005f;
            }
            else if (!isActive && opacity > 0f)
            {
                opacity -= 0.005f;
            }
        }
        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (minDepth < 1f || maxDepth == 3.40282347E+38f)
            {
                var color = new Color(255, 0, 40) * opacity;
                spriteBatch.Draw(Main.magicPixel, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), color);
            }
        }
        public override void Activate(Vector2 position, params object[] args)
        {
            isActive = true;
        }

        public override void Deactivate(params object[] args)
        {
            isActive = false;
        }

        public override void Reset()
        {
            opacity = 0f;
            isActive = false;
        }

        public override bool IsActive()
        {
            return isActive;
        }
    }
}