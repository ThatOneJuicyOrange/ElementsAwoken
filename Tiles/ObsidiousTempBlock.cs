using ElementsAwoken.Items.Placeable.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles
{
    public class ObsidiousTempBlock : ModTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "ElementsAwoken/Projectiles/Blank";
            return true;
        }
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileLighted[Type] = true;
            TileID.Sets.DrawsWalls[Type] = true;
            AddMapEntry(new Color(74, 59, 97));
            dustType = 6;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Dust dust = Main.dust[Dust.NewDust(new Vector2(i, j) * 16, 16, 16, 6)];
            dust.velocity *= 0.4f;
            dust.scale *= 1.4f;
            dust.noGravity = true;
        }
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            if (!ModContent.GetInstance<Config>().debugMode) return false;
            return base.CanKillTile(i, j, ref blockDamaged);
        }
    }
}