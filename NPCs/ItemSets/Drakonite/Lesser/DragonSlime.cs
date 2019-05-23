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
        public float spikeTimer = 60f;

        public override void SetDefaults()
        {
            npc.width = 30;
            npc.height = 50;
            npc.damage = 24;
            npc.defense = 6;
            npc.lifeMax = 32;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(0, 0, 20, 0);
            npc.knockBackResist = 0.5f;
            npc.aiStyle = 1;
            Main.npcFrameCount[npc.type] = 2;
            aiType = 1;
            animationType = NPCID.BlueSlime;
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

        public override void AI()
        {
            Vector3 RGB = new Vector3(2.0f, 0.75f, 1.5f);
            float multiplier = 1;
            float max = 2.25f;
            float min = 1.0f;
            RGB *= multiplier;
            if (RGB.X > max)
            {
                multiplier = 0.5f;
            }
            if (RGB.X < min)
            {
                multiplier = 1.5f;
            }
            if (spikeTimer > 0f)
            {
                spikeTimer -= 1f;
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
                    if (Main.netMode != 1 && spikeTimer == 0f)
                    {
                        for (int n = 0; n < 5; n++)
                        {
                            Vector2 vector4 = new Vector2((float)(n - 2), -4f);
                            vector4.X *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
                            vector4.Y *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
                            vector4.Normalize();
                            vector4 *= 4f + (float)Main.rand.Next(-50, 51) * 0.01f;
                            Projectile.NewProjectile(vector3.X, vector3.Y, vector4.X, vector4.Y, mod.ProjectileType("DragonSlimeSpike"), 9, 0f, Main.myPlayer, 0f, 0f);
                            spikeTimer = 30f;
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
                    if (Main.netMode != 1 && spikeTimer == 0f)
                    {
                        num15 = Main.player[npc.target].position.Y - vector3.Y - (float)Main.rand.Next(0, 200);
                        num16 = (float)Math.Sqrt((double)(num14 * num14 + num15 * num15));
                        num16 = 4.5f / num16;
                        num14 *= num16;
                        num15 *= num16;
                        spikeTimer = 50f;
                        Projectile.NewProjectile(vector3.X, vector3.Y, num14, num15, mod.ProjectileType("DragonSlimeSpike"), 9, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }
        }
    }
}
