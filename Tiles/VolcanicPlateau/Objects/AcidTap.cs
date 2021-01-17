using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using ElementsAwoken.Items.Placeable;
using ElementsAwoken.Projectiles.Environmental;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ElementsAwoken.Tiles.VolcanicPlateau.Objects
{
    public class AcidTap : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileLighted[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<AcidTapEntity>().Hook_AfterPlacement, -1, 0, true);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.Origin = new Point16(0, 2);
            TileObjectData.newTile.LavaDeath = false;

            AddMapEntry(new Color(149, 173, 87));
            disableSmartCursor = true;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
            TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.addTile(Type);
            animationFrameHeight = 36;
            minPick = 150;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<AcidTapItem>());
            ModContent.GetInstance<AcidTapEntity>().Kill(i, j);
            WorldGen.PlaceObject(i, j + 2, ModContent.TileType<AcidGeyser>(), true);
            //Point origin = new Point(i - frameX / 18, j - frameY / 18 + 2);
            //WorldGen.PlaceObject(origin.X, origin.Y, ModContent.TileType<AcidGeyser>(), true);
        }
        public override bool NewRightClick(int i, int j)
        {
            UI.AcidTapUI.Visible = true;
            Main.playerInventory = true;
            Tile t = Main.tile[i, j];
            Point topLeft = new Point(i - t.frameX / 18, j - t.frameY / 18);
            UI.AcidTapUI.tapX = topLeft.X;
            UI.AcidTapUI.tapY = topLeft.Y;
            Main.PlaySound(SoundID.MenuOpen, new Vector2(-1, -1));
            return true;
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            Tile tile = Main.tile[i, j];
            if (tile.frameX == 0 && tile.frameY == 0)
            {
                float scale = MathHelper.Lerp(0.7f, 1f, (float)((1 + Math.Sin((float)MyWorld.generalTimer / 30)) / 2));
                r = 0.95f * scale;
                g = 0.95f * scale;
                b = 0.6f * scale;
            }
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {

            Tile tile = Main.tile[i, j];
            if (tile.frameX == 0 && tile.frameY == 0)
            {
                int num = 3;
                for (int l = 0; l < num; l++)
                {
                    float off = (float)l * (6.28f / (float)num);
                    float scalePulse = MathHelper.Lerp(0.9f, 1.4f, (float)((1 + Math.Sin((float)MyWorld.generalTimer / 10 + off)) / 2));
                    Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
                    if (Main.drawToScreen) zero = Vector2.Zero;
                    Texture2D tex = mod.GetTexture("Tiles/VolcanicPlateau/Objects/AcidTap_Flame");
                    Vector2 drawPos = new Vector2(i * 16 + 5 - (int)Main.screenPosition.X, j * 16 + 9 - (int)Main.screenPosition.Y) + zero;
                    Color color = new Color(255, 255, 255, 0);
                    Main.spriteBatch.Draw(tex, drawPos, new Rectangle(0, (tile.frameX / 18) * 30, 6, 10), color, 0f, tex.Size() / 2, scalePulse, SpriteEffects.None, 0f);
                }
            }
        }
    }
}