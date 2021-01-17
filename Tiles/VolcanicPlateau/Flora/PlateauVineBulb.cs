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
    public class PlateauVineBulb : ModTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "ElementsAwoken/Projectiles/Blank";
            return true;
        }
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileCut[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.AlternateTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.AnchorAlternateTiles = new int[]
            {
                TileType<PlateauVines>()
            };
            TileObjectData.addTile(Type);
            soundType = SoundID.Grass;
            dustType = 14;
            AddMapEntry(new Color(251, 116, 7));
        }
        public override bool Drop(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            if ((tile.frameX / 18) != 3)
            {
                if (Main.rand.NextBool(10))
                {
                    Item.NewItem(i * 16, j * 16, 16, 16, ItemType<Items.ItemSets.ScarletSteel.Scarletite>(), Main.rand.Next(1, 4));
                }
                else
                {
                    Item.NewItem(i * 16, j * 16, 16, 16, ItemID.Hellstone, Main.rand.Next(3, 5));
                }
            }
            else
            {
                Item.NewItem(i * 16, j * 16, 16, 16, ItemType<Items.Materials.AcidDrop>(), Main.rand.Next(1, 4));
            }
            return base.Drop(i, j);
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Main.tile[i, j].frameX = Main.tile[i, j - 1].frameX;

            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen) zero = Vector2.Zero;
            Texture2D tex = mod.GetTexture("Tiles/VolcanicPlateau/Flora/PlateauVineBulbTex");
            Vector2 drawPos = new Vector2(i * 16 + 8 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero;
            Main.spriteBatch.Draw(tex, drawPos, new Rectangle(0, (tile.frameX / 18) * 30, 24, 30), Lighting.GetColor(i, j), 0f, new Vector2(tex.Width / 2, 0), 1f, SpriteEffects.None, 0f);
            return true;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            if ((tile.frameX / 18) != 3) Lighting.AddLight(new Vector2(i * 16, j * 16), 0.95f, 0.75f, 0.5f);
            else Lighting.AddLight(new Vector2(i * 16, j * 16), 0.75f, 0.95f, 0.5f);    
                Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen) zero = Vector2.Zero;
            Texture2D tex = mod.GetTexture("Tiles/VolcanicPlateau/Flora/PlateauVineBulbTex_Glow");
            Vector2 drawPos = new Vector2(i * 16 + 8 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero;
            Main.spriteBatch.Draw(tex, drawPos, new Rectangle(0, (tile.frameX / 18) * 30, 24, 30), Color.White, 0f, new Vector2(tex.Width / 2, 0), 1f, SpriteEffects.None, 0f);
        }
        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            if (!Main.tile[i, j - 1].active() || Main.tile[i, j - 1].type != TileType<PlateauVines>())
            {
                WorldGen.KillTile(i, j);
                WorldGen.SquareTileFrame(i, j);
            }
            return true;
        }
    }
}
