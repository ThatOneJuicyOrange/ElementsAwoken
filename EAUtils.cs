using System;
using System.Collections.Generic;
using System.IO;
using ElementsAwoken.Projectiles;
using ElementsAwoken.Projectiles.NPCProj;
using ElementsAwoken.Tiles.VolcanicPlateau;
using ElementsAwoken.Tiles.VolcanicPlateau.ObjectSpawners;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken
{
    public class EAUtils
    {
        public static string GetPriceString(int price)
        {
            string text2 = "";
            int platinum = 0;
            int gold = 0;
            int silver = 0;
            int copper = 0;

            if (price >= 1000000)
            {
                platinum = price / 1000000;
                price -= platinum * 1000000;
            }
            if (price >= 10000)
            {
                gold = price / 10000;
                price -= gold * 10000;
            }
            if (price >= 100)
            {
                silver = price / 100;
                price -= silver * 100;
            }
            if (price >= 1)
            {
                copper = price;
            }
            if (platinum > 0)
            {
                text2 = string.Concat(new object[]
                {
                        text2,
                        platinum,
                        " ",
                        Lang.inter[15].Value,
                        " "
                });
            }
            if (gold > 0)
            {
                text2 = string.Concat(new object[]
                {
                        text2,
                        gold,
                        " ",
                        Lang.inter[16].Value,
                        " "
                });
            }
            if (silver > 0)
            {
                text2 = string.Concat(new object[]
                {
                        text2,
                        silver,
                        " ",
                        Lang.inter[17].Value,
                        " "
                });
            }
            if (copper > 0)
            {
                text2 = string.Concat(new object[]
                {
                        text2,
                        copper,
                        " ",
                        Lang.inter[18].Value,
                        " "
                });
            }
            return text2;
        }
        public static bool TileNearLava(int x, int y, int maxDist)
        {
            for (int i = -maxDist; i <= maxDist; i++)
            {
                for (int j = -maxDist; j <= maxDist; j++)
                {
                    Tile t = Framing.GetTileSafely(x + i, y + j);
                    if (t.lava())  return true;
                }
            }
            return false;
        }
        public static void DrawSplitSentence(string sentence, float drawX, float drawY, float maxWidth, Color color, out float height)
        {
            string[] words = sentence.Split(new char[] { ' ' });
            IList<string> sentenceParts = new List<string>();
            sentenceParts.Add(string.Empty);

            int lineNum = 0;

            foreach (string word in words)
            {
                if (Main.fontMouseText.MeasureString(sentenceParts[lineNum] + word).X > maxWidth)
                {
                    lineNum++;
                    sentenceParts.Add(string.Empty);
                }
                sentenceParts[lineNum] += word + " ";
            }
            string message = "";
            foreach (string x in sentenceParts)
                message += x + "\n";
            Utils.DrawBorderStringFourWay(Main.spriteBatch, Main.fontMouseText, message, drawX, drawY, color, Color.Black, new Vector2(0.3f), 1f);
            height = Main.fontMouseText.MeasureString(message).Y - Main.fontMouseText.MeasureString("k").Y;
        }
        public static int MeasureSplitSentenceLines(string sentence, float maxWidth)
        {
            string[] words = sentence.Split(new char[] { ' ' });
            IList<string> sentenceParts = new List<string>();
            sentenceParts.Add(string.Empty);

            int lineNum = 0;

            foreach (string word in words)
            {
                if (Main.fontMouseText.MeasureString(sentenceParts[lineNum] + word).X > maxWidth)
                {
                    lineNum++;
                    sentenceParts.Add(string.Empty);
                }
                sentenceParts[lineNum] += word + " ";
            }
            int numLines = 0;
            foreach (string x in sentenceParts)
                numLines++;
            return numLines;
        }
        public static float MeasureSplitSentenceHeight(string sentence, float maxWidth)
        {
            string[] words = sentence.Split(new char[] { ' ' });
            IList<string> sentenceParts = new List<string>();
            sentenceParts.Add(string.Empty);

            int lineNum = 0;

            foreach (string word in words)
            {
                if (Main.fontMouseText.MeasureString(sentenceParts[lineNum] + word).X > maxWidth)
                {
                    lineNum++;
                    sentenceParts.Add(string.Empty);
                }
                sentenceParts[lineNum] += word + " ";
            }
            string message = "";
            foreach (string x in sentenceParts)
                message += x + "\n";
            return Main.fontMouseText.MeasureString(message).Y - Main.fontMouseText.MeasureString("k").Y;
        }
        public static void MergeOtherPlateauTiles(int type)
        {
            Main.tileMerge[type][TileType<ActiveIgneousRock>()] = true;
            Main.tileMerge[type][TileType<IgneousRock>()] = true;
            Main.tileMerge[type][TileType<MalignantFlesh>()] = true;
            Main.tileMerge[type][TileType<SpiderDoorSpawner>()] = true;
            Main.tileMerge[type][TileType<SulfuricBricks>()] = true;
            Main.tileMerge[type][TileType<SulfuricBricksUnsafe>()] = true;
            Main.tileMerge[type][TileType<SulfuricSediment>()] = true;
            Main.tileMerge[type][TileType<SulfuricSlate>()] = true;
            Main.tileMerge[type][TileType<SulfuricQuicksand>()] = true;
            Main.tileMerge[type][TileType<ScarletiteTile>()] = true;
            Main.tileMerge[type][TileType<PyroclasticRock>()] = true;
            Main.tileMerge[type][TileType<PyroclasticRockUnsafe>()] = true;
            Main.tileMerge[type][TileType<MineBossSpawner>()] = true;
            Main.tileMerge[type][TileType<SpiderCubeSpawner>()] = true;
        }
        public static void PushOtherEntities(object obj, List<int> otherTypes = null, float pushStrength = 0.05f)
        {
            if (obj is Projectile projectile)
            {
                if (otherTypes == null) otherTypes = new List<int>();
                for (int k = 0; k < Main.maxProjectiles; k++)
                {
                    Projectile other = Main.projectile[k];
                    if (k != projectile.whoAmI && (other.type == projectile.type || otherTypes.Contains(projectile.type)) && other.active && Math.Abs(projectile.position.X - other.position.X) + Math.Abs(projectile.position.Y - other.position.Y) < projectile.width)
                    {
                        if (projectile.position.X < other.position.X)
                        {
                            projectile.velocity.X -= pushStrength;
                        }
                        else
                        {
                            projectile.velocity.X += pushStrength;
                        }
                        if (projectile.position.Y < other.position.Y)
                        {
                            projectile.velocity.Y -= pushStrength;
                        }
                        else
                        {
                            projectile.velocity.Y += pushStrength;
                        }
                    }
                }
            }
            if (obj is NPC npc)
            {
                if (otherTypes == null) otherTypes = new List<int>();
                for (int k = 0; k < Main.maxNPCs; k++)
                {
                    NPC other = Main.npc[k];
                    if (k != npc.whoAmI && (other.type == npc.type || otherTypes.Contains(npc.type)) && other.active && Math.Abs(npc.position.X - other.position.X) + Math.Abs(npc.position.Y - other.position.Y) < npc.width)
                    {
                        if (npc.position.X < other.position.X)
                        {
                            npc.velocity.X -= pushStrength;
                        }
                        else
                        {
                            npc.velocity.X += pushStrength;
                        }
                        if (npc.position.Y < other.position.Y)
                        {
                            npc.velocity.Y -= pushStrength;
                        }
                        else
                        {
                            npc.velocity.Y += pushStrength;
                        }
                    }
                }
            }
        }
        public static void SpecialDoorPushX(NPC npc)
        {
            Player player = Main.LocalPlayer;
            if (npc.Hitbox.Intersects(player.Hitbox))
            {
                int pushDir = Math.Sign(player.Center.X - npc.Center.X);
                float push = (player.Center.X + -pushDir * (player.width / 2)) - (npc.Center.X + pushDir * (npc.width / 2));
                player.position.X -= push;
                player.velocity.X = 0;

                player.dashDelay = 0;
                player.dashTime = 0;
                player.GetModPlayer<MyPlayer>().eaDashDelay = 0;
                player.GetModPlayer<MyPlayer>().eaDashTime = 0;
            }
            for (int p = 0; p < Main.maxProjectiles; p++)
            {
                Projectile proj = Main.projectile[p];
                if (npc.Hitbox.Intersects(proj.Hitbox) && proj.tileCollide && proj.active)
                {
                    proj.Kill();
                }
            }
            for (int p = 0; p < Main.maxNPCs; p++)
            {
                NPC other = Main.npc[p];
                if (npc.Hitbox.Intersects(other.Hitbox) && other.active && !other.noTileCollide)
                {
                    int pushDir = Math.Sign(other.Center.X - npc.Center.X);
                    float push = (other.Center.X + -pushDir * (other.width / 2)) - (npc.Center.X + pushDir * (npc.width / 2));
                    other.position.X -= push;
                    other.velocity.X = 0;
                }
            }
        }
        public static void SpecialDoorPushY(NPC npc)
        {
            Player player = Main.LocalPlayer;
            if (npc.Hitbox.Intersects(player.Hitbox))
            {
                int pushDir = Math.Sign(player.Center.Y - npc.Center.Y);
                float push = (player.Center.Y + -pushDir * (player.height / 2)) - (npc.Center.Y + pushDir * (npc.height / 2));
                player.position.Y -= push;
            }
            for (int p = 0; p < Main.maxProjectiles; p++)
            {
                Projectile proj = Main.projectile[p];
                if (npc.Hitbox.Intersects(proj.Hitbox) && proj.tileCollide && proj.active)
                {
                    proj.Kill();
                }
            }
            for (int p = 0; p < Main.maxNPCs; p++)
            {
                NPC other = Main.npc[p];
                if (npc.Hitbox.Intersects(other.Hitbox) && other.active && !other.noTileCollide)
                {
                    int pushDir = Math.Sign(other.Center.Y - npc.Center.Y);
                    float push = (other.Center.Y + -pushDir * (other.height / 2)) - (npc.Center.Y + pushDir * (npc.height / 2));
                    other.position.Y -= push;
                    other.velocity.Y = 0;
                }
            }
        }
        public static int[] CalculatePotCash(int i, int j, float potValue)
        {
            int[] coins = new int[4];
            float num13 = (float)(200 + WorldGen.genRand.Next(-100, 101));
            if ((double)j < Main.worldSurface)
            {
                num13 *= 0.5f;
            }
            else if ((double)j < Main.rockLayer)
            {
                num13 *= 0.75f;
            }
            else if (j > Main.maxTilesY - 250)
            {
                num13 *= 1.25f;
            }
            num13 *= 1f + (float)Main.rand.Next(-20, 21) * 0.01f;
            if (Main.rand.Next(4) == 0)
            {
                num13 *= 1f + (float)Main.rand.Next(5, 11) * 0.01f;
            }
            if (Main.rand.Next(8) == 0)
            {
                num13 *= 1f + (float)Main.rand.Next(10, 21) * 0.01f;
            }
            if (Main.rand.Next(12) == 0)
            {
                num13 *= 1f + (float)Main.rand.Next(20, 41) * 0.01f;
            }
            if (Main.rand.Next(16) == 0)
            {
                num13 *= 1f + (float)Main.rand.Next(40, 81) * 0.01f;
            }
            if (Main.rand.Next(20) == 0)
            {
                num13 *= 1f + (float)Main.rand.Next(50, 101) * 0.01f;
            }
            if (Main.expertMode)
            {
                num13 *= 2.5f;
            }
            if (Main.expertMode && Main.rand.Next(2) == 0)
            {
                num13 *= 1.25f;
            }
            if (Main.expertMode && Main.rand.Next(3) == 0)
            {
                num13 *= 1.5f;
            }
            if (Main.expertMode && Main.rand.Next(4) == 0)
            {
                num13 *= 1.75f;
            }
            num13 *= potValue;
            if (NPC.downedBoss1)
            {
                num13 *= 1.1f;
            }
            if (NPC.downedBoss2)
            {
                num13 *= 1.1f;
            }
            if (NPC.downedBoss3)
            {
                num13 *= 1.1f;
            }
            if (NPC.downedMechBoss1)
            {
                num13 *= 1.1f;
            }
            if (NPC.downedMechBoss2)
            {
                num13 *= 1.1f;
            }
            if (NPC.downedMechBoss3)
            {
                num13 *= 1.1f;
            }
            if (NPC.downedPlantBoss)
            {
                num13 *= 1.1f;
            }
            if (NPC.downedQueenBee)
            {
                num13 *= 1.1f;
            }
            if (NPC.downedGolemBoss)
            {
                num13 *= 1.1f;
            }
            if (NPC.downedPirates)
            {
                num13 *= 1.1f;
            }
            if (NPC.downedGoblins)
            {
                num13 *= 1.1f;
            }
            if (NPC.downedFrost)
            {
                num13 *= 1.1f;
            }
            while ((int)num13 > 0)
            {
                if (num13 > 1000000f)
                {
                    int num14 = (int)(num13 / 1000000f);
                    if (num14 > 50 && Main.rand.Next(2) == 0)
                    {
                        num14 /= Main.rand.Next(3) + 1;
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        num14 /= Main.rand.Next(3) + 1;
                    }
                    num13 -= (float)(1000000 * num14);
                    coins[3] = num14;
                }
                else if (num13 > 10000f)
                {
                    int num15 = (int)(num13 / 10000f);
                    if (num15 > 50 && Main.rand.Next(2) == 0)
                    {
                        num15 /= Main.rand.Next(3) + 1;
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        num15 /= Main.rand.Next(3) + 1;
                    }
                    num13 -= (float)(10000 * num15);
                    coins[2] = num15;
                }
                else if (num13 > 100f)
                {
                    int num16 = (int)(num13 / 100f);
                    if (num16 > 50 && Main.rand.Next(2) == 0)
                    {
                        num16 /= Main.rand.Next(3) + 1;
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        num16 /= Main.rand.Next(3) + 1;
                    }
                    num13 -= (float)(100 * num16);
                    coins[1] = num16;
                }
                else
                {
                    int num17 = (int)num13;
                    if (num17 > 50 && Main.rand.Next(2) == 0)
                    {
                        num17 /= Main.rand.Next(3) + 1;
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        num17 /= Main.rand.Next(4) + 1;
                    }
                    if (num17 < 1)
                    {
                        num17 = 1;
                    }
                    num13 -= (float)num17;
                    coins[0] = num17;
                }
            }
            return coins;
        }
        public static int FindNumTilesNearby(int type, int i, int j, int radius)
        {
            int num = 0;
            for (int x = i - radius; x <= i + radius; x++)
            {
                for (int y = j - radius; y <= j + radius; y++)
                {
                    int newX = x;
                    int newY = y;
                    if (newX < 0) newX = 0;
                    if (newX > Main.maxTilesX) newX = Main.maxTilesX;
                    if (newY < 0) newY = 0;
                    if (newY > Main.maxTilesY) newY = Main.maxTilesY;
                    if (Framing.GetTileSafely(newX, newY).active() && Framing.GetTileSafely(newX, newY).type == type)
                    {
                        num++;
                    }
                }
            }
            return num;
        }
        public static int FindNumTilesNearby(List<int> types, int i, int j, int radius)
        {
            int num = 0;
            for (int x = i - radius; x <= i + radius; x++)
            {
                for (int y = j - radius; y <= j + radius; y++)
                {
                    int newX = x;
                    int newY = y;
                    if (newX < 0) newX = 0;
                    if (newX > Main.maxTilesX) newX = Main.maxTilesX;
                    if (newY < 0) newY = 0;
                    if (newY > Main.maxTilesY) newY = Main.maxTilesY;
                    if (Framing.GetTileSafely(newX, newY).active() && types.Contains(Framing.GetTileSafely(newX, newY).type))
                    {
                        num++;
                    }
                }
            }
            return num;
        }
        public static void OutwardsCircleDust(Vector2 pos, int dustID, int numDusts, float vel, bool fromCenter = false, int dustAlpha = 0, bool randomiseVel = false, float dustScale = 1.5f, float dustFadeIn = 0)
        {
            for (int i = 0; i < numDusts; i++)
            {
                Vector2 position = Vector2.One.RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + pos;
                Vector2 velocity = position - pos;
                Vector2 spawnPos = position + (fromCenter ? Vector2.Zero : velocity);
                Dust dust = Main.dust[Dust.NewDust(spawnPos, 0, 0, dustID, velocity.X * 2f, velocity.Y * 2f, dustAlpha, default(Color), dustScale)];
                dust.noGravity = true;
                dust.velocity = Vector2.Normalize(velocity) * vel * (randomiseVel ? Main.rand.NextFloat(0.8f, 1.2f) : 1);
                dust.fadeIn = dustFadeIn;
            }
        }
    }
}
