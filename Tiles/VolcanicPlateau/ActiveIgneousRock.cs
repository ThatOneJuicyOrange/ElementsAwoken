using System;
using ElementsAwoken.Items.Placeable.Tiles.Plateau;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles.VolcanicPlateau
{
    public class ActiveIgneousRock : ModTile
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

            drop = ModContent.ItemType<ActiveIgneousRockItem>();
            AddMapEntry(new Color(207, 114, 50));
            soundType = 21;
            soundStyle = 6;

            minPick = 70;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (MyWorld.awakenedPlateau)
            {
                Tile tile = Main.tile[i, j];
                if (tile.slope() == 0 && tile.halfBrick() == false)
                {
                    Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
                    if (Main.drawToScreen)
                    {
                        zero = Vector2.Zero;
                    }
                    int height = tile.frameY == 36 ? 18 : 16;
                    float num = MathHelper.Lerp(0.5f,1f,(float)((1 + Math.Sin(Main.GlobalTime * 3)) / 2));
                    Color color = Lighting.GetColor(i, j) * 3;
                    if (color == Color.Black) color = new Color(40, 40, 40);
                    color *= num;
                    Main.spriteBatch.Draw(mod.GetTexture("Tiles/VolcanicPlateau/ActiveIgneousRock_Glow"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, height), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                }
            }
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            IgneousRock.Geysers(i, j);
        }
        public override void RandomUpdate(int i, int j)
        {
            if (MyWorld.plateauWeather == 2)
            {
                IgneousRock.CheckGrowCrystal(i, j);
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
        }
    }
}