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
			npc.aiStyle = 3;
            aiType = NPCID.AnomuraFungus;
            animationType = 257;

            npc.damage = 40;
            npc.knockBackResist = 0.3f;
            npc.defense = 18;
            npc.lifeMax = 70;

            npc.width = 44;
			npc.height = 34;

            npc.value = Item.buyPrice(0, 0, 1, 0);

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
            float spawnChance = Main.hardMode ? 0.03f : 0.06f;
            return spawnInfo.player.ZoneJungle &&
                !spawnInfo.player.ZoneTowerStardust &&
                !spawnInfo.player.ZoneTowerSolar &&
                !spawnInfo.player.ZoneTowerVortex &&
                !spawnInfo.player.ZoneTowerNebula &&
                !spawnInfo.playerInTown &&
                NPC.downedQueenBee && !Main.snowMoon && !Main.pumpkinMoon ? spawnChance : 0f;
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

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Petal"), Main.rand.Next(1, 2));
        }
    }
}