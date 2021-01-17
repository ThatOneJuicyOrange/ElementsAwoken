using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using System;

namespace ElementsAwoken.Tiles.Objects
{
    public class ObsidiousDoor : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileSolid[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
            TileObjectData.newTile.Height = 1;
            TileObjectData.newTile.Width = 7;
            TileObjectData.newTile.LavaDeath = false;
            
            AddMapEntry(new Color(255, 136, 0));
            disableSmartCursor = true;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
            TileObjectData.addTile(Type);
			animationFrameHeight = 36;
        }
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }
        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.showItemIcon = true;
            player.showItemIcon2 = ItemType<Items.Other.ObsidiousDoorKey>();
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen) zero = Vector2.Zero;
            float value = (float)(1f + Math.Sin(MyWorld.generalTimer / 30f + (float)(tile.frameX / 18) * 0.44f)) / 2f;
            Color color = Color.Lerp(new Color(255, 165, 61), new Color(255, 122, 222), value);
            Main.spriteBatch.Draw(mod.GetTexture("Tiles/Objects/ObsidiousDoor_Glow"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, 16), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
        public override bool NewRightClick(int i, int j)
        {
            Player player = Main.LocalPlayer;
            if (player.HasItem(ItemType<Items.Other.ObsidiousDoorKey>()))
            {
                Item key = player.inventory[player.FindItem(ItemType<Items.Other.ObsidiousDoorKey>())];
                key.stack--;
                Tile tile = Main.tile[i, j];
                int frame = tile.frameX / 18;
                WorldGen.KillTile(i, j);
                for (int p = 0; p < 7; p++)
                {
                    WorldGen.PlaceTile(i - frame + p, j, TileID.Platforms, true, true, style: 13);
                }
            }
            return true;
        }
    }
}