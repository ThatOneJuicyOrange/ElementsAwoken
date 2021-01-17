using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using ElementsAwoken.Items.Placeable;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ElementsAwoken.Tiles.Objects
{
    public class FlagTest : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = false;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
            TileObjectData.newTile.Height = 3;

            AddMapEntry(new Color(217, 137, 85));
            disableSmartCursor = true;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16,16 };
            TileObjectData.addTile(Type);
			animationFrameHeight = 36;
        }

 
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<FlagTestItem>());
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile t = Main.tile[i, j];
            if (t.frameY == 0 || t.frameY == 18)
            {
                Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
                if (Main.drawToScreen) zero = Vector2.Zero;

                Texture2D texture = Main.magicPixel;
                Vector2 start = new Vector2(i + 0.5f, j + 0.5f) * 16;
                Vector2 position = start;
                float windScale = MathHelper.Clamp(-Main.windSpeed / 0.4f, -1f, 1f);
                float yScale = windScale >= 0 ? 1 - windScale : 1 + windScale;
                //Vector2 desiredPos = new Vector2(windScale * 80, yScale * 80);
                float rot = 110 * windScale;
                Vector2 desiredPos = new Vector2(0, 80).RotatedBy(MathHelper.ToRadians(rot));
                Vector2 mountedCenter = position + desiredPos;
                int size = 3;
                Vector2 vector2_4 = mountedCenter - position;
                float max = Vector2.Distance(mountedCenter, position);
                float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
                bool flag = true;
                if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                    flag = false;
                if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
                    flag = false;
                float num = 0;
                while (flag)
                {
                    if ((double)vector2_4.Length() < (double)size + 1.0)
                    {
                        flag = false;
                    }
                    else
                    {
                        num++;
                        size = (int)MathHelper.Lerp(1, 4, Vector2.Distance(mountedCenter, position) / max);
                        Vector2 vector2_1 = vector2_4;
                        vector2_1.Normalize();
                        position += vector2_1 * (float)size;
                        vector2_4 = mountedCenter - position;                        
                        float waveLength = 4;
                        float waveSizeScale = (Math.Abs(windScale)) * 2;
                        Vector2 offset = new Vector2((float)Math.Sin(num / waveLength - (float)MyWorld.generalTimer / 6f) * waveSizeScale, 0); // * (float)Math.Sin((float)MyWorld.generalTimer / 40f) * 3
                        Color color = new Color(135, 50, 67) * ((float)Lighting.GetColor(i, j).R / 255f);
                        float test = MathHelper.Clamp(Vector2.Distance(start, position) / 18f, 0, 1);
                        Vector2 drawPos = position - Main.screenPosition + offset.RotatedBy(rotation) * test + zero;
                        Main.spriteBatch.Draw(texture, drawPos, new Rectangle?(new Rectangle(0, 0, size, size)), color, rotation, (Vector2.One * size)/ 2f, 2f, SpriteEffects.None, 0.0f);
                    }
                }
            }
            return true;
        }
    }
}