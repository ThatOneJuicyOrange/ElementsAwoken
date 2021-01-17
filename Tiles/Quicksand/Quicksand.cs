using System;
using ElementsAwoken.Items.Placeable.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles.Quicksand
{
    public class Quicksand : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileBlendAll[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            //Main.tileSolidTop[Type] = true;
            drop = ModContent.ItemType<QuicksandItem>();
            AddMapEntry(new Color(186, 168, 84));

            minPick = 0;
            GlobalTiles.quicksands.Add(Type);
        }
        public override bool Dangersense(int i, int j, Player player)
        {
            return true;
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            Tile t = Framing.GetTileSafely(i, j);
            if (t.liquid > 0)
            {
                t.liquid = 0;
                Main.tile[i, j].lava(false);
                Main.tile[i, j].honey(false);
                WorldGen.SquareTileFrame(i, j, false);
                if (Main.netMode == 1) NetMessage.sendWater(i, j);
                else Liquid.AddWater(i, j);
            }
        }
        // to allow placing on each other
        public override bool CanPlace(int i, int j)
        {
            Main.tileSolid[Type] = true;
            return base.CanPlace(i, j);
        }
        public override void PlaceInWorld(int i, int j, Item item)
        {
            Main.tileSolid[Type] = false;
        }
        public override void RandomUpdate(int i, int j)
        {
            Tile t = Framing.GetTileSafely(i, j);
            if (t.type == Type)
            {
                for (int x = i - 1; x <= i + 1; x++)
                {
                    for (int y = j - 1; y <= j + 1; y++)
                    {
                        Tile tileCheck = Framing.GetTileSafely(x, y);
                        if ((x != i || j != y) && tileCheck.active())
                        {
                            if (TileID.Sets.Corrupt[tileCheck.type])
                            {
                                t.type = (ushort)ModContent.TileType<QuicksandCorrupt>();
                                WorldGen.SquareTileFrame(x, y, true);
                                return;
                            }
                            else if (TileID.Sets.Crimson[tileCheck.type])
                            {
                                t.type = (ushort)ModContent.TileType<QuicksandCrimson>();
                                WorldGen.SquareTileFrame(x, y, true);
                                return;
                            }
                            else if (TileID.Sets.Hallow[tileCheck.type])
                            {
                                t.type = (ushort)ModContent.TileType<QuicksandHallow>();
                                WorldGen.SquareTileFrame(x, y, true);
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}