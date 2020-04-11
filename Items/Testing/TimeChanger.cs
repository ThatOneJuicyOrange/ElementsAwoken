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
    public class TimeChanger : ModItem
    {
        public override string Texture { get { return "ElementsAwoken/Items/Testing/TimeSelecter"; } }
        int timeMode = 0;
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 1;
            item.useAnimation = 2;
            item.useTime = 2;
            item.useStyle = 4;
            item.UseSound = SoundID.Item60;
            item.autoReuse = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Time changer");
            Tooltip.SetDefault("just need a little bit more... time");
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
                if (timeMode >= 2)
                {
                    timeMode = 0;
                }
                string text = "";
                switch (timeMode)
                {
                    case 0:
                        text = "forward";
                        break;
                    case 1:
                        text = "back";
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
                            Main.time +=5;
                            break;
                        case 1:
                            Main.time -=5;
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
