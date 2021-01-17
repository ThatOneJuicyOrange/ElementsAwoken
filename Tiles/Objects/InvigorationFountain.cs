using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ElementsAwoken.Tiles.Objects
{
    public class InvigorationFountain : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = false;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
            TileObjectData.newTile.Height = 5;
            TileObjectData.newTile.Width = 3;
            TileObjectData.newTile.Origin = new Point16(1, 4);

            AddMapEntry(new Color(128, 128, 128));
            disableSmartCursor = true;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16 };
            TileObjectData.addTile(Type);
			animationFrameHeight = 90;
            mineResist = 4f;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            //Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<Items.Placeable.OrderBanner>());
        }
        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frameCounter++;
            if (frameCounter > 6)
            {
                frameCounter = 0;
                frame++;
                if (frame > 5)
                {
                    frame = 0;
                }
            }
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            Player player = Main.LocalPlayer;
            Tile tile = Framing.GetTileSafely(i, j);
            if (tile.frameX == 18 && tile.frameY == 72)
            {
                int fountWidth = 22;
                int fountDepth = 14;
                // buff
                if (player.wet && player.Left.X >= (i- fountWidth / 2) * 16 && player.Right.X <= (i + fountWidth / 2) * 16 && player.Top.Y >= j * 16 && player.Bottom.Y <= (j + fountDepth) * 16)
                {
                    player.AddBuff(ModContent.BuffType<Buffs.Invigorated>(), 3600);
                }
                    // dust

                for (int x = -fountWidth / 2; x < fountWidth / 2; x++)
                {
                    for (int y = 0; y < fountDepth; y++)
                    {
                        if (Main.rand.NextBool(20))
                        {
                            Tile water = Framing.GetTileSafely(i + x, j + y);
                            if (water.liquid > 100 && !water.lava() && !water.honey())
                            {
                                int dust = Dust.NewDust(new Vector2(i + x, j + y) * 16, 16, 16, ModContent.DustType<Dusts.InvigorationDust>());
                                Main.dust[dust].velocity *= 0.4f;
                                Main.dust[dust].scale *= 0.3f;
                                Main.dust[dust].noGravity = true;
                            }
                        }
                    }
                }
            }
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Framing.GetTileSafely(i, j);
            if (tile.frameX == 18 * 1 && tile.frameY == 18 * 2)
            {
                Lighting.AddLight(new Vector2(i * 16, j * 16),0.3f,0.3f,0.5f);
            }
            if (tile.frameX == 0 && tile.frameY == 0)
            {
                Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
                Vector2 center = new Vector2(i * 16 + 26, j * 16 + 22) - Main.screenPosition + zero;
                Texture2D tex = ModContent.GetTexture("ElementsAwoken/Extra/FountainCrystal");

                float num4 = (float)MyWorld.generalTimer / 100f;
                for (float num6 = 0f; num6 < 1f; num6 += 0.25f)
                {
                    spriteBatch.Draw(tex, center + new Vector2(0f, 8f).RotatedBy((double)((num6 + num4) * 6.28318548f), default(Vector2)), tex.Frame(), new Color(70, 129, 255, 50), 0, tex.Size() / 2, 1f, SpriteEffects.None, 0f);
                }
                for (float num7 = 0f; num7 < 1f; num7 += 0.34f)
                {
                    spriteBatch.Draw(tex, center + new Vector2(0f, 4f).RotatedBy((double)((num7 + num4) * 6.28318548f), default(Vector2)), tex.Frame(), new Color(120, 152, 255, 77), 0, tex.Size() / 2, 1f, SpriteEffects.None, 0f);
                }
            }
            return true;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Framing.GetTileSafely(i, j);
            if (tile.frameX == 18 * 2 && tile.frameY == 18 * 4)
            {
                Texture2D bloomTex = ModContent.GetTexture("ElementsAwoken/Extra/Bloom");
                Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);

                Vector2 center = new Vector2(i * 16 - 8, j * 16 - 46) - Main.screenPosition + zero;

                spriteBatch.End();
                spriteBatch.Begin(default, BlendState.Additive);
                Color color = Color.Lerp(new Color(140, 255, 255) * 0.4f, new Color(166, 185, 255) * 0.1f, (1 + (float)Math.Sin((float)MyWorld.generalTimer / 30)) / 2);
                spriteBatch.Draw(bloomTex, center, bloomTex.Frame(), color, 0, bloomTex.Size() / 2, 0.75f, 0, 0);

                spriteBatch.End();
                spriteBatch.Begin();
            }
        }
    }
}