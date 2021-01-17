using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.UI.Elements;
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
        public static QuestListUIDisplay MainDisplay;
        public static DragableScrollBar scrollLeft;
        public static DragableScrollBar scrollRight;

        public UIHoverImageButton UpButton;
        public UIHoverImageButton DownButton;
        public static bool Visible;

        private int uiWidth = 540;
        private int uiHeight = 340;
        public override void OnInitialize()
        {
            MainDisplay = new QuestListUIDisplay();
            MainDisplay.Left.Set(0, 0f);
            MainDisplay.Top.Set(0, 0f);
            MainDisplay.Width.Set(uiWidth, 0f);
            MainDisplay.Height.Set(uiHeight, 1f);

            int posX = Main.screenWidth / 2 - uiWidth / 2;
            int posY = Main.screenHeight / 2 - uiHeight - 30;

            scrollLeft = new DragableScrollBar();
            scrollLeft.SetView(100f, 1000f);
            scrollLeft.Left.Set(uiWidth / 2 - 30, 0f);
            scrollLeft.Top.Set(30f,0f);
            scrollLeft.Height.Set(uiHeight * 0.75f, 0f);
            MainDisplay.Append(scrollLeft);

            /*scrollRight = new DragableScrollBar();
            scrollRight.SetView(100f, 1000f);
            scrollRight.Left.Set(uiWidth - 36, 0f);
            scrollRight.Top.Set(30f, 0f);
            scrollRight.Height.Set(uiHeight * 0.75f, 0f);
            MainDisplay.Append(scrollRight);*/

            MainDisplay.Left.Set(posX, 0f);
            MainDisplay.Top.Set(posY, 0f);
            MainDisplay.Width.Set(uiWidth, 0f);
            MainDisplay.Height.Set(uiHeight, 0f);
            Append(MainDisplay);
        }
    }
    public class QuestListUIDisplay : DragableUI
    {
        public int questNum = 0;
        public int questSelected = 0;
        public float scrollMax = 100;
        public QuestListUIDisplay()
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
            float pageWidth = 260;
            int xPadding = 12;


            Texture2D pages = GetTexture("ElementsAwoken/Extra/QuestLog");
            spriteBatch.Draw(pages, new Vector2(uiX, uiY), Color.White);
            uiX += 12;
            uiY += 16;

            {
                float barSize = 100;
                int maxLines = 12;
                int distBetweenLines = 20;

                float maxHeight = distBetweenLines * maxLines;

                float value = scrollMax - barSize;
                if (value == 0) value = 1;
                int drawLine = (int)((QuestWorld.activeQuests.Count - maxLines) * (QuestListUI.scrollLeft.ViewPosition / value));

                float y = 0;

              
                if (QuestWorld.activeQuests.Count > 0)
                {
                    int i = drawLine;
                    float combinedHeight = 0;
                    for (int m = 0; m < QuestWorld.activeQuests.Count - 1; m++)
                    {
                        string name = m + ": " + QuestWorld.activeQuests[m].questName;
                        name += TitleProgress(m);
                        //combinedHeight += distBetweenLines * EAUtils.MeasureSplitSentenceLines(name, pageWidth - 20);
                        combinedHeight += EAUtils.MeasureSplitSentenceHeight(name, pageWidth - 20);
                    }
                    while (y < maxHeight && i <= QuestWorld.activeQuests.Count - 1)
                    {
                        drawLine++;
                        if (i < 0 || i > QuestWorld.activeQuests.Count - 1) continue;

                        Texture2D questIcon = QuestWorld.activeQuests[i].questIcon;
                        if (QuestWorld.activeQuests[i].identifier.Contains("WithOrder")) questIcon = GetTexture("ElementsAwoken/Extra/Quests/QuestIconOrder");
                        else if (QuestWorld.activeQuests[i].identifier.Contains("AgainstOrder")) questIcon = GetTexture("ElementsAwoken/Extra/Quests/QuestIconAgainstOrder");
                        else if (QuestWorld.activeQuests[i].identifier.Contains("Guide")) questIcon = Main.npcHeadTexture[1];
                        else if (QuestWorld.activeQuests[i].identifier.Contains("Merchant")) questIcon = Main.npcHeadTexture[2];
                        else if (QuestWorld.activeQuests[i].identifier.Contains("Nurse")) questIcon = Main.npcHeadTexture[3];
                        if (questIcon != null)
                        {
                            int maxIconHeight = 26;
                            int maxIconWidth = 26;
                            float scale = Math.Min((float)maxIconHeight / (float)questIcon.Height, (float)maxIconWidth / (float)questIcon.Width);
                            spriteBatch.Draw(questIcon, new Vector2(uiX + xPadding, uiY + y), null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0f);
                        }
                        int xOff = 32;

                        string name = QuestWorld.activeQuests[i].questName;
                        name += TitleProgress(i);
                        Color color = Color.White;
                        if (QuestWorld.activeQuests[i].completed) color = Color.Yellow;
                        if (QuestWorld.activeQuests[i].claimed) color = Color.Green;
                        if (Main.mouseX > uiX + xOff + xPadding && Main.mouseX < uiX + xOff + xPadding + Main.fontMouseText.MeasureString(name).X && Main.mouseY > uiY + y && Main.mouseY < uiY + y + Main.fontMouseText.MeasureString(name).Y)
                        {
                            color *= 0.5f;
                            if (Main.mouseLeft && Main.mouseLeftRelease) questSelected = i;
                        }
                        //Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, name, uiX + xPadding, uiY + y, color, Color.Black, new Vector2(0.3f), 1f);
                        float height = 0;
                        EAUtils.DrawSplitSentence(name, uiX + xPadding + xOff, uiY + y, pageWidth - 20, color, out height);

                        y += (int)height;
                        i++;
                    }
                    scrollMax = 100 * (combinedHeight / maxHeight);
                    QuestListUI.scrollLeft.SetView(barSize, scrollMax);
                    if (scrollMax <= 100) QuestListUI.scrollLeft.Visible = false;
                    else QuestListUI.scrollLeft.Visible = true;
                    //if (questNum + maxDisplayed < QuestWorld.activeQuests.Count) Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, "More Below", uiX + xPadding, uiY + y, Color.LightYellow, Color.Black, new Vector2(0.3f), 1f);
                }
                else
                {
                    Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, "No Current Quests", uiX + xPadding, uiY + y, Color.White, Color.Black, new Vector2(0.3f), 1f);
                }
                
            }

            // description on right
            uiX += 250;
            if (questSelected != -1 && QuestWorld.activeQuests.Count > 0)
            {
                if (questSelected > QuestWorld.activeQuests.Count - 1) return;
                Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, QuestWorld.activeQuests[questSelected].questName, uiX + xPadding + 10, uiY, Color.White, Color.Black, new Vector2(0.3f), 1.2f);
                float height = 0;
                if (QuestWorld.activeQuests[questSelected].identifier == "Guide9")
                {
                    if (WorldGen.oreTier2 == TileID.Mythril) QuestWorld.activeQuests[questSelected].description = "Gather 10 Mythril bars and give them to the Guide";
                    else QuestWorld.activeQuests[questSelected].description = "Gather 10 Orichalcum bars and give them to the Guide";
                }
                EAUtils.DrawSplitSentence(QuestWorld.activeQuests[questSelected].description, uiX + xPadding + 10, uiY + 45, 220, Color.White, out height);
                if (QuestWorld.activeQuests[questSelected] is PlaceQuest quest2)
                {
                    int y2 = 0;
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
                                spriteBatch.Draw(tex, new Vector2(uiX + xPadding + 10, uiY + 55 + height + y2), null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0f);
                                textWidth += (int)(tex.Width * scale + 10);
                            }
                        }
                        Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, progress, uiX + xPadding + textWidth + 10, uiY + 55 + height + y2, Color.White, Color.Black, new Vector2(0.3f), 1f);
                        y2 += 30;
                    }
                }
                else if (QuestWorld.activeQuests[questSelected] is DestroyQuest quest3)
                {
                    int y2 = 0;
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
                                spriteBatch.Draw(tex, new Vector2(uiX + xPadding + 10, uiY + 55 + height + y2), null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0f);
                                textWidth += (int)(tex.Width * scale + 10);
                            }
                        }
                        Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, progress, uiX + xPadding + textWidth + 10, uiY + 55 + height + y2, Color.White, Color.Black, new Vector2(0.3f), 1f);
                        y2 += 30;
                    }
                }
                Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, "[DEBUG]: ID: " + QuestWorld.activeQuests[questSelected].identifier, uiX + 10 + xPadding, uiY + Height.Pixels - 70, Color.White, Color.Black, new Vector2(0.3f), 1f);
            }
        }
        private static string TitleProgress(int activeQuestID)
        {
            string add = "";
            if (QuestWorld.activeQuests[activeQuestID] is SpecialQuest quest)
            {
                if (quest.thingsNeeded > 0) add = " " + quest.thingsDone + "/" + quest.thingsNeeded;
            }
            if (QuestWorld.activeQuests[activeQuestID] is KillQuest quest1)
            {
                if (quest1.killsGotten.Count == 1 && quest1.killsNeeded.Count == 1)
                {
                    add = " " + quest1.killsGotten[0] + "/" + quest1.killsNeeded[0];
                }
            }
            else if (QuestWorld.activeQuests[activeQuestID] is PlaceQuest quest2)
            {
                if (quest2.tilesNeeded.Count == 1)
                {
                    int placed = quest2.tilesPlaced[0];
                    int needed = quest2.tilesNeeded[0];
                    add = " " + placed + "/" + needed;
                }
            }
            else if (QuestWorld.activeQuests[activeQuestID] is DestroyQuest quest3)
            {
                if (quest3.tilesNeeded.Count == 1 && quest3.tilesDestroyed.Count == 1)
                {
                    add = " " + quest3.tilesDestroyed[0] + "/" + quest3.tilesNeeded[0];
                }
            }
            return add;
        }
    }
}