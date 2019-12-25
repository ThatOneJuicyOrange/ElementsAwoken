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
    public class CreditsSetup : ModItem
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
            DisplayName.SetDefault("Credits");
        }
        public override bool UseItem(Player player)
        {
            if (MyWorld.credits)
            {
                MyWorld.credits = false;               
                Main.NewText("credits off", Color.Purple.R, Color.Purple.G, Color.Purple.B);
            }
            else
            {
                MyWorld.credits = true;
               // MyWorld.creditsCounter = player.GetModPlayer<MyPlayer>().screenDuration * 10 - 30;
                Main.NewText("credits on", Color.Purple.R, Color.Purple.G, Color.Purple.B);
            }
            return true;
        }
    }
}
