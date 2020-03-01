using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.Ancients
{
    public class ShardBase : ModNPC
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            npc.lifeMax = 10000;
            npc.aiStyle = -1;
            npc.width = 58;
            npc.height = 72;

            npc.noGravity = true;
            npc.netAlways = true;
            npc.dontTakeDamage = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
                return false;
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            if (!P.active || P.dead) npc.TargetClosest(true);
            if (Main.netMode == 0)
            {
                if (!P.active || P.dead) npc.active = false;
            }
            else
            {
                if (!P.active || P.dead)
                {
                    npc.TargetClosest(true);
                    if (!P.active || P.dead)
                    {
                        npc.active = false;
                    }
                }
            }
            npc.Center = P.Center + new Vector2(0, -300);

            if (!AnyAncients())
            {
                npc.ai[1]++;
                if (npc.ai[1] == 180)
                {
                    Main.PlaySound(SoundLoader.customSoundType, (int)npc.position.X, (int)npc.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/NPC/AncientMergeRise"));
                }
                if (npc.ai[1] == 300 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (Main.autoPause == true)
                    {
                        string text = "Using autopause huh? What a wimp";
                        if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(text, Color.LightCyan);
                        else NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.LightCyan);
                    }
                    NPC aa = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AncientAmalgam"))];
                    aa.netUpdate = true;
                    npc.active = false;
                }
            }
        }
        private bool AnyAncients()
        {
            if (NPC.AnyNPCs(mod.NPCType("Izaris"))) return true;
            if (NPC.AnyNPCs(mod.NPCType("Kirvein"))) return true;
            if (NPC.AnyNPCs(mod.NPCType("Krecheus"))) return true;
            if (NPC.AnyNPCs(mod.NPCType("Xernon"))) return true;
            return false;
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}
