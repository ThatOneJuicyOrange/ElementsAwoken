using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Tiles.VolcanicPlateau.Flora
{
    public class FlareberryBush : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoFail[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.AnchorValidTiles = new[]
            {
				TileType<AshGrass>(),
            };
            TileObjectData.addTile(Type);
            soundType = SoundID.Grass;
        }
        public override bool Drop(int i, int j)
        {
            if (Main.tile[i, j].frameY == 0 && Main.tile[i, j].frameX % 54 == 0)
            {
                int stage = Main.tile[i, j].frameX / 54;
                if (stage == 0) Item.NewItem(i * 16, j * 16, 0, 0, ItemType<Items.Consumable.Flareberries>());
                else if (stage == 1) Item.NewItem(i * 16, j * 16, 0, 0, ItemType<Items.Consumable.SpoiledFlareberries>());
            }
            return false;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)zero = Vector2.Zero;
            Main.spriteBatch.Draw(mod.GetTexture("Tiles/VolcanicPlateau/Flora/FlareberryBush_Glow"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, 16), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public override void RandomUpdate(int i, int j)
        {
            if (Main.rand.NextBool(15))
            {
                if (Main.tile[i, j].frameY == 0 && Main.tile[i, j].frameX % 54 == 0)
                {
                    if (Main.tile[i, j].frameX < 54)
                    {
                        for (int x = i; x <= i + 2; x++)
                            for (int y = j; y <= j + 2; y++)
                                Main.tile[x, y].frameX += 54;
                    }
                    else if (Main.tile[i, j].frameX >= 54)
                    {
                        WorldGen.KillTile(i, j, noItem: true);
                    }
                }
            }
        }
    }
}
