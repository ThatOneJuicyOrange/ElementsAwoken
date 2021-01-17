using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using ElementsAwoken.Items.Placeable;
using Terraria.Enums;

namespace ElementsAwoken.Tiles.VolcanicPlateau.Objects
{
    public class SulfuricPillar : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.Height = 1;
            TileObjectData.newTile.Width = 3;
            TileObjectData.newTile.Origin = new Point16(1, 0);
            TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
            TileObjectData.newTile.LavaDeath = false;

            AddMapEntry(new Color(46, 51, 25));
            disableSmartCursor = true;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
            TileObjectData.addTile(Type);
        }

        public override bool CanPlace(int i, int j)
        {
            if (Main.gameMenu) return base.CanPlace(i, j); // for world gen
            bool solidBottom = true;
            for (int x = -1; x < 2; x++)
            {
                Tile below = Framing.GetTileSafely(i + x, j + 1);
                if (!(Main.tileSolid[below.type] || below.type ==ModContent.TileType<SulfuricPillar>()) || !below.active())
                { 
                    solidBottom = false;
                    break;
                }
            }
            if (solidBottom)  return base.CanPlace(i, j);
            return false;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<SulfuricPillarItem>());
        }
        public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
        {
            Tile above = Framing.GetTileSafely(i, j - 1);
            Tile below = Framing.GetTileSafely(i, j + 1);
            if (Main.tileSolid[below.type])
            {
                frameYOffset = 36;
            }
            else if (Main.tileSolid[above.type])
            {
                frameYOffset = 0;
            }
            else
            {
                frameYOffset = 18;
            }
        }
    }
}