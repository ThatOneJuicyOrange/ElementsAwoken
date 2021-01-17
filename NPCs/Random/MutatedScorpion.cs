using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Random
{
    public class MutatedScorpion : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 34;
            npc.height = 26;

            npc.npcSlots = 1f;

            npc.aiStyle = 3;
            aiType = NPCID.AnomuraFungus; // makes it so it doesnt despawn during day
            animationType = 257;

            npc.lifeMax = 30;
            npc.damage = 20;
            npc.defense = 6;
            npc.knockBackResist = 0.3f;

            npc.value = Item.buyPrice(0, 0, 2, 0);

            npc.rarity = 4;

            npc.HitSound = SoundID.NPCHit31;
            npc.DeathSound = SoundID.NPCDeath34;

            npc.catchItem = (short)mod.ItemType("WastelandSummon");
            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 60;
            npc.damage = 35;
            npc.defense = 8;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 120;
                npc.damage = 45;
                npc.defense = 12;
            }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mutated Scorpion");
            Main.npcFrameCount[npc.type] = 5;

            Main.npcCatchable[npc.type] = true;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/MutatedScorpion"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/MutatedScorpion1"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/MutatedScorpion2"), npc.scale);
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.player.ZoneDesert) &&
            !spawnInfo.player.ZoneTowerStardust &&
            !spawnInfo.player.ZoneTowerSolar &&
            !spawnInfo.player.ZoneTowerVortex &&
            !spawnInfo.player.ZoneTowerNebula &&
            !spawnInfo.playerInTown &&
            NPC.downedBoss1 &&!Main.snowMoon && !Main.pumpkinMoon ? 0.055f : 0f;
        }
    }
}