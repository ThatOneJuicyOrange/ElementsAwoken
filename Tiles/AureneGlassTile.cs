using System;
using System.IO;
using ElementsAwoken.Items.Placeable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles
{
    public class AureneGlassTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlendAll[Type] = true;
            TileID.Sets.DrawsWalls[Type] = true;
            drop = ModContent.ItemType<Items.Placeable.Tiles.AureneGlass>();
            AddMapEntry(new Color(100, 93, 143));
            soundType = 13;

        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
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
                Color color = new Color(255, 255, 255, 90);
                Main.spriteBatch.Draw(mod.GetTexture("Tiles/AureneGlassShadow"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, height), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}