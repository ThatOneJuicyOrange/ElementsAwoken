using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.CosmicObserver
{
    [AutoloadBossHead]
    public class CosmicObserver : ModNPC
    {
        public int orbCooldown = 75;
        int projectileBaseDamage = 20;
        bool reset = false;
        int moveAi = 0;

        float storeRot = 0;
        public override void SetDefaults()
        {
            npc.width = 104;
            npc.height = 104;

            npc.lifeMax = 5500;
            npc.damage = 40;
            npc.defense = 20;
            npc.knockBackResist = 0f;

            npc.scale = 1.2f;

            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;

            npc.HitSound = SoundID.NPCHit2;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.value = Item.buyPrice(0, 5, 0, 0);

            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[mod.BuffType("IceBound")] = true;
            npc.buffImmune[mod.BuffType("EndlessTears")] = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("A Cosmic Observer");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 70;
            npc.lifeMax = 12000;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 17500;
                npc.damage = 90;
                npc.defense = 30;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            if (npc.life <= npc.lifeMax / 2)
            {
                npc.frame.Y = 1 * frameHeight;
            }
            else
            {
                npc.frame.Y = 0;
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float spawnchance = MyWorld.downedCosmicObserver ? 0.0f : 0.02f; // around half the spawn chance of the wyvern
            MyPlayer modPlayer = spawnInfo.player.GetModPlayer<MyPlayer>(mod);

            if (modPlayer.increasedObserverChance && !NPC.AnyNPCs(mod.NPCType("CosmicObserver")))
            {
                return 0.4f;
            }
            if (MyWorld.downedCosmicObserver)
            {
                return 0.0f;
            }
            return spawnInfo.player.ZoneSkyHeight && !NPC.AnyNPCs(mod.NPCType("CosmicObserver")) && Main.hardMode ? spawnchance : 0f;
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CosmicShard"), Main.rand.Next(20, 35)); //Item spawn
            MyWorld.downedCosmicObserver = true;
        }

        public override void AI()
        {
            Player P = Main.player[npc.target];
            #region despawning
            if (!Main.player[npc.target].active || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (!Main.player[npc.target].active || Main.player[npc.target].dead)
                {
                    npc.localAI[3]++;
                }
            }
            if (npc.localAI[3] >= 300)
            {
                npc.active = false;
            }
            #endregion
            Lighting.AddLight(npc.Center, 0.2f, 1.4f, 0.2f);
            if (!reset)
            {
                Main.PlaySound(15, (int)P.position.X, (int)P.position.Y, 0);
                Main.NewText("A Cosmic Observer roams the skies", 175, 75, 255, false);
                reset = true;
            }
            npc.ai[0]--;
            npc.ai[1]++;
            npc.ai[2]--; // multiple laser burst
            if (npc.ai[1] >= ((npc.life > npc.lifeMax / 2) ? 1200 : 1400))
            {
                npc.ai[1] = 0;
            }
            if (npc.ai[2] <= 0)
            {
                npc.ai[2] = Main.rand.Next(60, 90);
            }
            if (npc.ai[1] <= 600)
            {
                if (npc.life > npc.lifeMax / 2)
                {
                    if (npc.ai[0] <= 0)
                    {
                        if (npc.life > npc.lifeMax / 2)
                        {
                            Laser(P, 4.5f, projectileBaseDamage);
                            npc.ai[0] = Main.rand.Next(60, 120);
                        }

                    }
                }
                else
                {
                    if (Main.netMode != 1 && npc.ai[0] <= 0 && npc.ai[2] <= 18)
                    {
                        Laser(P, 4f, projectileBaseDamage);
                        npc.ai[0] = 6;
                    }
                }
                Move(P, 0.075f, P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
            }
            if (npc.ai[1] == 600)
            {
                npc.localAI[1] = 0;
                npc.localAI[0] = 0;
            }
            if (npc.ai[1] > 600 && npc.ai[1] <= 1200)
            {
                //movement
                if (npc.ai[0] > 0)
                {
                    float playerX = P.Center.X - npc.Center.X;
                    float playerY = P.Center.Y - 75 - npc.Center.Y;
                    if (moveAi == 0)
                    {
                        playerX = P.Center.X - 600f - npc.Center.X;
                        if (Math.Abs(P.Center.X - 600f - npc.Center.X) <= 20)
                        {
                            moveAi = 1;
                        }
                    }
                    if (moveAi == 1)
                    {
                        playerX = P.Center.X + 600f - npc.Center.X;
                        if (Math.Abs(P.Center.X + 600f - npc.Center.X) <= 20)
                        {
                            moveAi = 0;
                        }
                    }
                    Move(P, 0.1f, playerX, playerY);
                }
                int numProj = 8;

                if (npc.ai[0] == 1)
                {
                    storeRot = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                }
                if (npc.ai[0] <= 0)
                {
                    npc.velocity.X *= 0.9f;
                    npc.velocity.Y *= 0.9f;

                    npc.localAI[1]++;
                    if (npc.localAI[0] < numProj && npc.localAI[1] % 5 == 0)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);

                        Vector2 projSpeed = new Vector2((float)((Math.Cos(storeRot) * 10) * -1), (float)((Math.Sin(storeRot) * 10) * -1));
                        float rotation = MathHelper.ToRadians(5);
                        float amount = npc.direction == -1 ? npc.localAI[0] - numProj / 2 : -(npc.localAI[0] - numProj / 2); // to make it from down to up
                        Vector2 perturbedSpeed = new Vector2(projSpeed.X, projSpeed.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, amount));
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("ObserverShard"), projectileBaseDamage, 3f, 0);
                        npc.localAI[0]++;
                    }
                }
                if (npc.localAI[0] >= numProj)
                {
                    npc.localAI[1] = 0;
                    npc.localAI[0] = 0;
                    npc.ai[0] = 120f;
                }
            }
            if (npc.ai[1] > 1200)
            {
                npc.rotation += 0.2f;

                npc.velocity.X = 0f;
                npc.velocity.Y = 0f;
                if (npc.ai[0] <= 0)
                {
                    Vector2 perturbedSpeed = new Vector2(7f, 7f).RotatedByRandom(MathHelper.ToRadians(360));
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("ObserverShard"), projectileBaseDamage, 0f, 0);
                    npc.ai[0] = 5;
                }
            }
            else
            {
                npc.rotation = npc.velocity.X * 0.1f;
            }
        }

        private void Move(Player P, float speed, float playerX, float playerY)
        {
            int maxDist = 1000;
            if (Vector2.Distance(P.Center, npc.Center) >= maxDist)
            {
                float moveSpeed = 14f;
                Vector2 toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
                toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
                toTarget.Normalize();
                npc.velocity = toTarget * moveSpeed;
            }
            else
            {
                if (Main.expertMode)
                {
                    speed += 0.1f;
                }
                if (npc.velocity.X < playerX)
                {
                    npc.velocity.X = npc.velocity.X + speed * 2;
                }
                else if (npc.velocity.X > playerX)
                {
                    npc.velocity.X = npc.velocity.X - speed * 2;
                }
                if (npc.velocity.Y < playerY)
                {
                    npc.velocity.Y = npc.velocity.Y + speed * 0.8f;
                    if (npc.velocity.Y < 0f && playerY > 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y + speed * 0.8f;
                        return;
                    }
                }
                else if (npc.velocity.Y > playerY)
                {
                    npc.velocity.Y = npc.velocity.Y - speed * 0.8f;
                    if (npc.velocity.Y > 0f && playerY < 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y - speed * 0.8f;
                        return;
                    }
                }
            }
        }

        private void Laser(Player P, float speed, int damage)
        {
            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 33);
            float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), mod.ProjectileType("ObserverLaser"), damage, 0f, 0);
        }

        public override bool CheckActive()
        {
            return false;
        }
    }
}
