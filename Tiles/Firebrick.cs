using ElementsAwoken.Items.Placeable.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles
{
    public class Firebrick : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true; 
            Main.tileLighted[Type] = true;

            drop = ModContent.ItemType<FirebrickItem>();
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Fire Brick");
            AddMapEntry(new Color(255, 136, 0), name);
            soundType = 21;
            soundStyle = 6;
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b) 
        {
            r = 0.25f;
            g = 0f;
            b = 0.05f;
        }
    }
}