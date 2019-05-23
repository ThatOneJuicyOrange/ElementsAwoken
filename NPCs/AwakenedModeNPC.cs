using Microsoft.Xna.Framework;
using ReLogic.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.NPCs
{
    public class AwakenedModeNPC : GlobalNPC
    {
        public override void ResetEffects(NPC npc)
        {
        }

        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public bool CalamityModRevengeance
        {
            get { return CalamityMod.CalamityWorld.revenge; }
        }
        public override void ScaleExpertStats(NPC npc, int numPlayers, float bossLifeScale)
        {
            if (MyWorld.awakenedMode)
            {
                if (!npc.boss)
                {
                    npc.lifeMax = (int)(npc.lifeMax * 1.75f);
                    npc.damage = (int)(npc.damage * 1.75f);
                    npc.defense = (int)(npc.defense * 1.5f);
                }
                if (ElementsAwoken.calamityEnabled)
                {
                    if (!CalamityModRevengeance)
                    {
                        npc.value *= 1.5f;
                        ScaleBossStats(npc);
                    }
                }
                else
                {
                    npc.value *= 1.5f;
                    ScaleBossStats(npc);
                }
            }
        }
        private void ScaleBossStats(NPC npc)
        {
            if (npc.type == NPCID.KingSlime)
            {
                npc.lifeMax = 3600;
                npc.damage = 80;
                npc.defense = 15;
            }
            if (npc.type == NPCID.EyeofCthulhu)
            {
                npc.lifeMax = 4800;
                npc.damage = 45;
                npc.defense = 17;
            }
            if (npc.type == NPCID.EaterofWorldsHead)
            {
                npc.lifeMax = 120;
                npc.damage = 60;
                npc.defense = 6;
            }
            if (npc.type == NPCID.EaterofWorldsBody)
            {
                npc.lifeMax = 350;
                npc.damage = 25;
                npc.defense = 9;
            }
            if (npc.type == NPCID.EaterofWorldsTail)
            {
                npc.lifeMax = 450;
                npc.damage = 23;
                npc.defense = 15;
            }
            if (npc.type == NPCID.BrainofCthulhu)
            {
                npc.lifeMax = 2500;
                npc.damage = 60;
                npc.defense = 20;
            }
            if (npc.type == NPCID.QueenBee)
            {
                npc.lifeMax = 6500;
                npc.damage = 65;
            }
            if (npc.type == NPCID.SkeletronHead)
            {
                npc.lifeMax = 11000;
                npc.damage = 90;
            }
            if (npc.type == NPCID.SkeletronHand)
            {
                npc.lifeMax = 2000;
                npc.damage = 65;
                npc.defense = 20;
            }
            if (npc.type == NPCID.WallofFlesh)
            {
                npc.lifeMax = 15000;
                npc.damage = 200;
                npc.defense = 25;
            }
            if (npc.type == NPCID.WallofFleshEye)
            {
                npc.lifeMax = 15000;
                npc.damage = 200;
                npc.defense = 9;
            }
            if (npc.type == NPCID.Retinazer)
            {
                npc.lifeMax = 45000;
                npc.damage = 90;
                npc.defense = 15;
            }
            if (npc.type == NPCID.Spazmatism)
            {
                npc.lifeMax = 53500;
                npc.damage = 100;
                npc.defense = 15;
            }
            if (npc.type == NPCID.TheDestroyer)
            {
                npc.lifeMax = 175000;
                npc.damage = 350;
                npc.defense = 5;
            }
            if (npc.type == NPCID.TheDestroyerBody)
            {
                npc.damage = 110;
                npc.defense = 40;
            }
            if (npc.type == NPCID.TheDestroyerTail)
            {
                npc.damage = 80;
                npc.defense = 45;
            }
            if (npc.type == NPCID.SkeletronPrime)
            {
                npc.lifeMax = 60000;
                npc.damage = 90;
            }
            if (npc.type == NPCID.PrimeCannon)
            {
                npc.lifeMax = 15000;
                npc.damage = 60;
                npc.defense = 30;
            }
            if (npc.type == NPCID.PrimeLaser)
            {
                npc.lifeMax = 13000;
                npc.damage = 60;
                npc.defense = 30;
            }
            if (npc.type == NPCID.PrimeSaw)
            {
                npc.lifeMax = 17500;
                npc.damage = 120;
                npc.defense = 45;
            }
            if (npc.type == NPCID.PrimeVice)
            {
                npc.lifeMax = 17500;
                npc.damage = 120;
                npc.defense = 42;
            }
            if (npc.type == NPCID.Plantera)
            {
                npc.lifeMax = 60000;
                npc.damage = 130;
                npc.defense = 24;
            }
            if (npc.type == NPCID.GolemHead)
            {
                npc.lifeMax = 35000;
                npc.damage = 130;
                npc.defense = 25;
            }
            if (npc.type == NPCID.GolemFistLeft || npc.type == NPCID.GolemFistRight)
            {
                npc.lifeMax = 15000;
                npc.damage = 120;
                npc.defense = 35;
            }
            if (npc.type == NPCID.Golem)
            {
                npc.lifeMax = 20000;
                npc.damage = 140;
                npc.defense = 35;
            }
            if (npc.type == NPCID.GolemHeadFree)
            {
                npc.damage = 140;
            }
            if (npc.type == NPCID.DukeFishron)
            {
                npc.lifeMax = 75000;
                npc.damage = 170;
                npc.defense = 60;
            }
            if (npc.type == NPCID.CultistBoss)
            {
                npc.lifeMax = 75000;
                npc.damage = 120;
                npc.defense = 60;
            }
            if (npc.type == NPCID.MoonLordCore)
            {
                npc.lifeMax = 100000;
                npc.defense = 90;
            }
            if (npc.type == NPCID.MoonLordHand)
            {
                npc.lifeMax = 50000;
                npc.defense = 55;
            }
            if (npc.type == NPCID.MoonLordHead)
            {
                npc.lifeMax = 75000;
                npc.defense = 65;
            }
        }
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (MyWorld.awakenedMode)
            {
                if (!ElementsAwoken.calamityEnabled)
                {
                    spawnRate = (int)(spawnRate / 2f);
                    maxSpawns = (int)(maxSpawns * 2f);
                }
            }
        }
    }
}