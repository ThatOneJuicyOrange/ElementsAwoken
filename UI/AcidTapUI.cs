using ElementsAwoken.Tiles.VolcanicPlateau.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.UI
{
    internal class AcidTapUI : UIState
    {
        public DragableUIPanel BackPanel;
        private VanillaItemSlotWrapper itemSlot;
        public TapDisplay UIDisplay;
        public static bool Visible;

        private int uiWidth = 150;
        private int uiHeight = 150;

        public static int tapX = 0;
        public static int tapY = 0;

        private int prevAmount = 0;

        public override void OnInitialize()
        {
            BackPanel = new DragableUIPanel();
            BackPanel.SetPadding(0);

            BackPanel.Left.Set(Main.screenWidth / 2 - uiWidth / 2, 0f);
            BackPanel.Top.Set(Main.screenHeight / 2 - uiHeight / 2 - 100, 0f);
            BackPanel.Width.Set(uiWidth, 0f);
            BackPanel.Height.Set(uiHeight, 0f);
            BackPanel.BackgroundColor = new Color(73, 94, 171);

            itemSlot = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 0.85f)
            {
                Left = { Pixels = uiWidth / 2 - 16 },
                Top = { Pixels = uiHeight - 64 },
                ValidItemFunc = item => item.IsAir
            };
            BackPanel.Append(itemSlot);

            UIDisplay = new TapDisplay();
            UIDisplay.Left.Set(0, 0f);
            UIDisplay.Top.Set(0, 0f);
            UIDisplay.Width.Set(uiWidth, 0f);
            UIDisplay.Height.Set(uiHeight, 1f);
            BackPanel.Append(UIDisplay);
            Append(BackPanel);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (tapX != 0 && tapY != 0)
            {                       
                AcidTapEntity tap = null;

                foreach (TileEntity current in TileEntity.ByID.Values)
                {
                    if (current.type == TileEntityType<AcidTapEntity>())
                    {
                        if (current.Position == new Point16(tapX,tapY))
                        {
                            tap = (AcidTapEntity)current;
                        }
                    }
                }
                if (tap != null)
                {
                    if (prevAmount > itemSlot.Item.stack)
                    {
                        tap.acidAmount = itemSlot.Item.stack;
                        ElementsAwoken.DebugModeText("setting tile item stack to: " + itemSlot.Item.stack);
                    }
                    if (tap.acidAmount != itemSlot.Item.stack)
                    {
                        if (tap.acidAmount > 0)
                        {
                            if (itemSlot.Item.stack == 0) itemSlot.Item.SetDefaults(ItemType<Items.Materials.AcidDrop>());
                            itemSlot.Item.stack = tap.acidAmount;
                            ElementsAwoken.DebugModeText("setting ui item stack to: " + tap.acidAmount);
                        }
                        else
                        {
                            itemSlot.Item.TurnToAir();
                            ElementsAwoken.DebugModeText("clearing ui item stack");
                        }
                    }
                    prevAmount = itemSlot.Item.stack;
                }
                Point playerWorld = Main.LocalPlayer.Center.ToTileCoordinates();
                if (playerWorld.X < tapX - Player.tileRangeX || playerWorld.X > tapX + Player.tileRangeX + 1 || playerWorld.Y < tapY - Player.tileRangeY || playerWorld.Y > tapY + Player.tileRangeY + 1)
                {
                    Visible = false;
                    Main.PlaySound(SoundID.MenuClose, new Vector2(-1, -1));
                }
            }
        }
    }
    public class TapDisplay : UIElement
    {
        public TapDisplay()
        {
            Width.Set(100, 0f);
            Height.Set(40, 0f);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle innerDimensions = GetInnerDimensions();
            float uiX = innerDimensions.X;
            float uiY = innerDimensions.Y;

            float titleScale = 0.64f;
            string title = "Acid Tap";
            Vector2 titleSize = Main.fontDeathText.MeasureString(title) * titleScale;


            float progressScale = 1f;
            string progress = "Error";

            if (AcidTapUI.tapX != 0 && AcidTapUI.tapY != 0)
            {
                AcidTapEntity tap = null;

                foreach (TileEntity current in TileEntity.ByID.Values)
                {
                    if (current.type == TileEntityType<AcidTapEntity>())
                    {
                        if (current.Position == new Point16(AcidTapUI.tapX, AcidTapUI.tapY))
                        {
                            tap = (AcidTapEntity)current;
                        }
                    }
                }
                if (tap != null)
                {
                    if (tap.acidAmount >= tap.acidMax)
                    {
                        progress = "Full";
                    }
                    else
                    {
                        progress = string.Format("{0:0}", ((float)tap.succTimer / (float)tap.delay) * 100);
                        progress = "Progress: " + progress + "%";
                    }
                }
            }

            Vector2 progressSize = Main.fontMouseText.MeasureString(progress) * progressScale;

            Utils.DrawBorderStringFourWay(spriteBatch, Main.fontDeathText, title, uiX + Width.Pixels / 2 - titleSize.X / 2, uiY + 13, Color.White, Color.Black, new Vector2(0.3f), titleScale);
            Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, progress, uiX + Width.Pixels / 2 - progressSize.X / 2, uiY + 60, Color.White, Color.Black, new Vector2(0.3f), progressScale);


        }
    }
}