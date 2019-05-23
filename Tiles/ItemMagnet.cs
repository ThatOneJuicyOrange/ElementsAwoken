using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Tiles
{
    public class ItemMagnet : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;

            disableSmartCursor = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(mod.GetTileEntity<ItemMagnetEntity>().Hook_AfterPlacement, -1, 0, true);

            AddMapEntry(new Color(244, 237, 39));

            drop = mod.ItemType("ItemMagnetTile");

            TileObjectData.addTile(Type);
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            mod.GetTileEntity<ItemMagnetEntity>().Kill(i, j);
        }
        /*public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Vector2 tileCenter = new Vector2(i * 16, j * 16);
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
                            if (currentChest != null && !Chest.isLocked(currentChest.x, currentChest.y) && Vector2.Distance(new Vector2(currentChest.x * 16, currentChest.y * 16), tileCenter) < 32)
                            {
                                for (int p = 0; p < currentChest.item.Length; p++)
                                {
                                    // add existing item
                                    if (sucked.IsTheSameAs(currentChest.item[p]) && currentChest.item[p].stack + sucked.stack < currentChest.item[p].maxStack)
                                    {
                                        currentChest.item[p].stack += sucked.stack;
                                        sucked.SetDefaults(0, false);
                                        break;
                                    }
                                    // add new item to chest
                                    else if (currentChest.item[p].type == 0 && currentChest.item[p].stack == 0)
                                    {
                                        currentChest.item[p] = sucked.Clone();
                                        currentChest.item[p].type = sucked.type;
                                        currentChest.item[p].stack = sucked.stack;
                                        sucked.SetDefaults(0, false);
                                    }                                   
                                }
                            }
                        }
                    }
                }
            }
        }*/
    }
}