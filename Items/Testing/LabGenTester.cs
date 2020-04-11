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
    public class LabGenTester : ModItem
    {
        public static int sizeMult = (int)(Math.Floor(Main.maxTilesX / 4200f)); //Small = 2; Medium = ~3; Large = 4;

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
            DisplayName.SetDefault("Lab Gen Tester");
            Tooltip.SetDefault("i just want a damn desk to generate is that so hard");
        }
        public override bool UseItem(Player player)
        {
            int xO = Main.maxTilesX / 2;
            int yO = (int)(Main.maxTilesY * .7f);
            LootStructures(xO, yO, player);
            return true;
        }
        private void LootStructures(int xO, int yO, Player player)
        {
            int structX = xO - 225 * sizeMult + Main.rand.Next(225 * sizeMult * 2);
            int structY = yO - 275 * sizeMult + Main.rand.Next(275 * sizeMult / 2);

            Point16 newStructurePosition = new Point16(structX, structY);
            newStructurePosition = new Point16((int)player.position.X / 16 , (int)player.position.Y / 16);
            structX = newStructurePosition.X;
            structY = newStructurePosition.Y;

            if (TileCheckSafe(structX, structY))
            {
                /*bool mirrored = false;
                if (Main.rand.Next(2) == 0)
                    mirrored = true;
                AncientsLab.Generate(structX, structY, mirrored);*/

                AzanaLab.Generate(structX, structY);
            }
        }
        private static bool TileCheckSafe(int i, int j)
        {
            if (i > 0 && i < Main.maxTilesX && j > 0 && j < Main.maxTilesY)
                return true;
            return false;
        }
    }
}
