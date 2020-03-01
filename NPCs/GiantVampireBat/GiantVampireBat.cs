using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.GiantVampireBat
{
    public class GiantVampireBat : ModNPC
    {
        public float shootTimer = 180f;

        public override void SetDefaults()
        {
            npc.width = 26;
            npc.height = 20;
            
            npc.aiStyle = 14;
            aiType = NPCID.CaveBat;

            npc.damage = 45;
            npc.defense = 16;
            npc.lifeMax = 100;
            npc.knockBackResist = 0.25f;

            animationType = 93;

            npc.value = Item.buyPrice(0, 0, 7, 50);

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath4;

            banner = npc.type;
            bannerItem = mod.ItemType("GiantVampireBatBanner");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Giant Vampire Bat");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            int x = spawnInfo.spawnTileX;
            int y = spawnInfo.spawnTileY;
            int tile = (int)Main.tile[x, y].type;
            bool oUnderworld = (y <= (Main.maxTilesY * 0.6f));
            bool oRockLayer = (y >= (Main.maxTilesY * 0.4f));
            return oUnderworld && oRockLayer && !spawnInfo.player.ZoneCrimson && !spawnInfo.player.ZoneCorrupt && !spawnInfo.player.ZoneDesert && !spawnInfo.player.ZoneDungeon && Main.hardMode ? 0.05f : 0f;
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(99) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.TrifoldMap, 1);
            }
            if (Main.rand.Next(99) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.DepthMeter, 1);
            }
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            if (npc.life + 4 < npc.lifeMax)
            {
                npc.life += 5;
                npc.HealEffect(5);
            }
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 90;
            npc.lifeMax = 200;
        }
    }
}