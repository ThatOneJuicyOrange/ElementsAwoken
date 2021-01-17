/*using Microsoft.Xna.Framework;
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
    internal class QuestListUI : UIState
    {
        public static DragableUIPanel BackPanel;
        public static DragableUIPanel InfoPanel;
        public QuestListUIDisplay UIDisplay;
        public QuestInfoUIDisplay InfoUIDisplay;
        public UIHoverImageButton UpButton;
        public UIHoverImageButton DownButton;
        public static bool Visible;

        private int uiWidth = 220;
        private int uiHeight = 212;

        private int infoWidth = 250;

        public override void OnInitialize()
        {
            BackPanel = new DragableUIPanel();
            BackPanel.SetPadding(0);

            int posX = Main.screenWidth / 2 - uiWidth / 2;
            int posY = Main.screenHeight / 2 - uiHeight - 30;
            int buttonSize = 22;
            UpButton = new UIHoverImageButton(GetTexture("ElementsAwoken/Extra/ButtonUp"), "Up");
            UpButton.Left.Set(uiWidth - 10 - buttonSize, 0f);
            UpButton.Top.Set(uiHeight - 10 - buttonSize * 2.5f, 0f);
            UpButton.Width.Set(buttonSize, 0f);
            UpButton.Height.Set(buttonSize, 0f);
            UpButton.OnClick += new MouseEvent(UpButtonClicked);
            BackPanel.Append(UpButton);

            DownButton = new UIHoverImageButton(GetTexture("ElementsAwoken/Extra/ButtonDown"), "Down");
            DownButton.Left.Set(uiWidth - 10 - buttonSize, 0f);
            DownButton.Top.Set(uiHeight - 10 - buttonSize , 0f);
            DownButton.Width.Set(buttonSize, 0f);
            DownButton.Height.Set(buttonSize, 0f);
            DownButton.OnClick += new MouseEvent(DownButtonClicked);
            BackPanel.Append(DownButton);

            BackPanel.Left.Set(posX, 0f);
            BackPanel.Top.Set(posY, 0f);
            BackPanel.Width.Set(uiWidth, 0f);
            BackPanel.Height.Set(uiHeight, 0f);
            BackPanel.BackgroundColor = new Color(73, 94, 171);

            UIDisplay = new QuestListUIDisplay();
            UIDisplay.Left.Set(0, 0f);
            UIDisplay.Top.Set(0, 0f);
            UIDisplay.Width.Set(uiWidth, 0f);
            UIDisplay.Height.Set(uiHeight, 1f);
            BackPanel.Append(UIDisplay);
            Append(BackPanel);


            InfoPanel = new DragableUIPanel();
            InfoPanel.SetPadding(0);

            InfoUIDisplay = new QuestInfoUIDisplay();
            InfoUIDisplay.Left.Set(0, 0f);
            InfoUIDisplay.Top.Set(0, 0f);
            InfoUIDisplay.Width.Set(infoWidth, 0f);
            InfoUIDisplay.Height.Set(uiHeight, 1f);
            InfoPanel.Append(InfoUIDisplay);

            InfoPanel.Left.Set(posX - infoWidth - 10, 0f);
            InfoPanel.Top.Set(posY, 0f);
            InfoPanel.Width.Set(infoWidth, 0f);
            InfoPanel.Height.Set(uiHeight, 0f);
            InfoPanel.BackgroundColor = new Color(73, 94, 171);

            Append(InfoPanel);
        }
        private void UpButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.PlaySound(SoundID.MenuTick);
            UIDisplay.questNum--;
        }
        private void DownButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.PlaySound(SoundID.MenuTick);
            UIDisplay.questNum++;
        }
    }
    public class QuestListUIDisplay : UIElement
    {
        public int questNum = 0;
        public QuestListUIDisplay()
        {
            Width.Set(100, 0f);
            Height.Set(40, 0f);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Player player = Main.LocalPlayer;

                int maxDisplayed = 6;
            if (QuestWorld.activeQuests.Count < maxDisplayed)
            {
                maxDisplayed = QuestWorld.activeQuests.Count;
                questNum = 0;
            }
            if (maxDisplayed + questNum > QuestWorld.activeQuests.Count - 1)
            {
                questNum--;
            }
            if (questNum < 0) questNum = 0;

            int y = 8;
            CalculatedStyle innerDimensions = GetInnerDimensions();
            float uiX = innerDimensions.X;
            float uiY = innerDimensions.Y;
            int xPadding = 15;
            if (QuestWorld.activeQuests.Count > 0)
            {
                for (int i = questNum; i < questNum + maxDisplayed; i++)
                {
                    if (i < 0 || i > QuestWorld.activeQuests.Count - 1) continue;
                    string name = i + ": " + QuestWorld.activeQuests[i].questName;
                    if (QuestWorld.activeQuests[i] is SpecialQuest quest)
                    {
                        if (quest.thingsNeeded > 0) name += " " + quest.thingsDone + "/" + quest.thingsNeeded;
                    }
                    if (QuestWorld.activeQuests[i] is KillQuest quest1)
                    {
                        if (quest1.killsGotten.Count == 1 && quest1.killsNeeded.Count == 1)
                        {
                            name += " " + quest1.killsGotten[0] + "/" + quest1.killsNeeded[0];
                        }
                    }
                    else if (QuestWorld.activeQuests[i] is PlaceQuest quest2)
                    {
                        if (quest2.tilesNeeded.Count == 1)
                        {
                            int placed = quest2.tilesPlaced[0];
                            int needed = quest2.tilesNeeded[0];
                            name += " " + placed + "/" + needed;
                        }
                    }
                    else if (QuestWorld.activeQuests[i] is DestroyQuest quest3)
                    {
                        if (quest3.tilesNeeded.Count == 1 && quest3.tilesDestroyed.Count == 1)
                        {
                            name += " " + quest3.tilesDestroyed[0] + "/" + quest3.tilesNeeded[0];
                        }
                    }
                    Color color = Color.White;
                    if (QuestWorld.activeQuests[i].completed) color = Color.Yellow;
                    if (QuestWorld.activeQuests[i].claimed) color = Color.Green;
                    if (Main.mouseX > uiX + xPadding && Main.mouseX < uiX + xPadding + Main.fontMouseText.MeasureString(name).X && Main.mouseY > uiY + y && Main.mouseY < uiY + y + Main.fontMouseText.MeasureString(name).Y)
                    {
                        color *= 0.5f;
                        if (Main.mouseLeft && Main.mouseLeftRelease) QuestInfoUIDisplay.questNum = i;
                    }
                    //Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, name, uiX + xPadding, uiY + y, color, Color.Black, new Vector2(0.3f), 1f);
                    float height = 0;
                    EAUtils.DrawSplitSentence(name, uiX + xPadding, uiY + y, Width.Pixels - 20, color, out height);
                    if (QuestWorld.activeQuests[i].identifier.Contains("WithOrder")) spriteBatch.Draw(GetTexture("ElementsAwoken/Extra/OrderQuest"), new Vector2(uiX + xPadding + 10 + Main.fontMouseText.MeasureString(name).X, uiY + y), null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    y += (int)height;
                }
                if (questNum + maxDisplayed < QuestWorld.activeQuests.Count) Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, "More Below", uiX + xPadding, uiY + y, Color.LightYellow, Color.Black, new Vector2(0.3f), 1f);
            }
            else
            {
                Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, "No Current Quests", uiX + xPadding, uiY + y, Color.White, Color.Black, new Vector2(0.3f), 1f);
            }
        }
    }
    public class QuestInfoUIDisplay : UIElement
    {
        public static int questNum = 0;
        public QuestInfoUIDisplay()
        {
            Width.Set(100, 0f);
            Height.Set(40, 0f);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Player player = Main.LocalPlayer;

            CalculatedStyle innerDimensions = GetInnerDimensions();
            float uiX = innerDimensions.X;
            float uiY = innerDimensions.Y;

            if (questNum != -1 && QuestWorld.activeQuests.Count > 0)
            {
                if (questNum > QuestWorld.activeQuests.Count - 1) return;
                Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, QuestWorld.activeQuests[questNum].questName, uiX + 10, uiY + 10, Color.White, Color.Black, new Vector2(0.3f), 1.2f);
                float height = 0;
                if (QuestWorld.activeQuests[questNum].identifier == "Guide9")
                {
                    if (WorldGen.oreTier2 == TileID.Mythril) QuestWorld.activeQuests[questNum].description = "Gather 10 Mythril bars and give them to the Guide";
                    else QuestWorld.activeQuests[questNum].description = "Gather 10 Orichalcum bars and give them to the Guide";
                }
                EAUtils.DrawSplitSentence(QuestWorld.activeQuests[questNum].description, uiX + 10, uiY + 45, Width.Pixels - 20,Color.White, out height);
                if (QuestWorld.activeQuests[questNum] is PlaceQuest quest2)
                {
                    int y = 0;
                    for (int k = 0; k < quest2.tilesPlaced.Count; k++)
                    {
                        int placed = quest2.tilesPlaced[k];
                        int needed = quest2.tilesNeeded[k];
                        string progress = placed + "/" + needed;
                        int textWidth = 0;
                        if (quest2.tileTextures.Count > 0)
                        {
                            if (quest2.tileTextures[k] != "")
                            {
                                Texture2D tex = GetTexture(quest2.tileTextures[k]);
                                int maxHeight = 26;
                                int maxWidth = 26;
                                float scale = Math.Min((float)maxHeight / (float)tex.Height, (float)maxWidth / (float)tex.Width);
                                spriteBatch.Draw(tex, new Vector2(uiX + 10, uiY + 55 + height + y), null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0f);
                                textWidth += (int)(tex.Width * scale + 10);
                            }
                        }
                        Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, progress, uiX + textWidth + 10, uiY + 55 + height + y, Color.White, Color.Black, new Vector2(0.3f), 1f);
                        y += 30;
                    }
                }
                else if (QuestWorld.activeQuests[questNum] is DestroyQuest quest3)
                {
                    int y = 0;
                    for (int k = 0; k < quest3.tilesDestroyed.Count; k++)
                    {
                        int placed = quest3.tilesDestroyed[k];
                        int needed = quest3.tilesNeeded[k];
                        string progress = placed + "/" + needed;
                        int textWidth = 0;

                        if (quest3.tileTextures.Count > 0)
                        {
                            if (quest3.tileTextures[k] != "")
                            {
                                Texture2D tex = GetTexture(quest3.tileTextures[k]);
                                int maxHeight = 26;
                                int maxWidth = 26;
                                float scale = Math.Min((float)maxHeight / (float)tex.Height, (float)maxWidth / (float)tex.Width);
                                spriteBatch.Draw(tex, new Vector2(uiX + 10, uiY + 55 + height + y), null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0f);
                                textWidth += (int)(tex.Width * scale + 10);
                            }
                        }
                        Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, progress, uiX + textWidth + 10, uiY + 55 + height + y, Color.White, Color.Black, new Vector2(0.3f), 1f);
                        y += 30;
                    }
                }
                Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, "[DEBUG]: ID: " + QuestWorld.activeQuests[questNum].identifier, uiX + 10, uiY + Height.Pixels - 30, Color.White, Color.Black, new Vector2(0.3f), 1f);
            }
        }
    }
}*/