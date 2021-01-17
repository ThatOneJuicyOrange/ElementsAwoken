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
    public class GravitySwitcher : ModItem
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
            item.GetGlobalItem<EATooltip>().testing = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gravity Switcher");
        }
        public override bool UseItem(Player player)
        {
            GravityPlayer modPlayer = player.GetModPlayer<GravityPlayer>();
            modPlayer.forceGrav *= -1;
            return true;
        }
    }
}
