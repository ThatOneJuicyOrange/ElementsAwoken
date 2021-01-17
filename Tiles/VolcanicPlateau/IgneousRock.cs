using System;
using ElementsAwoken.Items.Placeable.Tiles.Plateau;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles.VolcanicPlateau
{
    public class IgneousRock : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            EAUtils.MergeOtherPlateauTiles(Type);
            Main.tileMerge[Type][Terraria.ID.TileID.BoneBlock] = true;
            Main.tileMerge[Type][Terraria.ID.TileID.Ash] = true;

            drop = ModContent.ItemType<IgneousRockItem>();
            AddMapEntry(new Color(33, 41, 55));
            soundType = 21;
            soundStyle = 6;

            minPick = 70;
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            Geysers(i, j);
        }
        public static void Geysers(int i, int j)
        {
            if (Main.hasFocus && MyWorld.awakenedPlateau)
            {
                if (Main.tile[i, j - 1].lava() && (Main.rand.NextBool(4000) || (MyWorld.plateauWeather == 2 && Main.rand.NextBool(1500))))
                {
                    Vector2 pos = new Vector2(i, j) * 16;
                    Projectile.NewProjectile(pos.X, pos.Y, 0, -2, ModContent.ProjectileType<Projectiles.Environmental.PlateauSmallEruptionSpawn>(), 90, 0f, Main.myPlayer);
                }
                if (Main.tile[i, j - 1].lava() && MyWorld.plateauWeather == 2 && Main.rand.NextBool(3000))
                {
                    Vector2 pos = new Vector2(i, j) * 16;
                    Projectile.NewProjectile(pos.X, pos.Y, 0, -2, ModContent.ProjectileType<Projectiles.Environmental.LargeGeyserSpawn>(), 90, 0f, Main.myPlayer);
                }
            }
        }
        public static void CheckGrowCrystal(int i, int j)
        {
            Tile above = Framing.GetTileSafely(i, j - 1);
            Tile below = Framing.GetTileSafely(i, j + 1);
            Tile right = Framing.GetTileSafely(i + 1, j);
            Tile left = Framing.GetTileSafely(i - 1, j);
            if (!above.active() || !below.active() || !right.active() || !left.active())
            {               
                bool fail = true;
                int attempts = 0;
                while (fail && attempts < 20)
                {
                    attempts++;
                    switch (Main.rand.Next(4))
                    {
                        case 0:
                            if (!above.active())
                            {
                                GrowCrystal(i, j - 1,0);
                                fail = false;
                            }
                            else fail = true;
                            break;
                        case 1:
                            if (!below.active())
                            {
                                GrowCrystal(i, j + 1,1);
                                fail = false;
                            }
                            else fail = true;
                            break;
                        case 2:
                            if (!right.active())
                            {
                                GrowCrystal(i + 1, j,2);
                                fail = false;
                            }
                            else fail = true;
                            break;
                        case 3:
                            if (!left.active())
                            {
                                GrowCrystal(i - 1, j,3);
                                fail = false;
                            }
                            else fail = true;
                            break;
                        default: break;
                    }
                }

            }
        }
        private static void GrowCrystal(int i, int j, int dir)
        {
            if (Main.tile[i, j] != null)
            {
                if (Main.tile[i, j].liquid > 0) return;
                WorldGen.PlaceObject(i, j, ModContent.TileType<EruptionCrystalTileUnsafe>(),true);
                Main.tile[i, j].frameX = (short)(18 * Main.rand.Next(5));

                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(new Vector2(i,j) * 16, 16, 16, 6);
                }
            }
        }
        public override void RandomUpdate(int i, int j)
        {
            if (MyWorld.awakenedPlateau && !MyWorld.downedVoidLeviathan)
            {
                if (Main.rand.NextBool(500))
                {
                    Tile below = Framing.GetTileSafely(i, j + 1);
                    Tile above = Framing.GetTileSafely(i, j - 1);
                    Tile right = Framing.GetTileSafely(i + 1, j);
                    Tile left = Framing.GetTileSafely(i - 1, j);
                    if (((!Main.tileSolid[below.type] && below.active()) || !below.active()) ||
                        ((!Main.tileSolid[above.type] && above.active()) || !above.active()) ||
                        ((!Main.tileSolid[right.type] && right.active()) || !right.active()) ||
                        ((!Main.tileSolid[left.type] && left.active()) || !left.active()))
                    {
                        if (EAUtils.FindNumTilesNearby(ModContent.TileType<VoidMoss>(), i, j, 50) == 0)
                        {
                            Main.tile[i, j].type = (ushort)ModContent.TileType<VoidMoss>();
                            WorldGen.SquareTileFrame(i, j, true);
                        }
                    }
                }
            }
        
            if (MyWorld.plateauWeather == 2)
            {
                if (Main.rand.NextBool(5)) CheckGrowCrystal(i, j);
            }
            if (!Main.tile[i, j - 1].active())
            {
                if (Main.rand.NextBool(ElementsAwoken.plateauFlowerGrowChance))
                {
                    if (Main.rand.NextBool(20))
                    {
                        WorldGen.PlaceObject(i, j - 1, ModContent.TileType<Flora.VoidBulbTile>(), true);
                        NetMessage.SendObjectPlacment(-1, i, j - 1, ModContent.TileType<Flora.VoidBulbTile>(), 0, 0, -1, -1);
                    }
                    else
                    {
                        int type = ModContent.TileType<Flora.AshwillowTile>();
                        if (EAUtils.TileNearLava(i, j, 6)) type = ModContent.TileType<Flora.CinderlilyTile>();

                        WorldGen.PlaceObject(i, j - 1, type, true);
                        NetMessage.SendObjectPlacment(-1, i, j - 1, type, 0, 0, -1, -1);
                    }
                }
            }
            if (!Main.tile[i, j + 1].active() && Main.tile[i, j].slope() == 0)
            {
                if (Main.rand.NextBool(5))
                {
                    WorldGen.PlaceObject(i, j + 1, ModContent.TileType<Flora.PlateauVines>(), true);
                    NetMessage.SendObjectPlacment(-1, i, j + 1, ModContent.TileType<Flora.PlateauVines>(), 0, 0, -1, -1);
                    Main.tile[i, j + 1].frameX = (short)(Main.rand.Next(3) * 18);
                }
            }
            /*if (Main.tile[i, j - 1].lava() && (Main.rand.NextBool(3) || MyWorld.plateauWeather == 2) && Main.hardMode)
            {
                Vector2 pos = new Vector2(i, j) * 16;
                Projectile.NewProjectile(pos.X, pos.Y, 0, -2, ModContent.ProjectileType<Projectiles.Environmental.PlateauSmallEruptionSpawn>(), 0, 0f, Main.myPlayer);
            }*/
        }
    }
}