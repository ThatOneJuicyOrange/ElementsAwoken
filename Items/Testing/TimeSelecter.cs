using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Testing
{
    public class TimeSelecter : ModItem
    {
        int timeMode = 0;
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 1;
            item.useAnimation = 20;
            item.useTime = 20;
            item.useStyle = 4;
            item.UseSound = SoundID.Item60;
            item.consumable = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Time Selecter");
            Tooltip.SetDefault("dormammu, ive come to bargain");
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                timeMode++;
                if (timeMode >= 6)
                {
                    timeMode = 0;
                }
                string text = "";
                switch (timeMode)
                {
                    case 0:
                        text = "Morning";
                        break;
                    case 1:
                        text = "Midday";
                        break;
                    case 2:
                        text = "5:00pm";
                        break;
                    case 3:
                        text = "Night";
                        break;
                    case 4:
                        text = "10pm";
                        break;
                    case 5:
                        text = "midnight";
                        break;
                    default:
                        return base.CanUseItem(player);
                }
                Main.NewText(text, Color.White.R, Color.White.G, Color.White.B);
            }
            else
            {
                return true;
            }
            return base.CanUseItem(player);
        }
        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    switch (timeMode)
                    {
                        case 0:
                            Main.dayTime = true;
                            Main.time = 0;
                            break;
                        case 1:
                            Main.dayTime = true;
                            Main.time = 27000;
                            break;
                        case 2:
                            Main.dayTime = true;
                            Main.time = 45000;
                            break;
                        case 3:
                            Main.dayTime = false;
                            Main.time = 0;
                            break;
                        case 4:
                            Main.dayTime = false;
                            Main.time = 9000;
                            break;
                        case 5:
                            Main.dayTime = false;
                            Main.time = 16200;
                            break;
                        default:
                            return base.CanUseItem(player);
                    }

                    if (Main.netMode == 2)
                    {
                        NetMessage.SendData(MessageID.WorldData);
                    }
                }
            }
            return true;
        }
    }
}
