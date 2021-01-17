using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using ElementsAwoken.Items.Placeable;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ElementsAwoken.Tiles.Objects
{
    public class VolcanoxShrine : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileID.Sets.HasOutlines[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style5x4);
            TileObjectData.newTile.Height = 5;
            TileObjectData.newTile.Width = 12;
            TileObjectData.newTile.Origin = new Point16(6,4);

            AddMapEntry(new Color(28, 27, 44));
            disableSmartCursor = true;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16 };
            TileObjectData.addTile(Type);
            mineResist = 10;
        }
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            if (ModContent.GetInstance<Config>().debugMode) return true;
            return false;
        }
        public override bool CanExplode(int i, int j)
        {
            return false;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<VolcanoxShrineItem>());
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (MyWorld.volcShrineActivated != 0)
            {
                Tile tile = Main.tile[i, j];
                Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
                if (Main.drawToScreen)
                {
                    zero = Vector2.Zero;
                }
                int height = 16;
                Color color = Color.White * ((1 + (float)Math.Sin(Main.time / 20)) / 2);
                Main.spriteBatch.Draw(mod.GetTexture("Tiles/Objects/VolcanoxShrine_Glow"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, height), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(mod.GetTexture("Tiles/Objects/VolcanoxShrine_Crystals"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
        }
        public override void MouseOver(int i, int j)
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Volcanox.Volcanox>()) && MyWorld.volcShrineActivated == 0)
            {
                Player player = Main.LocalPlayer;
                player.showItemIcon2 = ModContent.ItemType<Items.BossSummons.VolcanoxSummon>();
                player.showItemIconText = "";
                player.noThrow = 2;
                player.showItemIcon = true;
            }
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (Main.tile[i, j].frameX == 0 && Main.tile[i, j].frameY == 0) // dont need to do this for every part
            {
                if (MyWorld.volcShrineActivated != 0)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int worldX = (i + 5) * 16;
                        int worldY = (j + 10) * 16;
                        if (!NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Volcanox.VolcanoxSpawner>()))
                        {
                            int n = NPC.NewNPC(worldX, worldY, ModContent.NPCType<NPCs.Bosses.Volcanox.VolcanoxSpawner>());
                            Main.npc[n].Center = new Vector2(worldX + 8, worldY + 8);
                            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n);
                        }
                    }
                }
            }
        }
        public override bool NewRightClick(int i, int j)
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Volcanox.Volcanox>()) && MyWorld.volcShrineActivated == 0)
            {
                int item = Main.LocalPlayer.FindItem(ModContent.ItemType<Items.BossSummons.VolcanoxSummon>());
                if (item != -1)
                {
                    Item crystal = Main.LocalPlayer.inventory[item];
                    crystal.stack--;
                    if (crystal.stack <= 0) crystal.TurnToAir();
                    if (MyWorld.volcShrineActivated == 0) Main.PlaySound(2, i * 16, j * 17, 119, 2, -0.5f);
                    MyWorld.volcShrineActivated = 1;
                    if (Main.netMode == 2) NetMessage.SendData(MessageID.WorldData);
                    return true;
                }
            }
            return true;
        }
    }
}