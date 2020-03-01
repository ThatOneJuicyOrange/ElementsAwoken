using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.ItemSets.Drakonite.Lesser
{
    public class DragonWarrior : ModNPC
	{
		public override void SetDefaults()
		{
			npc.width = 18;
			npc.height = 40;

			npc.damage = 21;
			npc.defense = 12;
			npc.lifeMax = 100;
            npc.knockBackResist = 0.75f;

            npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath3;

            npc.value = Item.buyPrice(0, 0, 2, 0);

			npc.aiStyle = 3;
			aiType = NPCID.Skeleton;
			animationType = NPCID.PossessedArmor;

            npc.buffImmune[24] = true;

            banner = npc.type;
            bannerItem = mod.ItemType("DragonWarriorBanner");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Warrior");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.PossessedArmor];
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
            bool underworld = (spawnInfo.spawnTileY >= (Main.maxTilesY - 200));
            bool rockLayer = (spawnInfo.spawnTileY >= (Main.maxTilesY * 0.4f));
            return !underworld && rockLayer && !spawnInfo.player.ZoneCrimson && !spawnInfo.player.ZoneCorrupt && !spawnInfo.player.ZoneDesert && !spawnInfo.player.ZoneDungeon && !Main.hardMode ? 0.06f : 0f;
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            if (Main.expertMode) player.AddBuff(BuffID.OnFire, MyWorld.awakenedMode ? 150 : 90, false);
        }
        public override void NPCLoot()
		{
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Drakonite"), Main.rand.Next(1, 3));
        }
    }
}
