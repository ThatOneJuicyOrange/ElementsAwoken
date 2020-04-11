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
    public class WastelandMinion : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 34;
            npc.height = 26;

            animationType = 257;
            aiType = NPCID.AnomuraFungus; 
            npc.aiStyle = 3;

            npc.damage = 20;
            npc.defense = 12;
            npc.lifeMax = 50;
            npc.knockBackResist = 0.3f;

            npc.HitSound = SoundID.NPCHit31;
            npc.DeathSound = SoundID.NPCDeath34;
            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 70;
            npc.damage = 35;
            npc.defense = 8;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 140;
                npc.damage = 45;
                npc.defense = 12;
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mutated Scorpion");
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            //gore
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/MutatedScorpion"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/MutatedScorpion1"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/MutatedScorpion2"), npc.scale);
            }
        }
        public override void AI()
        {
            npc.velocity *= 0.95f;

            for (int k = 0; k < Main.npc.Length; k++)
            {
                NPC other = Main.npc[k];
                if (k != npc.whoAmI && other.type == npc.type && other.active && Math.Abs(npc.position.X - other.position.X) + Math.Abs(npc.position.Y - other.position.Y) < npc.width)
                {
                    const float pushAway = 0.05f;
                    if (npc.position.X < other.position.X)
                    {
                        npc.velocity.X -= pushAway;
                    }
                    else
                    {
                        npc.velocity.X += pushAway;
                    }
                    if (npc.position.Y < other.position.Y)
                    {
                        npc.velocity.Y -= pushAway;
                    }
                    else
                    {
                        npc.velocity.Y += pushAway;
                    }
                }
            }
        }
    }
}