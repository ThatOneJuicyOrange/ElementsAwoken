using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace ElementsAwoken.UI
{
    internal class DragableScrollBar : UIScrollbar
    {
        public bool Visible = true;
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                base.Draw(spriteBatch);
                if (GetClippingRectangle(spriteBatch).Contains(new Microsoft.Xna.Framework.Point(Main.mouseX, Main.mouseY)))
                {
                    ElementsAwoken.usingScrollbar = true;
                }
            }
        }
    }
}
