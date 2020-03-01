using System;
using System.Collections.Generic;
using System.IO;
using ElementsAwoken.Projectiles.GlobalProjectiles;
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
            npc.width = 32;
            npc.height = 36; 
            
            npc.aiStyle = -1;

            npc.damage = 15;
            npc.defense = 8;
            npc.lifeMax = 40;
            npc.knockBackResist = 0.5f;

            npc.value = Item.buyPrice(0, 0, 2, 0);
            npc.npcSlots = 0.5f;

            npc.HitSound = SoundID.NPCHit7;
            npc.DeathSound = SoundID.NPCDeath6;

            npc.buffImmune[BuffID.OnFire] = true;

            banner = npc.type;
            bannerItem = mod.ItemType("DrakoniteElementalBanner");
            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
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
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.75f);
                npc.damage = (int)(npc.damage * 1.3f);
                npc.defense = 12;
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            int maxElementals =  1;
            if (MyWorld.awakenedMode) maxElementals = 2;
            bool underworld = (spawnInfo.spawnTileY >= (Main.maxTilesY - 200));
            bool caverns = (spawnInfo.spawnTileY >= (Main.maxTilesY * 0.4f));
            return !underworld && caverns && NPC.CountNPCS(npc.type) < maxElementals && !spawnInfo.player.ZoneCrimson && !spawnInfo.player.ZoneCorrupt && !spawnInfo.player.ZoneDesert && !spawnInfo.player.ZoneDungeon && !Main.hardMode ? 0.06f : 0f;
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            if (Main.expertMode) player.AddBuff(BuffID.OnFire, MyWorld.awakenedMode ? 150 : 90, false);
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Drakonite"), Main.rand.Next(1, 3));
        }
        public override void FindFrame(int frameHeight)
        {
            if (npc.ai[0] == -1f)
            {
                npc.frameCounter += 1.0;
                if (npc.frameCounter > 4.0)
                {
                    npc.frame.Y = npc.frame.Y + frameHeight;
                    npc.frameCounter = 0.0;
                }
                if (npc.frame.Y > frameHeight * 21)
                {
                    npc.frame.Y = frameHeight * 21;
                }
                else if (npc.frame.Y < frameHeight * 13)
                {
                    npc.frame.Y = frameHeight * 13;
                }
                npc.rotation += npc.velocity.X * 0.2f;
                return;
            }
            npc.frameCounter += 1.0;
            if (npc.frameCounter > 4.0)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.frame.Y > frameHeight * 10)
            {
                npc.frame.Y = 0;
            }
            npc.rotation = npc.velocity.X * 0.1f;
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.dontTakeDamage = false;

            if (MyWorld.awakenedMode) npc.ai[3]++;
            if (MyWorld.awakenedMode && npc.ai[3] > 600 && Collision.CanHit(npc.Center, 1, 1, P.Center, 1, 1))
            {
                npc.ai[0] = 5f;
                npc.ai[1] = 0f;
                npc.ai[3] = 0;
            }

            if (npc.justHit && Main.netMode != 1 && ((Main.expertMode && Main.rand.Next(6) == 0) || (MyWorld.awakenedMode && Main.rand.Next(4) == 0)))
            {
                npc.netUpdate = true;
                npc.ai[0] = -1f;
                npc.ai[1] = 0f;
            }
            if (npc.ai[0] == -1f)
            {
                npc.dontTakeDamage = true;
                npc.noGravity = false;
                npc.velocity.X = npc.velocity.X * 0.98f;
                npc.ai[1] += 1f;
                if (npc.ai[1] >= 120f)
                {
                    npc.ai[0] = (npc.ai[1] = (npc.ai[2] = 0f));
                    return;
                }
            }
            else if (npc.ai[0] == 0f)
            {
                npc.TargetClosest(true);
                if (Collision.CanHit(npc.Center, 1, 1, P.Center, 1, 1))
                {
                    npc.ai[0] = 1f;
                    return;
                }
                Vector2 toTarget = P.Center - npc.Center;
                toTarget.Y -= (float)(P.height / 4);
                if (toTarget.Length() > 800f) // go through walls
                {
                    npc.ai[0] = 2f;
                    return;
                }
                Vector2 center30 = npc.Center;
                center30.X = P.Center.X;
                Vector2 vector243 = center30 - npc.Center;
                if (vector243.Length() > 8f && Collision.CanHit(npc.Center, 1, 1, center30, 1, 1))
                {
                    npc.ai[0] = 3f;
                    npc.ai[1] = center30.X;
                    npc.ai[2] = center30.Y;
                    Vector2 center31 = npc.Center;
                    center31.Y = P.Center.Y;
                    if (vector243.Length() > 8f && Collision.CanHit(npc.Center, 1, 1, center31, 1, 1) && Collision.CanHit(center31, 1, 1, P.position, 1, 1))
                    {
                        npc.ai[0] = 3f;
                        npc.ai[1] = center31.X;
                        npc.ai[2] = center31.Y;
                    }
                }
                else
                {
                    center30 = npc.Center;
                    center30.Y = P.Center.Y;
                    if ((center30 - npc.Center).Length() > 8f && Collision.CanHit(npc.Center, 1, 1, center30, 1, 1))
                    {
                        npc.ai[0] = 3f;
                        npc.ai[1] = center30.X;
                        npc.ai[2] = center30.Y;
                    }
                }
                if (npc.ai[0] == 0f)
                {
                    npc.localAI[0] = 0f;
                    toTarget.Normalize();
                    toTarget *= 0.5f;
                    npc.velocity += toTarget;
                    npc.ai[0] = 4f;
                    npc.ai[1] = 0f;
                    return;
                }
            }
            else if (npc.ai[0] == 1f)
            {
                Vector2 value47 = P.Center - npc.Center;
                float num1382 = value47.Length();
                float num1383 = 2f;
                num1383 += num1382 / 200f;
                int num1384 = 50;
                value47.Normalize();
                value47 *= num1383;
                npc.velocity = (npc.velocity * (float)(num1384 - 1) + value47) / (float)num1384;
                if (!Collision.CanHit(npc.Center, 1, 1, P.Center, 1, 1))
                {
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                    return;
                }
            }
            else if (npc.ai[0] == 2f) // go through walls
            {
                npc.noTileCollide = true;
                Vector2 value48 = P.Center - npc.Center;
                float num1385 = value48.Length();
                float scaleFactor23 = 2f;
                int num1386 = 4;
                value48.Normalize();
                value48 *= scaleFactor23;
                npc.velocity = (npc.velocity * (float)(num1386 - 1) + value48) / (float)num1386;
                if (num1385 < 600f && !Collision.SolidCollision(npc.position, npc.width, npc.height))
                {
                    npc.ai[0] = 0f;
                    return;
                }
            }
            else if (npc.ai[0] == 3f)
            {
                Vector2 value49 = new Vector2(npc.ai[1], npc.ai[2]);
                Vector2 value50 = value49 - npc.Center;
                float num1387 = value50.Length();
                float num1388 = 1f;
                float num1389 = 3f;
                value50.Normalize();
                value50 *= num1388;
                npc.velocity = (npc.velocity * (num1389 - 1f) + value50) / num1389;
                if (npc.collideX || npc.collideY)
                {
                    npc.ai[0] = 4f;
                    npc.ai[1] = 0f;
                }
                if (num1387 < num1388 || num1387 > 800f || Collision.CanHit(npc.Center, 1, 1, P.Center, 1, 1))
                {
                    npc.ai[0] = 0f;
                    return;
                }

            }
            else if (npc.ai[0] == 4f)
            {
                if (npc.collideX)
                {
                    npc.velocity.X = npc.velocity.X * -0.8f;
                }
                if (npc.collideY)
                {
                    npc.velocity.Y = npc.velocity.Y * -0.8f;
                }
                Vector2 value51;
                if (npc.velocity.X == 0f && npc.velocity.Y == 0f)
                {
                    value51 = P.Center - npc.Center;
                    value51.Y -= (float)(P.height / 4);
                    value51.Normalize();
                    npc.velocity = value51 * 0.1f;
                }
                float scaleFactor24 = 1.5f;
                float num1390 = 20f;
                value51 = npc.velocity;
                value51.Normalize();
                value51 *= scaleFactor24;
                npc.velocity = (npc.velocity * (num1390 - 1f) + value51) / num1390;
                npc.ai[1] += 1f;
                if (npc.ai[1] > 180f)
                {
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                }
                if (Collision.CanHit(npc.Center, 1, 1, P.Center, 1, 1))
                {
                    npc.ai[0] = 0f;
                }
                npc.localAI[0] += 1f;
                if (npc.localAI[0] >= 5f && !Collision.SolidCollision(npc.position - new Vector2(10f, 10f), npc.width + 20, npc.height + 20))
                {
                    npc.localAI[0] = 0f;
                    Vector2 center32 = npc.Center;
                    center32.X = P.Center.X;
                    if (Collision.CanHit(npc.Center, 1, 1, center32, 1, 1) && Collision.CanHit(npc.Center, 1, 1, center32, 1, 1) && Collision.CanHit(P.Center, 1, 1, center32, 1, 1))
                    {
                        npc.ai[0] = 3f;
                        npc.ai[1] = center32.X;
                        npc.ai[2] = center32.Y;
                        return;
                    }
                    center32 = npc.Center;
                    center32.Y = P.Center.Y;
                    if (Collision.CanHit(npc.Center, 1, 1, center32, 1, 1) && Collision.CanHit(P.Center, 1, 1, center32, 1, 1))
                    {
                        npc.ai[0] = 3f;
                        npc.ai[1] = center32.X;
                        npc.ai[2] = center32.Y;
                        return;
                    }
                }
            }
            else if (npc.ai[0] == 5f)
            {
                npc.velocity *= 0.97f;
                npc.ai[1]++;
                if (npc.ai[1] < 180)
                {
                    if (Main.rand.NextBool(3))
                    {
                        Dust dust = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, 6, 0f, 0f, 200, default(Color), 0.5f)];
                        dust.noGravity = true;
                        dust.fadeIn = 1.3f;
                        Vector2 vector = Main.rand.NextVector2Square(-1, 1f);
                        vector.Normalize();
                        vector *= 3f;
                        dust.velocity = vector;
                        dust.position = npc.Center - vector * 15;
                    }
                }
                else if (npc.ai[1] == 180)
                {
                    float Speed = 10f;
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y,20);
                    float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                    Projectile beam = Main.projectile[Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("DrakoniteElementalBeam"), 30, 0f, 0)];
                    beam.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
                }
                else if (npc.ai[1] >= 210)
                {
                    npc.ai[0] = 3f;
                    npc.ai[1] = P.Center.X;
                    npc.ai[2] = P.Center.Y;
                }
            }
        }
    }
}