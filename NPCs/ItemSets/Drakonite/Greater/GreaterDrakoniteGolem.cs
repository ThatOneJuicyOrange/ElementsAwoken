﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.ItemSets.Drakonite.Greater
{
    public class GreaterDrakoniteGolem : ModNPC
	{
		public override void SetDefaults()
		{
			npc.width = 18;
			npc.height = 40;

			npc.damage = 50;
			npc.defense = 30;
			npc.lifeMax = 500;
            npc.knockBackResist = 0.50f;
            npc.aiStyle = 3;

            npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath3;

            npc.value = Item.buyPrice(0, 0, 50, 0);

			Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.PossessedArmor];
			aiType = NPCID.Skeleton;
			animationType = NPCID.PossessedArmor;
            npc.buffImmune[24] = true;

            /*banner = npc.type;
            bannerItem = mod.ItemType("DragonWarriorBanner");*/
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Drakoknight"); // thanks genih
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.PossessedArmor];
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
            int x = spawnInfo.spawnTileX;
            int y = spawnInfo.spawnTileY;
            int tile = (int)Main.tile[x, y].type;
            bool oUnderworld = (y <= (Main.maxTilesY * 0.6f));
            bool oRockLayer = (y >= (Main.maxTilesY * 0.4f));
            return oUnderworld && oRockLayer && Main.evilTiles < 80 && Main.sandTiles < 80 && Main.dungeonTiles < 80 && Main.hardMode && NPC.downedPlantBoss ? 0.06f : 0f;
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(mod.BuffType("Dragonfire"), 100, true);
        }
        public override void NPCLoot()
		{
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RefinedDrakonite"), Main.rand.Next(3, 6)); //Item spawn
        }		 
	}
}
