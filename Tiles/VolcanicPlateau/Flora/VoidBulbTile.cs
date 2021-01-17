using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Tiles.VolcanicPlateau.Flora
{
    public class VoidBulbTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileCut[Type] = true;
            Main.tileNoFail[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.StyleAlch);
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.AnchorValidTiles = new[]
            {
                TileType<SulfuricSediment>(),
                TileType<MalignantFlesh>(),
                TileType<AshGrass>(),
                TileType<IgneousRock>(),
                TileType<ActiveIgneousRock>()
            };
            TileObjectData.newTile.AnchorAlternateTiles = new[]
            {
                78,
				TileID.PlanterBox
            };
            TileObjectData.addTile(Type);
            soundType = SoundID.Grass;
        }
        public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
        {
            if (i % 2 == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
        }

        public override bool Drop(int i, int j)
        {
            int stage = Main.tile[i, j].frameX / 18;
            if (stage != 0) Item.NewItem(i * 16, j * 16, 0, 0, ItemType<Items.Materials.Flowers.VoidBulb>());
            if (stage == 2) Item.NewItem(i * 16, j * 16, 0, 0, ItemType<Items.Materials.Flowers.VoidBulbSeeds>(), Main.rand.Next(1, 4));
            return false;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            int stage = Main.tile[i, j].frameX / 18;
            if (stage == 2)
            {
                if (Main.rand.NextBool(200))
                {
                    Dust dust = Main.dust[Dust.NewDust(new Vector2(i * 16 + 2, j * 16), 12, 8, DustID.PinkFlame)];
                    dust.noGravity = true;
                    dust.scale *= 0.5f;
                    dust.velocity.Y = Main.rand.NextFloat(-3, 0.5f);
                }
            }
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (MyWorld.plateauWeather == 3)
            {
                if (Main.tile[i, j].frameX == 18) Main.tile[i, j].frameX += 18;
            }
            return base.PreDraw(i, j, spriteBatch);
        }
        public override void RandomUpdate(int i, int j)
        {
            if (Main.tile[i, j].frameX == 0)
            {
                Main.tile[i, j].frameX += 18;
            }
            else if (Main.tile[i, j].frameX == 18)
            {
                if (MyWorld.downedVoidLeviathan) Main.tile[i, j].frameX += 18;
            }
            else if(MyWorld.plateauWeather != 3)
            {
                Main.tile[i, j].frameX -= 18;
            }
        }
    }
}
