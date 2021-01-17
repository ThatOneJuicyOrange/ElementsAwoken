using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Enums;
using System;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Tiles.Objects
{
    public class PowerCell : ModTile
    {
        public Point16 tilePoint = new Point16();
        public PowerCellEntity myEntity = null;
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;

            disableSmartCursor = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(GetInstance<PowerCellEntity>().Hook_AfterPlacement, -1, 0, true);
            AddMapEntry(new Color(154, 214, 213));
            animationFrameHeight = 90;

            TileObjectData.addTile(Type);
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            tilePoint = new Point16(i, j - 1); // it finds the bottom right while we want top left
            foreach (TileEntity current in TileEntity.ByID.Values)
            {
                if (current.type == TileEntityType<PowerCellEntity>())
                {
                    if (current.Position == tilePoint)
                    {
                        myEntity = (PowerCellEntity)current;
                    }
                }
            }
            if (myEntity != null)
            {
                Tile tile = Main.tile[i, j];
                if (tile.frameY == 0)
                {
                    Texture2D barTexture = mod.GetTexture("Tiles/PowerCellOverlay");
                    Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
                    if (Main.drawToScreen)
                    {
                        zero = Vector2.Zero;
                    }

                    int barHeight = (int)(barTexture.Height * ((float)myEntity.energy / (float)myEntity.maxEnergy));
                    Rectangle barDest = new Rectangle(i * 16 - (int)Main.screenPosition.X + (int)zero.X, j * 16 - (int)Main.screenPosition.Y + (int)zero.Y + 6 + barTexture.Height - barHeight, barTexture.Width, barHeight);
                    Rectangle barLength = new Rectangle(0, 0, barTexture.Width, barHeight);
                    spriteBatch.Draw(barTexture, barDest, barLength, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
                }
            }
        }
        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            if (myEntity != null)
            {
                player.showItemIconText = "Energy: " + myEntity.energy;
                player.noThrow = 2;
                player.showItemIcon = true;
            }
        }
        /*public override void RightClick(int i, int j)
        {
            Player player = Main.LocalPlayer;
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
            if (modPlayer.energy > 0 && myEntity.energy < myEntity.maxEnergy)
            {
                myEntity.energy++;
                modPlayer.energy--;
            }
        }*/
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 16, 32, mod.ItemType("PowerCell"));
            GetInstance<PowerCellEntity>().Kill(i, j);
        }        
    }
}