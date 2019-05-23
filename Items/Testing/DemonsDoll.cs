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
    public class DemonsDoll : ModItem
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
            DisplayName.SetDefault("Demon's Doll");
            Tooltip.SetDefault("help");
        }
        public override bool UseItem(Player player)
        {
            if (Main.hardMode)
            {
                Main.hardMode = false;
                Main.NewText("The ancient spirits of light and dark have been sealed away once more...", Color.GreenYellow.R, Color.GreenYellow.G, Color.GreenYellow.B);
            }
            else
            {
                Main.hardMode = true;
                Main.NewText("The ancient spirits of light and dark have been released.", Color.GreenYellow.R, Color.GreenYellow.G, Color.GreenYellow.B);
            }
            return true;
        }
    }
}
