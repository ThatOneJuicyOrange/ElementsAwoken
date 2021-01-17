using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Tiles.VolcanicPlateau.Flora
{
    public class PlateauVines : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileCut[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.AlternateTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.AnchorAlternateTiles = new int[]
            {
                TileType<PlateauVines>(),
                TileType<MalignantFlesh>(),
                TileType<ActiveIgneousRock>(),
                TileType<IgneousRock>()
            };
            TileObjectData.addTile(Type);
            soundType = SoundID.Grass;
            dustType = 14;
            AddMapEntry(new Color(251, 116, 7));
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (Main.tile[i, j - 1].type == Type && Main.tile[i, j - 1].active()) Main.tile[i, j].frameX = Main.tile[i,j - 1].frameX;
            if (Main.tile[i, j + 1].type != Type && Main.tile[i, j + 1].type != TileType<PlateauVineBulb>())
            {
                Main.tile[i, j].frameY = 36;
            }
            else if (j % 2 == 0) Main.tile[i, j].frameY = 18;
            else Main.tile[i, j].frameY = 0;
            return base.PreDraw(i, j, spriteBatch);
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            float lightScale = 0.2f;
            if ((tile.frameX / 18) == 3) Lighting.AddLight(new Vector2(i * 16, j * 16), 0.75f * lightScale, 0.95f * lightScale, 0.5f * lightScale);
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen) zero = Vector2.Zero;
            Main.spriteBatch.Draw(mod.GetTexture("Tiles/VolcanicPlateau/Flora/PlateauVines_Glow"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, 16), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
        public override void RandomUpdate(int i, int j)
        {
            if (!Main.tile[i,j+1].active())
            {
                int vineLength = 0;
                for (int y = 0; y < 12; y++)
                {
                    Tile t = Framing.GetTileSafely(i, j - y);
                    if (t.type == Type) vineLength++;
                    else break;
                }
                //if (Main.tile[i, j - 9].type != Type)
                if (vineLength < 10)
                {
                    WorldGen.PlaceObject(i, j + 1, TileType<PlateauVines>(), true);
                    NetMessage.SendObjectPlacment(-1, i, j + 1, TileType<PlateauVines>(), 0, 0, -1, -1);
                }
                else if (EAUtils.FindNumTilesNearby(TileType<PlateauVineBulb>(), i, j, 15) == 0)
                {
                    WorldGen.PlaceObject(i, j + 1, TileType<PlateauVineBulb>(), true);
                    NetMessage.SendObjectPlacment(-1, i, j + 1, TileType<PlateauVineBulb>(), 0, 0, -1, -1);
                }
            }
        }
        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            if (!Main.tile[i, j - 1].active() || Main.tile[i, j - 1].slope() != 0)
            {
                WorldGen.KillTile(i, j);
                WorldGen.SquareTileFrame(i, j);
            }
            else if (!ValidTile().Contains(Main.tile[i, j - 1].type))
            {
                WorldGen.KillTile(i, j);
                WorldGen.SquareTileFrame(i, j);
            }
            return true;
        }
        private List<int> ValidTile()
        {
            List<int> idList = new List<int>()
            {
                TileType<PlateauVines>(),
                TileType<MalignantFlesh>(),
                TileType<ActiveIgneousRock>(),
                TileType<SulfuricSediment>(),
                TileType<IgneousRock>()
            };
            return idList;
        }
    }
}
