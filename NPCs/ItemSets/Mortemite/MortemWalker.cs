using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.ItemSets.Mortemite
{
    public class MortemWalker : ModNPC
	{
        public bool hasSpedUp = false;
		public override void SetDefaults()
		{
			npc.aiStyle = 3;
			npc.damage = 130;
			npc.width = 44; //324
			npc.height = 34; //216
			npc.defense = 30;
			npc.lifeMax = 1000;
			npc.knockBackResist = 0.3f;
			animationType = NPCID.Zombie;
			aiType = NPCID.Zombie;
            npc.value = Item.buyPrice(0, 1, 0, 0);
            npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
            banner = npc.type;
            bannerItem = mod.ItemType("MortemWalkerBanner");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mortem Walker");
            Main.npcFrameCount[npc.type] = 3;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.spawnTileY < Main.rockLayer) &&
            !spawnInfo.player.ZoneTowerStardust &&
            !spawnInfo.player.ZoneTowerSolar &&
            !spawnInfo.player.ZoneTowerVortex &&
            !spawnInfo.player.ZoneTowerNebula &&
            !spawnInfo.playerInTown &&
            !Main.snowMoon && !Main.pumpkinMoon && NPC.downedMoonlord && !Main.dayTime ? 0.04f : 0f;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.6f);
		}
		
		public override void HitEffect(int hitDirection, double damage)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, 32, hitDirection, -1f, 0, default(Color), 1f);
			}
			if (npc.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, 32, hitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
        public override void AI()
        {
            if (!hasSpedUp)
            {
                npc.velocity *= 2f;
                hasSpedUp = true;
            }
        }
        public override void NPCLoot()  //Npc drop
        {
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MortemiteDust"), Main.rand.Next(1, 2)); //Item spawn
            }

        }
    }
}