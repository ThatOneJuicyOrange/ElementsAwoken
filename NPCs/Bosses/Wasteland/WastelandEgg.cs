using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.Wasteland
{
    public class WastelandEgg : ModNPC
    {
        public int timer = 200;
        public override void SetDefaults()
        {
            npc.width = 38;
            npc.height = 42; 
            
            npc.npcSlots = 1f;

            npc.aiStyle = -1;

            npc.defense = 3;
            npc.lifeMax = 25;

            NPCID.Sets.NeedsExpertScaling[npc.type] = true;

            npc.knockBackResist = 0.1f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Egg");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 50;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 150;
                npc.defense = 6;
            }
        }
        public override void AI()
        {
            npc.ai[0]++;
            if (npc.ai[0] >= 60)
            {
                Vector2 spawnAt = npc.Center + new Vector2(0f, (float)npc.height / 2f);
                NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("WastelandMinion"));
                npc.active = false;
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/WastelandEgg"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/WastelandEgg2"), npc.scale);
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/WastelandEgg"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/WastelandEgg2"), npc.scale);
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1f;
            if (npc.frameCounter % 20 == 0)
            {
                npc.frame.Y += frameHeight;
            }
            if (npc.frame.Y > frameHeight * 1)
            {
                npc.frame.Y = 0;
            }
        }
    }
}