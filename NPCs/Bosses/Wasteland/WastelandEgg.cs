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
            npc.npcSlots = 1f;
            npc.width = 38;
            npc.height = 42;
            npc.defense = 3;
            npc.lifeMax = 25;
            npc.knockBackResist = 0.1f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Egg");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void AI()
        {
            timer--;
            if (timer <= 0)
            {
                Vector2 spawnAt = npc.Center + new Vector2(0f, (float)npc.height / 2f);
                NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("WastelandMinion"));
                npc.active = false;
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