using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Enums;

namespace ElementsAwoken.Tiles
{
    public class AutoDriller : ModTile
    {
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
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(mod.GetTileEntity<AutoDrillerEntity>().Hook_AfterPlacement, -1, 0, true);
            TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
            TileObjectData.newTile.Origin = new Point16(2, 3);
            TileObjectData.newTile.UsesCustomCanPlace = true;
            AddMapEntry(new Color(154, 214, 213));
            
            TileObjectData.addTile(Type);
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
                return true;
            }
            return false;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 16, 32, mod.ItemType("AutoDriller"));
            mod.GetTileEntity<AutoDrillerEntity>().Kill(i, j);
        }        
    }
}