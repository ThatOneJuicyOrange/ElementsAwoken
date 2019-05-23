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
    public class FireElemental : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 26;
            npc.height = 48;
            npc.damage = 18; //change
            npc.defense = 10; //change
            npc.lifeMax = 150; //change
            npc.value = Item.buyPrice(0, 0, 20, 0); //change
            npc.HitSound = SoundID.NPCHit54;
            npc.DeathSound = SoundID.NPCDeath52;
            npc.knockBackResist = 0.5f;
            npc.aiStyle = 5;
            npc.noGravity = true;
            npc.noTileCollide = true;
            aiType = NPCID.Wraith;
            animationType = NPCID.Wraith;
            banner = npc.type;
            bannerItem = mod.ItemType("FireElementalBanner");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Elemental");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            /*int x = spawnInfo.spawnTileX;
            int y = spawnInfo.spawnTileY;
            int tile = (int)Main.tile[x, y].type;
            bool oUnderworld = (y <= (Main.maxTilesY * 0.6f));
            bool oRockLayer = (y >= (Main.maxTilesY * 0.4f));*/

            return (spawnInfo.player.ZoneUnderworldHeight) &&
            !spawnInfo.player.ZoneTowerStardust &&
            !spawnInfo.player.ZoneTowerSolar &&
            !spawnInfo.player.ZoneTowerVortex &&
            !spawnInfo.player.ZoneTowerNebula &&
            !spawnInfo.playerInTown &&
            !Main.snowMoon && !Main.pumpkinMoon && NPC.downedBoss3 && !Main.dayTime ? 0.08f : 0f;
        }
        public override void AI()
        {
            Lighting.AddLight(npc.Center, 1.0f, 0.6f, 0.0f);
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.OnFire, 60, false);
        }
        public override void NPCLoot()
        {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FireEssence"), 1); //Item spawn
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.LivingFireBlock, Main.rand.Next(1, 10));
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.75f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.75f);
        }
    }
}