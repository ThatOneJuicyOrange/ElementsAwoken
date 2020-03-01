using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.ItemSets.Floral
{
    public class FlyingJaw : ModNPC
    {
        public override void SetDefaults()
        {
            npc.aiStyle = 2;
            npc.damage = 35;

            npc.width = 30;
            npc.height = 32;

            npc.defense = 15;
            npc.lifeMax = 80;
            npc.knockBackResist = 0f;

            npc.value = Item.buyPrice(0, 0, 1, 0);

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            banner = npc.type;
            bannerItem = mod.ItemType("FlyingJawBanner");
        }
        public override void FindFrame(int frameHeight)
        {
            npc.direction = Math.Sign(npc.velocity.X);
            npc.spriteDirection = npc.direction;

            npc.rotation = (float)Math.Atan2((double)(npc.velocity.Y * (float)npc.direction), (double)(npc.velocity.X * (float)npc.direction));
            npc.frameCounter += 0.15f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int frame = (int)npc.frameCounter;
            npc.frame.Y = frame * frameHeight;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flying Jaw");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float spawnChance = Main.hardMode ? 0.015f : 0.045f;
            return spawnInfo.player.ZoneJungle &&
                !spawnInfo.player.ZoneTowerStardust &&
                !spawnInfo.player.ZoneTowerSolar &&
                !spawnInfo.player.ZoneTowerVortex &&
                !spawnInfo.player.ZoneTowerNebula &&
                !spawnInfo.playerInTown &&
                NPC.downedBoss3 && !Main.snowMoon && !Main.pumpkinMoon ? spawnChance : 0f;
        }

        public override void NPCLoot()
        {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Petal"), Main.rand.Next(1, 2));
        }
    }
}