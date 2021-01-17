using ElementsAwoken.Items.Placeable.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles
{
    public class ShaditeTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;

            Main.tileSpelunker[Type] = true;
            TileID.Sets.Ore[Type] = true;
            Main.tileValue[Type] = 700;

            drop = ModContent.ItemType<Shadite>();
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Shadite");
            AddMapEntry(new Color(39, 31, 46), name);
            mineResist = 2f;
            minPick = 225;
            soundType = 21;
            soundStyle = 6;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (!Main.tile[i, j - 1].active())
            {
                if (Main.rand.NextBool())
                {
                    Dust dust = Main.dust[Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, 54)];
                    dust.noGravity = true;
                    dust.velocity.Y = 8 * Main.rand.NextFloat(-0.8f, -0.2f);
                    dust.fadeIn = 1.2f;
                    dust.velocity.X *= 0.4f;
                }
            }
        }
    }
}