using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using ElementsAwoken.Structures.VolcanicPlateau;
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
            item.GetGlobalItem<EATooltip>().testing = true;
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
            newStructurePosition = new Point16((int)player.position.X / 16, (int)player.position.Y / 16);
            structX = newStructurePosition.X;
            structY = newStructurePosition.Y;

            if (TileCheckSafe(structX, structY))
            {
                /*// oval
                structY -= 20;
                int radiusX = WorldGen.genRand.Next(2, 10);
                int radiusY = WorldGen.genRand.Next(2, 5);

                for (int z = 0; z < radiusY * 2; z++) // we work downwards
                {
                    float ratio = z / (float)(radiusY * 2); // This will give us a ratio from 0 to 1, depending on how far down we are
                    int width = (int)(Math.Sin(ratio * Math.PI) * radiusX * 2); // This will give us the width of the ellipsoid at height y

                    for (int q = -width / 2; q < width / 2; q++)
                    {
                        Vector2 tilePosition = new Vector2(structX,structY) + new Vector2(q, z);
                        WorldGen.KillTile((int)tilePosition.X, (int)tilePosition.Y);
                        WorldGen.PlaceTile((int)tilePosition.X, (int)tilePosition.Y, TileID.Marble);
                    }
                }
                */

                //Structures.QuicksandGeneration.Generate();
                //VolcanicPlateau.GenMineRoom(structX, structY, ModContent.GetTexture("ElementsAwoken/Structures/VolcanicPlateau/MineRoom5"), yOff: -6, secNum: 0, style: 5);
                int num = 3;
                Vector2[] cavePositions = new Vector2[num];
                for (int p = 0; p < num; p++)
                {
                    int i = structX + WorldGen.genRand.Next(0, 200);
                    int j = structY + WorldGen.genRand.Next(0, 200);

                    int radiusX = WorldGen.genRand.Next(20, 30);
                    int radiusY = WorldGen.genRand.Next(20, 30);

                    cavePositions[p] = new Vector2(i, j + radiusY);

                    for (int roundY = 0; roundY < radiusY * 2; roundY++) // we work downwards
                    {
                        float ratio = roundY / (float)(radiusY * 2); // This will give us a ratio from 0 to 1, depending on how far down we are
                        int width = (int)(Math.Sin(ratio * Math.PI) * radiusX * 2); // This will give us the width of the ellipsoid at height y

                        float flatten = radiusY * 2 * 0.75f;
                        if (width < 4) width = 0;
                        if (roundY >= flatten)
                        {
                            float value = (float)(roundY - flatten) / ((float)radiusY * 2f - flatten);
                            width = (int)Math.Floor(width * MathHelper.Lerp(1f, -1f, value));
                            if (width < radiusX * 0.5f) width = 0;
                        }
                        for (int roundX = -width / 2; roundX < width / 2; roundX++)
                        {
                            Vector2 tilePosition = new Vector2(i, j) + new Vector2(roundX, roundY);
                            Tile t = Framing.GetTileSafely((int)tilePosition.X, (int)tilePosition.Y);
                            WorldGen.KillTile((int)tilePosition.X, (int)tilePosition.Y);
                            WorldGen.PlaceTile((int)tilePosition.X, (int)tilePosition.Y, TileID.GoldBrick);
                        }
                    }
                    // find closest

                    if (p != 0)
                    {
                        Vector2 curr = new Vector2(i, j + radiusY);
                        Vector2 closest = Vector2.Zero;
                        for (int z = 0; z < num; z++)
                        {
                            if (z == p) continue;
                            Vector2 other = cavePositions[z];
                            if (Vector2.Distance(curr, other) < Vector2.Distance(closest, curr)) closest = other;
                        }
                        DigTunnel(curr, closest, WorldGen.genRand.Next(4, 8));
                    }
                }
                /*int radiusX = WorldGen.genRand.Next(2, 10);
                int radiusY = WorldGen.genRand.Next(2, 5);
                Main.NewText(radiusX);
                for (int q = structX - radiusX; q <= structX + radiusX; q++)
                {
                    for (int z = structY - radiusY; z <= structY + radiusY; z++)
                    {
                        Tile t = Framing.GetTileSafely(q, z);
                        if (Vector2.Distance(new Vector2(structX, structY), new Vector2(q, z)) <= radiusX) // Vector2.Distance(new Vector2(structX, structY), new Vector2(q, z))
                        {
                            Main.NewText((MathHelper.Distance(structX, q)));
                            WorldGen.KillTile(q, z);
                            WorldGen.PlaceTile(q, z, TileID.Marble);
                        }
                    }
                }     */

                /*bool mirrored = false;
                if (Main.rand.Next(2) == 0)
                    mirrored = true;
                AncientsLab.Generate(structX, structY, mirrored);*/

                //AzanaLab.Generate(structX, structY);
                //VolcanicPlateau.GenerateStructures(structX, structY);

                //VolcanicPlateau.Streak((int)player.position.X / 16, (int)player.position.Y / 16, 50, TileID.Marble);
                //VolcanicPlateau.Temple(structX - 900, structY - 160);
            }
        }
        private static void DigTunnel(Vector2 start, Vector2 end, int tunnelHeight)
        {
            int distX = (int)(end.X - start.X);
            int distY = (int)(end.Y - start.Y);
            int dirX = Math.Sign(distX);
            int dirY = Math.Sign(distY);
            float ratio = Math.Abs((float)distY / (float)distX);
            int x = (int)start.X;
            float y = (int)start.Y;
            while ((dirX > 0 ? x < end.X : x > end.X) && (dirY > 0 ? y < end.Y : y > end.Y))
            {
                x += dirX;
                y += (float)dirY * ratio;
                for (int i = -tunnelHeight / 2; i < tunnelHeight / 2; i++)
                {
                    for (int j = -tunnelHeight / 2; j < tunnelHeight / 2; j++)
                    {
                        if (Math.Abs(x) > 30000 || Math.Abs(y) > 30000) return;
                        int y2 = (int)Math.Floor(y);
                        //int y2 = (int)start.Y; // debug
                        WorldGen.KillTile(x + i, y2 + j);
                        WorldGen.PlaceTile(x + i, y2 + j, TileID.PlatinumBrick);
                    }
                }
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
