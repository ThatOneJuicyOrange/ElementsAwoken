using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.InfernoSpirit
{
    public class InfernoSpirit : ModNPC
    {
        public override void SetDefaults()
        {
            npc.aiStyle = 86;
            npc.damage = 120;
            npc.width = 40; 
            npc.height = 24;
            npc.defense = 12;
            npc.lifeMax = 1200;
            npc.knockBackResist = 0.05f;
            npc.value = Item.buyPrice(0, 2, 0, 0);
            npc.HitSound = SoundID.NPCHit52;
            npc.DeathSound = SoundID.NPCDeath55;
            banner = npc.type;
            bannerItem = mod.ItemType("InfernoSpiritBanner");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Inferno Spirit");
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void FindFrame(int frameHeight)
        {
            if (npc.velocity.X < 0f)
            {
                npc.direction = -1;
            }
            else
            {
                npc.direction = 1;
            }
            if (npc.direction == 1)
            {
                npc.spriteDirection = 1;
            }
            if (npc.direction == -1)
            {
                npc.spriteDirection = -1;
            }
            npc.rotation = (float)Math.Atan2((double)(npc.velocity.Y * (float)npc.direction), (double)(npc.velocity.X * (float)npc.direction));
            npc.frameCounter += 0.15f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int frame = (int)npc.frameCounter;
            npc.frame.Y = frame * frameHeight;
        }

        public override void AI()
        {
            Lighting.AddLight(npc.Center, 1.0f, 0.2f, 0.7f);

            int dust = Dust.NewDust(npc.position, npc.width, npc.height, 6);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.player.ZoneDungeon) &&
            !spawnInfo.player.ZoneTowerStardust &&
            !spawnInfo.player.ZoneTowerSolar &&
            !spawnInfo.player.ZoneTowerVortex &&
            !spawnInfo.player.ZoneTowerNebula &&
            !spawnInfo.playerInTown &&
            !Main.snowMoon && !Main.pumpkinMoon && NPC.downedMoonlord ? 0.045f : 0f;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.8f);
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.OnFire, 400, true);
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 6, hitDirection, -1f, 0, default(Color), 1f);
            }
            if (npc.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 6, hitDirection, -1f, 0, default(Color), 1f);
                }
            }
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Pyroplasm"), Main.rand.Next(1, 4));
        }
    }
}