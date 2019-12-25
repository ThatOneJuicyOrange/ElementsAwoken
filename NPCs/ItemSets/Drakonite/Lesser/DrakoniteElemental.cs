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
            npc.width = 40;
            npc.height = 40; 
            
            npc.aiStyle = 91;
            animationType = 483;

            npc.damage = 15;
            npc.defense = 12;
            npc.lifeMax = 40;
            npc.knockBackResist = 0.5f;

            npc.value = Item.buyPrice(0, 0, 2, 0);
            npc.npcSlots = 0.5f;

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
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.75f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.75f);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            bool underworld = (spawnInfo.spawnTileY >= (Main.maxTilesY - 200));
            bool rockLayer = (spawnInfo.spawnTileY >= (Main.maxTilesY * 0.4f));
            return !underworld && rockLayer && Main.evilTiles < 80 && Main.sandTiles < 80 && Main.dungeonTiles < 80 && !Main.hardMode ? 0.06f : 0f;
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            if (Main.expertMode) player.AddBuff(BuffID.OnFire, MyWorld.awakenedMode ? 300 : 120, false);
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Drakonite"), Main.rand.Next(1, 3));
        }
    }
}