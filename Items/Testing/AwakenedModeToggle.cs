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
    public class AwakenedModeToggle : ModItem
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
            DisplayName.SetDefault("awakawakaner");
            Tooltip.SetDefault("abc");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
        }
        public override bool UseItem(Player player)
        {
            if (MyWorld.awakenedMode)
            {
                MyWorld.awakenedMode = false;
                Main.NewText("The spirits of Terraria sleep...", Color.Purple.R, Color.Purple.G, Color.Purple.B);
            }
            else
            {
                MyWorld.awakenedMode = true;
                Main.NewText("The spirits of Terraria awaken once more...", Color.Purple.R, Color.Purple.G, Color.Purple.B);
            }
            return true;
        }
    }
}
