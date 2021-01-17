using ElementsAwoken.Items.ItemSets.Stellarium;
using ElementsAwoken.Items.Placeable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Tiles.VolcanicPlateau
{
    public class EruptionCrystalTileUnsafe : ModTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "ElementsAwoken/Tiles/VolcanicPlateau/EruptionCrystalTile";
            return true;
        }
        public override void SetDefaults()
        {
            Main.tileShine[Type] = 1100;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(255, 255, 125));

            soundType = 13;
        }
        public override bool CanPlace(int i, int j)
        {
            Tile below = Framing.GetTileSafely(i, j + 1);
            Tile above = Framing.GetTileSafely(i, j - 1);
            Tile right = Framing.GetTileSafely(i + 1, j);
            Tile left = Framing.GetTileSafely(i - 1, j);
            if ((Main.tileSolid[below.type] && below.active()) ||
                (Main.tileSolid[above.type] && above.active()) ||
                (Main.tileSolid[right.type] && right.active()) ||
                (Main.tileSolid[left.type] && left.active())) return true;
            return false;
        }
        public override bool Drop(int i, int j)
        {
            Tile t = Main.tile[i, j];
            int num = MyWorld.plateauWeather == 2 ? 2 : 1;
            Item.NewItem(i * 16, j * 16, 16, 16, ItemType<EruptionCrystal>(), num);
            return base.Drop(i, j);
        }
        public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
        {
            Tile below = Framing.GetTileSafely(i, j + 1);
            Tile above = Framing.GetTileSafely(i, j - 1);
            Tile right = Framing.GetTileSafely(i + 1, j);
            Tile left = Framing.GetTileSafely(i - 1, j);
            if (Main.tileSolid[below.type] && below.active()) frameYOffset = 0; // below
            else if (Main.tileSolid[above.type] && above.active()) frameYOffset = 18;// above
            else if (Main.tileSolid[right.type] && right.active()) frameYOffset = 36;// right
            else if (Main.tileSolid[left.type] && left.active()) frameYOffset = 54; // left
            else WorldGen.KillTile(i, j);
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (MyWorld.plateauWeather == 2 && j > Main.maxTilesY - 200)
            {
                Texture2D bloomTex = GetTexture("ElementsAwoken/Extra/Bloom"); 
                Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);

                Vector2 center = new Vector2(i + 0.5f, j + 0.5f) * 16 - Main.screenPosition + zero;

                spriteBatch.End();
                spriteBatch.Begin(default, BlendState.Additive);
                //new Color(247, 124, 130) * 0.6f * (1 - ((1 + (float)Math.Sin((float)MyWorld.generalTimer / 200)) / 5))
                Color color = Color.Lerp(new Color(247, 124, 130) * 0.3f, new Color(245, 188, 191) * 0.6f, (1 + (float)Math.Sin((float)MyWorld.generalTimer / 100)) / 2);
                spriteBatch.Draw(bloomTex, center, bloomTex.Frame(), color, 0, bloomTex.Size() / 2, 1f, 0, 0);

                spriteBatch.End();
                spriteBatch.Begin();
            }
        }
        public override void RandomUpdate(int i, int j)
        {
            if (MyWorld.plateauWeather != 2)
            {
                //if (Main.rand.NextBool(20))
                {
                    for (int p = 0; p < 3; p++)
                    {
                        Gore.NewGore(new Vector2(i + 0.5f, j + 0.5f) * 16, Vector2.Zero, mod.GetGoreSlot("Gores/EruptionCrystal" + p), 1f);
                    }
                    WorldGen.KillTile(i, j, noItem: true);
                }
            }
        }
    }
}