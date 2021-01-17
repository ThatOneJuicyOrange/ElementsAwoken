using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.UI
{
    internal class PromptInfoUI : UIState
    {
        public DragableUIPanel BackPanel;
        public UIHoverImageButton CloseButton;
        public UIDisplay UIDisplay;
        public static bool Visible;

        private int uiWidth = 500;
        private int uiHeight = 250;

        public override void OnInitialize()
        {
            BackPanel = new DragableUIPanel();
            BackPanel.SetPadding(0);

            BackPanel.Left.Set(Main.screenWidth / 2 - uiWidth / 2, 0f);
            BackPanel.Top.Set(Main.screenHeight / 2 - uiHeight / 2, 0f);
            BackPanel.Width.Set(uiWidth, 0f);
            BackPanel.Height.Set(uiHeight, 0f);
            BackPanel.BackgroundColor = new Color(73, 94, 171);


            int buttonSize = 44;
            CloseButton = new UIHoverImageButton(GetTexture("ElementsAwoken/Extra/ButtonClose2x2"), "Close");
            CloseButton.Left.Set(uiWidth - 10 - buttonSize, 0f);
            CloseButton.Top.Set(uiHeight - 10 - buttonSize, 0f);
            CloseButton.Width.Set(buttonSize, 0f);
            CloseButton.Height.Set(buttonSize, 0f);
            CloseButton.OnClick += new MouseEvent(CloseButtonClicked);
            BackPanel.Append(CloseButton);

            UIDisplay = new UIDisplay();
            UIDisplay.Left.Set(0, 0f);
            UIDisplay.Top.Set(0, 0f);
            UIDisplay.Width.Set(uiWidth, 0f);
            UIDisplay.Height.Set(uiHeight, 1f);
            BackPanel.Append(UIDisplay);
            Append(BackPanel);
        }

        private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.PlaySound(SoundID.MenuClose);
            Visible = false;
        }
    }
    public class UIDisplay : UIElement
    {
        public UIDisplay()
        {
            Width.Set(100, 0f);
            Height.Set(40, 0f);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle innerDimensions = GetInnerDimensions();

            float uiX = innerDimensions.X;
            float uiY = innerDimensions.Y;

            string sentence = "You have activated a boss prompt! Boss prompts cause effects on your world after 30 minutes after defeating a boss. These can be disabled in the EA Config (Settings > Mod Configurations > Elements Awoken)";
            string[] words = sentence.Split(new char[] { ' ' });
            IList<string> sentenceParts = new List<string>();
            sentenceParts.Add(string.Empty);

            int lineNum = 0;

            float descX = uiX + 20;
            float descY = uiY + 20;

            foreach (string word in words)
            {
                if (Main.fontMouseText.MeasureString(sentenceParts[lineNum] + word).X > 480)
                {
                    lineNum++;
                    sentenceParts.Add(string.Empty);
                }
                sentenceParts[lineNum] += word + " ";
            }
            string message = "";
            foreach (string x in sentenceParts)
                 message += x + "\n";
            Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, message, descX, descY, Color.White, Color.Black, new Vector2(0.3f), 1f);

            //innerDimensions.Width;
            float promptY = uiY + innerDimensions.Height - 48 - 32;
            spriteBatch.Draw(GetTexture("ElementsAwoken/Buffs/Prompts/ScorpionBreakout"), new Vector2(uiX+ 64, promptY), null, Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            spriteBatch.Draw(GetTexture("ElementsAwoken/Buffs/Prompts/InfernacesWrath"), new Vector2(uiX + 64 * 2, promptY), null, Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            spriteBatch.Draw(GetTexture("ElementsAwoken/Buffs/Prompts/DarkenedSkies"), new Vector2(uiX + 64 * 3, promptY), null, Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            spriteBatch.Draw(GetTexture("ElementsAwoken/Buffs/Prompts/Hypothermia"), new Vector2(uiX + 64 * 4, promptY), null, Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            spriteBatch.Draw(GetTexture("ElementsAwoken/Buffs/Prompts/StormSurge"), new Vector2(uiX + 64 * 5, promptY), null, Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            spriteBatch.Draw(GetTexture("ElementsAwoken/Buffs/Prompts/Psychosis"), new Vector2(uiX + 64 * 6, promptY), null, Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
        }
    }
}