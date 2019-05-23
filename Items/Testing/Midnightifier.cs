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
    public class Midnightifier : ModItem
    {
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
            DisplayName.SetDefault("Midnightifier");
            Tooltip.SetDefault("AAAAAAAAAAAAAAAAAAAAAAAA its spooky and dark");
        }

        public override bool UseItem(Player player)
        {
            if (Main.netMode != 1)
            {
                Main.dayTime = false;
                Main.time = 16220;
                if (Main.netMode == 2)
                {
                    NetMessage.SendData(MessageID.WorldData);
                }
            }
            return true;
        }
    }
}
