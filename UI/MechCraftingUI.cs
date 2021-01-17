using ElementsAwoken.Items.ItemSets.ScarletSteel;
using ElementsAwoken.Items.Tech.Materials;
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
    internal class MechCraftingUI : UIState
    {
        private VanillaItemSlotWrapper[] itemSlots = new VanillaItemSlotWrapper[8];
        private int[] itemsNeeded = new int[8] { 3, 10, 1, 20, 20, 10, 15, 5 };

        private VanillaItemSlotWrapper mechSlot;

        public UIHoverImageButton craftButton;
        public UIHoverImageButton quickFillButton;

        public UIPanel BackPanel;
        public MechUIDisplay UIDisplay;
        public static bool Visible;

        private int uiWidth = 350;
        private int uiHeight = 350;

        public override void OnInitialize()
        {
            BackPanel = new UIPanel();
            BackPanel.SetPadding(0);

            BackPanel.Left.Set(570, 0f);
            BackPanel.Top.Set(20, 0f);
            BackPanel.Width.Set(uiWidth, 0f);
            BackPanel.Height.Set(uiHeight, 0f);
            BackPanel.BackgroundColor = new Color(32, 50, 103);

            float scale = 0.85f;
            int slotWidth = (int)(52 * scale);
            int context = ItemSlot.Context.ChestItem;

            int d1 = 75;
            int d2 = 26;

            // top left
            itemSlots[0] = new VanillaItemSlotWrapper(context, scale, itemsNeeded[0] + "x Capacitor", GetTexture("ElementsAwoken/Extra/Capacitor_Shadow"))
            {
                Left = { Pixels = d1 },
                Top = { Pixels = d2 },
                ValidItemFunc = item => item.IsAir || !item.IsAir && item.type == ItemType<Capacitor>()
            };
            BackPanel.Append(itemSlots[0]);

            itemSlots[1] = new VanillaItemSlotWrapper(context, scale, itemsNeeded[1] + "x Scarlet Steel", GetTexture("ElementsAwoken/Extra/ScarletSteel_Shadow"))
            {
                Left = { Pixels = d2 },
                Top = { Pixels = d1 },
                ValidItemFunc = item => item.IsAir || !item.IsAir && item.type == ItemType<ScarletSteel>()
            };
            BackPanel.Append(itemSlots[1]);
            // top right
            itemSlots[2] = new VanillaItemSlotWrapper(context, scale, itemsNeeded[2] + "x AA Batteries", GetTexture("ElementsAwoken/Extra/AABattery_Shadow"))
            {
                Left = { Pixels = uiWidth - d1 - slotWidth },
                Top = { Pixels = d2 },
                ValidItemFunc = item => item.IsAir || !item.IsAir && item.type == ItemType<Items.Tech.Batteries.AABattery>()
            };
            BackPanel.Append(itemSlots[2]);

            itemSlots[3] = new VanillaItemSlotWrapper(context, scale, itemsNeeded[3] + "x Scarlet Steel", GetTexture("ElementsAwoken/Extra/ScarletSteel_Shadow"))
            {
                Left = { Pixels = uiWidth - d2 - slotWidth},
                Top = { Pixels = d1 },
                ValidItemFunc = item => item.IsAir || !item.IsAir && item.type == ItemType<ScarletSteel>()
            };
            BackPanel.Append(itemSlots[3]);
            // bottom left
            itemSlots[4] = new VanillaItemSlotWrapper(context, scale, itemsNeeded[4] + "x Gold Wire", GetTexture("ElementsAwoken/Extra/GoldWire_Shadow"))
            {
                Left = { Pixels = d1 },
                Top = { Pixels = uiHeight - d2 - slotWidth},
                ValidItemFunc = item => item.IsAir || !item.IsAir && item.type == ItemType<GoldWire>()
            };
            BackPanel.Append(itemSlots[4]);

            itemSlots[5] = new VanillaItemSlotWrapper(context, scale, itemsNeeded[5] + "x Scarlet Steel", GetTexture("ElementsAwoken/Extra/ScarletSteel_Shadow"))
            {
                Left = { Pixels = d2 },
                Top = { Pixels = uiHeight - d1 - slotWidth},
                ValidItemFunc = item => item.IsAir || !item.IsAir && item.type == ItemType<ScarletSteel>()
            };
            BackPanel.Append(itemSlots[5]);
            // bottom right
            itemSlots[6] = new VanillaItemSlotWrapper(context, scale, itemsNeeded[6] + "x CopperWire", GetTexture("ElementsAwoken/Extra/CopperWire_Shadow"))
            {
                Left = { Pixels = uiWidth - d1 - slotWidth},
                Top = { Pixels = uiHeight - d2 - slotWidth},
                ValidItemFunc = item => item.IsAir || !item.IsAir && item.type == ItemType<CopperWire>()
            };
            BackPanel.Append(itemSlots[6]);

            itemSlots[7] = new VanillaItemSlotWrapper(context, scale, itemsNeeded[7] + "x Scarlet Steel", GetTexture("ElementsAwoken/Extra/ScarletSteel_Shadow"))
            {
                Left = { Pixels = uiWidth - d2 - slotWidth},
                Top = { Pixels = uiHeight - d1 - slotWidth},
                ValidItemFunc = item => item.IsAir || !item.IsAir && item.type == ItemType<ScarletSteel>()
            };
            BackPanel.Append(itemSlots[7]);
            // middle
            mechSlot = new VanillaItemSlotWrapper(context, scale)
            {
                Left = { Pixels = uiWidth / 2 - slotWidth / 2 },
                Top = { Pixels = uiHeight / 2 - slotWidth / 2 },
                ValidItemFunc = null,
                noItemInsert = true
            };
            BackPanel.Append(mechSlot);

            int buttonSize = 30;
            craftButton = new UIHoverImageButton(Main.reforgeTexture[0], "Forge");
            craftButton.Left.Set(uiWidth / 2 - buttonSize / 2, 0f);
            craftButton.Top.Set(uiHeight / 2 + slotWidth + 10 - buttonSize / 2, 0f);
            craftButton.Width.Set(buttonSize, 0f);
            craftButton.Height.Set(buttonSize, 0f);
            craftButton.OnClick += new MouseEvent(CraftButtonClicked);
            BackPanel.Append(craftButton);

            buttonSize = 20;
            quickFillButton = new UIHoverImageButton(Main.cursorTextures[7], "Quick Fill");
            quickFillButton.Left.Set(uiWidth / 2 - buttonSize / 2, 0f);
            quickFillButton.Top.Set(uiHeight / 2 - slotWidth - 10 - buttonSize / 2, 0f);
            quickFillButton.Width.Set(buttonSize, 0f);
            quickFillButton.Height.Set(buttonSize, 0f);
            quickFillButton.OnClick += new MouseEvent(QuickFillButtonClicked);
            BackPanel.Append(quickFillButton);

            UIDisplay = new MechUIDisplay();
            UIDisplay.Left.Set(0, 0f);
            UIDisplay.Top.Set(0, 0f);
            UIDisplay.Width.Set(uiWidth, 0f);
            UIDisplay.Height.Set(uiHeight, 1f);
            BackPanel.Append(UIDisplay);
            Append(BackPanel);
        }
        private void QuickFillButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            bool didSomething = false;
            for (int i = 0; i < 58; i++)
            {
                Item item = Main.LocalPlayer.inventory[i];
                if (item.stack > 0)
                {
                    if (item.type == ItemType<ScarletSteel>())
                    {
                        for (int s = 1; s < 8; s += 2)
                        {
                            bool filled = QuickFillSlot(s, item);
                            if (!didSomething) didSomething = filled;
                        }
                    }
                    else if (item.type == ItemType<Capacitor>())
                    {
                        bool filled = QuickFillSlot(0, item);
                        if (!didSomething) didSomething = filled;
                    }
                    else if (item.type == ItemType<Items.Tech.Batteries.AABattery>())
                    {
                        bool filled = QuickFillSlot(2, item);
                        if (!didSomething) didSomething = filled;
                    }
                    else if (item.type == ItemType<GoldWire>())
                    {
                        bool filled = QuickFillSlot(4, item);
                        if (!didSomething) didSomething = filled;
                    }
                    else if (item.type == ItemType<CopperWire>())
                    {
                        bool filled = QuickFillSlot(6, item);
                        if (!didSomething) didSomething = filled;
                    }
                }
            }
            if (didSomething) Main.PlaySound(SoundID.Grab);
        }
        private bool QuickFillSlot(int slotNum, Item item)
        {
            int amountNeeded = itemsNeeded[slotNum] - itemSlots[slotNum].Item.stack;
            if (item.stack >= itemsNeeded[slotNum])
            {
                if (itemSlots[slotNum].Item.stack < itemsNeeded[slotNum])
                {
                    item.stack -= amountNeeded;

                    if (itemSlots[slotNum].Item.stack <= slotNum) itemSlots[slotNum].Item.SetDefaults(item.type);
                    itemSlots[slotNum].Item.stack = itemsNeeded[slotNum];
                    return true;
                }
            }
            else
            {
                if (itemSlots[slotNum].Item.stack < itemsNeeded[slotNum])
                {
                    int sub = slotNum;
                    if (itemSlots[slotNum].Item.stack <= slotNum)
                    {
                        itemSlots[slotNum].Item.SetDefaults(item.type);
                        sub = 1;
                    }
                    itemSlots[slotNum].Item.stack += item.stack - sub;
                    item.TurnToAir();
                    return true;
                }
            }
            return false;
        }
        private void CraftButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            bool hasMats = true;
            for (int i = 0; i <= 7; i++)
            {
                if (itemSlots[i].Item.stack < itemsNeeded[i])
                {
                    hasMats = false;
                    break;
                }
            }
            if (mechSlot.Item.IsAir && hasMats)
            {
                Main.PlaySound(2, -1, -1, 37);
                Item item = new Item();
                item.SetDefaults(ItemType<Items.Testing.MechSuitTest>());
                mechSlot.Item = item;
                for (int i = 0; i <= 7; i++)
                {
                    itemSlots[i].Item.stack -= itemsNeeded[i];
                    if (itemSlots[i].Item.stack <= 0) itemSlots[i].Item.TurnToAir();
                }
            }
            if (!hasMats) Main.PlaySound(SoundID.Dig, -1, -1, 1, 1, -0.5f);
        }
        public override void Update(GameTime gameTime)
        {
            if (!Main.playerInventory) Visible = false;
            if (Vector2.Distance(Main.LocalPlayer.Center / 16, Main.LocalPlayer.GetModPlayer<MyPlayer>().currentMechStation) > Player.tileRangeX)
            {
                Visible = false;
                Main.PlaySound(SoundID.MenuClose);
            }
            base.Update(gameTime);
            itemSlots[2].draw = MyWorld.mechBlueprints > 1;
            itemSlots[3].draw = MyWorld.mechBlueprints > 1;
            itemSlots[4].draw = MyWorld.mechBlueprints > 2;
            itemSlots[5].draw = MyWorld.mechBlueprints > 2;
            itemSlots[6].draw = MyWorld.mechBlueprints > 3;
            itemSlots[7].draw = MyWorld.mechBlueprints > 3;  
        }
    }
    public class MechUIDisplay : UIElement
    {
        public MechUIDisplay()
        {
            Width.Set(100, 0f);
            Height.Set(40, 0f);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle innerDimensions = GetInnerDimensions();

            float uiX = innerDimensions.X;
            float uiY = innerDimensions.Y;
            for (int i = 0; i < MyWorld.mechBlueprints; i++)
            {
                if (i > 3) break;
                spriteBatch.Draw(GetTexture("ElementsAwoken/Extra/MechBlueprint" + i), new Vector2(uiX, uiY), null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}