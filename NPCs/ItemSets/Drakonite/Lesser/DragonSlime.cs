using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace ElementsAwoken.NPCs.ItemSets.Drakonite.Lesser
{
    public class DragonSlime : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 42;
            npc.height = 30;

            npc.aiStyle = 1;
            aiType = 1;
            animationType = NPCID.BlueSlime;

            npc.damage = 24;
            npc.defense = 6;
            npc.lifeMax = 32;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.value = Item.buyPrice(0, 0, 2, 0);
            npc.knockBackResist = 0.5f;

            npc.buffImmune[24] = true;

            banner = npc.type;
            bannerItem = mod.ItemType("DragonSlimeBanner");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Slime");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            bool underworld = (spawnInfo.spawnTileY >= (Main.maxTilesY - 200));
            bool rockLayer = (spawnInfo.spawnTileY >= (Main.maxTilesY * 0.4f));
            return !underworld && rockLayer && !spawnInfo.player.ZoneCrimson && !spawnInfo.player.ZoneCorrupt && !spawnInfo.player.ZoneDesert && !spawnInfo.player.ZoneDungeon && !Main.hardMode ? 0.06f : 0f;
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            if (Main.expertMode) player.AddBuff(BuffID.OnFire, MyWorld.awakenedMode ? 150 : 90, false);
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Drakonite"), Main.rand.Next(1, 3));
        }

        public override void AI()
        {
            if (npc.localAI[1] > 0f)
            {
                npc.localAI[1] -= 1f;
            }
            if (!npc.wet && !Main.player[npc.target].npcTypeNoAggro[npc.type])
            {
                Vector2 vector3 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                float num14 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector3.X;
                float num15 = Main.player[npc.target].position.Y - vector3.Y;
                float num16 = (float)Math.Sqrt((double)(num14 * num14 + num15 * num15));
                if (Main.expertMode && num16 < 120f && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height) && npc.velocity.Y == 0f)
                {
                    npc.ai[0] = -40f;
                    if (npc.velocity.Y == 0f)
                    {
                        npc.velocity.X = npc.velocity.X * 0.9f;
                    }
                    if (Main.netMode != NetmodeID.MultiplayerClient && npc.localAI[1] == 0f)
                    {
                        int num = MyWorld.awakenedMode ? 5 : 3;
                        for (int n = 0; n < num; n++)
                        {
                            Vector2 vector4 = new Vector2((float)(n - 2), -4f);
                            vector4.X *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
                            vector4.Y *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
                            vector4.Normalize();
                            vector4 *= 4f + (float)Main.rand.Next(-50, 51) * 0.01f;
                            Projectile.NewProjectile(vector3.X, vector3.Y, vector4.X, vector4.Y, mod.ProjectileType("DragonSlimeSpike"), 9, 0f, Main.myPlayer, 0f, 0f);
                            npc.localAI[1] = 30f;
                        }
                    }
                }
                else if (num16 < 200f && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height) && npc.velocity.Y == 0f)
                {
                    npc.ai[0] = -40f;
                    if (npc.velocity.Y == 0f)
                    {
                        npc.velocity.X = npc.velocity.X * 0.9f;
                    }
                    if (Main.netMode != NetmodeID.MultiplayerClient && npc.localAI[1] == 0f)
                    {
                        num15 = Main.player[npc.target].position.Y - vector3.Y - (float)Main.rand.Next(0, 200);
                        num16 = (float)Math.Sqrt((double)(num14 * num14 + num15 * num15));
                        num16 = 4.5f / num16;
                        num14 *= num16;
                        num15 *= num16;
                        npc.localAI[1] = 50f;
                        Projectile.NewProjectile(vector3.X, vector3.Y, num14, num15, mod.ProjectileType("DragonSlimeSpike"), 9, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }
        }
    }
}
