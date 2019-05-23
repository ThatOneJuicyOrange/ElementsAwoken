using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Elementals
{
    public class SkyElemental : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 26;
            npc.height = 48;
            npc.damage = 38; //change
            npc.defense = 18; //change
            npc.lifeMax = 200; //change
            npc.value = Item.buyPrice(0, 0, 30, 0); //change
            npc.HitSound = SoundID.NPCHit54;
            npc.DeathSound = SoundID.NPCDeath52;
            npc.knockBackResist = 0.5f;
            npc.aiStyle = 5;
            npc.noGravity = true;
            npc.noTileCollide = true;
            aiType = NPCID.Wraith;
            animationType = NPCID.Wraith;
            banner = npc.type;
            bannerItem = mod.ItemType("SkyElementalBanner");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sky Elemental");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            /*int x = spawnInfo.spawnTileX;
            int y = spawnInfo.spawnTileY;
            int tile = (int)Main.tile[x, y].type;
            bool oUnderworld = (y <= (Main.maxTilesY * 0.6f));
            bool oRockLayer = (y >= (Main.maxTilesY * 0.4f));*/

            return (spawnInfo.player.ZoneSkyHeight) &&
            !spawnInfo.player.ZoneTowerStardust &&
            !spawnInfo.player.ZoneTowerSolar &&
            !spawnInfo.player.ZoneTowerVortex &&
            !spawnInfo.player.ZoneTowerNebula &&
            !spawnInfo.playerInTown &&
            !Main.snowMoon && !Main.pumpkinMoon && NPC.downedMechBossAny && !Main.dayTime ? 0.09f : 0f;
        }
        public override void AI()
        {
            Lighting.AddLight(npc.Center, 0.0f, 0.5f, 1.0f);
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.Slow, 60, false);
        }
        public override void NPCLoot()
        {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SkyEssence"), 1); //Item spawn
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Cloud, Main.rand.Next(10, 20));
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.75f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.75f);
        }
    }
}