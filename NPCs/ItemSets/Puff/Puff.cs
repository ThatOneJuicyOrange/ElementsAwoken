using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.ItemSets.Puff
{
	public class Puff : ModNPC
	{
		public override void SetDefaults()
		{
			npc.aiStyle = 1;
			npc.damage = 6;
			npc.width = 32; //324
			npc.height = 22; //216
			npc.defense = 2;
			npc.lifeMax = 13;
			animationType = 1;
            npc.value = Item.buyPrice(0, 0, 1, 0);
			npc.lavaImmune = false;
			npc.noGravity = false;
			npc.noTileCollide = false;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
            banner = npc.type;
            bannerItem = mod.ItemType("PuffBanner");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Puff");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.5f);
		}
		
		public override void OnHitPlayer(Player player, int damage, bool crit)
		{
			if (Main.expertMode)
			{
				player.AddBuff(mod.BuffType("Cuddled"), 2000);
			}
            else
            {
                player.AddBuff(mod.BuffType("Cuddled"), 500);
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
            !Main.snowMoon && !Main.pumpkinMoon && Main.dayTime && !Main.hardMode ? 0.065f : 0f;
        }
        public override void NPCLoot()  //Npc drop
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Puffball"), Main.rand.Next(1, 3)); //Item spawn
        }
    }
}