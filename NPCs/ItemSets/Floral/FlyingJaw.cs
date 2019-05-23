using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.ItemSets.Floral
{
    public class FlyingJaw : ModNPC
    {
        public override void SetDefaults()
        {
            npc.aiStyle = 2;
            npc.damage = 35;
            npc.width = 30; //324
            npc.height = 32; //216
            npc.defense = 15;
            npc.lifeMax = 80;
            npc.knockBackResist = 0f;
            npc.value = Item.buyPrice(0, 0, 60, 0);
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            banner = npc.type;
            bannerItem = mod.ItemType("FlyingJawBanner");
        }
        public override void FindFrame(int frameHeight)
        {
            if (npc.velocity.X < 0f)
            {
                npc.direction = -1;
            }
            else
            {
                npc.direction = 1;
            }
            if (npc.direction == 1)
            {
                npc.spriteDirection = 1;
            }
            if (npc.direction == -1)
            {
                npc.spriteDirection = -1;
            }
            npc.rotation = (float)Math.Atan2((double)(npc.velocity.Y * (float)npc.direction), (double)(npc.velocity.X * (float)npc.direction));
            npc.frameCounter += 0.15f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int frame = (int)npc.frameCounter;
            npc.frame.Y = frame * frameHeight;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flying Jaw");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.ZoneJungle &&
                !spawnInfo.player.ZoneTowerStardust &&
                !spawnInfo.player.ZoneTowerSolar &&
                !spawnInfo.player.ZoneTowerVortex &&
                !spawnInfo.player.ZoneTowerNebula &&
                !spawnInfo.playerInTown &&
                NPC.downedBoss3 && !Main.snowMoon && !Main.pumpkinMoon ? 0.045f : 0f;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.65f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.65f);
        }

        public override void NPCLoot()  //Npc drop
        {
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Petal"), Main.rand.Next(3, 6)); //Item spawn
            }

        }
    }
}