using ElementsAwoken.Items.Placeable.Tiles.Plateau;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles.VolcanicPlateau
{
    public class VoidMoss : ModTile
    {
        public static int _type;

        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            //SetModTree(new treehere());
            Main.tileBlendAll[this.Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            dustType = 31;
            AddMapEntry(new Color(241, 118, 222));
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!effectOnly) Main.tile[i, j].type = (ushort)ModContent.TileType<IgneousRock>();
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.5f;
            g = 0.25f;
            b = 0.5f;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (Main.rand.NextBool(80))
            {
                if (!Framing.GetTileSafely(i, j - 1).active())
                    {
                    Dust dust = Main.dust[Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.PinkFlame)];
                    dust.velocity.X *= 0.5f;
                    dust.velocity.Y = Main.rand.NextFloat(-5, -2);
                    dust.noGravity = true;
                    dust.fadeIn = 1;
                }
            }
            ulong randSeed = Main.TileFrameSeed ^ (ulong)((long)j << 32 | (long)((ulong)i));
            Color color = new Color(100, 100, 100, 0);
            int frameX = Main.tile[i, j].frameX;
            int frameY = Main.tile[i, j].frameY;
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen) zero = Vector2.Zero;
            for (int k = 0; k < 7; k++)
            {
                float x = (float)Utils.RandomInt(ref randSeed, -5, 6) * 0.15f;
                float y = (float)Utils.RandomInt(ref randSeed, -5, 6) * 0.35f;
                Main.spriteBatch.Draw(mod.GetTexture("Tiles/VolcanicPlateau/VoidMoss_Glow"), new Vector2((float)(i * 16 - (int)Main.screenPosition.X) + x, (float)(j * 16 - (int)Main.screenPosition.Y) + y) + zero, new Rectangle(frameX, frameY, 16, 16), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
            }
        }
        public override void FloorVisuals(Player player)
        {
            if (Math.Abs(player.velocity.X) > 1)
            {
                if (Main.rand.NextBool(3))
                {
                    Dust dust = Main.dust[Dust.NewDust(player.Bottom, player.width, 2, DustID.PinkFlame, 0, -2)];
                    dust.velocity.X *= 0.5f;
                }
            }
        }
        public override void RandomUpdate(int i, int j)
        {
            if (MyWorld.downedVoidLeviathan)
            {
                if (Main.rand.NextBool(500)) Main.tile[i, j].type = (ushort)ModContent.TileType<IgneousRock>();
            }
            else
            {
                for (int x = i - 1; x <= i + 1; x++)
                {
                    for (int y = j - 1; y <= j + 1; y++)
                    {
                        if ((x != i || j != y) && Main.tile[x, y].active() && Main.tile[x, y].type == ModContent.TileType<IgneousRock>())
                        {
                            if (EAUtils.FindNumTilesNearby(Type, i, j, 25) < 10)
                            {
                                Tile below = Framing.GetTileSafely(x, y + 1);
                                Tile above = Framing.GetTileSafely(x, y - 1);
                                Tile right = Framing.GetTileSafely(x + 1, y);
                                Tile left = Framing.GetTileSafely(x - 1, y);
                                if (((!Main.tileSolid[below.type] && below.active()) || !below.active()) ||
                                    ((!Main.tileSolid[above.type] && above.active()) || !above.active()) ||
                                    ((!Main.tileSolid[right.type] && right.active()) || !right.active()) ||
                                    ((!Main.tileSolid[left.type] && left.active()) || !left.active()))
                                {
                                    Main.tile[x, y].type = Type;
                                    WorldGen.SquareTileFrame(x, y, true);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}