using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using ElementsAwoken.Structures;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ElementsAwoken.Items.Testing
{
    public class WindMaximiser : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 1;

            item.useAnimation = 2;
            item.useTime = 2;

            item.useStyle = 4;
            item.consumable = false;
            item.autoReuse = true;
            item.GetGlobalItem<EATooltip>().testing = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blower");
            Tooltip.SetDefault("what is the maximum wind speed");
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool UseItem(Player player)
        {
            float num = 0.005f;
            if (player.altFunctionUse == 2)
            {
                Main.windSpeedSet -= num;
                Main.windSpeed -= num;
                if (Main.windSpeedSet < -2) Main.windSpeedSet = -2;
            }
            else
            {
                Main.windSpeedSet += num;
                Main.windSpeed += num;
                if (Main.windSpeedSet > 2) Main.windSpeedSet = 2;
            }
            //if (MathHelper.Distance(Main.windSpeedSet, Main.windSpeed) > 0.2f) Main.windSpeed = Main.windSpeedSet;
            return true;
        }
    }
}
