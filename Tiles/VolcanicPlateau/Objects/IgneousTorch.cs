using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using ElementsAwoken.Items.Placeable;
using Terraria.Enums;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ElementsAwoken.Tiles.VolcanicPlateau.Objects
{
    public class IgneousTorch : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileLighted[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.newTile.Height = 1;
            TileObjectData.newTile.Width = 1;
            TileObjectData.newTile.Origin = new Point16(0,0);
            TileObjectData.newTile.LavaDeath = false;

            AddMapEntry(new Color(240, 64, 0));
            disableSmartCursor = true;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
            TileObjectData.addTile(Type);
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.9f;
            g = 0.75f;
            b = 0.3f;
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<IgneousTorchItem>());
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            ulong randSeed = Main.TileFrameSeed ^ (ulong)((long)j << 32 | (long)((ulong)i));
            Color color = new Color(100, 100, 100, 0);
            int width = 20;
            int offsetY = 0;
            if (WorldGen.SolidTile(i, j - 1))
            {
                offsetY = 2;
                if (WorldGen.SolidTile(i - 1, j + 1) || WorldGen.SolidTile(i + 1, j + 1))
                {
                    offsetY = 4;
                }
            }
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            for (int k = 0; k < 7; k++)
            {
                float x = (float)Utils.RandomInt(ref randSeed, -10, 11) * 0.15f;
                float y = (float)Utils.RandomInt(ref randSeed, -10, 1) * 0.35f;
                Main.spriteBatch.Draw(mod.GetTexture("Tiles/VolcanicPlateau/Objects/IgneousTorch_Flame"), new Vector2((float)(i * 16 - (int)Main.screenPosition.X) - (width - 16f) / 2f + x + 6, (float)(j * 16 - (int)Main.screenPosition.Y + offsetY) + y - 10) + zero, null, color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
            }
        }

    }
}