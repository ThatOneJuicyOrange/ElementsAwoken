using ElementsAwoken.Items.Placeable.Tiles.Plateau;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles.VolcanicPlateau
{
    public class AshGrass : ModTile
    {
        public static int _type;

        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            //SetModTree(new treehere());
            Main.tileBlendAll[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            dustType = 31;
            AddMapEntry(new Color(96, 86, 112));
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!effectOnly) Main.tile[i, j].type = (ushort)ModContent.TileType<MalignantFlesh>();
        }
        public override void FloorVisuals(Player player)
        {
            if (Math.Abs(player.velocity.X) > 1)
            {
                if (Main.rand.NextBool(3))
                {
                    Dust dust = Main.dust[Dust.NewDust(player.Bottom, player.width, 2, 31, 0, -2)];
                    dust.color = new Color(190, 190, 190);
                    dust.velocity.X *= 0.5f;
                }
            }
        }
        public override void RandomUpdate(int i, int j)
        {
            for (int x = i - 1; x <= i + 1; x++)
            {
                for (int y = j - 1; y <= j + 1; y++)
                {
                    if ((x != i || j != y) && Main.tile[x, y].active() && Main.tile[x, y].type == ModContent.TileType<MalignantFlesh>())
                    {
                        Tile below = Framing.GetTileSafely(x,y + 1);
                        Tile above = Framing.GetTileSafely(x,y - 1);
                        Tile right = Framing.GetTileSafely(x + 1,y);
                        Tile left = Framing.GetTileSafely(x - 1,y);
                        if (((!Main.tileSolid[below.type] && below.active()) || !below.active()) ||
                            ((!Main.tileSolid[above.type] && above.active()) || !above.active()) ||
                            ((!Main.tileSolid[right.type] && right.active()) || !right.active()) ||
                            ((!Main.tileSolid[left.type] && left.active()) || !left.active()))
                        {
                            Main.tile[x, y].type = Type;
                            WorldGen.SquareTileFrame(x, y, true);
                        }
                    }
                }
            }
            if (Main.tile[i, j].slope() == 0 && !Main.tile[i, j].halfBrick())
            {
                if (Main.rand.NextBool(30))
                {
                    if (SpaceAbove(i, j) && EAUtils.FindNumTilesNearby(ModContent.TileType<Flora.FlareberryBush>(), i, j, 5) == 0)
                    {
                        WorldGen.PlaceObject(i, j - 1, ModContent.TileType<Flora.FlareberryBush>(), true);
                        NetMessage.SendObjectPlacment(-1, i, j - 1, ModContent.TileType<Flora.FlareberryBush>(), 0, 0, -1, -1);
                        for (int p = 0; p < 30; p++)
                        {
                            Dust dust = Main.dust[Dust.NewDust(new Vector2(i - 1, j - 3) * 16, 48, 48, 31, 0, -2)];
                            dust.color = new Color(190, 190, 190);
                            dust.velocity.X *= 0.5f;
                        }
                    }
                }
                else
                {
                    if (!Main.tile[i, j - 1].active() && Main.tile[i, j - 1].liquid == 0)
                    {
                        WorldGen.PlaceObject(i, j - 1, ModContent.TileType<Flora.AshStem>(), true);
                        Main.tile[i, j - 1].frameX = (short)(Main.rand.Next(3) * 18);
                        NetMessage.SendObjectPlacment(-1, i, j - 1, ModContent.TileType<Flora.AshStem>(), 0, 0, -1, -1);
                    }
                }
            }
        }
        private bool SpaceAbove(int i, int j)
        {
            for (int x = i - 1; x <= i + 1; x++)
            {
                for (int y = j - 3; y <= j - 1; y++)
                {
                    if (Main.tile[x, y].active() && Main.tile[x,y].type != ModContent.TileType<Flora.AshStem>()) return false;
                }
            }
            return true;
        }
    }
}