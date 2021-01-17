using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken
{
    public class TheOrchard
    {
        public static float orangeScale = 1f;
        public static void DrawOrchardGame()
        {
            Player player = Main.LocalPlayer;
            TheOrchardPlayer orchardPlayer = player.GetModPlayer<TheOrchardPlayer>();

            player.showItemIcon = false;

            int gameWidth = 800;
            int gameHeight = 450;
            Vector2 screenCenter = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
            Rectangle UIRect = new Rectangle(Main.screenWidth / 2 - gameWidth / 2, Main.screenHeight / 2 - gameHeight / 2, gameWidth, gameHeight);
            Main.spriteBatch.Draw(Main.magicPixel, new Rectangle((int)UIRect.X, (int)UIRect.Y, gameWidth, gameHeight), new Rectangle?(new Rectangle(0, 0, 1, 1)), Color.LightBlue);
            Texture2D orange = GetTexture("ElementsAwoken/Extra/Orange");
            if (new Rectangle((int)(screenCenter.X - orange.Width / 2), (int)(screenCenter.Y - orange.Height / 2), orange.Width, orange.Height).Contains(new Point(Main.mouseX, Main.mouseY)))
            {
                if (Main.mouseLeft && Main.mouseLeftRelease)
                {
                    orchardPlayer.oranges++;
                    orangeScale = 1.2f;
                }
            }
            if (orangeScale > 1f)
            {
                orangeScale -= (orangeScale - 1) * 0.05f;
            }
            else orangeScale = 1;
            Main.spriteBatch.Draw(orange, screenCenter, null, Color.White, 0, orange.Size() / 2, orangeScale, SpriteEffects.None, 0f);

            string sellButton = "Sell Oranges";
            Vector2 sellButtonLoc = new Vector2(UIRect.X, UIRect.Y) + new Vector2(80, 30);
            Vector2 sellButtonLocTopLeft = sellButtonLoc - Main.fontMouseText.MeasureString(sellButton) /2;
            float sellButtonScale = 1f;
            if (Main.mouseX > sellButtonLocTopLeft.X && Main.mouseX < sellButtonLocTopLeft.X + Main.fontMouseText.MeasureString(sellButton).X && Main.mouseY > sellButtonLocTopLeft.Y && Main.mouseY < sellButtonLocTopLeft.Y + Main.fontMouseText.MeasureString(sellButton).Y)
            {
                sellButtonScale *= 1.1f;
                if (Main.mouseLeft && Main.mouseLeftRelease && orchardPlayer.oranges > 0)
                {
                    orchardPlayer.money += (float)orchardPlayer.oranges * 0.75f;
                    orchardPlayer.oranges = 0;
                    Main.PlaySound(SoundID.Coins);
                }
            }
            Utils.DrawBorderStringFourWay(Main.spriteBatch, Main.fontMouseText, sellButton, sellButtonLoc.X, sellButtonLoc.Y, Color.White, Color.Black, Main.fontMouseText.MeasureString(sellButton) / 2, sellButtonScale);

            string oranges = "Oranges: " + orchardPlayer.oranges;
            Utils.DrawBorderStringFourWay(Main.spriteBatch, Main.fontMouseText, oranges, UIRect.X + UIRect.Width / 2, UIRect.Y + 50, Color.White, Color.Black, Main.fontMouseText.MeasureString(oranges) / 2, 1f);

            string money = "$" + string.Format("{0:0.00}", orchardPlayer.money);
            Utils.DrawBorderStringFourWay(Main.spriteBatch, Main.fontMouseText, money, UIRect.X + UIRect.Width / 2, UIRect.Y + 70, Color.White, Color.Black, Main.fontMouseText.MeasureString(money) / 2, 1f);

        }
    }
}
