using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.ItemSets.Stellarium
{
    public class StellarCenturion : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 26;
            npc.height = 48;

            npc.damage = 10;
            npc.defense = 5;
            npc.lifeMax = 100;
            npc.knockBackResist = 0.5f;

            npc.value = Item.buyPrice(0, 0, 10, 0);

            npc.HitSound = SoundID.NPCHit54;
            npc.DeathSound = SoundID.NPCDeath52;

            npc.aiStyle = 5;

            npc.noGravity = true;
            npc.noTileCollide = true;

            aiType = NPCID.Wraith;
            animationType = NPCID.Wraith;
            //banner = npc.type;
            //bannerItem = mod.ItemType("StellarCenturionBanner");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stellar Centurion");
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
            !Main.snowMoon && !Main.pumpkinMoon && NPC.downedMoonlord && !Main.dayTime ? 0.2f : 0f;
        }
        public override void AI()
        {
            Lighting.AddLight(npc.Center, 0.4f, 0.4f, 0.7f);
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Stellorite"), Main.rand.Next(3, 6));
        }
    }
}