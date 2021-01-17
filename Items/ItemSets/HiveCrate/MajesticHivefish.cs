using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.HiveCrate
{
    public class MajesticHivefish : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 18;

            item.maxStack = 30;

            item.useAnimation = 17;
            item.useTime = 17;
            item.useStyle = 2;
            item.UseSound = SoundID.Item3;

            item.consumable = true;
            item.useTurn = true;

            item.healMana = 120;

            item.value = 10000;
            item.rare = 1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Majestic Hivefish");
        }
    }
}
