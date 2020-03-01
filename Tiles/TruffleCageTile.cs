using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;

namespace ElementsAwoken.Tiles
{
    public class TruffleCageTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            ModTranslation name = CreateMapEntryName();
            AddMapEntry(new Color(217, 137, 85));
            disableSmartCursor = true;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
            TileObjectData.addTile(Type);
			animationFrameHeight = 36;
        }

 
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            if (Main.rand.Next(6) == 0)
            {
                Vector2 pos = new Vector2(i * 16, j * 16);
                NPC npc = Main.npc[NPC.NewNPC((int)pos.X + 24, (int)pos.Y+16, NPCID.TruffleWorm)];
                npc.immune[Main.myPlayer] = 60;
                npc.ai[1] = -120;
                for (int p = 0; p < 120; p++)
                {
                    Dust dust = Main.dust[Dust.NewDust(pos, 48, 32, 13)];
                }
                Main.PlaySound(SoundID.Shatter, (int)pos.X, (int)pos.Y);
            }
            else Item.NewItem(i * 16, j * 16, 32, 16, mod.ItemType("TruffleCage"));
        }
		
		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			frameCounter++;
			if (frameCounter > 560)
			{
				frameCounter = 500;
				frame++;
				if (frame > 18)
				{
					frame = 0;
                    frameCounter = 0;
                }
            }
		}
    }
}