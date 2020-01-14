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
    public class Hailstormer : ModItem
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
            DisplayName.SetDefault("Hailstormer");
            Tooltip.SetDefault("man them jokers was big! size of a quarter, doggone!!!! ... \nIt said KAPOOYA!!! KAPOOYA!... and the water and hail just came in...\ni looked out my.. over my door and i looked out my door and it start hittin me in my head I took off runnin'... 'n ran to my restroom\nthen I called my mama to see was she all right.");
        }

        public override bool UseItem(Player player)
        {
            MyWorld.hailStormTime = Main.rand.Next(1800, 7200);
            return true;
        }
    }
}
