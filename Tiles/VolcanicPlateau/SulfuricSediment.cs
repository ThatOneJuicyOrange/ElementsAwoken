using System;
using System.Collections.Generic;
using ElementsAwoken.Items.Placeable.Tiles.Plateau;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles.VolcanicPlateau
{
    public class SulfuricSediment : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            EAUtils.MergeOtherPlateauTiles(Type);

            drop = ModContent.ItemType<SulfuricSedimentItem>();
            AddMapEntry(new Color(145, 151, 49));

            minPick = 50;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (MyWorld.awakenedPlateau)
            {
                Tile tile = Main.tile[i, j];
                if (tile.slope() == 0 && tile.halfBrick() == false && ElementsAwoken.glowMap[i % 100,j % 100])
                {
                    Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
                    if (Main.drawToScreen)
                    {
                        zero = Vector2.Zero;
                    }
                    Color color = Lighting.GetColor(i, j) * 2;
                    if (color == Color.Black) color = new Color(10, 10, 10);
                    Main.spriteBatch.Draw(mod.GetTexture("Tiles/VolcanicPlateau/SulfuricSediment_Glow"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, 16), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                }
            }
        }
        public override void RandomUpdate(int i, int j)
        {
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
                        WorldGen.PlaceObject(i, j - 1, ModContent.TileType<Flora.ToxithornTile>(), true);
                        NetMessage.SendObjectPlacment(-1, i, j - 1, ModContent.TileType<Flora.ToxithornTile>(), 0, 0, -1, -1);
                    }
                }
            }
            GrowVine(i, j);
            GrowGeyser(i, j);
        }
        public static void GrowVine(int i, int j)
        {
            if (!Main.tile[i, j + 1].active() && Main.tile[i, j].slope() == 0 && EAUtils.FindNumTilesNearby(ModContent.TileType<Flora.PlateauVines>(),i,j,1) == 0)
            {
                if (Main.rand.NextBool(5))
                {
                    WorldGen.PlaceObject(i, j + 1, ModContent.TileType<Flora.PlateauVines>(), true);
                    NetMessage.SendObjectPlacment(-1, i, j + 1, ModContent.TileType<Flora.PlateauVines>(), 0, 0, -1, -1);
                    Main.tile[i, j + 1].frameX = 3 * 18;
                }
            }
        }
        public static void GrowGeyser(int i, int j)
        {
            if (Main.rand.NextBool(30) && j > Main.maxTilesY - 120)
            {
                if (SpaceAbove(i, j) && EAUtils.FindNumTilesNearby(new List<int> { ModContent.TileType<Objects.AcidGeyser>(), ModContent.TileType<Objects.SulfurVent>() }, i, j, 10) == 0)
                {
                    int type = Main.rand.Next(new int[] { ModContent.TileType<Objects.AcidGeyser>(), ModContent.TileType<Objects.SulfurVent>() });
                    WorldGen.PlaceObject(i, j - 1, type, true);
                    NetMessage.SendObjectPlacment(-1, i, j - 1, type, 0, 0, -1, -1);
                }
            }
        }
        private static bool SpaceAbove(int i, int j)
        {
            for (int x = i; x <= i + 1; x++)
            {
                for (int y = j - 2; y <= j - 1; y++)
                {
                    if (Main.tile[x, y].active()) return false;
                }
            }
            return true;
        }
    }
}