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

namespace ElementsAwoken.Tiles.VolcanicPlateau
{
    public class EriusCrystal : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
             
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.newTile.AnchorWall = false;
            TileObjectData.newTile.LavaDeath = false;

            AddMapEntry(new Color(46, 51, 25));
            disableSmartCursor = true;
            //TileObjectData.newTile.CoordinateHeights = new int[] { 16,16,16 };
            TileObjectData.addTile(Type);
        }
        public override bool CanExplode(int i, int j)
        {
            return false;
        }
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            if (!MyWorld.downedErius) return false;
            return base.CanKillTile(i, j, ref blockDamaged);
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen) zero = Vector2.Zero;
            Main.spriteBatch.Draw(mod.GetTexture("Tiles/VolcanicPlateau/EriusCrystal_Glow"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, 16), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            if (tile.frameX == 18 * 1 && tile.frameY == 18 * 1)  Lighting.AddLight(new Vector2(i * 16, j * 16), 0.75f, 0.95f, 0.5f);

            if (tile.frameX == 18 * 2 && tile.frameY == 18 * 2)
            {
                Texture2D bloomTex = ModContent.GetTexture("ElementsAwoken/Extra/Bloom");

                Vector2 center = new Vector2(i * 16 - 8, j * 16 - 8) - Main.screenPosition + zero;
                float pulseSpeed = 30;
                /*int eriusCube = NPC.FindFirstNPC(ModContent.NPCType<NPCs.Liftable.EriusCube>());
                if (eriusCube >= 0)
                {
                    pulseSpeed = MathHelper.Lerp(10, 30, Math.Min(Vector2.Distance(Main.npc[eriusCube].Center, new Vector2(i, j) * 16) / 800, 1f)); // makes it pulse funny when the cube is moving due to changing value
                }*/
                spriteBatch.End();
                spriteBatch.Begin(default, BlendState.Additive);
                Color color = Color.Lerp(new Color(205, 237, 116) * 0.4f, new Color(117, 217, 74) * 0.1f, (1 + (float)Math.Sin((float)MyWorld.generalTimer / pulseSpeed)) / 2);
                spriteBatch.Draw(bloomTex, center, bloomTex.Frame(), color, 0, bloomTex.Size() / 2, 0.75f, 0, 0);

                spriteBatch.End();
                spriteBatch.Begin();
            }
        }

    }
}