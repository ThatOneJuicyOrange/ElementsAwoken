using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.ItemSets.Drakonite.Lesser
{
    public class DrakoniteElemental : ModNPC
    {
        public override void SetDefaults()
        {
            npc.npcSlots = 0.5f;
            npc.aiStyle = 91;
            npc.damage = 15;
            npc.width = 20; //324
            npc.height = 30; //216
            npc.defense = 15;
            npc.lifeMax = 40;
            npc.knockBackResist = 0.5f;
            animationType = 483;
            Main.npcFrameCount[npc.type] = 22;
            npc.value = Item.buyPrice(0, 0, 20, 0);
            npc.HitSound = SoundID.NPCHit7;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.buffImmune[24] = true;

            banner = npc.type;
            bannerItem = mod.ItemType("DrakoniteElementalBanner");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Drakonite Elemental");
            Main.npcFrameCount[npc.type] = 22;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            int x = spawnInfo.spawnTileX;
            int y = spawnInfo.spawnTileY;
            int tile = (int)Main.tile[x, y].type;
            bool oUnderworld = (y <= (Main.maxTilesY * 0.6f));
            bool oRockLayer = (y >= (Main.maxTilesY * 0.4f));
            return oUnderworld && oRockLayer && Main.evilTiles < 80 && Main.sandTiles < 80 && Main.dungeonTiles < 80 && !Main.hardMode ? 0.06f : 0f;
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.OnFire, 180, false);
        }
        public override void NPCLoot()
        {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Drakonite"), Main.rand.Next(3, 6)); //Item spawn
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.75f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.75f);
        }
    }
}