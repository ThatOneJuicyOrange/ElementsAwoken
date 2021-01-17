using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Tiles.VolcanicPlateau.Flora
{
    public class AshStem : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileCut[Type] = true;
            Main.tileNoFail[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileNoAttach[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
            TileObjectData.newTile.AnchorValidTiles = new[]
            {
				TileType<AshGrass>(),
                Type
            };
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
            TileObjectData.addTile(Type);
            soundType = SoundID.Grass;
        }
        public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
        {
            if (i % 2 == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
        }
        public override void PlaceInWorld(int i, int j, Item item)
        {
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            float lightScale = 0.2f;
            if (tile.frameY == 0) Lighting.AddLight(new Vector2(i * 16, j * 16), 0.75f * lightScale, 0.95f * lightScale, 0.5f * lightScale);
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen) zero = Vector2.Zero;
            float value = (float)(1f + Math.Sin(MyWorld.generalTimer / 30f + (float)i * (Math.PI / 8f))) / 2f;
            Color color = Color.Lerp(Color.White, new Color(255, 150, 255), value);
            Main.spriteBatch.Draw(mod.GetTexture("Tiles/VolcanicPlateau/Flora/AshStem_Glow"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, 16), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile above = Framing.GetTileSafely(i, j - 1);
            Tile below = Framing.GetTileSafely(i, j + 1);

            if (!above.active() || above.type != Type)
            {
                Main.tile[i, j].frameY = 0;
            }
            else if (below.type == Type && above.type == Type)
            {
                Main.tile[i, j].frameY = 18;
            }
            else
            {
                Main.tile[i, j].frameY = 36;
            }
            if (!below.active() || below.slope() != 0 || below.halfBrick()) WorldGen.KillTile(i, j);
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen) zero = Vector2.Zero;
            Main.spriteBatch.Draw(Main.tileTexture[Type], new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, tile.frameY == 36 ? 22 : 16), Lighting.GetColor(i, j), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            return false;
        }
        public override void RandomUpdate(int i, int j)
        {
            if (!Main.tile[i, j - 1].active())
            {
                int height = 0;
                for (int y = 0; y < 4; y++)
                {
                    Tile t = Framing.GetTileSafely(i, j + y);
                    if (t.type == Type) height++;
                    else break;
                }
                if (height < 3)
                {
                    WorldGen.PlaceObject(i, j - 1, TileType<AshStem>(), true);
                    Main.tile[i, j - 1].frameX = Main.tile[i, j].frameX;
                    NetMessage.SendObjectPlacment(-1, i, j - 1, TileType<AshStem>(), 0, 0, -1, -1);
                }
            }
        }
    }
}
