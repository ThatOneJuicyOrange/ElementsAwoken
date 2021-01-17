using System;
using ElementsAwoken.Items.Placeable.Tiles.Plateau;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles.VolcanicPlateau
{
    public class PyroclasticRockUnsafe : ModTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "ElementsAwoken/Tiles/VolcanicPlateau/PyroclasticRock";
            return true;
        }
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            EAUtils.MergeOtherPlateauTiles(Type);

            drop = ModContent.ItemType<PyroclasticRockItem>();
            AddMapEntry(new Color(22, 25, 36));
            soundType = 21;
            soundStyle = 6;

            minPick = 200;
        }
        public override void RandomUpdate(int i, int j)
        {
            Tile right = Framing.GetTileSafely(i + 1, j);
            Tile left = Framing.GetTileSafely(i - 1, j);
            if (!right.active())
            {
                right.liquid = 255;
                right.lava(true);
                WorldGen.SquareTileFrame(i + 1, j);
            }
            if (!left.active())
            {
                left.liquid = 255;
                left.lava(true);
                WorldGen.SquareTileFrame(i - 1, j);
            }
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!effectOnly)
            {
                Main.tile[i,j].liquid = 255;
                Main.tile[i, j].lava(true);
                WorldGen.SquareTileFrame(i + 1, j);
            }
        }
    }
}