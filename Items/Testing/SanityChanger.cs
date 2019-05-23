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
    public class SanityChanger : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 1;
            item.useAnimation = 3;
            item.useTime = 3;
            item.useStyle = 4;
            item.autoReuse = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("sanitinator");
            Tooltip.SetDefault("this game drives me crazy");
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool UseItem(Player player)
        {
            AwakenedPlayer modPlayer = player.GetModPlayer<AwakenedPlayer>(mod);
            if (player.altFunctionUse != 2)
            {
                modPlayer.sanity++;
            }
            else
            {
                modPlayer.sanity--;
            }
            return true;
        }
    }
}
