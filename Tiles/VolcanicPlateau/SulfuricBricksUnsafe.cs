using System;
using ElementsAwoken.Items.Placeable.Tiles.Plateau;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles.VolcanicPlateau
{
    public class SulfuricBricksUnsafe : ModTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "ElementsAwoken/Tiles/VolcanicPlateau/SulfuricBricks";
            return true;
        }
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            EAUtils.MergeOtherPlateauTiles(Type);

            drop = ModContent.ItemType<SulfuricBricksItem>();
            AddMapEntry(new Color(46, 51, 25));
            soundType = 21;
            soundStyle = 6;

            minPick = 50;
        }
        public override bool CanExplode(int i, int j)
        {
            return false;
        }
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            if (!MyWorld.downedErius) return false;
            return base.CanKillTile(i, j, ref blockDamaged);
        }
    }
}