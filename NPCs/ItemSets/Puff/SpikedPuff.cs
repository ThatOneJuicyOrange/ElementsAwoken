using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace ElementsAwoken.NPCs.ItemSets.Puff
{

    public class SpikedPuff : ModNPC
    {
        public float spikeTimer = 60f;

        public override void SetDefaults()
        {
            npc.width = 30;
            npc.height = 50;

            npc.damage = 24;
            npc.defense = 6;
            npc.lifeMax = 32;
            npc.knockBackResist = 0.5f;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.value = Item.buyPrice(0, 0, 1, 0);

            npc.aiStyle = 1;
            aiType = 1;

            animationType = NPCID.BlueSlime;
            banner = npc.type;
            bannerItem = mod.ItemType("SpikedPuffBanner");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spiked Puff");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            if (Main.expertMode)
            {
                player.AddBuff(mod.BuffType("Cuddled"), 2000);
            }
            else
            {
                player.AddBuff(mod.BuffType("Cuddled"), 500);
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.spawnTileY < Main.rockLayer) &&
            !spawnInfo.player.ZoneTowerStardust &&
            !spawnInfo.player.ZoneTowerSolar &&
            !spawnInfo.player.ZoneTowerVortex &&
            !spawnInfo.player.ZoneTowerNebula &&
            !spawnInfo.playerInTown &&
            !spawnInfo.invasion &&
            !Main.snowMoon && Main.expertMode &&!Main.pumpkinMoon && NPC.downedBoss1 && Main.dayTime && !Main.hardMode ? 0.04f : 0f;
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Puffball"), Main.rand.Next(2, 4));
        }

        public override void AI()
        {
            spikeTimer--;
            if (!npc.wet && !Main.player[npc.target].npcTypeNoAggro[npc.type])
            {
                Vector2 vector = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                float num8 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector.X;
                float num9 = Main.player[npc.target].position.Y - vector.Y;
                float num10 = (float)Math.Sqrt((double)(num8 * num8 + num9 * num9));
                if (Main.expertMode && num10 < 120f && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height) && npc.velocity.Y == 0f)
                {
                    npc.ai[0] = -40f;
                    if (npc.velocity.Y == 0f)
                    {
                        npc.velocity.X = npc.velocity.X * 0.9f;
                    }
                    if (Main.netMode != NetmodeID.MultiplayerClient && spikeTimer <= 0f)
                    {
                        int num = MyWorld.awakenedMode ? 5 : 3;
                        for (int i = 0; i < num; i++)
                        {
                            Vector2 vector2 = new Vector2((float)(i - 2), -4f);
                            vector2.X *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
                            vector2.Y *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
                            vector2.Normalize();
                            vector2 *= 4f + (float)Main.rand.Next(-50, 51) * 0.01f;
                            int damage = 7;
                            Projectile.NewProjectile(vector.X, vector.Y, vector2.X, vector2.Y, mod.ProjectileType("PuffSpike"), damage, 0f, Main.myPlayer, 0f, 0f);
                            spikeTimer = 70f;
                        }
                    }
                }
                else if (num10 < 200f && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height) && npc.velocity.Y == 0f)
                {
                    npc.ai[0] = -40f;
                    if (npc.velocity.Y == 0f)
                    {
                        npc.velocity.X = npc.velocity.X * 0.9f;
                    }
                    if (Main.netMode != NetmodeID.MultiplayerClient && spikeTimer <= 0f)
                    {
                        num9 = Main.player[npc.target].position.Y - vector.Y - (float)Main.rand.Next(0, 200);
                        num10 = (float)Math.Sqrt((double)(num8 * num8 + num9 * num9));
                        num10 = 4.5f / num10;
                        num8 *= num10;
                        num9 *= num10;
                        spikeTimer = 90f;
                        int damage = 5;
                        Projectile.NewProjectile(vector.X, vector.Y, num8, num9, mod.ProjectileType("PuffSpike"), damage, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }
        }
    }
}
