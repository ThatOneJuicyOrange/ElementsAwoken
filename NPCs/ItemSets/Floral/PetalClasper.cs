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
    public class PetalClasper : ModNPC
	{
		public override void SetDefaults()
		{
			npc.npcSlots = 0.3f;
			npc.aiStyle = 3;
			npc.damage = 40;
			npc.width = 44; //324
			npc.height = 34; //216
			npc.defense = 18;
			npc.lifeMax = 70;
			npc.knockBackResist = 0.3f;
			animationType = 257;
			aiType = NPCID.AnomuraFungus;
			Main.npcFrameCount[npc.type] = 5;
            npc.value = Item.buyPrice(0, 0, 60, 0);
            npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
            banner = npc.type;
            bannerItem = mod.ItemType("PetalClasperBanner");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Petal Clasper");
            Main.npcFrameCount[npc.type] = 5;
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

        public override void NPCLoot()  //Npc drop
        {
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Petal"), Main.rand.Next(3, 6)); //Item spawn
            }

        }
    }
}