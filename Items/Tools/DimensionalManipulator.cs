using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tools
{
    public class DimensionalManipulator : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 1;
            item.value = Item.buyPrice(0, 25, 0, 0);
            item.rare = 6;
            item.useAnimation = 20;
            item.useTime = 20;
            item.useStyle = 4;
            item.UseSound = SoundID.Item60;
            item.consumable = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dimensional Manipulator");
            Tooltip.SetDefault("Allows the wielder to adjust the time to their liking\nLeft click to change to the time\nRight Click to set the desired time");
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Main.dayTime = !Main.dayTime;
                    Main.time = 0; //16220 for midnight 9000 for 10pm
                    if (Main.netMode == 2)
                    {
                        NetMessage.SendData(MessageID.WorldData);
                    }
                }
            }
            else
            {
                ModContent.GetInstance<ElementsAwoken>().VoidTimerChangerUI.SetState(new UI.DimensionalManipulatorUI());
            }
            return true;
        }
    }
}
