using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;

namespace ElementsAwoken.Tiles.Objects
{
    public class TheOrchardArcade : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = false;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.Height = 4;

            AddMapEntry(new Color(217, 137, 85));
            disableSmartCursor = true;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16 };
            TileObjectData.addTile(Type);
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<Items.Placeable.TheOrchardArcadeItem>());
        }
        public override bool NewRightClick(int i, int j)
        {
            if (!Main.LocalPlayer.GetModPlayer<TheOrchardPlayer>().inGame)
            {
                Main.LocalPlayer.GetModPlayer<TheOrchardPlayer>().inGame = true;
                Main.PlaySound(SoundID.MenuOpen);
            }
            return true;
        }
    }
}