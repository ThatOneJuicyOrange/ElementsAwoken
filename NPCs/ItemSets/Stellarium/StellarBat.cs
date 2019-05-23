using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.ItemSets.Stellarium
{
    public class StellarBat : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 26;
            npc.height = 20;

            npc.damage = 80;
            npc.defense = 15;
            npc.lifeMax = 800;
            npc.knockBackResist = 0.65f;

            animationType = 93;
            npc.aiStyle = 2;

            npc.value = Item.buyPrice(0, 0, 20, 0);

            npc.HitSound = SoundID.NPCHit54;
            npc.DeathSound = SoundID.NPCDeath52;

            banner = npc.type;
            bannerItem = mod.ItemType("StellarBatBanner");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stellar Bat");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.75f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.75f);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.player.ZoneSkyHeight) &&
            !spawnInfo.player.ZoneTowerStardust &&
            !spawnInfo.player.ZoneTowerSolar &&
            !spawnInfo.player.ZoneTowerVortex &&
            !spawnInfo.player.ZoneTowerNebula &&
            !spawnInfo.playerInTown &&
            !Main.snowMoon && !Main.pumpkinMoon && NPC.downedMoonlord ? 0.15f : 0f;
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Stellorite"), Main.rand.Next(3, 6));
        }
        public override void AI()
        {
            Lighting.AddLight(npc.Center, 0.4f, 0.4f, 0.7f);
        }
    }
}