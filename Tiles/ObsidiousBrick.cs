using ElementsAwoken.Items.Placeable.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles
{
    public class ObsidiousBrick : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true; 
            Main.tileLighted[Type] = true;
            Main.tileMerge[Type][ModContent.TileType<ObsidiousArenaManager>()] = true;
            Main.tileMerge[Type][TileID.Ash] = true;
            //drop = ModContent.ItemType<FirebrickItem>();
            ModTranslation name = CreateMapEntryName();
            AddMapEntry(new Color(74, 59, 97));
            soundType = 21;
            soundStyle = 6;
        }
    }
}