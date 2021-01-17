using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace ElementsAwoken.Items.Testing
{
    public class GodArmor : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.accessory = true;
            item.GetGlobalItem<EATooltip>().testing = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("God Plating");
            Tooltip.SetDefault("perfectly balanced, like all things should be");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 500;
        }
    }
}
