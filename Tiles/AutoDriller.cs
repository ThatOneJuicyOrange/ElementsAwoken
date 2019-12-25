using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Enums;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Tiles
{
    public class AutoDriller : ModTile
    {
        public Point16 tilePoint = new Point16();
        public AutoDrillerEntity myEntity = null;
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;

            disableSmartCursor = true;

            TileObjectData.newTile.Width = 5;
            TileObjectData.newTile.Height = 5;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16 };
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(GetInstance<AutoDrillerEntity>().Hook_AfterPlacement, -1, 0, true);
            TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
            TileObjectData.newTile.Origin = new Point16(2, 3);
            TileObjectData.newTile.UsesCustomCanPlace = true;
            AddMapEntry(new Color(154, 214, 213));
            animationFrameHeight = 90;

            TileObjectData.addTile(Type);
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            tilePoint = new Point16(i - 4, j - 4); // it finds the bottom right while we want top left
        }
        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (myEntity == null)
            {
                foreach (TileEntity current in TileEntity.ByID.Values)
                {
                    if (current.type == TileEntityType<AutoDrillerEntity>())
                    {
                        if (current.Position == tilePoint)
                        {
                            myEntity = (AutoDrillerEntity)current;
                        }
                    }
                }
            }
            else
            {

                if (myEntity.enabled)
                {
                    frame = 1;
                }
                else
                {
                    frame = 0;
                }
            }
        }
        public override bool CanPlace(int i, int j)
        {
        Tile anchorLeft = Framing.GetTileSafely(i - 2, j + 2);
            Tile anchorRight = Framing.GetTileSafely(i + 2, j + 2);

            /*for (int k = 0; k < 50; k++)
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2((i - 2) * 16, (j + 2) * 16), 16, 16, 60)];
                dust.velocity *= 0.1f;
                dust.noGravity = true;

                Dust dust2 = Main.dust[Dust.NewDust(new Vector2((i + 2) * 16, (j + 2) * 16), 16, 16, 60)];
                dust2.velocity *= 0.1f;
                dust2.noGravity = true;
            }*/

            if (Main.tileSolid[anchorLeft.type] && anchorLeft.active() && Main.tileSolid[anchorRight.type] && anchorRight.active())
            {
                return base.CanPlace(i, j);
            }
            return false;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 16, 32, mod.ItemType("AutoDriller"));
            GetInstance<AutoDrillerEntity>().Kill(i, j);
        }        
    }
}