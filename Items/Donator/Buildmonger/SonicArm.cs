using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.DataStructures;

namespace ElementsAwoken.Items.Donator.Buildmonger
{
    public class SonicArm : ModItem
    {
        public int shootTimer = 120;

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.rare = 4;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.accessory = true;

            item.GetGlobalItem<EATooltip>().donator = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sonic Arm");
            Tooltip.SetDefault("Whips occasionally crack, releasing a sonic wave\nThe Buildmonger's donator item");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.sonicArm = true;
        }
    }
}
