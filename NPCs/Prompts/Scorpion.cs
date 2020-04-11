using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Prompts 
{
    public class Scorpion : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 44;
            npc.height = 34;

            npc.damage = 15;

            npc.defense = 3;
            npc.lifeMax = 30;
            npc.knockBackResist = 1f;

            npc.aiStyle = 3;
            animationType = NPCID.Scorpion;
            aiType = NPCID.AnomuraFungus;

            npc.value = Item.buyPrice(0, 0, 0, 20);
            npc.HitSound = SoundID.NPCHit31;
            npc.DeathSound = SoundID.NPCDeath34;

            npc.friendly = false;
            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scorpion");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 60;
            npc.damage = 35;
            npc.defense = 8;
            npc.knockBackResist = 0.6f;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 120;
                npc.damage = 45;
                npc.defense = 12;
                npc.knockBackResist = 0.2f;
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, 561, npc.scale);
                Gore.NewGore(npc.position, npc.velocity, 562, npc.scale);
                Gore.NewGore(npc.position, npc.velocity, 563, npc.scale);
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.spawnTileY < Main.rockLayer) &&
            !spawnInfo.player.ZoneTowerStardust &&
            !spawnInfo.player.ZoneTowerSolar &&
            !spawnInfo.player.ZoneTowerVortex &&
            !spawnInfo.player.ZoneTowerNebula &&
            !spawnInfo.playerInTown &&
            !spawnInfo.invasion &&
            MyWorld.desertPrompt > ElementsAwoken.bossPromptDelay ? 0.065f : 0f;
        }
    }
}