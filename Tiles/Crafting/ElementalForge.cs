using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;

namespace ElementsAwoken.Tiles.Crafting
{
    public class ElementalForge : ModTile
    {
        public override void SetDefaults()
        {
			Main.tileLighted[Type] = true;
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Elemental Forge");
            AddMapEntry(new Color(133, 133, 133), name);
            disableSmartCursor = true;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
            TileObjectData.addTile(Type);
			adjTiles = new int[] { TileID.Furnaces };
			animationFrameHeight = 38;
			dustType = 6;
        }
 
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
 
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, mod.ItemType("ElementalForge"));
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
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = Main.DiscoR / 255f;
            g = Main.DiscoG / 255f;
            b = Main.DiscoB / 255f; 
        }
    }
}