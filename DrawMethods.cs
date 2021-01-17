using ElementsAwoken.Effects;
using ElementsAwoken.Items.Tech.Weapons.Tier6;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System.IO;
using System.Reflection;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace ElementsAwoken
{
    public class DrawMethods
    {
        public static void DrawHearts()
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");

            Player player = Main.player[Main.myPlayer];
            var modPlayer = player.GetModPlayer<MyPlayer>();
            float lifePerHeart = 10f;
            if (Main.LocalPlayer.ghost)
            {
                return;
            }

            int lifeForHeart = player.statLifeMax / 20;

            int voidHeartLife = modPlayer.voidHeartsUsed * 2; // multiply by however many hearts you want it to make when you use one
            if (voidHeartLife < 0)
            {
                voidHeartLife = 0;
            }
            if (voidHeartLife > 0)
            {
                lifeForHeart = player.statLifeMax / (20 + voidHeartLife / 4);
                lifePerHeart = player.statLifeMax / 20f;
            }

            int chaosHeartLife = modPlayer.chaosHeartsUsed * 2;
            if (chaosHeartLife < 0)
            {
                chaosHeartLife = 0;
            }
            if (chaosHeartLife > 0)
            {
                lifeForHeart = player.statLifeMax / (20 + chaosHeartLife / 4);
                lifePerHeart = player.statLifeMax / 20f;
            }
            // statLifeMax2 is the actual player life, it equals statLifeMax plus bonuses
            int playerLife = player.statLifeMax2 - player.statLifeMax;
            if (lifeForHeart == 0) lifeForHeart = 1; // shouldnt happen but failsafe
            lifePerHeart += playerLife / lifeForHeart;

            var info = typeof(Main).GetField("UI_ScreenAnchorX", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            int screenAnchorX = (int)info.GetValue(null);

            for (int heartNum = 1; heartNum < (int)(player.statLifeMax2 / lifePerHeart) + 1; heartNum++)
            {
                float scale = 1f;
                bool lastHeart = false;
                int lifeStat;
                if (player.statLife >= heartNum * lifePerHeart)
                {
                    lifeStat = 255;
                    if (player.statLife == heartNum * lifePerHeart)
                    {
                        lastHeart = true;
                    }
                }
                else
                {
                    float num7 = (player.statLife - (heartNum - 1) * lifePerHeart) / lifePerHeart;
                    lifeStat = (int)(30f + 225f * num7);
                    if (lifeStat < 30)
                    {
                        lifeStat = 30;
                    }
                    scale = num7 / 4f + 0.75f;
                    if (scale < 0.75)
                    {
                        scale = 0.75f;
                    }
                    if (num7 > 0f)
                    {
                        lastHeart = true;
                    }
                }
                if (lastHeart)
                {
                    scale += Main.cursorScale - 1f;
                }
                int xPos = 0;
                int yPos = 0;
                if (heartNum > 10)
                {
                    xPos -= 260;
                    yPos += 26;
                }
                if (heartNum > 20)
                {
                    xPos = 0;
                    yPos = 0;
                }
                int a = (int)(lifeStat * 0.9f);
                if (!player.ghost)
                {
                    if (chaosHeartLife > 0)
                    {
                        chaosHeartLife--;
                        Texture2D heart4Texture = mod.GetTexture("Extra/Heart4");
                        Main.spriteBatch.Draw(heart4Texture, new Vector2((float)(500 + 26 * (heartNum - 1) + xPos + screenAnchorX + Main.heartTexture.Width / 2), 32f + ((float)Main.heartTexture.Height - (float)Main.heartTexture.Height * scale) / 2f + (float)yPos + (float)(Main.heartTexture.Height / 2)), new Rectangle?(new Rectangle(0, 0, Main.heartTexture.Width, Main.heartTexture.Height)), new Color(lifeStat, lifeStat, lifeStat, a), 0f, new Vector2((float)(Main.heartTexture.Width / 2), (float)(Main.heartTexture.Height / 2)), scale, SpriteEffects.None, 0f);
                    }
                    else if (voidHeartLife > 0)
                    {
                        voidHeartLife--;
                        Texture2D heart3Texture = mod.GetTexture("Extra/Heart3");
                        if (modPlayer.voidCompressor)
                        {
                            heart3Texture = mod.GetTexture("Extra/Heart3Alt");
                        }
                        else
                        {
                            heart3Texture = mod.GetTexture("Extra/Heart3");
                        }
                        Main.spriteBatch.Draw(heart3Texture, new Vector2((float)(500 + 26 * (heartNum - 1) + xPos + screenAnchorX + Main.heartTexture.Width / 2), 32f + ((float)Main.heartTexture.Height - (float)Main.heartTexture.Height * scale) / 2f + (float)yPos + (float)(Main.heartTexture.Height / 2)), new Rectangle?(new Rectangle(0, 0, Main.heartTexture.Width, Main.heartTexture.Height)), new Color(lifeStat, lifeStat, lifeStat, a), 0f, new Vector2((float)(Main.heartTexture.Width / 2), (float)(Main.heartTexture.Height / 2)), scale, SpriteEffects.None, 0f);
                        //Main.spriteBatch.Draw(heart3Texture, new Vector2((float)(500 + 26 * (heartNum - 1) + xPos + screenAnchorX + heart3Texture.Width / 2), 32f + ((float)Main.heartTexture.Height - (float)Main.heartTexture.Height * scale) / 2f + (float)yPos + (float)(Main.heartTexture.Height / 2)), new Rectangle?(new Rectangle(0, 0, heart3Texture.Width, heart3Texture.Height)), new Color(lifeStat, lifeStat, lifeStat, a), 0.0f, new Vector2((float)(heart3Texture.Width / 2), (float)(heart3Texture.Height / 2)), scale, SpriteEffects.None, 0.0f);
                    }
                }
            }
        }

        public static void DrawSpirit()
        {
            Player player = Main.LocalPlayer;
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            var info = typeof(Main).GetField("UI_ScreenAnchorX", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            int screenAnchorX = (int)info.GetValue(null);

            int spiritPerStar = 10;

            int arg_3D_0 = modPlayer.spiritMax / 20;
            string text = "Spirit";
            Vector2 vector = Main.fontMouseText.MeasureString(text);
            int num = 50;
            if (vector.X >= 45f)
            {
                num = (int)vector.X + 5;
            }
            DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, Main.fontMouseText, text, new Vector2((float)(800 - num + screenAnchorX), 6f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
            for (int i = 1; i < modPlayer.spiritMax / spiritPerStar + 1; i++)
            {
                bool flag = false;
                float num2 = 1f;
                int num3;
                if (modPlayer.spirit >= i * spiritPerStar)
                {
                    num3 = 255;
                    if (modPlayer.spirit == i * spiritPerStar)
                    {
                        flag = true;
                    }
                }
                else
                {
                    float num4 = (float)(modPlayer.spirit - (i - 1) * spiritPerStar) / (float)spiritPerStar;
                    num3 = (int)(30f + 225f * num4);
                    if (num3 < 30)
                    {
                        num3 = 30;
                    }
                    num2 = num4 / 4f + 0.75f;
                    if ((double)num2 < 0.75)
                    {
                        num2 = 0.75f;
                    }
                    if (num4 > 0f)
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    num2 += Main.cursorScale - 1f;
                }
                int a = (int)((double)((float)num3) * 0.9);
                Texture2D spiritTexture = ModContent.GetTexture("ElementsAwoken/Extra/Spirit");
                Main.spriteBatch.Draw(spiritTexture, new Vector2((float)(775 + screenAnchorX), (float)(30 + spiritTexture.Height / 2) + ((float)spiritTexture.Height - (float)spiritTexture.Height * num2) / 2f + (float)(28 * (i - 1))), new Rectangle?(new Rectangle(0, 0, spiritTexture.Width, spiritTexture.Height)), new Color(num3, num3, num3, a), 0f, new Vector2((float)(spiritTexture.Width / 2), (float)(spiritTexture.Height / 2)), num2, SpriteEffects.None, 0f);
            }

            if (!Main.mouseText)
            {
                int num1 = 24;
                int num2 = 28 * modPlayer.spiritMax / spiritPerStar;
                if (Main.mouseX > 762 + screenAnchorX && Main.mouseX < 762 + num1 + screenAnchorX && Main.mouseY > 30 && Main.mouseY < 30 + num2)
                {
                    Main.player[Main.myPlayer].showItemIcon = false;
                    string mouseText = modPlayer.spirit + "/" + modPlayer.spiritMax;
                    Vector2 drawPos = new Vector2(Main.mouseX + 17, Main.mouseY + 17);
                    float strWidth = Main.fontMouseText.MeasureString(mouseText).X;
                    if (drawPos.X + strWidth > Main.screenWidth) drawPos.X = Main.screenWidth - strWidth - 10;
                    ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, mouseText, drawPos, new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
                }
            }
        }

        public static void DrawVoidBloodGlow()
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");

            Player player = Main.player[Main.myPlayer];
            float lifePerHeart = 20f;
            if (player.ghost)
            {
                return;
            }

            int num = player.statLifeMax / 20;
            int num2 = (player.statLifeMax - 400) / 5;
            if (num2 < 0)
            {
                num2 = 0;
            }
            if (num2 > 0)
            {
                num = player.statLifeMax / (20 + num2 / 4);
                lifePerHeart = (float)player.statLifeMax / 20f;
            }
            int num3 = player.statLifeMax2 - player.statLifeMax;
            lifePerHeart += (float)(num3 / num);

            var info = typeof(Main).GetField("UI_ScreenAnchorX", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            int screenAnchorX = (int)info.GetValue(null);

            for (int heartNum = 1; heartNum < (int)(player.statLifeMax2 / lifePerHeart) + 1; heartNum++)
            {
                float scale = 1f;
                bool lastHeart = false;
                int lifeStat;
                if (player.statLife >= heartNum * lifePerHeart)
                {
                    lifeStat = 255;
                    if (player.statLife == heartNum * lifePerHeart)
                    {
                        lastHeart = true;
                    }
                }
                else
                {
                    float num7 = (player.statLife - (heartNum - 1) * lifePerHeart) / lifePerHeart;
                    lifeStat = (int)(30f + 225f * num7);
                    if (lifeStat < 30)
                    {
                        lifeStat = 30;
                    }
                    scale = num7 / 4f + 0.75f;
                    if (scale < 0.75)
                    {
                        scale = 0.75f;
                    }
                    if (num7 > 0f)
                    {
                        lastHeart = true;
                    }
                }
                if (lastHeart)
                {
                    scale += Main.cursorScale - 1f;
                }
                int xPos = 0;
                int yPos = 0;
                if (heartNum > 10)
                {
                    xPos -= 260;
                    yPos += 26;
                }
                if (heartNum > 20)
                {
                    xPos = 0;
                    yPos = 0;
                }
                xPos -= (ElementsAwoken.heartGlowTex.Width - Main.heartTexture.Width) / 2;
                yPos -= (ElementsAwoken.heartGlowTex.Height - Main.heartTexture.Height) / 2;

                int a = (int)(lifeStat * 0.9f);
                if (!player.ghost)
                {
                    Main.spriteBatch.Draw(ElementsAwoken.heartGlowTex, new Vector2((float)(500 + 26 * (heartNum - 1) + xPos + screenAnchorX + ElementsAwoken.heartGlowTex.Width / 2), 32f + ((float)ElementsAwoken.heartGlowTex.Height - (float)ElementsAwoken.heartGlowTex.Height * scale) / 2f + (float)yPos + (float)(ElementsAwoken.heartGlowTex.Height / 2)), new Rectangle?(new Rectangle(0, 0, ElementsAwoken.heartGlowTex.Width, ElementsAwoken.heartGlowTex.Height)), new Color(lifeStat, lifeStat, lifeStat, a), 0f, new Vector2((float)(ElementsAwoken.heartGlowTex.Width / 2), (float)(ElementsAwoken.heartGlowTex.Height / 2)), scale, SpriteEffects.None, 0f);
                }
            }
        }

        public static void DrawShield()
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");

            Player player = Main.player[Main.myPlayer];
            var modPlayer = player.GetModPlayer<MyPlayer>();
            float lifePerHeart = 5f;
            if (Main.LocalPlayer.ghost)
            {
                return;
            }

            int lifeForHeart = player.statLifeMax / 20;

            int shieldLife = modPlayer.shieldHearts; // multiply by however many hearts you want it to make when you use one
            if (shieldLife < 0)
            {
                shieldLife = 0;
            }
            if (shieldLife > 0)
            {
                lifeForHeart = player.statLifeMax / (20 + shieldLife / 4);
                lifePerHeart = player.statLifeMax / 20f;
            }

            // statLifeMax2 is the actual player life, it equals statLifeMax plus bonuses
            int playerLife = modPlayer.shieldHearts * 5;
            lifePerHeart += playerLife / lifeForHeart;

            var info = typeof(Main).GetField("UI_ScreenAnchorX", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            int screenAnchorX = (int)info.GetValue(null);

            for (int heartNum = 1; heartNum < (int)(player.statLifeMax2 / lifePerHeart) + 1; heartNum++)
            {
                float scale = 1f;
                bool lastHeart = false;
                int lifeStat;
                if (player.statLife >= heartNum * lifePerHeart)
                {
                    lifeStat = 255;
                    if (player.statLife == heartNum * lifePerHeart)
                    {
                        lastHeart = true;
                    }
                }
                else
                {
                    float num7 = (player.statLife - (heartNum - 1) * lifePerHeart) / lifePerHeart;
                    lifeStat = (int)(30f + 225f * num7);
                    if (lifeStat < 30)
                    {
                        lifeStat = 30;
                    }
                    scale = num7 / 4f + 0.75f;
                    if (scale < 0.75)
                    {
                        scale = 0.75f;
                    }
                    if (num7 > 0f)
                    {
                        lastHeart = true;
                    }
                }
                if (lastHeart)
                {
                    scale += Main.cursorScale - 1f;
                }
                int xPos = 0;
                int yPos = 0;
                if (heartNum > 10)
                {
                    xPos -= 260;
                    yPos += 26;
                }
                if (heartNum > 20)
                {
                    xPos = 0;
                    yPos = 0;
                }
                int a = (int)(lifeStat * 0.9f);
                if (!player.ghost)
                {
                    if (shieldLife > 0)
                    {
                        shieldLife--;
                        Texture2D heart4Texture = mod.GetTexture("Extra/ShieldHeart");
                        Main.spriteBatch.Draw(heart4Texture, new Vector2((float)(500 + 26 * (heartNum - 1) + xPos + screenAnchorX + Main.heartTexture.Width / 2), 32f + ((float)Main.heartTexture.Height - (float)Main.heartTexture.Height * scale) / 2f + (float)yPos + (float)(Main.heartTexture.Height / 2)), new Rectangle?(new Rectangle(0, 0, Main.heartTexture.Width, Main.heartTexture.Height)), new Color(lifeStat, lifeStat, lifeStat, a), 0f, new Vector2((float)(Main.heartTexture.Width / 2), (float)(Main.heartTexture.Height / 2)), scale, SpriteEffects.None, 0f);
                    }
                }
            }
        }

        public static void DrawSulphurBreath()
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            Player player = Main.LocalPlayer;
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            bool flag = false;
            if (player.dead || player.ghost) return;
            Vector2 value = Main.player[Main.myPlayer].Top + new Vector2(0f, Main.player[Main.myPlayer].gfxOffY);
            if (Main.playerInventory && Main.screenHeight < 1000)
            {
                value.Y += (float)(Main.player[Main.myPlayer].height - 20);
            }
            value = Vector2.Transform(value - Main.screenPosition, Main.GameViewMatrix.ZoomMatrix);
            if (!Main.playerInventory || Main.screenHeight >= 1000)
            {
                value.Y -= 100f;
            }
            value /= Main.UIScale;
            if (Main.ingameOptionsWindow || Main.InGameUI.IsVisible)
            {
                value = new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2 + 236));
                if (Main.InGameUI.IsVisible)
                {
                    value.Y = (float)(Main.screenHeight - 64);
                }
            }
            value.Y -= Main.bubbleTexture.Height * 1.2f;
            if (modPlayer.sulphurBreath > 0 && !flag)
            {
                int numIcons = 20;
                for (int i = 1; i < modPlayer.sulphurBreathMax / numIcons + 1; i++)
                {
                    float num2 = 1f;
                    int num3;
                    if (modPlayer.sulphurBreath >= i * numIcons)
                    {
                        num3 = 255;
                    }
                    else
                    {
                        float num4 = (float)(modPlayer.sulphurBreath - (i - 1) * numIcons) / (float)numIcons;
                        num3 = (int)(30f + 225f * num4);
                        if (num3 < 30)
                        {
                            num3 = 30;
                        }
                        num2 = num4 / 4f + 0.75f;
                        if ((double)num2 < 0.75)
                        {
                            num2 = 0.75f;
                        }
                    }
                    int num5 = 0;
                    int num6 = 0;
                    if (i > 10)
                    {
                        num5 -= 260;
                        num6 += 26;
                    }
                    Texture2D breathTex = mod.GetTexture("Extra/SulphurBreath");
                    Main.spriteBatch.Draw(breathTex, value + new Vector2((float)(26 * (i - 1) + num5) - 125f, 32f + ((float)breathTex.Height - (float)breathTex.Height * num2) / 2f + (float)num6), new Rectangle?(new Rectangle(0, 0, breathTex.Width, breathTex.Height)), new Color(num3, num3, num3, num3), 0f, default(Vector2), num2, SpriteEffects.None, 0f);
                }
            }
        }
        public static void DrawCriticalHeatBar()
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            Player player = Main.LocalPlayer;
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            bool flag = false;
            if (player.dead || player.ghost || modPlayer.sulphurBreath > 0) return;
            Vector2 value = Main.player[Main.myPlayer].Top + new Vector2(0f, Main.player[Main.myPlayer].gfxOffY);
            if (Main.playerInventory && Main.screenHeight < 1000)
            {
                value.Y += (float)(Main.player[Main.myPlayer].height - 20);
            }
            value = Vector2.Transform(value - Main.screenPosition, Main.GameViewMatrix.ZoomMatrix);
            if (!Main.playerInventory || Main.screenHeight >= 1000)
            {
                value.Y -= 100f;
            }
            value /= Main.UIScale;
            if (Main.ingameOptionsWindow || Main.InGameUI.IsVisible)
            {
                value = new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2 + 236));
                if (Main.InGameUI.IsVisible)
                {
                    value.Y = (float)(Main.screenHeight - 64);
                }
            }
            value.Y -= Main.bubbleTexture.Height * 1.2f;
            if (modPlayer.criticalHeatTimer > 0 && !flag)
            {
                int numIcons = modPlayer.criticalHeatMax / 10;
                for (int i = 1; i < modPlayer.criticalHeatMax / numIcons + 1; i++)
                {
                    float num2 = 1f;
                    int num3;
                    if (modPlayer.criticalHeatTimer >= i * numIcons)
                    {
                        num3 = 255;
                    }
                    else
                    {
                        float num4 = (float)(modPlayer.criticalHeatTimer - (i - 1) * numIcons) / (float)numIcons;
                        num3 = (int)(30f + 225f * num4);
                        if (num3 < 30)
                        {
                            num3 = 30;
                        }
                        num2 = num4 / 4f + 0.75f;
                        if ((double)num2 < 0.75)
                        {
                            num2 = 0.75f;
                        }
                    }
                    int num5 = 0;
                    int num6 = 0;
                    if (i > 10)
                    {
                        num5 -= 260;
                        num6 += 26;
                    }
                    Texture2D breathTex = mod.GetTexture("Extra/CriticalHeatIcon");
                    Main.spriteBatch.Draw(breathTex, value + new Vector2((float)(26 * (i - 1) + num5) - 125f, 32f + ((float)breathTex.Height - (float)breathTex.Height * num2) / 2f + (float)num6), new Rectangle?(new Rectangle(0, 0, breathTex.Width, breathTex.Height)), new Color(num3, num3, num3, num3), 0f, default(Vector2), num2, SpriteEffects.None, 0f);
                }
            }
        }
        public static void DrawEnergyBar()
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            Player player = Main.player[Main.myPlayer];
            var modPlayer = player.GetModPlayer<PlayerEnergy>();
            if (Main.LocalPlayer.ghost)
            {
                return;
            }
            if (!player.ghost && modPlayer.maxEnergy > 0)
            {
                var info = typeof(Main).GetField("UI_ScreenAnchorX", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
                int screenAnchorX = (int)info.GetValue(null);

                Texture2D backgroundTexture = mod.GetTexture("Extra/EnergyUI");
                int barPosLeft = 415 + screenAnchorX + (MyWorld.awakenedMode ? -154 : 0);
                Main.spriteBatch.Draw(backgroundTexture, new Rectangle(barPosLeft, 48, backgroundTexture.Width, backgroundTexture.Height), null, Color.White, 0f, new Vector2(backgroundTexture.Width / 2, backgroundTexture.Height / 2), SpriteEffects.None, 0f);

                Texture2D barTexture = mod.GetTexture("Extra/EnergyBar");
                Rectangle barDest = new Rectangle(barPosLeft + 12, 48, (int)(barTexture.Width * ((float)modPlayer.energy / (float)modPlayer.maxEnergy)), barTexture.Height);
                Rectangle barLength = new Rectangle(0, 0, (int)(barTexture.Width * ((float)modPlayer.energy / (float)modPlayer.maxEnergy)), barTexture.Height);
                Main.spriteBatch.Draw(barTexture, barDest, barLength, Color.White, 0f, new Vector2(barTexture.Width / 2, barTexture.Height / 2), SpriteEffects.None, 0f);

                DynamicSpriteFont fontType = Main.fontMouseText;
                string text = "Energy: " + modPlayer.energy + "/" + modPlayer.maxEnergy;
                Vector2 textSize = fontType.MeasureString(text);
                float textPositionLeft = barPosLeft - textSize.X / 2;
                DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, text, new Vector2(textPositionLeft, 6f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
            }
        }

        public static void DrawHeatBar(Item item)
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            Player player = Main.player[Main.myPlayer];
            Railgun railItem = (Railgun)item.modItem;
            if (Main.LocalPlayer.ghost || player.dead)
            {
                return;
            }

            float heat = MathHelper.Clamp(railItem.heat, 0, 1300);
            float maxHeatDisplayed = 1300;

            var info = typeof(Main).GetField("UI_ScreenAnchorX", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            int screenAnchorX = (int)info.GetValue(null);

            Texture2D backgroundTexture = mod.GetTexture("Extra/HeatBarUI");
            Vector2 UIPos = new Vector2(Main.screenWidth / 2 + 40, Main.screenHeight / 2 - backgroundTexture.Width - 10);
            Vector2 UISize = new Vector2(backgroundTexture.Width, backgroundTexture.Height);

            Main.spriteBatch.Draw(backgroundTexture, new Rectangle((int)UIPos.X, (int)UIPos.Y, backgroundTexture.Width, backgroundTexture.Height), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);

            Texture2D barTexture = mod.GetTexture("Extra/HeatBar");
            int barHeight = (int)(barTexture.Height * (heat / maxHeatDisplayed));
            Rectangle barDest = new Rectangle((int)UIPos.X, (int)UIPos.Y + barTexture.Height - barHeight + 4, barTexture.Width, barHeight);
            Rectangle barLength = new Rectangle(0, 0, barTexture.Width, barHeight);
            Main.spriteBatch.Draw(barTexture, barDest, barLength, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);

            if (!Main.mouseText)
            {
                if (Main.mouseX > UIPos.X && Main.mouseX < UIPos.X + UISize.X && Main.mouseY > UIPos.Y && Main.mouseY < UIPos.Y + UISize.Y)
                {
                    string mouseText = (int)railItem.heat + "";
                    ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, mouseText, new Vector2(Main.mouseX + 17, Main.mouseY + 17), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
                }
            }
        }

        public static void DrawInsanityUI()
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            Texture2D UITex = mod.GetTexture("Extra/InsanityUIIcon");
            Player player = Main.player[Main.myPlayer];
            var modPlayer = player.GetModPlayer<AwakenedPlayer>();
            float sanityPerEye = 30f;
            if (player.ghost || !MyWorld.awakenedMode)
            {
                return;
            }

            var info = typeof(Main).GetField("UI_ScreenAnchorX", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            int screenAnchorX = (int)info.GetValue(null);
            Vector2 UIPos = new Vector2(500f - Main.heartTexture.Width * 5 - 32, 32f);

            Vector2 UISize = new Vector2(6 * Main.heartTexture.Width, Main.heartTexture.Height);
            for (int eyeNum = 1; eyeNum < (int)(modPlayer.sanityMax / sanityPerEye) + 1; eyeNum++)
            {
                float scale = 1f;
                bool lastHeart = false;
                int insanityStat;
                if (modPlayer.sanity >= eyeNum * sanityPerEye)
                {
                    insanityStat = 255;
                    if (modPlayer.sanity == eyeNum * sanityPerEye)
                    {
                        lastHeart = true;
                    }
                }
                else
                {
                    float num7 = (modPlayer.sanity - (eyeNum - 1) * sanityPerEye) / sanityPerEye;
                    insanityStat = (int)(30f + 225f * num7);
                    if (insanityStat < 30)
                    {
                        insanityStat = 30;
                    }
                    scale = num7 / 4f + 0.75f;
                    if (scale < 0.75)
                    {
                        scale = 0.75f;
                    }
                    if (num7 > 0f)
                    {
                        lastHeart = true;
                    }
                }
                if (lastHeart)
                {
                    scale += Main.cursorScale - 1f;
                }
                int xPos = 0;
                int yPos = 0;
                if (eyeNum > 5)
                {
                    xPos -= 130;
                    yPos += 26;
                    UISize.Y = Main.heartTexture.Height * 2;
                }
                xPos -= (UITex.Width - Main.heartTexture.Width) / 2;
                yPos -= (UITex.Height - Main.heartTexture.Height) / 2;

                if (modPlayer.sanity < modPlayer.sanityMax * 0.4f)
                {
                    int amount = 1;
                    if (modPlayer.sanity < modPlayer.sanityMax * 0.3f) amount = 2;
                    else if (modPlayer.sanity < modPlayer.sanityMax * 0.2f) amount = 3;
                    else if (modPlayer.sanity < modPlayer.sanityMax * 0.1f) amount = 4;
                    if (modPlayer.sanityGlitchFrame != 0)
                    {
                        xPos += Main.rand.Next(-amount, amount);
                        yPos += Main.rand.Next(-amount, amount);
                    }
                }

                int a = (int)(insanityStat * 0.9f);
                if (!player.ghost)
                {
                    Main.spriteBatch.Draw(UITex, new Vector2((float)(UIPos.X + 26 * (eyeNum - 1) + xPos + screenAnchorX + Main.heartTexture.Width / 2), UIPos.Y + ((float)Main.heartTexture.Height - (float)Main.heartTexture.Height * scale) / 2f + (float)yPos + (float)(Main.heartTexture.Height / 2)), new Rectangle?(new Rectangle(0, 0, UITex.Width, UITex.Height)), new Color(insanityStat, insanityStat, insanityStat, a), 0f, new Vector2((float)(UITex.Width / 2), (float)(UITex.Height / 2)), scale, SpriteEffects.None, 0f);
                }
            }

            DynamicSpriteFont fontType = Main.fontMouseText;
            string text = "Sanity: " + modPlayer.sanity + "/" + modPlayer.sanityMax;
            Vector2 textSize = fontType.MeasureString(text);
            float textPositionLeft = UIPos.X + UISize.X / 2 + screenAnchorX - textSize.X / 2;
            DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, text, new Vector2(textPositionLeft, 6f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);

            if (!Main.mouseText)
            {
                if (Main.mouseX > UIPos.X + screenAnchorX && Main.mouseX < UIPos.X + UISize.X + screenAnchorX && Main.mouseY > 32 && Main.mouseY < 32 + Main.heartTexture.Height + UISize.Y)
                {
                    string mouseText = modPlayer.sanity + "/" + modPlayer.sanityMax;
                    ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, mouseText, new Vector2(Main.mouseX + 17, Main.mouseY + 17), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
                    int i = 0;
                    foreach (string effectText in modPlayer.sanityEffects)
                    {
                        i++;
                        int yPos = 25 * i;
                        ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, effectText, new Vector2(Main.mouseX + 17, Main.mouseY + 17 + yPos), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
                    }
                }
            }

            if (modPlayer.sanityRegen != 0)
            {
                Texture2D arrowTexture = mod.GetTexture("Extra/SanityArrow");
                int arrowHeight = 26;
                Rectangle arrowDest = new Rectangle((int)(textPositionLeft + textSize.X + arrowTexture.Width / 2) + 4, 6 + arrowHeight / 2, arrowTexture.Width, arrowHeight);
                Rectangle arrowLength = new Rectangle(0, arrowHeight * modPlayer.sanityArrowFrame, arrowTexture.Width, arrowHeight);
                SpriteEffects doFlip = modPlayer.sanityRegen < 0 ? SpriteEffects.None : SpriteEffects.FlipVertically;
                Main.spriteBatch.Draw(arrowTexture, arrowDest, arrowLength, Color.White, 0f, new Vector2(arrowTexture.Width / 2, arrowHeight / 2), doFlip, 0f);
            }
        }

        public static void DrawEnergyUI()
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            Texture2D UITex = mod.GetTexture("Extra/EnergyUIIcon");
            Player player = Main.player[Main.myPlayer];
            var modPlayer = player.GetModPlayer<PlayerEnergy>();
            if (player.ghost || modPlayer.maxEnergy == 0)
            {
                return;
            }
            float energyPerEye = modPlayer.maxEnergy / 10;

            var info = typeof(Main).GetField("UI_ScreenAnchorX", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            int screenAnchorX = (int)info.GetValue(null);
            Vector2 UIPos = new Vector2(500f - Main.heartTexture.Width * 5 - 32, 32f);
            Vector2 UISize = new Vector2(6 * Main.heartTexture.Width, Main.heartTexture.Height);
            if (MyWorld.awakenedMode) UIPos.X -= UISize.X + 32;
            for (int eyeNum = 1; eyeNum <= 10; eyeNum++)
            {
                float scale = 1f;
                bool lastHeart = false;
                int energyStat;
                if (modPlayer.energy >= eyeNum * energyPerEye)
                {
                    energyStat = 255;
                    if (modPlayer.energy == eyeNum * energyPerEye)
                    {
                        lastHeart = true;
                    }
                }
                else
                {
                    float num7 = (modPlayer.energy - (eyeNum - 1) * energyPerEye) / energyPerEye;
                    energyStat = (int)(30f + 225f * num7);
                    if (energyStat < 30)
                    {
                        energyStat = 30;
                    }
                    scale = num7 / 4f + 0.75f;
                    if (scale < 0.75)
                    {
                        scale = 0.75f;
                    }
                    if (num7 > 0f)
                    {
                        lastHeart = true;
                    }
                }
                if (lastHeart)
                {
                    scale += Main.cursorScale - 1f;
                }
                int xPos = 0;
                int yPos = 0;
                if (eyeNum > 5)
                {
                    xPos -= 130;
                    yPos += 26;
                    UISize.Y = Main.heartTexture.Height * 2;
                }
                xPos -= (UITex.Width - Main.heartTexture.Width) / 2;
                yPos -= (UITex.Height - Main.heartTexture.Height) / 2;

                int a = (int)(energyStat * 0.9f);
                if (!player.ghost)
                {
                    Main.spriteBatch.Draw(UITex, new Vector2((float)(UIPos.X + 26 * (eyeNum - 1) + xPos + screenAnchorX + Main.heartTexture.Width / 2), UIPos.Y + ((float)Main.heartTexture.Height - (float)Main.heartTexture.Height * scale) / 2f + (float)yPos + (float)(Main.heartTexture.Height / 2)), new Rectangle?(new Rectangle(0, 0, UITex.Width, UITex.Height)), new Color(energyStat, energyStat, energyStat, a), 0f, new Vector2((float)(UITex.Width / 2), (float)(UITex.Height / 2)), scale, SpriteEffects.None, 0f);
                }
            }

            DynamicSpriteFont fontType = Main.fontMouseText;
            string text = "Energy: " + modPlayer.energy + "/" + modPlayer.maxEnergy;
            Vector2 textSize = fontType.MeasureString(text);
            float textPositionLeft = UIPos.X + UISize.X / 2 + screenAnchorX - textSize.X / 2;
            DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, text, new Vector2(textPositionLeft, 6f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);

            if (!Main.mouseText)
            {
                if (Main.mouseX > UIPos.X + screenAnchorX && Main.mouseX < UIPos.X + UISize.X + screenAnchorX && Main.mouseY > 32 && Main.mouseY < 32 + Main.heartTexture.Height + UISize.Y)
                {
                    string mouseText = modPlayer.energy + "/" + modPlayer.maxEnergy;
                    ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, mouseText, new Vector2(Main.mouseX + 17, Main.mouseY + 17), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
                }
            }
        }

        public static void DrawInsanityBar()
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            Player player = Main.player[Main.myPlayer];
            var modPlayer = player.GetModPlayer<AwakenedPlayer>();
            if (Main.LocalPlayer.ghost)
            {
                return;
            }
            if (!player.ghost && MyWorld.awakenedMode)
            {
                var info = typeof(Main).GetField("UI_ScreenAnchorX", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
                int screenAnchorX = (int)info.GetValue(null);

                Texture2D backgroundTexture = mod.GetTexture("Extra/InsanityUI");
                int barPosLeft = 415 + screenAnchorX;
                Main.spriteBatch.Draw(backgroundTexture, new Rectangle(barPosLeft, 48, backgroundTexture.Width, backgroundTexture.Height), null, Color.White, 0f, new Vector2(backgroundTexture.Width / 2, backgroundTexture.Height / 2), SpriteEffects.None, 0f);

                // above 40% sanity
                if (modPlayer.sanity >= modPlayer.sanityMax * 0.4f)
                {
                    Texture2D barTexture = mod.GetTexture("Extra/InsanityBar");
                    Rectangle barDest = new Rectangle(barPosLeft + 18, 49, (int)(barTexture.Width * ((float)modPlayer.sanity / (float)modPlayer.sanityMax)), barTexture.Height);
                    Rectangle barLength = new Rectangle(0, 0, (int)(barTexture.Width * ((float)modPlayer.sanity / (float)modPlayer.sanityMax)), barTexture.Height);
                    Main.spriteBatch.Draw(barTexture, barDest, barLength, Color.White, 0f, new Vector2(barTexture.Width / 2, barTexture.Height / 2), SpriteEffects.None, 0f);
                }
                else
                {
                    Texture2D barTexture = mod.GetTexture("Extra/InsanityBarDistorted");
                    int barHeight = 34;
                    Rectangle barDest = new Rectangle(barPosLeft + 18, 48, (int)(barTexture.Width * ((float)modPlayer.sanity / (float)modPlayer.sanityMax)), barHeight);
                    Rectangle barLength = new Rectangle(0, barHeight * modPlayer.sanityGlitchFrame, (int)(barTexture.Width * ((float)modPlayer.sanity / (float)modPlayer.sanityMax)), barHeight);
                    Main.spriteBatch.Draw(barTexture, barDest, barLength, Color.White, 0f, new Vector2(barTexture.Width / 2, barHeight / 2), SpriteEffects.None, 0f);
                }
                if (modPlayer.sanityRegen != 0)
                {
                    Texture2D arrowTexture = mod.GetTexture("Extra/SanityArrow");
                    int arrowHeight = 26;
                    Rectangle barDest = new Rectangle(barPosLeft + 74, 46, arrowTexture.Width, arrowHeight);
                    Rectangle barLength = new Rectangle(0, arrowHeight * modPlayer.sanityArrowFrame, arrowTexture.Width, arrowHeight);
                    SpriteEffects doFlip = modPlayer.sanityRegen < 0 ? SpriteEffects.None : SpriteEffects.FlipVertically;
                    Main.spriteBatch.Draw(arrowTexture, barDest, barLength, Color.White, 0f, new Vector2(arrowTexture.Width / 2, arrowHeight / 2), doFlip, 0f);
                }
                DynamicSpriteFont fontType = Main.fontMouseText;
                string text = "Sanity: " + modPlayer.sanity + "/" + modPlayer.sanityMax;
                Vector2 textSize = fontType.MeasureString(text);
                float textPositionLeft = barPosLeft - textSize.X / 2;
                DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, text, new Vector2(textPositionLeft, 6f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
            }
        }

        public static void DrawInsanityOverlay()
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            Player player = Main.player[Main.myPlayer];
            var modPlayer = player.GetModPlayer<AwakenedPlayer>();

            if (modPlayer.sanity > modPlayer.sanityMax * 0.50f) return;

            Color color = new Color(255, InsanityOverlay.gbValues, InsanityOverlay.gbValues) * InsanityOverlay.transparency;
            int width = ElementsAwoken.insanityTex.Width;
            int num = 10;
            Rectangle rect = Main.player[Main.myPlayer].getRect();
            rect.Inflate((width - rect.Width) / 2, (width - rect.Height) / 2 + num / 2);
            rect.Offset(-(int)Main.screenPosition.X, -(int)Main.screenPosition.Y + (int)Main.player[Main.myPlayer].gfxOffY - num);
            Rectangle destinationRectangle1 = Rectangle.Union(new Rectangle(0, 0, 1, 1), new Rectangle(rect.Right - 1, rect.Top - 1, 1, 1));
            Rectangle destinationRectangle2 = Rectangle.Union(new Rectangle(Main.screenWidth - 1, 0, 1, 1), new Rectangle(rect.Right, rect.Bottom - 1, 1, 1));
            Rectangle destinationRectangle3 = Rectangle.Union(new Rectangle(Main.screenWidth - 1, Main.screenHeight - 1, 1, 1), new Rectangle(rect.Left, rect.Bottom, 1, 1));
            Rectangle destinationRectangle4 = Rectangle.Union(new Rectangle(0, Main.screenHeight - 1, 1, 1), new Rectangle(rect.Left - 1, rect.Top, 1, 1));
            Main.spriteBatch.Draw(Main.magicPixel, destinationRectangle1, new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
            Main.spriteBatch.Draw(Main.magicPixel, destinationRectangle2, new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
            Main.spriteBatch.Draw(Main.magicPixel, destinationRectangle3, new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
            Main.spriteBatch.Draw(Main.magicPixel, destinationRectangle4, new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
            Main.spriteBatch.Draw(ElementsAwoken.insanityTex, rect, color);
        }

        public static void BlackScreenTrans()
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");

            Player player = Main.player[Main.myPlayer];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            Color color = Color.Black * modPlayer.screenTransAlpha;
            Rectangle rect = new Rectangle(0, 0, Main.screenWidth, Main.screenHeight);
            Main.spriteBatch.Draw(Main.magicPixel, rect, color);
        }

        public static void DrawComputer(SpriteBatch spriteBatch)
        {
            var mod = ModLoader.GetMod("ElementsAwoken");
            var background = mod.GetTexture("Extra/ComputerText");
            var player = Main.player[Main.myPlayer].GetModPlayer<MyPlayer>();
            string text = player.computerText;
            spriteBatch.Draw(background, new Rectangle(Main.screenWidth / 2, 150, background.Width, background.Height), null, Color.White, 0f, new Vector2(background.Width / 2, background.Height / 2), SpriteEffects.None, 0f);
            Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, text, Main.screenWidth / 2 - 230, 86, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
        }

        public static void DrawSanityBook()
        {
            Player player = Main.player[Main.myPlayer];
            AwakenedPlayer awakenedPlayer = player.GetModPlayer<AwakenedPlayer>();

            var background = ModContent.GetTexture("ElementsAwoken/Extra/InsanityBookUI");
            Main.spriteBatch.Draw(background, new Rectangle(Main.screenWidth - 350, Main.screenHeight - 250, background.Width, background.Height), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            // draw the positive
            Utils.DrawBorderStringFourWay(Main.spriteBatch, Main.fontMouseText, "Regens:", Main.screenWidth - 330, Main.screenHeight - 220, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
            for (int i = 0; i < awakenedPlayer.sanityRegens.Count; i++)
            {
                string text = awakenedPlayer.sanityRegensName[i] + ": " + awakenedPlayer.sanityRegens[i];
                int yPos = Main.screenHeight - 200 + 25 * i;
                Utils.DrawBorderStringFourWay(Main.spriteBatch, Main.fontMouseText, text, Main.screenWidth - 330, yPos, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
            }
            // draw the negative
            Utils.DrawBorderStringFourWay(Main.spriteBatch, Main.fontMouseText, "Drains:", Main.screenWidth - 150, Main.screenHeight - 220, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
            for (int i = 0; i < awakenedPlayer.sanityDrains.Count; i++)
            {
                string text = awakenedPlayer.sanityDrainsName[i] + ": " + awakenedPlayer.sanityDrains[i];
                int textLength = (int)Main.fontMouseText.MeasureString(text).X;
                int xPos = Main.screenWidth - 150;
                if (Main.screenWidth - 150 + textLength > Main.screenWidth) xPos = Main.screenWidth - textLength - 35;
                int yPos = Main.screenHeight - 200 + 25 * i;
                Utils.DrawBorderStringFourWay(Main.spriteBatch, Main.fontMouseText, text, xPos, yPos, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
            }
        }

        public static void DrawInfoAccs()
        {
            Player player = Main.player[Main.myPlayer];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            int amountOfInfoActive = ElementsAwoken.CountAvailableInfo() - 1; // - 1 so it starts at 0 when 1 is equipped
            int amountOfInfoEquipped = ElementsAwoken.CountEquippedInfo() - 1;

            float num4 = 215f;
            int whichInfoDrawing = -1;
            string text = "";

            for (int infoNum = 0; infoNum < 3; infoNum++)
            {
                string text2 = "";
                string hoverText = "";

                if (infoNum == 0 && modPlayer.alchemistTimer)
                {
                    if ((!modPlayer.hideEAInfo[0] || Main.playerInventory))
                    {
                        hoverText = "Buff Damage Per Second";
                        whichInfoDrawing = infoNum;

                        text2 = modPlayer.buffDPS + " buff damage per second";
                        if (modPlayer.buffDPS <= 0)
                        {
                            text2 = Language.GetTextValue("GameUI.NoDPS");
                        }
                    }
                    amountOfInfoEquipped++;
                    if (!modPlayer.hideEAInfo[0])
                    {
                        amountOfInfoActive++;
                    }
                }
                else if (infoNum == 1 && modPlayer.dryadsRadar)
                {
                    if ((!modPlayer.hideEAInfo[1] || Main.playerInventory))
                    {
                        hoverText = "Nearby Evil Biomes";
                        whichInfoDrawing = infoNum;

                        text2 = modPlayer.nearbyEvil + " nearby";
                    }
                    amountOfInfoEquipped++;
                    if (!modPlayer.hideEAInfo[1])
                    {
                        amountOfInfoActive++;
                    }
                }
                else if (infoNum == 2 && modPlayer.rainMeter)
                {
                    if ((!modPlayer.hideEAInfo[2] || Main.playerInventory))
                    {
                        whichInfoDrawing = infoNum;

                        if (player.position.Y / 16 < Main.maxTilesY - 200)
                        {
                            hoverText = "Rain Time";
                            text2 = Main.rainTime / 60 + " seconds remaining";
                            if (Main.rainTime == 0) text2 = "Clear";
                        }
                        else
                        {
                            hoverText = "Plateau Weather Time";
                            text2 = MyWorld.plateauWeatherTime / 60 + " seconds remaining";
                            if (MyWorld.plateauWeatherTime == 0 || MyWorld.plateauWeather <= 0) text2 = "Plateau Clear";
                        }
                    }
                    amountOfInfoEquipped++;
                    if (!modPlayer.hideEAInfo[2])
                    {
                        amountOfInfoActive++;
                    }
                }
                if (text2 != "")
                {
                    int mH = (int)((typeof(Main).GetField("mH", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)).GetValue(null));
                    if ((Main.npcChatText == null || Main.npcChatText == "") && player.sign < 0)
                    {
                        int distBetweenInfo = 22;
                        if (Main.screenHeight < 650)
                        {
                            distBetweenInfo = 20;
                        }

                        int iconPosX;
                        int iconPosY;
                        if (!Main.playerInventory)
                        {
                            iconPosX = Main.screenWidth - 280;
                            iconPosY = -32;
                            if (Main.mapStyle == 1 && Main.mapEnabled)
                            {
                                iconPosY += 254;
                            }
                        }
                        else if (Main.ShouldDrawInfoIconsHorizontally)
                        {
                            iconPosX = Main.screenWidth - 280 + 20 * amountOfInfoEquipped - 10;
                            iconPosY = 94;
                            if (Main.mapStyle == 1 && Main.mapEnabled)
                            {
                                iconPosY += 254;
                            }
                            if (amountOfInfoEquipped + 1 > 12)
                            {
                                iconPosX -= 20 * 12;
                                iconPosY += 26;
                            }
                        }
                        else
                        {
                            int num28 = (int)(52f * Main.inventoryScale);
                            iconPosX = 697 - num28 * 4 + Main.screenWidth - 800 + 20 * (amountOfInfoEquipped % 2);
                            iconPosY = 114 + mH + num28 * 7 + num28 / 2 + 20 * (amountOfInfoEquipped / 2) + 8 * (amountOfInfoEquipped / 4) - 20;
                            if (Main.EquipPage == 2)
                            {
                                iconPosX += num28 + num28 / 2;
                                iconPosY -= num28;
                            }
                        }
                        if (whichInfoDrawing >= 0)
                        {
                            string ext = "";
                            if (whichInfoDrawing == 2 && player.position.Y / 16 > Main.maxTilesY - 200) ext = "1";
                            Texture2D tex = ModContent.GetTexture("ElementsAwoken/Extra/EAInfo" + whichInfoDrawing + ext);
                            Vector2 vector = new Vector2((float)iconPosX, (float)(iconPosY + 74 + distBetweenInfo * amountOfInfoActive + 52));

                            Color white = Color.White;
                            bool flag14 = false;
                            if (Main.playerInventory)
                            {
                                vector = new Vector2((float)iconPosX, (float)iconPosY);
                                if ((float)Main.mouseX >= vector.X && (float)Main.mouseY >= vector.Y && (float)Main.mouseX <= vector.X + (float)tex.Width && (float)Main.mouseY <= vector.Y + (float)tex.Height && !PlayerInput.IgnoreMouseInterface)
                                {
                                    flag14 = true;
                                    player.mouseInterface = true;
                                    if (Main.mouseLeft && Main.mouseLeftRelease)
                                    {
                                        Main.PlaySound(12, -1, -1, 1, 1f, 0f);
                                        Main.mouseLeftRelease = false;
                                        modPlayer.hideEAInfo[whichInfoDrawing] = !modPlayer.hideEAInfo[whichInfoDrawing];
                                    }
                                    if (!Main.mouseText)
                                    {
                                        text = hoverText;
                                        Main.mouseText = true;
                                    }
                                }
                                if (modPlayer.hideEAInfo[whichInfoDrawing])
                                {
                                    white = new Color(80, 80, 80, 70);
                                }
                            }
                            else if ((float)Main.mouseX >= vector.X && (float)Main.mouseY >= vector.Y && (float)Main.mouseX <= vector.X + (float)tex.Width && (float)Main.mouseY <= vector.Y + (float)tex.Height && !Main.mouseText)
                            {
                                Main.mouseText = true;
                                text = hoverText;
                            }
                            //UILinkPointNavigator.SetPosition(1558 + amountOfInfoEquipped - 1, vector + tex.Size() * 0.75f);
                            if (!Main.playerInventory && modPlayer.hideEAInfo[whichInfoDrawing])
                            {
                                white = Color.Transparent;
                            }
                            Main.spriteBatch.Draw(tex, vector, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), white, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                            if (flag14)
                            {
                                Texture2D outline = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar.ToString() + "UI" + Path.DirectorySeparatorChar.ToString() + "InfoIcon_13");
                                Main.spriteBatch.Draw(outline, vector - Vector2.One * 2f, null, Main.OurFavoriteColor, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, hoverText, new Vector2(Main.mouseX, Main.mouseX), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
                            }
                            iconPosX += 20;
                        }
                        if (!Main.playerInventory)
                        {
                            Vector2 vector2 = new Vector2(1f);

                            Vector2 vector3 = Main.fontMouseText.MeasureString(text2);
                            if (vector3.X > num4)
                            {
                                vector2.X = num4 / vector3.X;
                            }
                            if (vector2.X < 0.58f)
                            {
                                vector2.Y = 1f - vector2.X / 3f;
                            }
                            for (int num31 = 0; num31 < 5; num31++)
                            {
                                int num32 = 0;
                                int num33 = 0;
                                Color black = Color.Black;
                                if (num31 == 0)
                                {
                                    num32 = -2;
                                }
                                if (num31 == 1)
                                {
                                    num32 = 2;
                                }
                                if (num31 == 2)
                                {
                                    num33 = -2;
                                }
                                if (num31 == 3)
                                {
                                    num33 = 2;
                                }
                                if (num31 == 4)
                                {
                                    black = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
                                }
                                /*if (i > num2 && i < num2 + 2)
                                {
                                    black = new Color((int)(black.R / 3), (int)(black.G / 3), (int)(black.B / 3), (int)(black.A / 3));
                                }*/

                                DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, Main.fontMouseText, text2, new Vector2((float)(iconPosX + num32), (float)(iconPosY + 74 + distBetweenInfo * amountOfInfoActive + num33 + 48)), black, 0f, default(Vector2), vector2, SpriteEffects.None, 0f);
                            }
                        }
                        if (!string.IsNullOrEmpty(text))
                        {
                            if (Main.playerInventory)
                            {
                                Main.player[Main.myPlayer].mouseInterface = true;
                            }
                            Vector2 drawTextPos = new Vector2(Main.mouseX, Main.mouseY) + new Vector2(16.0f);
                            if (drawTextPos.X + Main.fontMouseText.MeasureString(text).X > Main.screenWidth)
                            {
                                drawTextPos.X -= Main.fontMouseText.MeasureString(text).X; // to stop it drawing off the side
                            }

                            Utils.DrawBorderStringFourWay(Main.spriteBatch, Main.fontMouseText, text, drawTextPos.X, drawTextPos.Y, new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), Color.Black, new Vector2());
                        }
                    }
                }
            }
        }

        public static void DrawEncounterText(SpriteBatch spriteBatch)
        {
            var mod = ModLoader.GetMod("ElementsAwoken");
            var player = Main.player[Main.myPlayer].GetModPlayer<MyPlayer>();
            string text = player.encounterText;
            if (player.encounterTextTimer > 0)
            {
                Vector2 textSize = Main.fontDeathText.MeasureString(text);
                float textPositionLeft = Main.screenWidth / 2 - textSize.X / 2;

                Vector2 pos = new Vector2(textPositionLeft, Main.screenHeight / 2 - 200);
                float rand = player.finalText ? 3.5f : 2f;
                pos.X += Main.rand.NextFloat(-rand, rand);
                pos.Y += Main.rand.NextFloat(-rand, rand);
                Color color = player.finalText ? new Color(player.encounterTextAlpha, 0, 0, player.encounterTextAlpha) : new Color(player.encounterTextAlpha, player.encounterTextAlpha, player.encounterTextAlpha, player.encounterTextAlpha);
                DrawStringOutlined(spriteBatch, Main.fontDeathText, text, pos, color, 1f);
            }
        }

        public static void DrawAboveHeadText()
        {
            Player player = Main.LocalPlayer;
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            string text = modPlayer.aboveHeadText;
            float timer = modPlayer.aboveHeadTimer;
            float scale = 0.5f;
            float fadeTime = 60;
            float duration = modPlayer.aboveHeadDuration;
            if (timer > 0)
            {
                Vector2 textSize = Main.fontDeathText.MeasureString(text) * scale;
                float textPositionLeft = Main.screenWidth / 2 - textSize.X / 2;

                Vector2 pos = new Vector2(textPositionLeft, player.Top.Y - 60 - Main.screenPosition.Y);
                Color color = Color.White;
                if (timer >= duration - fadeTime) color *= 1 - MathHelper.Clamp((timer - (duration - fadeTime)) / fadeTime, 0, 1);
                if (timer <= fadeTime) color *= MathHelper.Clamp(timer / fadeTime, 0, 1);
                DrawStringOutlined(Main.spriteBatch, Main.fontDeathText, text, pos, color, scale);
            }
        }

        public static void DrawLorekeepersUI()
        {
            Player player = Main.LocalPlayer;
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            Color color = new Color(200, 200, 200, 200);
            Color textColor = new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor);
            int num2;
            string[] array = Utils.WordwrapString(modPlayer.tomeText, Main.fontMouseText, 460, 10, out num2);
            num2++;
            int num3 = num2;
            if (modPlayer.tomeTex != null)
            {
                num3 += (int)MathHelper.Clamp((float)modPlayer.tomeTexHeight / 30f, 1, 9999);
            }
            Main.spriteBatch.Draw(Main.chatBackTexture, new Vector2((float)(Main.screenWidth / 2 - Main.chatBackTexture.Width / 2), 100f), new Rectangle?(new Rectangle(0, 0, Main.chatBackTexture.Width, (num3 + 1) * 30)), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(Main.chatBackTexture, new Vector2((float)(Main.screenWidth / 2 - Main.chatBackTexture.Width / 2), (float)(100 + (num3 + 1) * 30)), new Rectangle?(new Rectangle(0, Main.chatBackTexture.Height - 30, Main.chatBackTexture.Width, 30)), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
            for (int i = 0; i < num2; i++)
            {
                if (array[i] != null)
                {
                    Utils.DrawBorderStringFourWay(Main.spriteBatch, Main.fontMouseText, array[i], (float)(170 + (Main.screenWidth - 800) / 2), (float)(120 + i * 30), textColor, Color.Black, Vector2.Zero, 1f);
                }
            }
            Rectangle rectangle = new Rectangle(Main.screenWidth / 2 - Main.chatBackTexture.Width / 2, 100, Main.chatBackTexture.Width, (num2 + 2) * 30);
            if (modPlayer.tomeTex != null)
            {
                Texture2D tex = modPlayer.tomeTex;
                Main.spriteBatch.Draw(tex, new Vector2((float)(Main.screenWidth / 2 + Main.chatBackTexture.Width / 2 - 10), 100f + (num2 + 1) * 30), new Rectangle(0, 0, tex.Width, modPlayer.tomeTexHeight), Color.White, 0f, new Vector2((float)tex.Width, 0), 1f, SpriteEffects.None, 0f);
            }
            float y = (float)(130 + num3 * 30);
            float x = (float)(170 + (Main.screenWidth - 800) / 2);
            Vector2 stringSize = ChatManager.GetStringSize(Main.fontMouseText, "Close", Vector2.One, -1f);
            Rectangle closeRect = new Rectangle((int)x, (int)y, (int)stringSize.X, (int)stringSize.Y);

            int num = (int)Main.mouseTextColor;
            Color textColor2 = new Color(num, (int)((double)num / 1.1), num / 2, num);

            float scale = 1f;
            if (closeRect.Contains(new Point(Main.mouseX, Main.mouseY)))
            {
                if (!closeRect.Contains(new Point(Main.lastMouseX, Main.lastMouseY))) Main.PlaySound(SoundID.MenuTick);
                if (Main.mouseLeft)
                {
                    modPlayer.tomeUI = false;
                    Main.PlaySound(11, -1, -1, 0);
                }
                scale = 1.05f;
            }
            else if (closeRect.Contains(new Point(Main.lastMouseX, Main.lastMouseY))) Main.PlaySound(SoundID.MenuTick);
            if (player.controlInv)
            {
                modPlayer.tomeUI = false;
                Main.playerInventory = false;
                Main.PlaySound(11, -1, -1, 0);
            }
            Utils.DrawBorderStringFourWay(Main.spriteBatch, Main.fontMouseText, "Close", x, y, textColor2, Color.Black, Vector2.Zero, scale);
        }

        public static void AvailableQuestText()
        {
            Player player = Main.LocalPlayer;
            QuestNPC townNPC = Main.npc[player.talkNPC].GetGlobalNPC<QuestNPC>();
            if (player.talkNPC == -1 || Main.InGuideCraftMenu || (townNPC.hasQuestAvailable == "" && townNPC.hasQuestActive == "" && townNPC.hasQuestToClaim == -1)) return;
            Color color = new Color(200, 200, 200, 200);
            Color textColor = new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor);
            int num2;
            string[] array = Utils.WordwrapString(Main.npcChatText, Main.fontMouseText, 460, 10, out num2);
            num2++;
            float y = (float)(130 + num2 * 30);
            float x = (float)(400 + (Main.screenWidth - 800) / 2);
            Vector2 stringSize = ChatManager.GetStringSize(Main.fontMouseText, "Quest", Vector2.One, -1f);
            Rectangle closeRect = new Rectangle((int)x, (int)y, (int)stringSize.X, (int)stringSize.Y);
            int num = (int)Main.mouseTextColor;
            Color textColor2 = new Color(num, (int)((double)num / 1.1), num / 2, num);
            if (townNPC.hasQuestActive != "" && townNPC.hasQuestAvailable == "") textColor2 *= 0.5f;
            float scale = 0.95f;
            if (closeRect.Contains(new Point(Main.mouseX, Main.mouseY)))
            {
                if (!closeRect.Contains(new Point(Main.lastMouseX, Main.lastMouseY))) Main.PlaySound(SoundID.MenuTick);
                if (Main.mouseLeft && ElementsAwoken.mouseLeftReleasedDuringChat)
                {
                    if (townNPC.hasQuestActive == "" && townNPC.hasQuestAvailable != "") townNPC.UpdateQuest();
                    else townNPC.UpdateQuest(true);
                    Main.PlaySound(SoundID.MenuTick);
                }
                scale = 1.05f;
            }
            else if (closeRect.Contains(new Point(Main.lastMouseX, Main.lastMouseY))) Main.PlaySound(SoundID.MenuTick);
            Utils.DrawBorderStringFourWay(Main.spriteBatch, Main.fontMouseText, "Quest", x + stringSize.X / 2, y + stringSize.Y / 2, textColor2, Color.Black, stringSize / 2, scale);
        }

        public static void DrawFireResistance()
        {
            Player player = Main.LocalPlayer;
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            if (!Main.playerInventory) return;

            int mH = (int)((typeof(Main).GetField("mH", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)).GetValue(null));
            Main.inventoryScale = 0.8f;

            if (Main.mouseX > Main.screenWidth - 64 - 28 && Main.mouseX < (int)((float)(Main.screenWidth - 64 - 28) + 56f * Main.inventoryScale) && Main.mouseY > 174 + mH && Main.mouseY < (int)((float)(174 + mH) + 448f * Main.inventoryScale) && !PlayerInput.IgnoreMouseInterface)
            {
                Main.player[Main.myPlayer].mouseInterface = true;
            }
            int numAccSlots = 8 + Main.player[Main.myPlayer].extraAccessorySlots;
            if (numAccSlots == 8 && (Main.player[Main.myPlayer].armor[8].type > 0 || Main.player[Main.myPlayer].armor[18].type > 0 || Main.player[Main.myPlayer].dye[8].type > 0))
            {
                numAccSlots = 9;
            }

            int num45 = Main.screenWidth - 64 - 28;
            int up = 1;
            if (ElementsAwoken.thoriumEnabled) up++;
            int num46 = (int)((float)(108 + mH) + (float)((numAccSlots - up) * 56) * Main.inventoryScale);

            Vector2 vector = new Vector2((float)(num45 - 10 - 47 - 47 - 14), (float)num46 + (float)Main.inventoryBackTexture.Height * 0.5f);
            Texture2D tex = ModContent.GetTexture("ElementsAwoken/Extra/FireResist");
            Main.spriteBatch.Draw(tex, vector, null, Color.White, 0f, tex.Size() / 2f, Main.inventoryScale, SpriteEffects.None, 0f);

            string resist = (modPlayer.fireResistance * 100).ToString() + "%";
            Vector2 value2 = Main.fontMouseText.MeasureString(resist);
            float textScale = Main.inventoryScale * 0.8f;
            ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, resist, vector - value2 * 0.5f * textScale, Color.White, 0f, Vector2.Zero, new Vector2(textScale), -1f, 2f);

            if (Utils.CenteredRectangle(vector, tex.Size()).Contains(new Point(Main.mouseX, Main.mouseY)) && !PlayerInput.IgnoreMouseInterface)
            {
                Main.player[Main.myPlayer].mouseInterface = true;
                string value3 = (modPlayer.fireResistance * 100) + "% Fire Resistance";
                if (!string.IsNullOrEmpty(value3))
                {
                    Main.hoverItemName = value3;
                }
            }
        }
    
        internal static void DrawStringOutlined(SpriteBatch spriteBatch, DynamicSpriteFont font, string text, Vector2 position, Color color, float scale, int borderWidth = 1)
        {
            // outlines
            spriteBatch.DrawString(font, text, new Vector2(position.X - borderWidth, position.Y), Color.Black * (color.A / 255f), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, text, new Vector2(position.X + borderWidth, position.Y), Color.Black * (color.A / 255f), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, text, new Vector2(position.X, position.Y - borderWidth), Color.Black * (color.A / 255f), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, text, new Vector2(position.X, position.Y + borderWidth), Color.Black * (color.A / 255f), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            // actual text
            spriteBatch.DrawString(font, text, new Vector2(position.X, position.Y), color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }
}