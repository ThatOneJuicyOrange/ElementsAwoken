using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles
{
    public class ItemMagnetEntity : ModTileEntity
    {

        public override void Update()
        {
            Vector2 tileCenter = new Vector2(Position.X * 16, Position.Y * 16);
            for (int k = 0; k < Main.item.Length; k++)
            {
                Item sucked = Main.item[k];
                if (Vector2.Distance(sucked.Center, tileCenter) < 800)
                {
                    Vector2 toTarget = new Vector2(tileCenter.X - sucked.Center.X, tileCenter.Y - sucked.Center.Y);
                    toTarget.Normalize();
                    sucked.velocity += toTarget *= 0.3f;

                    if (Vector2.Distance(sucked.Center, tileCenter) < 30)
                    {
                        for (int chest = 0; chest < 1000; chest++)
                        {
                            Chest currentChest = Main.chest[chest];
                            if (currentChest != null && !Chest.isLocked(currentChest.x, currentChest.y) && Vector2.Distance(new Vector2(currentChest.x * 16, currentChest.y * 16), tileCenter) < 48)
                            {
                                // adding the items
                                foreach (Item chestItem in currentChest.item)
                                {
                                    if (sucked.IsTheSameAs(chestItem))
                                    {
                                        int available = chestItem.maxStack - chestItem.stack;
                                        int amount = Math.Min(available, sucked.stack);
                                        chestItem.stack += amount;
                                        sucked.stack -= amount;
                                        if (sucked.stack <= 0)
                                        {
                                            //sucked.SetDefaults(0, false);
                                            VanishItem(sucked);
                                            break;
                                        }
                                    }
                                }
                                for (int l = 0; l < currentChest.item.Length; l++)
                                {
                                    Item chestItem = currentChest.item[l];
                                    if (chestItem.type == 0)
                                    {
                                        chestItem.SetDefaults(sucked.type);
                                        chestItem.stack = sucked.stack;
                                        VanishItem(sucked);
                                        break;
                                    }
                                }
                            }
                        }
                    }                    
                }
            }
            for (int chest = 0; chest < 1000; chest++)
            {
                Chest currentChest = Main.chest[chest];
                if (currentChest != null && !Chest.isLocked(currentChest.x, currentChest.y) && Vector2.Distance(new Vector2(currentChest.x * 16, currentChest.y * 16), tileCenter) < 48)
                {
                    // sorting the coins
                    for (int l = 0; l < currentChest.item.Length; l++)
                    {
                        Item chestItem = currentChest.item[l];
                        if (chestItem.type == ItemID.CopperCoin && chestItem.stack == chestItem.maxStack)
                        {
                            ChangeCoinTo(1, currentChest, chestItem);
                        }
                        if (chestItem.type == ItemID.SilverCoin && chestItem.stack == chestItem.maxStack)
                        {
                            ChangeCoinTo(2, currentChest, chestItem);
                        }
                        if (chestItem.type == ItemID.GoldCoin && chestItem.stack == chestItem.maxStack)
                        {
                            ChangeCoinTo(3, currentChest, chestItem);
                        }
                    }
                }
            }
        }
        private void ChangeCoinTo(int type, Chest chest, Item coin)
        {
            /**
             * 0- copper
             * 1- silver
             * 2- gold
             * 3- platinum
            **/
            VanishItem(coin);
            Item newCoin = new Item();
            if (type == 1)
            {
                newCoin.SetDefaults(ItemID.SilverCoin, false);
            }
            else if (type == 2)
            {
                newCoin.SetDefaults(ItemID.GoldCoin, false);
            }
            else if(type == 3)
            {
                newCoin.SetDefaults(ItemID.PlatinumCoin, false);
            }
            foreach (Item chestItem in chest.item)
            {
                if (newCoin.IsTheSameAs(chestItem))
                {
                    int available = chestItem.maxStack - chestItem.stack;
                    int amount = Math.Min(available, newCoin.stack); // newcoin.stack is always 1 lol
                    chestItem.stack += amount;
                    newCoin.stack -= amount;
                    if (newCoin.stack <= 0)
                    {
                        newCoin.SetDefaults(0, false);
                        break;
                    }
                }
            }
            for (int l = 0; l < chest.item.Length; l++)
            {
                Item chestItem = chest.item[l];
                if (chestItem.type == 0)
                {
                    chestItem.SetDefaults(newCoin.type);
                    break;
                }
            }
        }
        private void VanishItem(Item item)
        {
            item.type = 0;
            item.stack = 0;
            item.netID = 0;
            item.active = false;
        }
        public override bool ValidTile(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            return tile.active() && tile.type == mod.TileType<ItemMagnet>() && tile.frameX == 0 && tile.frameY == 0;
        }

        public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
        {
            //Main.NewText("i " + i + " j " + j + " t " + type + " s " + style + " d " + direction);
            if (Main.netMode == 1)
            {
                NetMessage.SendTileSquare(Main.myPlayer, i, j, 3);
                NetMessage.SendData(87, -1, -1, null, i, j, Type, 0f, 0, 0, 0);
                return -1;
            }
            return Place(i, j);
        }
    }
}