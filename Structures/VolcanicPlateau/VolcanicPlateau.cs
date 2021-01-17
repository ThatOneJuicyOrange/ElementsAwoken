using BaseMod;
using ElementsAwoken.BaseMod;
using ElementsAwoken.Tiles.VolcanicPlateau;
using ElementsAwoken.Tiles.VolcanicPlateau.Decor;
using ElementsAwoken.Tiles.VolcanicPlateau.Objects;
using ElementsAwoken.Tiles.VolcanicPlateau.ObjectSpawners;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Structures.VolcanicPlateau
{
    public class VolcanicPlateau
    {
        private static int primaryTile = TileType<IgneousRock>();
        private static int streakTile = TileType<ActiveIgneousRock>();
        private static int secondaryTile = TileType<MalignantFlesh>();
        private static int lakeTile = TileType<PyroclasticRockUnsafe>();

        private static int primarySulphurTile = TileType<SulfuricSediment>();
        private static int secondarySulphurTile = TileType<SulfuricSlate>();
        public static void Generate(int x, int y)
        {
            GenTiles(x, y);
            GeneratePlateaus(x, y);
            RoofToBlocks(x, y);
            GenerateDirtPatches(x, y);
            GenerateLavaPatches(x, y);
            GenerateScarletite(x, y);
            SpreadAshGrass(x, y);
            Tree(x, y);
            ActiveStreaks(x, y);
            SilkenCaverns(x, y);
            QuicksandPatches(x, y);
        }
        public static void GenerateStructures(int x, int y)
        {
            ReplacePots(x, y);
            LakeArch(x, y);
            Mine(x + 260, y + 130, 40, 1);
            Mine(x + 260, y + 130, 40, -1);
            GenMineBossRoom(x + 260, y + 130 + 8);
            CindariShrine(x, y);
            Well(x, y);
            Spike(x, y);
            Temple(x, y);
            MiniTemples(x, y);
            ReplaceDecor(x, y);
            Geysers(x, y);
        }
        // draining
        public static void DrainLava(int x, int y)
        {
            ReducePlateauLava(x, y);
        }
        public static void PostMLChanges(int x, int y)
        {
            GenAntNest(x, y);
            DrainAntNest(x, y);
            RemoveAcidWeb(x, y);
        }
        private static void GenAntNest(int x, int y)
        {
            int nestStartX = EAWorldGen.plateauWidth - 208;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(163, 74, 83)] = primaryTile;
            colorToTile[new Color(0, 165, 255)] = primarySulphurTile;
            colorToTile[new Color(0, 255, 97)] = secondarySulphurTile;
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1; //don't touch when genning

            Texture2D tex = GetTexture("ElementsAwoken/Structures/VolcanicPlateau/AntHill");
            TexGen gen = BaseWorldGenTex.GetTexGenerator(tex, colorToTile);
            gen.Generate(x + nestStartX, y, true, true, true);
            SlopeTiles(x + nestStartX, y, tex.Width, tex.Height);
        }
        private static void DrainAntNest(int x, int y)
        {
            int nestStartX = EAWorldGen.plateauWidth - 208;
            int nestStart = 67;
            for (int i = 0; i < 159; i++)
            {
                for (int j = 0; j < 200 - nestStart; j++)
                {
                    Point pos = new Point(x + nestStartX + i, y + nestStart + j);
                    if (Main.tile[pos.X, pos.Y].lava())
                    {
                        Main.tile[pos.X, pos.Y].lava(false);
                        Main.tile[pos.X, pos.Y].liquid = 0;
                    }
                }
            }
        }
        private static void RemoveAcidWeb(int x, int y)
        {
            x += 828;
            y += 60;
            for (int i = 0; i < 500; i++)
            {
                for (int j = y; j < Main.maxTilesY; j++)
                {
                    Point pos = new Point(x + i, j);
                    if (Main.tile[pos.X, pos.Y].type == TileType<AcidWeb>())
                    {
                        WorldGen.KillTile(pos.X, pos.Y, noItem: true);
                    }
                }
            }
        }
        private static void ReducePlateauLava(int x, int y)
        {
            for (int i = 0; i < EAWorldGen.plateauWidth; i++)
            {
                if (i < 510)
                {
                    for (int j = 0; j < 110; j++)
                    {
                        bool remove = WorldGen.genRand.NextBool();
                        if (remove)
                        {
                            Point pos = new Point(x + i, y + j);
                            if (Main.tile[pos.X, pos.Y].lava())
                            {
                                Main.tile[pos.X, pos.Y].lava(false);
                                Main.tile[pos.X, pos.Y].liquid = 0;
                            }
                        }
                    }
                }
                if (i > 860)
                {
                    for (int j = 0; j < 200; j++)
                    {
                        Point pos = new Point(x + i, y + j);
                        bool remove = WorldGen.genRand.Next(100) < 75;
                        if (j > 110) remove = !Main.tileSolid[Framing.GetTileSafely(pos.X, pos.Y + 1).type];
                        if (remove)
                        {
                            if (Main.tile[pos.X, pos.Y].lava())
                            {
                                Main.tile[pos.X, pos.Y].lava(false);
                                Main.tile[pos.X, pos.Y].liquid = 0;
                            }
                        }
                    }
                }
            }
        }
        private static void SpreadAshGrass(int x, int y)
        {
            for (int i = 0; i < EAWorldGen.plateauWidth; i++)
            {
                for (int j = 0; j < 200; j++)
                {
                    Point tilePoint = new Point(x + i, y + j);
                    Tile t = Framing.GetTileSafely(tilePoint.X, tilePoint.Y);
                    if (t.type == secondaryTile)
                    {
                        Tile below = Framing.GetTileSafely(tilePoint.X, tilePoint.Y + 1);
                        Tile above = Framing.GetTileSafely(tilePoint.X, tilePoint.Y - 1);
                        Tile right = Framing.GetTileSafely(tilePoint.X + 1, tilePoint.Y);
                        Tile left = Framing.GetTileSafely(tilePoint.X - 1, tilePoint.Y);
                        if (((!Main.tileSolid[below.type] && below.active()) || !below.active()) ||
                            ((!Main.tileSolid[above.type] && above.active()) || !above.active()) ||
                            ((!Main.tileSolid[right.type] && right.active()) || !right.active()) ||
                            ((!Main.tileSolid[left.type] && left.active()) || !left.active())) t.type = (ushort)TileType<AshGrass>();
                    }
                }
            }
        }
        // gen
        private static void GeneratePlateaus(int x, int y)
        {
            int genSpot = 0;
            while (genSpot < 480)
            {
                int texNum = 0;
                genSpot += WorldGen.genRand.Next(10, 80);
                if (genSpot > 480) break;
                if (genSpot > 380) texNum = WorldGen.genRand.Next(4);
                else if (genSpot > 340) texNum = WorldGen.genRand.Next(6);
                else texNum = WorldGen.genRand.Next(7);
                GenPlateaus(x + genSpot, y + 111, GetTexture("ElementsAwoken/Structures/VolcanicPlateau/Plateau" + texNum));
            }
            /*int numPlateaus = 0;
            while (numPlateaus < 6)
            {
                for (int k = 0; k < 500; k++)
                {
                    if (k < 14) continue;
                    if (WorldGen.genRand.NextBool(60))
                    {
                        int texNum = k < 400 ? WorldGen.genRand.Next(7) : WorldGen.genRand.Next(4);
                        GenPlateaus(x + k, y + 111, GetTexture("ElementsAwoken/Structures/VolcanicPlateau/Plateau" + texNum));
                        numPlateaus++;
                    }
                }
            }*/
        }
        private static void GenerateLavaPatches(int i, int j)
        {
            for (int k = 0; k < 60; k++)
            {
                int x = WorldGen.genRand.Next(515);
                int y = WorldGen.genRand.Next(200);

                //if (x < 515 || x > 845)
                {
                    x += i;
                    y += j;
                    LiquidPatch(x, y, WorldGen.genRand.Next(4, 9), WorldGen.genRand.Next(7, 11), new List<int>() { primaryTile, primarySulphurTile });
                    TilePatch(x, y, WorldGen.genRand.Next(9, 15), WorldGen.genRand.Next(14, 18), -2, new List<int>() { primaryTile, primarySulphurTile });
                }
            }
            for (int k = 0; k < 80; k++)
            {
                int x = WorldGen.genRand.Next(515);
                int y = WorldGen.genRand.Next(200);

                //if (x < 515 || x > 845)
                {
                    x += i;
                    y += j;
                    LiquidPatch(x, y, WorldGen.genRand.Next(3, 5), WorldGen.genRand.Next(3, 7), new List<int>() { primaryTile, primarySulphurTile });
                }
            }
        }
        private static void GenerateDirtPatches(int i, int j)
        {
            for (int k = 0; k < 120; k++)
            {
                int x = WorldGen.genRand.Next(515);
                int y = WorldGen.genRand.Next(200);
                x += i;
                y += j;
                TilePatch(x, y, WorldGen.genRand.Next(12, 24), WorldGen.genRand.Next(9, 18), secondaryTile, new List<int>() { primaryTile });

            }
            for (int k = 0; k < 80; k++)
            {
                int x = WorldGen.genRand.Next(845, EAWorldGen.plateauWidth);
                int y = 60 + WorldGen.genRand.Next(100);
                x += i;
                y += j;
                TilePatch(x, y, WorldGen.genRand.Next(12, 24), WorldGen.genRand.Next(9, 18), primarySulphurTile, new List<int>() { secondarySulphurTile });
            }
        }
        private static void GenerateScarletite(int i, int j)
        {
            for (int k = 0; k < 70; k++)
            {
                int x = WorldGen.genRand.Next(515);
                int y = WorldGen.genRand.Next(200);
                x += i;
                y += j;
                TilePatch(x, y, WorldGen.genRand.Next(5, 7), WorldGen.genRand.Next(2, 12), TileType<ScarletiteTile>(), new List<int>() { primaryTile, secondaryTile });
            }
            for (int k = 0; k < 70; k++)
            {
                int x = 507 + WorldGen.genRand.Next(352);
                int y = 85 + WorldGen.genRand.Next(115);
                x += i;
                y += j;
                TilePatch(x, y, WorldGen.genRand.Next(9, 13), WorldGen.genRand.Next(6, 14), TileType<ScarletiteTile>(), new List<int>() { primaryTile, secondaryTile });
            }
        }
        private static void ActiveStreaks(int i, int j)
        {
            for (int k = 0; k < 80; k++)
            {
                int x = WorldGen.genRand.Next(EAWorldGen.plateauWidth);
                int y = WorldGen.genRand.Next(200);
                x += i;
                y += j;
                Streak(x, y, WorldGen.genRand.Next(12, 30), streakTile, new List<int>() { primaryTile, secondaryTile });
            }
        }
        private static void RoofToBlocks(int x, int y)
        {
            List<int> allowedBlocks = new List<int>() { primaryTile, secondaryTile, primarySulphurTile, secondarySulphurTile };
            for (int i = 0; i < EAWorldGen.plateauWidth; i++)
            {
                for (int j = 0; j < 60; j++)
                {
                    Point pos = new Point(x + i, y + j);
                    Tile t = Main.tile[pos.X, pos.Y];
                    if (t.type == TileID.Ash) t.type = (ushort)primaryTile;
                    if (j > 25 && !allowedBlocks.Contains(t.type))
                    {
                        t.type = 0;
                        t.active(false);
                    }
                }
            }
        }
        private static void SilkenCaverns(int x, int y)
        {
            int num = 30;
            Vector2[] cavePositions = new Vector2[num];
            for (int p = 0; p < num; p++)
            {
                int i = x + WorldGen.genRand.Next(872, EAWorldGen.plateauWidth - 36);
                int j = y + WorldGen.genRand.Next(77, 200);
                bool tooClose = true;
                int trials = 0;
                while (tooClose && trials < 5000)
                {
                    trials++;
                    i = x + WorldGen.genRand.Next(872, EAWorldGen.plateauWidth - 36);
                    j = y + WorldGen.genRand.Next(77, 200);
                    for (int d = 0; d < num; d++)
                    {
                        if (d == p) continue;
                        Vector2 curr = new Vector2(i, j);
                        Vector2 other = cavePositions[d];
                        if (Vector2.Distance(curr, other) > 50) tooClose = false;
                    }
                }
                if (tooClose) continue; // stop if its still too close
                int radiusX = WorldGen.genRand.Next(15, 30);
                int radiusY = WorldGen.genRand.Next(15, 20);

                cavePositions[p] = new Vector2(i, j + radiusY);

                for (int roundY = 0; roundY < radiusY * 2; roundY++) // we work downwards
                {
                    float ratio = roundY / (float)(radiusY * 2); // This will give us a ratio from 0 to 1, depending on how far down we are
                    int width = (int)(Math.Sin(ratio * Math.PI) * radiusX * 2); // This will give us the width of the ellipsoid at height y
                    width += WorldGen.genRand.Next(-2, 2);
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
                    }
                }
            }
            // loop through all and create a tunnel between them
            for (int p = 0; p < num; p++)
            {
                if (p != 0)
                {
                    Vector2 curr = cavePositions[p];
                    Vector2 closest = Vector2.Zero;
                    Vector2 middle = Vector2.Zero;
                    for (int z = 0; z < num; z++)
                    {
                        if (z == p) continue;
                        Vector2 other = cavePositions[z];
                        if (Vector2.Distance(curr, other) < Vector2.Distance(closest, curr)) closest = other;
                        if (Vector2.Distance(curr, other) < Vector2.Distance(middle, curr) && Vector2.Distance(curr, other) > 50) middle = other;
                    }
                    DigTunnel(curr, closest, WorldGen.genRand.Next(4, 7));
                    DigTunnel(curr, middle, WorldGen.genRand.Next(4, 7));
                }
            }
            for (int p = 0; p < 140; p++)
            {
                int i = x + WorldGen.genRand.Next(872, EAWorldGen.plateauWidth - 36);
                int j = y + WorldGen.genRand.Next(77, 200);
                Webs(i, j);
            }
        }
        private static void DigTunnel(Vector2 start, Vector2 end, int tunnelHeight)
        {
            float dist = Vector2.Distance(start, end);
            for (float d = 0; d < 1; d += 1f / (float)dist)
            {
                Vector2 tile = Vector2.Lerp(start, end, d);
                for (int i = -tunnelHeight / 2; i < tunnelHeight / 2; i++)
                {
                    for (int j = -tunnelHeight / 2; j < tunnelHeight / 2; j++)
                    {
                        WorldGen.KillTile((int)tile.X + i, (int)tile.Y + j);
                    }
                }
            }
            // the hell i didnt know there was vector2.lerp
            /*int distX = (int)(end.X - start.X);
            int distY = (int)(end.Y - start.Y);
            int dirX = Math.Sign(distX);
            int dirY = Math.Sign(distY);
            float ratio = Math.Abs((float)distY / (float)distX);
            if (ratio > tunnelHeight) ratio = tunnelHeight; // untested
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
                        WorldGen.KillTile(x + i, y2 + j);
                    }
                }
            }*/
        }
        public static void QuicksandPatches(int x, int y)
        {
            int genSpot = x + 900;
            while (genSpot < x + EAWorldGen.plateauWidth - 36)
            {
                int radiusX = WorldGen.genRand.Next(4, 15);
                int radiusY = WorldGen.genRand.Next(2, 6);

                int i = genSpot;
                genSpot += WorldGen.genRand.Next(radiusX + 10, 120);
                int j = Main.maxTilesY - 200 + 30;
                for (int t = 0; t < 200; t++)
                {
                    Tile tile = Framing.GetTileSafely(i, j + t);
                    if (tile.active() && tile.type == primarySulphurTile)
                    {
                        j += t - radiusY / 2;
                        break;
                    }
                }
                for (int z = 0; z < radiusY * 2; z++) // we work downwards
                {
                    float ratio = z / (float)(radiusY * 2); // This will give us a ratio from 0 to 1, depending on how far down we are
                    int width = (int)(Math.Sin(ratio * Math.PI) * radiusX * 2); // This will give us the width of the ellipsoid at height y

                    for (int q = -width / 2; q < width / 2; q++)
                    {
                        Vector2 tilePosition = new Vector2(i, j) + new Vector2(q, z);
                        Tile t = Framing.GetTileSafely((int)tilePosition.X, (int)tilePosition.Y);
                        if (t.type == primarySulphurTile)
                        {
                            WorldGen.KillTile((int)tilePosition.X, (int)tilePosition.Y);
                            WorldGen.PlaceTile((int)tilePosition.X, (int)tilePosition.Y, TileType<SulfuricQuicksand>());
                        }
                    }
                }
            }
        }
        // structures
        private static void Tree(int x, int y)
        {
            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(163, 74, 83)] = primaryTile;
            colorToTile[new Color(0, 165, 255)] = primarySulphurTile;
            colorToTile[new Color(0, 255, 97)] = secondarySulphurTile;
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1; //don't touch when genning

            x += WorldGen.genRand.Next(900, 1100);
            for (int j = 40; j < 60; j++)
            {
                if (Main.tile[x, y + j].active() && Main.tileSolid[Main.tile[x, y + j].type])
                {
                    y += j;
                    break;
                }
            }
            TexGen gen = BaseWorldGenTex.GetTexGenerator(GetTexture("ElementsAwoken/Structures/VolcanicPlateau/Tree"), colorToTile);
            gen.Generate(x - (gen.width / 2), y - 61, true, true);
        }
        private static void LakeArch(int x, int y)
        {
            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(0, 0, 255)] = TileID.IridescentBrick;
            colorToTile[new Color(255, 255, 0)] = TileID.GoldBrick;
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1; //don't touch when genning

            x += 499;
            y -= 20;
            Texture2D tex = GetTexture("ElementsAwoken/Structures/VolcanicPlateau/LakeArch");

            TexGen gen = BaseWorldGenTex.GetTexGenerator(tex, colorToTile);
            gen.Generate(x, y, true, true, true);
            for (int i = 0; i < tex.Width; i++)
            {
                for (int j = 0; j < tex.Height; j++)
                {
                    int tX = x + i;
                    int tY = y + j;
                    if (Main.tile[tX, tY].type == TileID.GoldBrick)
                    {
                        WorldGen.KillTile(tX, tY);
                        WorldGen.PlaceObject(tX, tY, TileType<Tiles.Objects.VolcanoxShrine>());
                    }
                }
            }
            PlaceBezierRope(new Point(x + 167, y + 39), new Point(x + 113, y + 17), TileType<Tiles.BezierRope.ChainRope>());
            PlaceBezierRope(new Point(x + 186, y + 39), new Point(x + 242, y + 17), TileType<Tiles.BezierRope.ChainRope>());
            PlaceBezierRope(new Point(x + 163, y + 46), new Point(x + 61, y + 26), TileType<Tiles.BezierRope.ChainRope>());
            PlaceBezierRope(new Point(x + 190, y + 46), new Point(x + 294, y + 26), TileType<Tiles.BezierRope.ChainRope>());

        }
        private static void CindariShrine(int x, int y)
        {
            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(255, 255, 0)] = TileType<Tiles.CindariShrineBlock>();
            colorToTile[new Color(26, 26, 26)] = TileID.ObsidianBrick;
            colorToTile[new Color(255, 100, 0)] = TileID.ObsidianBrick;
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1; //don't touch when genning

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(85, 101, 66)] = WallID.MetalFence;
            colorToWall[new Color(15, 15, 15)] = WallID.ObsidianBrickUnsafe;
            colorToWall[new Color(60, 30, 30)] = WallID.HellstoneBrickUnsafe;
            colorToWall[new Color(150, 150, 150)] = 0; //turn into air
            colorToWall[Color.Black] = -1; //don't touch when genning

            Dictionary<Color, int> colorToSlope = new Dictionary<Color, int>();
            colorToSlope[new Color(0, 11, 154)] = 2;
            colorToSlope[new Color(151, 0, 154)] = 1;
            colorToSlope[new Color(150, 150, 150)] = 0;
            colorToSlope[Color.Black] = -2; //don't touch when genning

            Dictionary<Color, int> colorToPlatform = new Dictionary<Color, int>();
            colorToPlatform[new Color(191, 142, 111)] = 13;
            colorToSlope[Color.Black] = 0;

            Dictionary<Color, int> colorToFurniture = new Dictionary<Color, int>();
            colorToTile[new Color(170, 48, 114)] = TileID.Books;
            colorToSlope[Color.Black] = -1;

            x += WorldGen.genRand.Next(160, 360);
            y += WorldGen.genRand.Next(118, 120);
            Texture2D tex = GetTexture("ElementsAwoken/Structures/VolcanicPlateau/CindariShrine");

            TexGen gen = BaseWorldGenTex.GetTexGenerator(GetTexture("ElementsAwoken/Structures/VolcanicPlateau/CindariShrine"), colorToTile, GetTexture("ElementsAwoken/Structures/VolcanicPlateau/CindariShrineWalls"), colorToWall, slopeTex: GetTexture("ElementsAwoken/Structures/VolcanicPlateau/CindariShrineSlopes"), colorToSlopes: colorToSlope, platformStyle: 13);
            gen.Generate(x - (gen.width / 2), y - (gen.height / 2), true, true);
            TexGen genPlats = BaseWorldGenTex.GetPlatformTexGenerator(GetTexture("ElementsAwoken/Structures/VolcanicPlateau/CindariShrinePlatforms"), colorToPlatform);
            genPlats.Generate(x - (gen.width / 2), y - (gen.height / 2), true, true);
            for (int i = 0; i < tex.Width; i++)
            {
                for (int j = 0; j < tex.Height; j++)
                {
                    int bookX = x + i - gen.width / 2;
                    int bookY = y + j - gen.height / 2;
                    if (Main.tile[bookX, bookY + 1].type == TileID.Platforms) WorldGen.PlaceTile(bookX, bookY, TileID.Books);
                }
            }
        }
        private static void Well(int x, int y)
        {
            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(26, 26, 26)] = TileID.ObsidianBrick;
            colorToTile[new Color(150, 67, 22)] = TileID.CopperPlating;
            colorToTile[new Color(103, 103, 103)] = TileID.Chain;
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1; //don't touch when genning

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(85, 101, 66)] = WallID.MetalFence;
            colorToWall[new Color(11, 11, 11)] = WallID.ObsidianBrickUnsafe;
            colorToWall[new Color(150, 150, 150)] = 0; //turn into air
            colorToWall[Color.Black] = -1; //don't touch when genning

            Dictionary<Color, int> colorToSlope = new Dictionary<Color, int>();
            colorToSlope[new Color(60, 255, 0)] = 1;
            colorToSlope[new Color(206, 0, 255)] = 2;
            colorToSlope[new Color(0, 30, 255)] = 3;
            colorToSlope[new Color(0, 255, 255)] = 4;
            colorToSlope[new Color(150, 150, 150)] = 0;
            colorToSlope[Color.Black] = -2; //don't touch when genning

            // probably very inefficient
            x += WorldGen.genRand.Next(40, 450);
            // find first tile
            for (int j = 55; j < 110; j++)
            {
                if (Main.tile[x, y + j].active() && Main.tileSolid[Main.tile[x, y + j].type])
                {
                    y += j;
                    break;
                }
            }
            TexGen gen = BaseWorldGenTex.GetTexGenerator(GetTexture("ElementsAwoken/Structures/VolcanicPlateau/Well"), colorToTile, GetTexture("ElementsAwoken/Structures/VolcanicPlateau/WellWalls"), colorToWall, GetTexture("ElementsAwoken/Structures/VolcanicPlateau/WellLava"), null, GetTexture("ElementsAwoken/Structures/VolcanicPlateau/WellSlopes"), colorToSlope);
            gen.Generate(x - (gen.width / 2), y - 9, true, true);
        }
        public static void Temple(int x, int y)
        {
            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(46, 51, 25)] = TileType<SulfuricBricksUnsafe>();
            colorToTile[new Color(255, 0, 0)] = TileType<SpiderDoorSpawner>();
            colorToTile[new Color(169, 21, 145)] = TileID.Marble; // fucking furniture
            colorToTile[new Color(255, 255, 0)] = TileID.PlatinumBrick; // fucking furniture
            colorToTile[new Color(0, 0, 255)] = TileID.Gold; // fucking furniture
            colorToTile[new Color(0, 165, 255)] = primarySulphurTile;
            colorToTile[new Color(0, 255, 97)] = secondarySulphurTile;
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1; //don't touch when genning


            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(46, 51, 25)] = WallType<SulfuricBrickWall>();
            colorToWall[new Color(150, 150, 150)] = 0; //turn into air
            colorToWall[Color.Black] = -1; //don't touch when genning

            x += WorldGen.genRand.Next(890, 910);
            y += 88;
            EAWorldGen.spiderTempleLoc.X = x;
            EAWorldGen.spiderTempleLoc.Y = y;
            Texture2D tex = GetTexture("ElementsAwoken/Structures/VolcanicPlateau/SulfuricTemple");
            Texture2D wallTex = GetTexture("ElementsAwoken/Structures/VolcanicPlateau/SulfuricTempleWalls");
            TexGen gen = BaseWorldGenTex.GetTexGenerator(tex, colorToTile, wallTex, colorToWall);
            gen.Generate(x, y, true, true, true);
            // lava removal
            for (int i = x; i < x + tex.Width; i++)
            {
                for (int j = y + tex.Height; j > y; j--)
                {
                    Tile t = Framing.GetTileSafely(i, j);
                    t.lava(false);
                    t.liquid = 0;
                }
            }
            for (int i = x; i < x + tex.Width; i++)
            {
                for (int j = y + tex.Height; j > y; j--)
                {
                    Tile tM = Framing.GetTileSafely(i, j);
                    if (tM.type == TileID.Marble)
                    {
                        WorldGen.KillTile(i, j);
                        WorldGen.KillTile(i - 1, j);
                        WorldGen.KillTile(i + 1, j);
                        WorldGen.PlaceObject(i, j, TileType<SulfuricPillar>(), true);
                    }
                    if (tM.type == TileID.PlatinumBrick)
                    {
                        WorldGen.KillTile(i, j);
                        WorldGen.PlaceObject(i, j, TileType<EriusCrystal>(), true);
                    }
                    if (tM.type == TileID.Gold)
                    {
                        WorldGen.KillTile(i, j);
                        WorldGen.PlaceObject(i, j, TileType<SpiderPressurePlate>(), true);
                    }
                }
            }
        }
        private static void Spike(int x, int y)
        {
            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(26, 26, 26)] = TileID.ObsidianBrick;
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1; //don't touch when genning

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(85, 101, 66)] = WallID.MetalFence;
            colorToWall[new Color(11, 11, 11)] = WallID.ObsidianBrickUnsafe;
            colorToWall[new Color(150, 150, 150)] = 0; //turn into air
            colorToWall[Color.Black] = -1; //don't touch when genning

            Dictionary<Color, int> colorToSlope = new Dictionary<Color, int>();
            colorToSlope[new Color(60, 255, 0)] = 1;
            colorToSlope[new Color(206, 0, 255)] = 2;
            colorToSlope[new Color(0, 30, 255)] = 3;
            colorToSlope[new Color(0, 255, 255)] = 4;
            colorToSlope[new Color(150, 150, 150)] = 0;
            colorToSlope[Color.Black] = -2; //don't touch when genning


            int genSpot = 0;
            int startY = y;
            while (genSpot < 450)
            {
                genSpot += WorldGen.genRand.Next(15, 180);
                if (genSpot > 450) break;
                y = startY;
                for (int j = 38; j < 110; j++)
                {
                    if (Main.tile[x + genSpot, y + j].active() && Main.tileSolid[Main.tile[x + genSpot, y + j].type])
                    {
                        y += j;
                        break;
                    }
                }
                int spikeNum = WorldGen.genRand.Next(3);
                TexGen gen = BaseWorldGenTex.GetTexGenerator(GetTexture("ElementsAwoken/Structures/VolcanicPlateau/Spike" + spikeNum), colorToTile, GetTexture("ElementsAwoken/Structures/VolcanicPlateau/SpikeWalls" + spikeNum), colorToWall, null, null, GetTexture("ElementsAwoken/Structures/VolcanicPlateau/SpikeSlopes" + spikeNum), colorToSlope);
                gen.Generate(x + genSpot - (gen.width / 2), y - 14, true, true);
            }
        }
        private static void MiniTemples(int x, int y)
        {
            int num = 4;
            int[] xPos = new int[num];
            int[] yPos = new int[num];
            for (int n = 0; n < num; n++)
            {
                bool tooClose = true;
                bool gen = true;
                int i = 0;
                int j = 0;
                int trials = 0;
                while (tooClose)
                {
                    i = WorldGen.genRand.Next(900, EAWorldGen.plateauWidth - 50);
                    j = WorldGen.genRand.Next(105, 160);
                    xPos[n] = i;
                    yPos[n] = j;
                    bool nearOther = false;
                    for (int p = 0; p < num; p++)
                    {
                        if (p == n) continue;
                        Vector2 curr = new Vector2(x + i, y + j);
                        Vector2 other = new Vector2(xPos[p], yPos[p]);
                        Vector2 large = new Vector2(EAWorldGen.spiderTempleLoc.X + 85 / 2, EAWorldGen.spiderTempleLoc.Y + 54 / 2);
                        if (Vector2.Distance(curr, other) < 20 || Vector2.Distance(curr, large) < 60) nearOther = true;
                    }
                    tooClose = nearOther;
                    trials++;
                    if (trials > 5000)
                    {
                        gen = false;
                        break;
                    }
                }
                if (gen) GenMiniTemple(x + i, y + j, GetTexture("ElementsAwoken/Structures/VolcanicPlateau/MiniTemple" + n));
            }
        }
        private static void ReplaceDecor(int x, int y)
        {
            for (int i = 0; i < EAWorldGen.plateauWidth; i++)
            {
                for (int j = 0; j < 200; j++)
                {
                    Tile t = Framing.GetTileSafely(i + x, j + y);
                    if (t.type == TileID.SmallPiles || t.type == TileID.LargePiles || t.type == TileID.LargePiles2)
                    {
                        WorldGen.KillTile(i + x, j + y);
                    }
                }
            }
            int genSpot = 0;
            int tempY = y;
            while (genSpot < EAWorldGen.plateauWidth)
            {
                genSpot += WorldGen.genRand.Next(2, 8);
                for (int j = 40; j < 110; j++)
                {
                    if (Framing.GetTileSafely(genSpot + x, y + j).active() && Main.tileSolid[Framing.GetTileSafely(genSpot + x, y + j).type])
                    {
                        y += j;
                        break;
                    }
                }
                int type = 0;
                int style = 0;
                int rand = 4;
                if (genSpot > 870) rand = 5;
                switch (Main.rand.Next(rand))
                {
                    case 0:
                        type = TileType<Plateau1x1>();
                        break;
                    case 1:
                        type = TileType<Plateau1x1>();
                        break;
                    case 2:
                        type = TileType<Plateau2x1>();
                        break;
                    case 3:
                        type = TileType<Plateau3x1>();
                        break;
                    case 4:
                        type = TileType<Plateau3x2>();
                        break;
                    default:
                        type = TileType<Plateau1x1>();
                        break;
                }
                Tile beneath = Framing.GetTileSafely(genSpot + x, y + 1);
                if (beneath.type == TileType<IgneousRock>())
                {
                    if (type == TileType<Plateau1x1>())
                    {
                        style = WorldGen.genRand.Next(2, 6);
                    }
                    else if (type == TileType<Plateau2x1>())
                    {
                        style = 1;
                    }
                    else if (type == TileType<Plateau3x1>())
                    {
                        style = 1;
                    }
                }
                else if (beneath.type == TileType<MalignantFlesh>() || beneath.type == TileType<AshGrass>())
                {
                    if (type == TileType<Plateau1x1>())
                    {
                        style = WorldGen.genRand.Next(0, 2);
                    }
                }
                else if (beneath.type == TileType<SulfuricSediment>() || beneath.type == TileType<SulfuricSlate>())
                {
                    if (type == TileType<Plateau1x1>())
                    {
                        style = WorldGen.genRand.Next(6, 8);
                    }
                    else if (type == TileType<Plateau2x1>())
                    {
                        style = 3;
                    }
                    else if (type == TileType<Plateau3x1>())
                    {
                        style = WorldGen.genRand.Next(2, 6);
                    }
                }
                else continue;
                if (!Main.tileSolid[Framing.GetTileSafely(genSpot + x, y - 1).type] && Framing.GetTileSafely(genSpot + x, y - 1).type != TileType<SulfuricQuicksand>()) WorldGen.KillTile(genSpot + x, y - 1);
                WorldGen.PlaceObject(genSpot + x, y - 1, type, true, style);
                // WorldGen.PlaceTile(genSpot + x, y - 1, TileID.GoldBrick);
                y = tempY;
            }
        }
        private static void ReplacePots(int x, int y)
        {
            for (int i = 0; i < EAWorldGen.plateauWidth; i++)
            {
                for (int j = 0; j < 200; j++)
                {
                    Tile t = Framing.GetTileSafely(i + x, j + y);
                    Tile tAbove = Framing.GetTileSafely(i + x, j + y - 1);
                    if (t.type == TileID.Pots && tAbove.type == TileID.Pots)
                    {
                        if (i > 860)
                        {
                            WorldGen.KillTile(i + x, j + y);
                            WorldGen.PlaceObject(i + x, j + y, TileType<SulfuricPot>(), false, WorldGen.genRand.Next(0, 3));
                        }
                        else
                        {
                            WorldGen.KillTile(i + x, j + y);
                            WorldGen.PlaceObject(i + x, j + y, TileType<IgneousPot>(), false, WorldGen.genRand.Next(0, 3));
                        }
                    }
                }
            }
        }
        private static void Geysers(int x, int y)
        {
            int numGenerated = 0;
            int numGeysers = 50;
            int platX = x;
            int platY = y;
            for (int p = 0; p < numGeysers; p++)
            {
                int trials = 0;
                bool invalidSpot = true;

                while (trials < 10000 && invalidSpot)
                {
                    x = platX + WorldGen.genRand.Next(900, EAWorldGen.plateauWidth - 50);
                    y = platY + WorldGen.genRand.Next(105, 190);
                    for (int j = y; j < Main.maxTilesY - 10; j++)
                    {
                        if (Framing.GetTileSafely(x, j).active() && Main.tileSolid[Framing.GetTileSafely(x, j).type])
                        {
                            y = j;
                            break;
                        }
                    }
                    bool bad = false;
                    for (int tX = 0; tX < 2; tX++)
                    {
                        for (int tY = 0; tY < 3; tY++)
                        {
                            Tile t = Framing.GetTileSafely(x+ tX, y - 2 + tY);
                            if (tY < 2 && Main.tileSolid[t.type]) bad = true;
                            if (tY == 2 && (!Main.tileSolid[t.type] || t.type != TileType<SulfuricSediment>() && t.type != TileType<SulfuricSlate>())) bad = true;
                        }
                    }
                    invalidSpot = bad;
                }
                if (invalidSpot) continue;
                int type = WorldGen.genRand.NextBool() ? TileType<AcidGeyser>() : TileType<SulfurVent>();
                Framing.GetTileSafely(x, y).slope(0);
                Framing.GetTileSafely(x+1, y).slope(0);
                WorldGen.PlaceObject(x, y - 1, type, true);
                numGenerated++;
            }
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            mod.Logger.InfoFormat("{0} geysers generated", numGenerated);
        }
        private static void Mine(int x, int y, int sections, int dir, int prevStraight = 0, int secNum = 0)
        {
            bool forceUp = y > Main.maxTilesY - 50;
            bool forceDown = y < Main.maxTilesY - 100;
            if (sections < 5 && dir == 1) forceUp = y > Main.maxTilesY - 100;
            if (sections < 5 && dir == 1) forceDown = false;
                bool force = forceUp || forceDown;
            if ((((WorldGen.genRand.NextBool(5) || force) && prevStraight > 0) || prevStraight > 13) && secNum > 10)
            {
                if (Main.rand.NextBool())
                {
                    bool willBeTooHigh = y - 7 < Main.maxTilesY - 100;
                    bool willBeTooLow = y + 7 > Main.maxTilesY - 50;
                    if ((Main.rand.NextBool() && !willBeTooLow) || willBeTooHigh)
                    {
                        int num = (dir == 1 ? 0 : 1);
                        GenMineRoom(x, y, GetTexture("ElementsAwoken/Structures/VolcanicPlateau/MineStairs" + num), secNum: secNum); // down
                        x += 7 * dir;
                        y += 7;
                    }
                    else
                    {
                        y -= 7;
                        int num = (dir == 1 ? 1 : 0);
                        GenMineRoom(x, y, GetTexture("ElementsAwoken/Structures/VolcanicPlateau/MineStairs" + num), secNum: secNum); // up
                        x += 7 * dir;
                    }
                }
                else
                {
                    bool willBeTooHigh = y - 16 < Main.maxTilesY - 100;
                    bool willBeTooLow = y + 16 > Main.maxTilesY - 50;
                    if ((Main.rand.NextBool() && !willBeTooLow) || willBeTooHigh)
                    {
                        GenMineRoom(x, y, GetTexture("ElementsAwoken/Structures/VolcanicPlateau/MineRoom1"), secNum: secNum, style: 1); // down
                        x += 6 * dir;
                        y += 16;
                        int num = WorldGen.genRand.Next(3, 7);
                        if (Main.rand.Next(5) < 4 && prevStraight > 3 && secNum > 10 + num) MinePathToTreasure(x, y, (int)MathHelper.Clamp(num, 3, prevStraight), dir);
                    }
                    else
                    {
                        y -= 16;
                        GenMineRoom(x, y, GetTexture("ElementsAwoken/Structures/VolcanicPlateau/MineRoom1"), secNum: secNum, style: 1); // up
                        x += 6 * dir;
                        if (Main.rand.Next(5) < 4)
                        {
                            int num = WorldGen.genRand.Next(3, 7);

                            if (Main.rand.NextBool() && prevStraight > 3 && secNum > 10 + num)
                            {
                                MinePathToTreasure(x, y, (int)MathHelper.Clamp(num, 3, prevStraight), dir);
                            }
                            else
                            {
                                int xOff2 = (dir == 1 ? 3 : -3);
                                int yOff2 = (dir == 1 ? 14 : 16);
                                GenMineRoom(x, y, GetTexture("ElementsAwoken/Structures/VolcanicPlateau/MineRoom4"), xOff2, yOff2, secNum, style: 4);
                            }
                        }
                    }
                }
                prevStraight = 0;
            }
            else
            {
                GenMineRoom(x, y, GetTexture("ElementsAwoken/Structures/VolcanicPlateau/MineRoom0"), secNum: secNum, style: 0);
                x += 6 * dir;
                prevStraight++;
            }
            if (x >= EAWorldGen.plateauLoc.X + 430) sections = 0;
            if (sections == 0 && dir == 1)
            {
                x += 18;
                if (y > Main.maxTilesY - 80) y = Main.maxTilesY - 80;
                GenMineRoom(x, y, GetTexture("ElementsAwoken/Structures/VolcanicPlateau/MineRoom5"), yOff: -6, secNum: secNum, style: 5);
            }
            if (sections > 0) Mine(x, y, sections - 1, dir, prevStraight, secNum + 1);
        }
        private static void MinePathToTreasure(int x, int y, int num, int dir)
        {
            for (int i = 1; i <= num; i++)
            {
                if (i == 1)
                {
                    x -= 6 * dir;
                }
                else if (i < num)
                {
                    x -= 6 * dir;
                    GenMineRoom(x, y, GetTexture("ElementsAwoken/Structures/VolcanicPlateau/MineRoom0"), secNum: num, style: 0);
                }
                else
                {
                    x -= 9 * dir;
                    y -= 3;
                    GenMineRoom(x, y, GetTexture("ElementsAwoken/Structures/VolcanicPlateau/MineRoom3"), secNum: num, style: 3);
                }
            }
        }
        // misc
        private static void GenPlateaus(int x, int y, Texture2D tex)
        {
            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(163, 74, 83)] = primaryTile;
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1; //don't touch when genning

            TexGen gen = BaseWorldGenTex.GetTexGenerator(tex, colorToTile);
            gen.Generate(x, y - (gen.height), true, true, true);
        }
        private static void GenMiniTemple(int x, int y, Texture2D tex)
        {
            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(46, 51, 25)] = TileType<SulfuricBricks>();
            colorToTile[new Color(255, 0, 0)] = TileType<SpiderCubeSpawner>();
            colorToTile[new Color(169, 21, 145)] = TileID.Marble; // fucking furniture
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1; //don't touch when genning

            TexGen gen = BaseWorldGenTex.GetTexGenerator(tex, colorToTile);
            gen.Generate(x - (gen.width / 2), y - (gen.height / 2), true, true, true);
            int topLeftX = x - gen.width / 2;
            int topLeftY = y - gen.height / 2;
            for (int i = topLeftX; i < topLeftX + gen.width; i++)
            {
                for (int j = topLeftY; j < topLeftY + gen.height; j++)
                {
                    Tile t = Framing.GetTileSafely(i, j);
                    t.lava(false);
                    t.liquid = 0;
                }
            }
            for (int i = topLeftX; i < topLeftX + gen.width; i++)
            {
                for (int j = topLeftY + gen.height; j > topLeftY; j--)
                {
                    Tile tM = Framing.GetTileSafely(i, j);
                    if (tM.type == TileID.Marble)
                    {
                        WorldGen.KillTile(i, j);
                        WorldGen.KillTile(i - 1, j);
                        WorldGen.KillTile(i + 1, j);
                        WorldGen.PlaceObject(i, j, TileType<SulfuricPillar>(), true);
                    }
                }
            }
        }
        public static void GenMineRoom(int x, int y, Texture2D tex, int xOff = 0, int yOff = 0, int secNum = 0, int style = 0)
        {
            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(163, 74, 83)] = primaryTile;
            colorToTile[new Color(74, 163, 81)] = TileID.WoodenBeam;
            colorToTile[new Color(0, 255, 255)] = TileID.GoldBrick;
            colorToTile[new Color(255, 255, 0)] = TileID.Marble;
            colorToTile[new Color(214, 192, 156)] = TileID.BoneBlock;
            colorToTile[new Color(255, 0, 0)] = TileType<TheKeeperSpawner>();
            colorToTile[new Color(184, 151, 155)] = TileType<ScarletiteTile>(); // steel
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1; //don't touch when genning

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(150, 150, 150)] = 0; //turn into air
            colorToWall[Color.Black] = -1; //don't touch when genning

            TexGen gen = BaseWorldGenTex.GetTexGenerator(tex, colorToTile, platformStyle: 13);
            if (style == 5) gen = BaseWorldGenTex.GetTexGenerator(tex, colorToTile, GetTexture("ElementsAwoken/Structures/VolcanicPlateau/MineRoom5Walls"), colorToWall, platformStyle: 13);
            Vector2 genSpot = new Vector2(x - (gen.width / 2) + xOff, y + yOff);
            gen.Generate((int)genSpot.X, (int)genSpot.Y, true, true, true);
            for (int i = (int)genSpot.X; i < genSpot.X + gen.width; i++)
            {
                for (int j = (int)genSpot.Y; j < genSpot.Y + gen.height; j++)
                {
                    Tile t = Framing.GetTileSafely(i, j);
                    if (t.type == TileID.Marble)
                    {
                        WorldGen.KillTile(i, j);
                        WorldGen.PlaceTile(i, j, TileID.Platforms, true, true, style: 13);
                    }
                    else if (t.type == TileID.GoldBrick)
                    {
                        WorldGen.KillTile(i, j);
                        if (WorldGen.genRand.NextBool(8))/*if (secNum % 2 == 0)*/ WorldGen.PlaceTile(i, j, TileType<IgneousTorch>(), true, true);
                    }
                }
            }
            if (style == 0)
            {
                WorldGen.PlaceObject(x + WorldGen.genRand.Next(tex.Width), y + yOff + tex.Height - 1, TileType<IgneousPot>(), false, WorldGen.genRand.Next(0, 3));
            }
            else if (style == 5)
            {
                Point topLeft = new Point(x - 31, y - 7);
                PlaceBezierRope(new Point(topLeft.X + 9, topLeft.Y + 8), new Point(topLeft.X + 20, topLeft.Y + 6), TileType<Tiles.BezierRope.SkullRope>());
                PlaceBezierRope(new Point(topLeft.X + 24, topLeft.Y + 10), new Point(topLeft.X + 39, topLeft.Y + 11), TileType<Tiles.BezierRope.SkullRope>());
                PlaceBezierRope(new Point(topLeft.X + 31, topLeft.Y + 7), new Point(topLeft.X + 53, topLeft.Y + 18), TileType<Tiles.BezierRope.SkullRope>());
                PlaceBezierRope(new Point(topLeft.X + 6, topLeft.Y + 20), new Point(topLeft.X + 19, topLeft.Y + 16), TileType<Tiles.BezierRope.SkullRope>());
            }
        }

        private static void GenMineBossRoom(int x, int y)
        {
            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(163, 74, 83)] = primaryTile;
            colorToTile[new Color(255, 255, 0)] = TileID.Marble;
            colorToTile[new Color(0, 255, 255)] = TileID.GoldBrick;
            colorToTile[new Color(0, 0, 255)] = TileType<MineBossSpawner>();
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1; //don't touch when genning

            TexGen gen = BaseWorldGenTex.GetTexGenerator(GetTexture("ElementsAwoken/Structures/VolcanicPlateau/MineBossRoom"), colorToTile);
            Vector2 genSpot = new Vector2(x - (gen.width / 2), y);
            gen.Generate((int)genSpot.X, (int)genSpot.Y, true, true, true);
            EAWorldGen.mineBossArenaLoc.X = (int)genSpot.X;
            EAWorldGen.mineBossArenaLoc.Y = (int)genSpot.Y;
            for (int i = (int)genSpot.X; i < genSpot.X + gen.width; i++)
            {
                for (int j = (int)genSpot.Y; j < genSpot.Y + gen.height; j++)
                {
                    Tile t = Framing.GetTileSafely(i, j);
                    t.liquid = 0;
                    if (t.type == TileID.Marble)
                    {
                        WorldGen.KillTile(i, j);
                        WorldGen.PlaceTile(i, j, TileID.Platforms, true, true, style: 13);
                    }
                    else if (t.type == TileID.GoldBrick)
                    {
                        WorldGen.KillTile(i, j);
                        WorldGen.PlaceTile(i, j, TileType<IgneousTorch>(), true, true);
                    }
                }
            }
        }
        private static void GenTiles(int x, int y)
        {
            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(0, 0, 255)] = TileID.IridescentBrick;
            colorToTile[new Color(163, 74, 83)] = primaryTile;
            colorToTile[new Color(81, 57, 133)] = lakeTile;
            colorToTile[new Color(0, 165, 255)] = primarySulphurTile;
            colorToTile[new Color(0, 255, 97)] = secondarySulphurTile;
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1; //don't touch when genning

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(163, 74, 83)] = WallType<IgneousRockWall>(); 
            colorToWall[new Color(0, 255, 97)] = WallType<SulfuricSedimentWall>();
            colorToWall[new Color(150, 150, 150)] = 0; //turn into air
            colorToWall[Color.Black] = -1; //don't touch when genning

            TexGen gen = BaseWorldGenTex.GetTexGenerator(GetTexture("ElementsAwoken/Structures/VolcanicPlateau/PlateauBase"), colorToTile, GetTexture("ElementsAwoken/Structures/VolcanicPlateau/PlateauWalls"), colorToWall, GetTexture("ElementsAwoken/Structures/VolcanicPlateau/PlateauLava"));
            gen.Generate(x, y, true, true, true);
        }
        private static void LiquidPatch(int i, int j, double strength, int steps, List<int> otherTypes = null)
        {
            double num = strength;
            float num2 = (float)steps;
            Vector2 vector;
            vector.X = (float)i;
            vector.Y = (float)j;
            Vector2 vector2;
            vector2.X = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
            vector2.Y = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
            while (num > 0.0 && num2 > 0f)
            {
                num = strength * (double)(num2 / (float)steps);
                num2 -= 1f;
                int num3 = (int)((double)vector.X - num * 0.5);
                int num4 = (int)((double)vector.X + num * 0.5);
                int num5 = (int)((double)vector.Y - num * 0.5);
                int num6 = (int)((double)vector.Y + num * 0.5);
                if (num3 < 0)
                {
                    num3 = 0;
                }
                if (num4 > Main.maxTilesX)
                {
                    num4 = Main.maxTilesX;
                }
                if (num5 < 0)
                {
                    num5 = 0;
                }
                if (num6 > Main.maxTilesY)
                {
                    num6 = Main.maxTilesY;
                }
                for (int k = num3; k < num4; k++)
                {
                    for (int l = num5; l < num6; l++)
                    {
                        if ((double)(Math.Abs((float)k - vector.X) + Math.Abs((float)l - vector.Y)) < strength * 0.5 * (1.0 + (double)WorldGen.genRand.Next(-10, 11) * 0.015) && Main.tile[k, l].active() && (otherTypes.Contains(Main.tile[k, l].type) || otherTypes == null))
                        {
                            Main.tile[k, l].liquid = 255;
                            Main.tile[k, l].lava(true);
                            Main.tile[k, l].active(false);
                            Main.tile[k, l].type = 0;
                            WorldGen.SquareTileFrame(k, l, true);
                            if (Main.netMode == 2)
                            {
                                NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                            }
                        }
                    }
                }
                vector += vector2;
                vector2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                if (vector2.X > 1f)
                {
                    vector2.X = 1f;
                }
                if (vector2.X < -1f)
                {
                    vector2.X = -1f;
                }
            }
        }
        private static void Streak(int x, int y, double strength, int type, List<int> otherTypes = null)
        {
            strength *= 2;
            Vector2 pos = new Vector2(x, y) * 16;
            Vector2 rotation = Vector2.One.RotatedBy(WorldGen.genRand.NextFloat(0, (float)Math.PI * 2));
            for (int k = 0; k < strength; k++)
            {
                pos += rotation * 8;
                Point tilePos = pos.ToTileCoordinates();
                int width = (int)Math.Round(Math.Sin(k / strength * Math.PI) * 2);
                for (int i = 1 - width; i <= 1 + width; i++)
                {
                    for (int j = 1 - width; j <= 1 + width; j++)
                    {
                        int posX = tilePos.X + i;
                        int posY = tilePos.Y + j;
                        if (posX < 0 || posX > Main.maxTilesX || posY < 0 || posY > Main.maxTilesY) return;
                        Tile tile = Framing.GetTileSafely(posX, posY);
                        if (tile.active() && (otherTypes.Contains(tile.type) || otherTypes == null))
                        {
                            tile.type = (ushort)type;
                            tile.active(true);
                            WorldGen.SquareTileFrame(posX, posY, true);
                        }
                    }
                }
            }
        }
        private static void TilePatch(int i, int j, double strength, int steps, int type, List<int> otherTypes = null)
        {
            double num = strength;
            float num2 = (float)steps;
            Vector2 vector;
            vector.X = (float)i;
            vector.Y = (float)j;
            Vector2 vector2;
            vector2.X = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
            vector2.Y = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
            while (num > 0.0 && num2 > 0f)
            {
                if (vector.Y < 0f && num2 > 0f && type == 59)
                {
                    num2 = 0f;
                }
                num = strength * (double)(num2 / (float)steps);
                num2 -= 1f;
                int num3 = (int)((double)vector.X - num * 0.5);
                int num4 = (int)((double)vector.X + num * 0.5);
                int num5 = (int)((double)vector.Y - num * 0.5);
                int num6 = (int)((double)vector.Y + num * 0.5);
                if (num3 < 0) num3 = 0;
                if (num4 > Main.maxTilesX) num4 = Main.maxTilesX;
                if (num5 < 0) num5 = 0;
                if (num6 > Main.maxTilesY) num6 = Main.maxTilesY;

                for (int k = num3; k < num4; k++)
                {
                    for (int l = num5; l < num6; l++)
                    {
                        if ((double)(Math.Abs((float)k - vector.X) + Math.Abs((float)l - vector.Y)) < strength * 0.5 * (1.0 + (double)WorldGen.genRand.Next(-10, 11) * 0.015) && Main.tile[k, l].active() && (otherTypes.Contains(Main.tile[k, l].type) || otherTypes == null))
                        {
                            if (type >= 0) Main.tile[k, l].type = (ushort)type;
                            else if (type == -2)
                            {
                                Main.tile[k, l].active(false);
                            }
                            WorldGen.SquareTileFrame(k, l, true);
                            if (Main.netMode == 2)
                            {
                                NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                            }
                        }
                    }
                }
                vector += vector2;
                vector2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                if (vector2.X > 1f)
                {
                    vector2.X = 1f;
                }
                if (vector2.X < -1f)
                {
                    vector2.X = -1f;
                }
            }
        }
        private static void Cavinator(int i, int j, int steps)
        {
            double num = (double)WorldGen.genRand.Next(7, 15);
            int num2 = 1;
            if (WorldGen.genRand.Next(2) == 0)
            {
                num2 = -1;
            }
            Vector2 vector;
            vector.X = (float)i;
            vector.Y = (float)j;
            int k = WorldGen.genRand.Next(20, 40);
            Vector2 vector2;
            vector2.Y = (float)WorldGen.genRand.Next(10, 20) * 0.01f;
            vector2.X = (float)num2;
            while (k > 0)
            {
                k--;
                float scale = 0.25f; // * MathHelper.Lerp(0.3f, 1f, MathHelper.Min((float)steps / 25f, 0.5f)); // default 0.5
                int num3 = (int)((double)vector.X - num * scale);
                int num4 = (int)((double)vector.X + num * scale);
                int num5 = (int)((double)vector.Y - num * scale);
                int num6 = (int)((double)vector.Y + num * scale);
                if (num3 < 0)
                {
                    num3 = 0;
                }
                if (num4 > Main.maxTilesX)
                {
                    num4 = Main.maxTilesX;
                }
                if (num5 < 0)
                {
                    num5 = 0;
                }
                if (num6 > Main.maxTilesY)
                {
                    num6 = Main.maxTilesY;
                }
                double num7 = num * (double)WorldGen.genRand.Next(80, 120) * 0.01;
                for (int l = num3; l < num4; l++)
                {
                    for (int m = num5; m < num6; m++)
                    {
                        float arg_14B_0 = Math.Abs((float)l - vector.X);
                        float num8 = Math.Abs((float)m - vector.Y);
                        if (Math.Sqrt((double)(arg_14B_0 * arg_14B_0 + num8 * num8)) < num7 * 0.4 && TileID.Sets.CanBeClearedDuringGeneration[(int)Main.tile[l, m].type])
                        {
                            Main.tile[l, m].active(false);
                        }
                    }
                }
                vector += vector2;
                vector2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                vector2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                if (vector2.X > (float)num2 + 0.5f)
                {
                    vector2.X = (float)num2 + 0.5f;
                }
                if (vector2.X < (float)num2 - 0.5f)
                {
                    vector2.X = (float)num2 - 0.5f;
                }
                if (vector2.Y > 2f)
                {
                    vector2.Y = 2f;
                }
                if (vector2.Y < 0f)
                {
                    vector2.Y = 0f;
                }
            }
            if (steps > 0 && (double)((int)vector.Y) < Main.rockLayer + 50.0)
            {
                Cavinator((int)vector.X, (int)vector.Y, steps - 1);
            }
        }
        private static void CaveOpenater(int i, int j)
        {
            double num = (double)WorldGen.genRand.Next(7, 12);
            int num2 = 1;
            if (WorldGen.genRand.Next(2) == 0)
            {
                num2 = -1;
            }
            Vector2 vector;
            vector.X = (float)i;
            vector.Y = (float)j;
            int k = 100;
            Vector2 vector2;
            vector2.Y = 0f;
            vector2.X = (float)num2;
            while (k > 0)
            {
                if (Main.tile[(int)vector.X, (int)vector.Y].wall == 0)
                {
                    k = 0;
                }
                k--;
                int num3 = (int)((double)vector.X - num * 0.5);
                int num4 = (int)((double)vector.X + num * 0.5);
                int num5 = (int)((double)vector.Y - num * 0.5);
                int num6 = (int)((double)vector.Y + num * 0.5);
                if (num3 < 0)
                {
                    num3 = 0;
                }
                if (num4 > Main.maxTilesX)
                {
                    num4 = Main.maxTilesX;
                }
                if (num5 < 0)
                {
                    num5 = 0;
                }
                if (num6 > Main.maxTilesY)
                {
                    num6 = Main.maxTilesY;
                }
                double num7 = num * (double)WorldGen.genRand.Next(80, 120) * 0.01;
                for (int l = num3; l < num4; l++)
                {
                    for (int m = num5; m < num6; m++)
                    {
                        float arg_14E_0 = Math.Abs((float)l - vector.X);
                        float num8 = Math.Abs((float)m - vector.Y);
                        if (Math.Sqrt((double)(arg_14E_0 * arg_14E_0 + num8 * num8)) < num7 * 0.4)
                        {
                            Main.tile[l, m].active(false);
                        }
                    }
                }
                vector += vector2;
                vector2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                vector2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                if (vector2.X > (float)num2 + 0.5f)
                {
                    vector2.X = (float)num2 + 0.5f;
                }
                if (vector2.X < (float)num2 - 0.5f)
                {
                    vector2.X = (float)num2 - 0.5f;
                }
                if (vector2.Y > 0f)
                {
                    vector2.Y = 0f;
                }
                if ((double)vector2.Y < -0.5)
                {
                    vector2.Y = -0.5f;
                }
            }
        }
        private static void Webs(int i, int j)
        {
            if (!Main.tile[i, j].active())
            {
                if ((double)j <= Main.worldSurface)
                {
                    if (Main.tile[i, j].wall <= 0)
                    {
                        //goto IL_145;
                    }
                }
                while (!Main.tile[i, j].active() && j > (int)WorldGen.worldSurfaceLow)
                {
                    j--;
                }
                j++;
                int num3 = 1;
                if (WorldGen.genRand.Next(2) == 0)
                {
                    num3 = -1;
                }
                while (!Main.tile[i, j].active() && i > 10 && i < Main.maxTilesX - 10)
                {
                    i += num3;
                }
                i -= num3;
                if ((double)j > Main.worldSurface || Main.tile[i, j].wall > 0)
                {
                    WorldGen.TileRunner(i, j, (double)WorldGen.genRand.Next(4, 24), WorldGen.genRand.Next(2, 9), TileType<AcidWeb>(), true, (float)num3, -1f, false, false);
                }
            }
        }
        private static void PlaceBezierRope(Point startTile, Point endTile, int type)
        {
            Tile start = Framing.GetTileSafely(startTile.X, startTile.Y);
            WorldGen.PlaceTile(startTile.X, startTile.Y, type, true, true);
            start.frameX = (short)endTile.X;
            start.frameY = (short)endTile.Y;
        }
        private static void SlopeTiles(int x, int y, int width, int height)
        {
            for (int i = x; i < width; i++)
            {
                for (int j = y; j < height; j++)
                {
                    if (Main.tile[i, j].type != 48 && Main.tile[i, j].type != 137 && Main.tile[i, j].type != 232 && Main.tile[i, j].type != 191 && Main.tile[i, j].type != 151 && Main.tile[i, j].type != 274)
                    {
                        if (!Main.tile[i, j - 1].active())
                        {
                            if (WorldGen.SolidTile(i, j) && TileID.Sets.CanBeClearedDuringGeneration[(int)Main.tile[i, j].type])
                            {
                                if (!Main.tile[i - 1, j].halfBrick() && !Main.tile[i + 1, j].halfBrick() && Main.tile[i - 1, j].slope() == 0 && Main.tile[i + 1, j].slope() == 0)
                                {
                                    if (WorldGen.SolidTile(i, j + 1))
                                    {
                                        if (!WorldGen.SolidTile(i - 1, j) && !Main.tile[i - 1, j + 1].halfBrick() && WorldGen.SolidTile(i - 1, j + 1) && WorldGen.SolidTile(i + 1, j) && !Main.tile[i + 1, j - 1].active())
                                        {
                                            if (WorldGen.genRand.Next(2) == 0)
                                            {
                                                WorldGen.SlopeTile(i, j, 2);
                                            }
                                            else
                                            {
                                                WorldGen.PoundTile(i, j);
                                            }
                                        }
                                        else if (!WorldGen.SolidTile(i + 1, j) && !Main.tile[i + 1, j + 1].halfBrick() && WorldGen.SolidTile(i + 1, j + 1) && WorldGen.SolidTile(i - 1, j) && !Main.tile[i - 1, j - 1].active())
                                        {
                                            if (WorldGen.genRand.Next(2) == 0)
                                            {
                                                WorldGen.SlopeTile(i, j, 1);
                                            }
                                            else
                                            {
                                                WorldGen.PoundTile(i, j);
                                            }
                                        }
                                        else if (WorldGen.SolidTile(i + 1, j + 1) && WorldGen.SolidTile(i - 1, j + 1) && !Main.tile[i + 1, j].active() && !Main.tile[i - 1, j].active())
                                        {
                                            WorldGen.PoundTile(i, j);
                                        }
                                        if (WorldGen.SolidTile(i, j))
                                        {
                                            if (WorldGen.SolidTile(i - 1, j) && WorldGen.SolidTile(i + 1, j + 2) && !Main.tile[i + 1, j].active() && !Main.tile[i + 1, j + 1].active() && !Main.tile[i - 1, j - 1].active())
                                            {
                                                WorldGen.KillTile(i, j, false, false, false);
                                            }
                                            else if (WorldGen.SolidTile(i + 1, j) && WorldGen.SolidTile(i - 1, j + 2) && !Main.tile[i - 1, j].active() && !Main.tile[i - 1, j + 1].active() && !Main.tile[i + 1, j - 1].active())
                                            {
                                                WorldGen.KillTile(i, j, false, false, false);
                                            }
                                            else if (!Main.tile[i - 1, j + 1].active() && !Main.tile[i - 1, j].active() && WorldGen.SolidTile(i + 1, j) && WorldGen.SolidTile(i, j + 2))
                                            {
                                                if (WorldGen.genRand.Next(5) == 0)
                                                {
                                                    WorldGen.KillTile(i, j, false, false, false);
                                                }
                                                else if (WorldGen.genRand.Next(5) == 0)
                                                {
                                                    WorldGen.PoundTile(i, j);
                                                }
                                                else
                                                {
                                                    WorldGen.SlopeTile(i, j, 2);
                                                }
                                            }
                                            else if (!Main.tile[i + 1, j + 1].active() && !Main.tile[i + 1, j].active() && WorldGen.SolidTile(i - 1, j) && WorldGen.SolidTile(i, j + 2))
                                            {
                                                if (WorldGen.genRand.Next(5) == 0)
                                                {
                                                    WorldGen.KillTile(i, j, false, false, false);
                                                }
                                                else if (WorldGen.genRand.Next(5) == 0)
                                                {
                                                    WorldGen.PoundTile(i, j);
                                                }
                                                else
                                                {
                                                    WorldGen.SlopeTile(i, j, 1);
                                                }
                                            }
                                        }
                                    }
                                    if (WorldGen.SolidTile(i, j) && !Main.tile[i - 1, j].active() && !Main.tile[i + 1, j].active())
                                    {
                                        WorldGen.KillTile(i, j, false, false, false);
                                    }
                                }
                            }
                            else if (!Main.tile[i, j].active() && Main.tile[i, j + 1].type != 151 && Main.tile[i, j + 1].type != 274)
                            {
                                if (Main.tile[i + 1, j].type != 190 && Main.tile[i + 1, j].type != 48 && Main.tile[i + 1, j].type != 232 && WorldGen.SolidTile(i - 1, j + 1) && WorldGen.SolidTile(i + 1, j) && !Main.tile[i - 1, j].active() && !Main.tile[i + 1, j - 1].active())
                                {
                                    WorldGen.PlaceTile(i, j, (int)Main.tile[i, j + 1].type, false, false, -1, 0);
                                    if (WorldGen.genRand.Next(2) == 0)
                                    {
                                        WorldGen.SlopeTile(i, j, 2);
                                    }
                                    else
                                    {
                                        WorldGen.PoundTile(i, j);
                                    }
                                }
                                if (Main.tile[i - 1, j].type != 190 && Main.tile[i - 1, j].type != 48 && Main.tile[i - 1, j].type != 232 && WorldGen.SolidTile(i + 1, j + 1) && WorldGen.SolidTile(i - 1, j) && !Main.tile[i + 1, j].active() && !Main.tile[i - 1, j - 1].active())
                                {
                                    WorldGen.PlaceTile(i, j, (int)Main.tile[i, j + 1].type, false, false, -1, 0);
                                    if (WorldGen.genRand.Next(2) == 0)
                                    {
                                        WorldGen.SlopeTile(i, j, 1);
                                    }
                                    else
                                    {
                                        WorldGen.PoundTile(i, j);
                                    }
                                }
                            }
                        }
                        else if (!Main.tile[i, j + 1].active() && WorldGen.genRand.Next(2) == 0 && WorldGen.SolidTile(i, j) && !Main.tile[i - 1, j].halfBrick() && !Main.tile[i + 1, j].halfBrick() && Main.tile[i - 1, j].slope() == 0 && Main.tile[i + 1, j].slope() == 0 && WorldGen.SolidTile(i, j - 1))
                        {
                            if (WorldGen.SolidTile(i - 1, j) && !WorldGen.SolidTile(i + 1, j) && WorldGen.SolidTile(i - 1, j - 1))
                            {
                                WorldGen.SlopeTile(i, j, 3);
                            }
                            else if (WorldGen.SolidTile(i + 1, j) && !WorldGen.SolidTile(i - 1, j) && WorldGen.SolidTile(i + 1, j - 1))
                            {
                                WorldGen.SlopeTile(i, j, 4);
                            }
                        }
                        if (TileID.Sets.Conversion.Sand[(int)Main.tile[i, j].type])
                        {
                            Tile.SmoothSlope(i, j, false);
                        }
                    }
                }
            }
            for (int k = x; k < width; k++)
            {
                for (int l = y; l < height; l++)
                {
                    if (WorldGen.genRand.Next(2) == 0 && !Main.tile[k, l - 1].active() && Main.tile[k, l].type != 137 && Main.tile[k, l].type != 48 && Main.tile[k, l].type != 232 && Main.tile[k, l].type != 191 && Main.tile[k, l].type != 151 && Main.tile[k, l].type != 274 && Main.tile[k, l].type != 75 && Main.tile[k, l].type != 76 && WorldGen.SolidTile(k, l) && Main.tile[k - 1, l].type != 137 && Main.tile[k + 1, l].type != 137)
                    {
                        if (WorldGen.SolidTile(k, l + 1) && WorldGen.SolidTile(k + 1, l) && !Main.tile[k - 1, l].active())
                        {
                            WorldGen.SlopeTile(k, l, 2);
                        }
                        if (WorldGen.SolidTile(k, l + 1) && WorldGen.SolidTile(k - 1, l) && !Main.tile[k + 1, l].active())
                        {
                            WorldGen.SlopeTile(k, l, 1);
                        }
                    }
                    if (Main.tile[k, l].slope() == 1 && !WorldGen.SolidTile(k - 1, l))
                    {
                        WorldGen.SlopeTile(k, l, 0);
                        WorldGen.PoundTile(k, l);
                    }
                    if (Main.tile[k, l].slope() == 2 && !WorldGen.SolidTile(k + 1, l))
                    {
                        WorldGen.SlopeTile(k, l, 0);
                        WorldGen.PoundTile(k, l);
                    }
                }
            }
        }
    }
}