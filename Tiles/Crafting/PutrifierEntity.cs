using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Tiles.Crafting
{
    public class PutrifierEntity : ModTileEntity
    {
        public int soundCD = 0;
        public override void Update()
        {
            // just doesnt do anything in multiplayer wtf (added it so it only does this in singleplayer in case it does do it in multi and decies to bitch)
            if (Main.netMode == 0)
            {
                Item iron = null;
                Item fragment = null;
                for (int k = 0; k < Main.maxItems; k++)
                {
                    Item item = Main.item[k];
                    if (Vector2.Distance(item.Center, new Vector2(Position.X * 16, Position.Y * 16)) <= 50 && item.active)
                    {
                        if (item.type == mod.ItemType("SunFragment"))
                        {
                            fragment = item;
                            ChewDust(item);
                        }
                        if (item.type == ItemID.IronOre || item.type == ItemID.LeadOre)
                        {
                            iron = item;
                            ChewDust(item);
                        }
                    }
                }
                soundCD--;
                if (iron != null && fragment != null)
                {
                    if (iron.active && fragment.active)
                    {
                        if (iron.stack > 0 && fragment.stack > 0)
                        {
                            if (iron.stack > 1)
                            {
                                iron.stack--;
                                if (Main.netMode == NetmodeID.MultiplayerClient) NetMessage.SendData(MessageID.SyncItem, -1, -1, null, iron.whoAmI, 1f);
                            }
                            else
                            {
                                iron.active = false;
                                if (Main.netMode == NetmodeID.MultiplayerClient) NetMessage.SendData(MessageID.SyncItem, -1, -1, null, iron.whoAmI, 1f);
                            }
                            if (fragment.stack > 1)
                            {
                                fragment.stack--;
                                if (Main.netMode == NetmodeID.MultiplayerClient) NetMessage.SendData(MessageID.SyncItem, -1, -1, null, fragment.whoAmI, 1f);
                            }
                            else
                            {
                                fragment.active = false;
                                if (Main.netMode == NetmodeID.MultiplayerClient) NetMessage.SendData(MessageID.SyncItem, -1, -1, null, fragment.whoAmI, 1f);
                            }
                            int ore = Item.NewItem(Position.X * 16, Position.Y * 16, 32, 16, mod.ItemType("PutridOre"));
                            if (Main.netMode == NetmodeID.MultiplayerClient) NetMessage.SendData(MessageID.SyncItem, -1, -1, null, ore, 1f);
                            if (soundCD <= 0)
                            {
                                Main.PlaySound(SoundID.NPCDeath13, new Vector2(Position.X * 16, Position.Y * 16));
                                soundCD = 20;
                            }
                        }
                    }
                }
            }
        }
        private void ChewDust(Item item)
        {
            if (Main.rand.Next(20) == 0)
            {
                int dust = Dust.NewDust(item.Center, item.width, item.height, 2, 0f, 0f, 100, new Color(14, 122, 82));
            }
        }
        public override void NetSend(BinaryWriter writer, bool lightSend)
        {
            writer.Write(soundCD);
        }
        public override void NetReceive(BinaryReader reader, bool lightReceive)
        {
            soundCD = reader.ReadInt32();
        }
        public override bool ValidTile(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            return tile.active() && tile.type == TileType<Putrifier>() && tile.frameX == 0 && tile.frameY == 0;
        }

        public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
        {
            //Main.NewText("i " + i + " j " + j + " t " + type + " s " + style + " d " + direction);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                NetMessage.SendTileSquare(Main.myPlayer, i, j, 4);
                NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i, j, Type, 0f, 0, 0, 0);
                return -1;
            }
            return Place(i, j);
        }
    }
}