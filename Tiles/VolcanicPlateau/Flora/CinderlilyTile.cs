using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Tiles.VolcanicPlateau.Flora
{
    public class CinderlilyTile : ModTile
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
            if (stage != 0) Item.NewItem(i * 16, j * 16, 0, 0, ItemType<Items.Materials.Flowers.Cinderlily>());
            if (stage == 2) Item.NewItem(i * 16, j * 16, 0, 0, ItemType<Items.Materials.Flowers.CinderlilySeeds>(), Main.rand.Next(1, 4));
            return false;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            int stage = Main.tile[i, j].frameX / 18;
            if (stage == 2)
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, 6)];
                dust.noGravity = true;
                dust.scale *= 1f;
                dust.velocity.Y = Main.rand.NextFloat(-3, 0.5f);
            }
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (MyWorld.plateauWeather == 2)
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
                if (EAUtils.TileNearLava(i, j, 6)) Main.tile[i, j].frameX += 18;
            }
            else if (MyWorld.plateauWeather != 2)
            {
                Main.tile[i, j].frameX -= 18;
            }
        }
    }
}
