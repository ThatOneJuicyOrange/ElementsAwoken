using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Projectiles.NPCProj;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace ElementsAwoken.NPCs.Liftable
{
    public class FallenHarpy : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 42;
            npc.height = 20;

            npc.aiStyle = -1;

            npc.defense = 15;
            npc.lifeMax = 50;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.immortal = true;
            npc.dontTakeDamage = true;
            npc.townNPC = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fallen Harpy");
        }
        public override void FindFrame(int frameHeight)
        {

        }
        public override void AI()
        {
            SpecialQuest quest = (SpecialQuest)QuestSystem.FindQuest("FallenHarpy");
            QuestNPC questNPC = npc.GetGlobalNPC<QuestNPC>();
            if (!quest.active && !quest.claimed)
            {
                questNPC.hasQuestAvailable = quest.identifier;
            }
            else
            {
                NPC harpy = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<FallenHarpyHeld>())];
                harpy.Bottom = npc.Bottom;
                npc.active = false;
            }
            bool tooFar = true;
            for (int p = 0; p < Main.maxPlayers; p++)
            {
                if (Main.player[p].active && Vector2.Distance(Main.player[p].position / 16, npc.position / 16) < 200)
                {
                    tooFar = false;
                    break;
                }
            }
            if (tooFar)
            {
                npc.townNPC = false;
                npc.active = false;
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            SpecialQuest quest = (SpecialQuest)QuestSystem.FindQuest("FallenHarpy");

            return (spawnInfo.spawnTileY < Main.rockLayer) &&
            !spawnInfo.player.ZoneTowerStardust &&
            !spawnInfo.player.ZoneTowerSolar &&
            !spawnInfo.player.ZoneTowerVortex &&
            !spawnInfo.player.ZoneTowerNebula &&
            !spawnInfo.playerInTown &&
            !spawnInfo.invasion &&
            Vector2.Distance(new Vector2(Main.spawnTileX, Main.spawnTileY), spawnInfo.player.Center / 16) > 500 &&
            !Main.snowMoon && !Main.pumpkinMoon && Main.dayTime && !Main.hardMode && !quest.completed && NPC.AnyNPCs(NPCID.Nurse) ? 0.065f : 0f;
        }
        public override string GetChat()
        {
            return "Please help me... I got hit by a fallen star. If you take me to the nurse, I'm sure she can do something.";
        }
    }
}
