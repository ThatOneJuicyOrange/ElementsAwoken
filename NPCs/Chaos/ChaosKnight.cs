using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Chaos
{
    public class ChaosKnight : ModNPC
	{
		public override void SetDefaults()
		{
			npc.width = 40;
			npc.height = 56;

			npc.damage = 200;
			npc.defense = 100;
			npc.lifeMax = 3000;
            npc.knockBackResist = 0.75f;

            npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath3;
            npc.value = Item.buyPrice(0, 0, 20, 0);

			npc.aiStyle = 3;
			Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.PossessedArmor];
			aiType = NPCID.Skeleton;
			animationType = NPCID.PossessedArmor;

            npc.buffImmune[24] = true;

            /*banner = npc.type;
            bannerItem = mod.ItemType("ChaosKnightBanner");*/
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Knight");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.PossessedArmor];
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
            return (spawnInfo.spawnTileY < Main.rockLayer) &&
            !spawnInfo.player.ZoneTowerStardust &&
            !spawnInfo.player.ZoneTowerSolar &&
            !spawnInfo.player.ZoneTowerVortex &&
            !spawnInfo.player.ZoneTowerNebula &&
            !spawnInfo.playerInTown &&
            !Main.snowMoon && !Main.pumpkinMoon && !Main.dayTime && MyWorld.downedVoidLeviathan ? 0.065f : 0f;
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.Slow, 300, false);
            player.AddBuff(BuffID.BrokenArmor, 300, false);
            player.AddBuff(BuffID.WitheredArmor, 300, false);
            player.AddBuff(BuffID.WitheredWeapon, 300, false);
        }
        public override void NPCLoot()
		{
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ChaoticFlare"), Main.rand.Next(3, 6));
            }
        }		 
	}
}
