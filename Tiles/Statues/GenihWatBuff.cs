using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using System.Collections.Generic;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Tiles.Statues
{
    public class GenihWatBuff : ModTile
    {
        bool swearing = true;
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
            TileObjectData.newTile.Height = 4;
            TileObjectData.newTile.Width = 2;
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(144, 148, 144));
            dustType = 11;
            disableSmartCursor = true;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (Main.rand.Next(200) == 0 && swearing)
            {
                Swear(i, j);
            }
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, mod.ItemType("GenihWatBuffStatue"));
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            Player player = Main.player[Main.myPlayer];
            if (closer && player.FindBuffIndex(mod.BuffType("StatueBuffBurst")) == -1 && player.FindBuffIndex(mod.BuffType("StatueBuffAmadis")) == -1 && player.FindBuffIndex(mod.BuffType("StatueBuffRanipla")) == -1 && player.FindBuffIndex(mod.BuffType("StatueBuffOrange")) == -1 && player.FindBuffIndex(mod.BuffType("StatueBuffOinite")) == -1)
            {
                player.AddBuff(mod.BuffType("StatueBuffGenihWat"), 60, true);
            }
            if (swearing)
            {
                MyWorld.swearingEnemies = true;
            }
            else
            {
                MyWorld.swearingEnemies = false;
            }
        }
        public override bool NewRightClick(int i, int j)
        {
            Main.PlaySound(SoundID.MenuTick);
            if (swearing)
            {
                swearing = false;
            }
            else
            {
                swearing = true;
                Swear(i, j);
            }
            return true;

        }
        private void Swear(int posX, int posY)
        {
            string s = "";
            for (int l = 0; l < 4; l++)
            {
                int choice = Main.rand.Next(7);
                if (choice == 0)
                {
                    s = s + "$";
                }
                if (choice == 1)
                {
                    s = s + "#";
                }
                if (choice == 2)
                {
                    s = s + "!";
                }
                if (choice == 3)
                {
                    s = s + "$";
                }
                if (choice == 4)
                {
                    s = s + "@";
                }
                if (choice == 5)
                {
                    s = s + "*";
                }
                if (choice == 6)
                {
                    s = s + "?";
                }
            }
            CombatText.NewText(new Rectangle(posX * 16, posY * 16, 32, 64), Color.Red, s, false, false);
        }
    }
}