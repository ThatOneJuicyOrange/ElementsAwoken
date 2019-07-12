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
    public class Scorpion : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 44;
            npc.height = 34;

            npc.damage = 35;

            npc.defense = 6;
            npc.lifeMax = 30;
            npc.knockBackResist = 0.3f;

            npc.aiStyle = 3;
            animationType = NPCID.Scorpion;
            aiType = NPCID.AnomuraFungus;

            npc.value = Item.buyPrice(0, 0, 5, 0);
            npc.HitSound = SoundID.NPCHit31;
            npc.DeathSound = SoundID.NPCDeath34;

            npc.friendly = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scorpion");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.spawnTileY < Main.rockLayer) &&
            !spawnInfo.player.ZoneTowerStardust &&
            !spawnInfo.player.ZoneTowerSolar &&
            !spawnInfo.player.ZoneTowerVortex &&
            !spawnInfo.player.ZoneTowerNebula &&
            !spawnInfo.playerInTown &&
            NPC.downedBoss1 && !MyWorld.downedWasteland && !Main.snowMoon && !Main.pumpkinMoon ? 0.065f : 0f;
        }
    }
}