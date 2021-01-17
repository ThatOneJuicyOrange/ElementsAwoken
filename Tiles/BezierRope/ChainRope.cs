using ElementsAwoken.Items.Placeable.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ElementsAwoken.Tiles.BezierRope
{
    public class ChainRope : BezierRopeBase
    {
        public ChainRope() : base(56) { }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Main.tileFrameImportant[Type] = true;
        }
      /*  public override void NearbyEffects(int i, int j, bool closer)
        {
            Tile t = Main.tile[i, j];

            if (t.frameX != 0 && t.frameY != 0)
            {
                Player player = Main.LocalPlayer;
                var dist = (int)Vector2.Distance(player.Center / 16, new Vector2(i, j));
                if (dist <= 200)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int worldX = i * 16;
                        int worldY = j * 16;
                        bool npcExists = false;
                        for (int n = 0; n < Main.npc.Length; n++)
                        {
                            NPC nPC = Main.npc[n];
                            if (nPC.active && nPC.ai[1] == t.frameX && nPC.ai[2] == t.frameY)
                            {
                                npcExists = true;
                                break;
                            }
                        }
                        if (!npcExists)
                        {
                            int n = NPC.NewNPC(worldX, worldY, ModContent.NPCType<ChainRopeNPC>());
                            Main.npc[n].TopLeft = new Vector2(worldX, worldY);
                            ChainRopeNPC npc = (ChainRopeNPC)Main.npc[n].modNPC;
                            npc.tex = Main.tileTexture[t.type];
                            npc.parent = t;
                            Main.npc[n].ai[0] = 56;
                            Main.npc[n].ai[1] = t.frameX;
                            Main.npc[n].ai[2] = t.frameY;
                            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n);
                        }
                    }
                }
            }
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile t = Main.tile[i, j];
            if (ModContent.GetInstance<Config>().debugMode && Main.LocalPlayer.HeldItem.type == ModContent.ItemType<Items.Placeable.BezierRopeTest>())
            {
                for (int k = 0; k < 16; k++)
                {
                    Dust dust = Main.dust[Dust.NewDust(new Vector2(i, j) * 16, 16, 16, 6)];
                    dust.noGravity = true;
                    dust.velocity *= 0;
                }
                for (int k = 0; k < 16; k++)
                {
                    Dust dust = Main.dust[Dust.NewDust(new Vector2(t.frameX * 16, t.frameY * 16), 16, 16, DustID.PinkFlame)];
                    dust.noGravity = true;
                    dust.velocity *= 0;
                }
            }
            return false;
        }*/
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            if (!MyWorld.downedVolcanox && !ModContent.GetInstance<Config>().debugMode) return false;
            return base.CanKillTile(i, j, ref blockDamaged);
        }
    }
}