using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.MutatedScorpion 
{
    public class MutatedScorpion : ModNPC
    {
        public override void SetDefaults()
        {
            npc.npcSlots = 1f;
            npc.aiStyle = 3;
            npc.damage = 35;
            npc.width = 44;
            npc.height = 34;
            npc.defense = 6;
            npc.lifeMax = 30;
            npc.knockBackResist = 0.3f;
            animationType = 257;
            aiType = NPCID.AnomuraFungus; // makes it so it doesnt despawn during day
            npc.value = Item.buyPrice(0, 0, 2, 0);
            npc.HitSound = SoundID.NPCHit31;
            npc.DeathSound = SoundID.NPCDeath34;
            npc.catchItem = (short)mod.ItemType("WastelandSummon");
            npc.friendly = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mutated Scorpion");
            Main.npcFrameCount[npc.type] = 5;

            Main.npcCatchable[npc.type] = true;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.player.ZoneDesert) &&
            !spawnInfo.player.ZoneTowerStardust &&
            !spawnInfo.player.ZoneTowerSolar &&
            !spawnInfo.player.ZoneTowerVortex &&
            !spawnInfo.player.ZoneTowerNebula &&
            !spawnInfo.playerInTown &&
            NPC.downedBoss1 &&!Main.snowMoon && !Main.pumpkinMoon ? 0.065f : 0f;
        }
    }
}