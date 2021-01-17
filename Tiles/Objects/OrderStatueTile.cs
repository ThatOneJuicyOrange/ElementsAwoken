using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using ElementsAwoken.Items.Placeable;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Tiles.Objects
{
    public class OrderStatueTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style5x4);
            TileObjectData.newTile.Height = 10;
            TileObjectData.newTile.Width = 5;
            TileObjectData.newTile.Origin = new Point16(2, 9);
            TileObjectData.newTile.LavaDeath = false;

            AddMapEntry(new Color(28, 27, 44));
            disableSmartCursor = true;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16, 16, 16, 16, 16, 16 };
            TileObjectData.addTile(Type);
        }
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            if (ModContent.GetInstance<Config>().debugMode) return true;
            if (!MyWorld.cultArrived) Main.PlaySound(2, i * 16, j * 17, 119, 2, -0.5f);
            MyWorld.cultArrived = true;
            if (Main.netMode == 2) NetMessage.SendData(MessageID.WorldData);
            return false;
        }
        public override bool CanExplode(int i, int j)
        {
            return false;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<OrderStatue>());
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (MyWorld.cultArrived)
            {
                Tile tile = Main.tile[i, j];
                Vector2 center = new Vector2(i * 16 + 40, j * 16 + 20);
                int dustWidth = 40;
                if (tile.frameY == 0 && tile.frameX == 0)
                {
                    for (int d = 0; d < 18; d++)
                    {
                        Vector2 position = center + Main.rand.NextVector2Circular(dustWidth * 0.5f, dustWidth * 0.5f);
                        Dust dust = Dust.NewDustPerfect(position, 54, Vector2.Zero);
                        dust.noGravity = true;
                        dust.fadeIn = 1f;
                        dust.scale *= 0.5f;
                        dust.velocity.Y = -8 * Main.rand.NextFloat(0.3f, 1f);
                        dust.velocity.X = Main.rand.NextFloat(-1, 1);
                    }
                }
            }
        }
    }
}