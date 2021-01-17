using ElementsAwoken.Items.ItemSets.ScarletSteel;
using ElementsAwoken.Items.Placeable.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles.VolcanicPlateau
{
    public class ScarletiteTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;

            EAUtils.MergeOtherPlateauTiles(Type);

            Main.tileSpelunker[Type] = true;
            TileID.Sets.Ore[Type] = true;
            Main.tileValue[Type] = 700;

            drop = ModContent.ItemType<Scarletite>();
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Scarletite");
            AddMapEntry(new Color(209, 86, 56), name);
            mineResist = 6f;
            minPick = 60;
            soundType = 21;
            soundStyle = 6;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (Main.rand.NextBool(5))
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, 6)];
                dust.noGravity = true;
                dust.scale *= 1.2f;
            }
        }
    }
}