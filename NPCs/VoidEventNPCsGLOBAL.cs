using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ElementsAwoken.NPCs
{
    public class VoidEventNPCsGLOBAL : GlobalNPC
    {
        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo) // run on clients only
        {
            if (MyWorld.voidInvasionUp && (Main.invasionX == Main.spawnTileX)) //If the invasion is up and the invasion has reached the spawn pos
            {
                pool.Clear();
                if (!Main.dayTime && Main.time <= 16220) // before midnight
                {
                    pool.Clear();
                    int numCasters = NPC.CountNPCS(mod.NPCType("ReaverSlime"));
                    if (numCasters > 7) pool.Add(ModLoader.GetMod("ElementsAwoken").NPCType("ZergCaster"), 0.0f);
                    else if (numCasters > 5) pool.Add(ModLoader.GetMod("ElementsAwoken").NPCType("ZergCaster"), 0.01f);
                    else pool.Add(ModLoader.GetMod("ElementsAwoken").NPCType("ZergCaster"), 0.3f);

                    int numSlimes = NPC.CountNPCS(mod.NPCType("ReaverSlime"));
                    if (numSlimes > 15) pool.Add(ModLoader.GetMod("ElementsAwoken").NPCType("ReaverSlime"), 0.0f);
                    else pool.Add(ModLoader.GetMod("ElementsAwoken").NPCType("ReaverSlime"), 0.4f);

                    int numKnights = NPC.CountNPCS(mod.NPCType("VoidKnight"));
                    if (numKnights > 10) pool.Add(ModLoader.GetMod("ElementsAwoken").NPCType("VoidKnight"), 0.0f);
                    else pool.Add(ModLoader.GetMod("ElementsAwoken").NPCType("VoidKnight"), 0.4f);

                    int numImmolators = NPC.CountNPCS(mod.NPCType("Immolator"));
                    if (numImmolators > 6) pool.Add(ModLoader.GetMod("ElementsAwoken").NPCType("Immolator"), 0.0f);
                    else if (numImmolators > 3) pool.Add(ModLoader.GetMod("ElementsAwoken").NPCType("Immolator"), 0.1f);
                    else pool.Add(ModLoader.GetMod("ElementsAwoken").NPCType("Immolator"), 0.5f);

                    int numElementals = NPC.CountNPCS(mod.NPCType("VoidElemental"));
                    if (numElementals > 10) pool.Add(ModLoader.GetMod("ElementsAwoken").NPCType("VoidElemental"), 0.0f);
                    else if (numElementals > 5) pool.Add(ModLoader.GetMod("ElementsAwoken").NPCType("VoidElemental"), 0.2f);
                    else pool.Add(ModLoader.GetMod("ElementsAwoken").NPCType("VoidElemental"), 0.6f);
                }
                else // after midnight
                {
                    pool.Clear();

                    //int numWyrms = NPC.CountNPCS(mod.NPCType("ShadeWyrmHead"));
                    if (!NPC.AnyNPCs(mod.NPCType("ShadeWyrmHead"))) pool.Add(ModLoader.GetMod("ElementsAwoken").NPCType("ShadeWyrmHead"), 0.0001f);
                    /*else if (numWyrms == 1) // 1 wyrms
                    {
                        pool.Add(ModLoader.GetMod("ElementsAwoken").NPCType("ShadeWyrmHead"), 0.002f);
                    }
                    else if (numWyrms == 2) // 2 wyrms
                    {
                        pool.Add(ModLoader.GetMod("ElementsAwoken").NPCType("ShadeWyrmHead"), 0.001f);
                    }
                    else if (numWyrms == 3) // 3 wyrms
                    {
                        pool.Add(ModLoader.GetMod("ElementsAwoken").NPCType("ShadeWyrmHead"), 0.0f);
                    }*/

                    int numGolems = NPC.CountNPCS(mod.NPCType("VoidGolem"));
                    if (numGolems > 10) pool.Add(ModLoader.GetMod("ElementsAwoken").NPCType("VoidGolem"), 0.0f);
                    else if (numGolems > 5) pool.Add(ModLoader.GetMod("ElementsAwoken").NPCType("VoidGolem"), 0.1f);
                    else pool.Add(ModLoader.GetMod("ElementsAwoken").NPCType("VoidGolem"), 0.5f);

                    int numHunters = NPC.CountNPCS(mod.NPCType("EtherealHunter"));
                    if (numHunters > 10) pool.Add(ModLoader.GetMod("ElementsAwoken").NPCType("EtherealHunter"), 0.0f);
                    else if (numHunters > 5) pool.Add(ModLoader.GetMod("ElementsAwoken").NPCType("EtherealHunter"), 0.1f);
                    else pool.Add(ModLoader.GetMod("ElementsAwoken").NPCType("EtherealHunter"), 0.5f);

                    int numCrawlers = NPC.CountNPCS(mod.NPCType("VoidCrawler"));
                    if (numCrawlers > 10) pool.Add(ModLoader.GetMod("ElementsAwoken").NPCType("VoidCrawler"), 0.0f);
                    else if (numCrawlers > 5) pool.Add(ModLoader.GetMod("ElementsAwoken").NPCType("VoidCrawler"), 0.1f);
                    else pool.Add(ModLoader.GetMod("ElementsAwoken").NPCType("VoidCrawler"), 0.5f);
                }
            }
        }

        //Changing the spawn rate
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (MyWorld.voidInvasionUp && (Main.invasionX == (double)Main.spawnTileX))
            {
                spawnRate = 25; //Lower (?) the number, the more spawns
                maxSpawns = 100; //Max spawns of NPCs depending on NPC value
            }
        }

        public override void PostAI(NPC npc)
        {
            if (MyWorld.voidInvasionUp)
            {
                //Changes NPCs so they do not despawn
                bool tooFar = false;
                if (npc.type != mod.NPCType("ShadeWyrmHead") || npc.type != mod.NPCType("ShadeWyrmBody") || npc.type != mod.NPCType("ShadeWyrmTail"))
                {
                    if (Vector2.Distance(Main.player[npc.target].Center, npc.Center) >= 2000)
                    {
                        tooFar = true;
                    }
                    else
                    {
                        tooFar = false;
                    }
                }
                if (Main.invasionX == (double)Main.spawnTileX)
                {
                    if (!tooFar)
                    {
                        npc.timeLeft = 500;
                    }
                    else if (tooFar && !npc.townNPC)
                    {
                        npc.active = false;
                    }
                }
            }
        }
    }
}