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

namespace ElementsAwoken.NPCs.Bosses.Ancients
{
    [AutoloadBossHead]
    public class Xernon : ModNPC
    {
        public float originX = 0;
        public float originY = 0;

        public int projectileBaseDamage = 100;

        public int specialType = 0;
        public bool attack2Inverse = false;
        public int attack2Timer = 0;
        public int attack2ShootTimer = 0;
        public override void SetDefaults()
        {
            npc.width = 88;
            npc.height = 88;

            npc.aiStyle = -1;

            npc.lifeMax = 200000;
            npc.damage = 100;
            npc.defense = 60;
            npc.knockBackResist = 0f;

            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;

            npc.HitSound = SoundID.NPCHit5;
            npc.DeathSound = SoundID.Item27;

            npc.scale *= 1.3f;
            npc.alpha = 255; // starts transparent
            npc.value = Item.buyPrice(0, 3, 0, 0);
            npc.npcSlots = 1f;

            music = MusicID.LunarBoss;
            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/InfernaceTheme");

            // all EA modded buffs (unless i forget to add new ones)
            npc.buffImmune[mod.BuffType("IceBound")] = true;
            npc.buffImmune[mod.BuffType("ExtinctionCurse")] = true;
            npc.buffImmune[mod.BuffType("HandsOfDespair")] = true;
            npc.buffImmune[mod.BuffType("EndlessTears")] = true;
            npc.buffImmune[mod.BuffType("AncientDecay")] = true;
            npc.buffImmune[mod.BuffType("SoulInferno")] = true;
            npc.buffImmune[mod.BuffType("DragonFire")] = true;
            npc.buffImmune[mod.BuffType("Discord")] = true;
            // all vanilla buffs
            for (int num2 = 0; num2 < 206; num2++)
            {
                npc.buffImmune[num2] = true;
            }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Xernon");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 250000;
            npc.damage = 120;
            npc.defense = 70;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 300000;
                npc.damage = 175;
                npc.defense = 80;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            npc.frameCounter++;

            if (npc.frameCounter > 6)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }

            if (npc.frame.Y >= frameHeight * Main.npcFrameCount[npc.type])
            {
                npc.frame.Y = 0;
            }
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.OnFire, 180, false);
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (npc.ai[0] < 180)
            {
                return false;
            }
            return true;
        }
        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            if (npc.ai[0] < 180)
            {
                return false;
            }
            return base.CanBeHitByProjectile(projectile);
        }
        public override bool? CanBeHitByItem(Player player, Item item)
        {
            if (npc.ai[0] < 180)
            {
                return false;
            }
            return true;
        }
        public override bool PreNPCLoot()
        {
            return false;
        }

        public override bool CheckActive()
        {
            return false;
        }

        public override void AI()
        {
            npc.TargetClosest(true);
            Player P = Main.player[npc.target];
            Lighting.AddLight(npc.Center, 0.3f, 0f, 1.5f);

            // despawn if no players
            if (!Main.player[npc.target].active || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (!Main.player[npc.target].active || Main.player[npc.target].dead)
                {
                    npc.localAI[0]++;
                    npc.velocity.Y = npc.velocity.Y + 0.11f;
                    if (npc.localAI[0] >= 300)
                    {
                        npc.active = false;
                    }
                }
                else
                    npc.localAI[0] = 0;
            }

            if (npc.ai[0] < 180)
            {
                if (npc.ai[0] == 0)
                {
                    originX = P.Center.X;
                    originY = P.Center.Y;
                }
                if (npc.ai[0] < 60)
                {
                    npc.alpha = 255;
                }
                else
                {
                    npc.alpha = 0;
                    Vector2 target = new Vector2(originX + 200, originY - 250);
                    Vector2 toTarget = new Vector2(target.X - npc.Center.X, target.Y - npc.Center.Y);
                    toTarget.Normalize();
                    if (Vector2.Distance(target, npc.Center) > 5)
                    {
                        npc.velocity = toTarget * 6;
                    }
                    else
                    {
                        npc.velocity *= 0f;
                    }
                }
                npc.ai[0]++;
            }
            else
            {
                npc.ai[1]++;
                npc.ai[2]--;
                if (npc.ai[1] < 1800)
                {
                    if (npc.ai[2] <= 0)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 21);
                        float projSpeed = 12f;
                        float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                        if (npc.life > npc.lifeMax * 0.6f)
                        {
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * projSpeed) * -1), (float)((Math.Sin(rotation) * projSpeed) * -1), mod.ProjectileType("XernonBolt"), projectileBaseDamage, 5f, 0);
                            npc.ai[2] = 120;
                        }
                        else if (npc.life > npc.lifeMax * 0.3f)
                        {
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * projSpeed) * -1), (float)((Math.Sin(rotation) * projSpeed) * -1), mod.ProjectileType("XernonBoltExplosive"), projectileBaseDamage, 5f, 0);
                            npc.ai[2] = 140;
                        }
                        else
                        {
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * projSpeed) * -1), (float)((Math.Sin(rotation) * projSpeed) * -1), mod.ProjectileType("XernonBoltSuper"), projectileBaseDamage, 5f, 0);
                            npc.ai[2] = 150;
                        }
                    }
                }
                else
                {
                    if (npc.ai[1] == 1800)
                    {
                        specialType = Main.rand.Next(2);
                    }
                    if (specialType == 0)
                    {
                        if (npc.ai[3] == 0)
                        {
                            Vector2 target = new Vector2(P.Center.X, P.Center.Y - 500f);
                            Vector2 toTarget = new Vector2(target.X - npc.Center.X, target.Y - npc.Center.Y);
                            toTarget.Normalize();
                            if (Vector2.Distance(target, npc.Center) > 20)
                            {
                                npc.velocity = toTarget * 16;
                            }
                            else
                            {
                                npc.ai[3]++;
                            }
                        }
                        else
                        {
                            npc.velocity *= 0f;
                            if (npc.ai[3] == 5)
                            {
                                for (int i = 0; i < 2; i++)
                                {
                                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, i == 1 ? -8f : 8f, 2f, mod.ProjectileType("XernonBoltHoming"), projectileBaseDamage, 5f, 0);
                                }
                            }
                            npc.ai[3]++;
                            if (npc.ai[3] == 180)
                            {
                                npc.ai[1] = Main.rand.Next(-600, 0);
                                npc.ai[3] = 0;
                                npc.netUpdate = true;
                            }
                        }
                    }
                    else
                    {
                        if (npc.ai[3] == 0)
                        {
                            Vector2 target = new Vector2(P.Center.X + 400f, P.Center.Y - 250f);
                            Vector2 toTarget = new Vector2(target.X - npc.Center.X, target.Y - npc.Center.Y);
                            toTarget.Normalize();
                            if (Vector2.Distance(target, npc.Center) > 20)
                            {
                                npc.velocity = toTarget * 16;
                            }
                            else
                            {
                                npc.ai[3]++;
                            }
                        }
                        else if (npc.ai[3] == 1)
                        {
                            attack2Timer++;
                            attack2ShootTimer--;
                            if (attack2Timer > 90)
                            {
                                npc.ai[3]++;
                                attack2Timer = 0;
                            }
                            npc.velocity.X = -6f;
                            npc.velocity.Y = 0f;
                            if (attack2ShootTimer <= 0)
                            {
                                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 43);
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 6f, mod.ProjectileType("XernonCrystal"), projectileBaseDamage, 5f, 0);
                                attack2ShootTimer = 10;
                            }
                        }
                        else if (npc.ai[3] == 2)
                        {
                            Vector2 target = new Vector2(P.Center.X - 400f, P.Center.Y + 250f);
                            Vector2 toTarget = new Vector2(target.X - npc.Center.X, target.Y - npc.Center.Y);
                            toTarget.Normalize();
                            if (Vector2.Distance(target, npc.Center) > 20)
                            {
                                npc.velocity = toTarget * 16;
                            }
                            else
                            {
                                npc.ai[3]++;
                            }
                        }
                        else
                        {
                            attack2Timer++;
                            attack2ShootTimer--;
                            if (attack2Timer > 90)
                            {
                                npc.ai[1] = Main.rand.Next(-600, 0);
                                npc.ai[3] = 0;
                                attack2Timer = 0;
                                npc.netUpdate = true;
                            }
                            npc.velocity.X = 6f;
                            npc.velocity.Y = 0f;
                            if (attack2ShootTimer <= 0)
                            {
                                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 43);
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, -6f, mod.ProjectileType("XernonCrystal"), projectileBaseDamage, 5f, 0);
                                attack2ShootTimer = 10;
                            }
                        }
                    }
                }
                float speed = 0.2f;
                float playerX = P.Center.X;
                float playerY = P.Center.Y;
                bool playerAbove = P.Center.Y < npc.Center.Y;
                if (playerAbove)
                {
                    playerY = P.Center.Y + 400f;
                }
                else
                {
                    playerY = P.Center.Y - 400f;
                }
                Move(P, speed, new Vector2(playerX,playerY));
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("XernonShard"), 0, 0f, 0, i);
                }
                for (int k = 0; k < 80; k++)
                {
                    int dust = Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, mod.DustType("AncientBlue"), npc.oldVelocity.X * 0.5f, npc.oldVelocity.Y * 0.5f, 100, default(Color), 2f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = 1f + Main.rand.Next(10) * 0.1f;
                }
            }
        }
        private void Move(Player P, float speed, Vector2 target)
        {
            Vector2 desiredVelocity = target - npc.Center;
            if (Main.expertMode) speed *= 1.1f;
            if (MyWorld.awakenedMode) speed *= 1.1f;
            if (Vector2.Distance(P.Center, npc.Center) >= 2500) speed = 2;

            if (npc.velocity.X < desiredVelocity.X)
            {
                npc.velocity.X = npc.velocity.X + speed;
                if (npc.velocity.X < 0f && desiredVelocity.X > 0f)
                {
                    npc.velocity.X = npc.velocity.X + speed;
                }
            }
            else if (npc.velocity.X > desiredVelocity.X)
            {
                npc.velocity.X = npc.velocity.X - speed;
                if (npc.velocity.X > 0f && desiredVelocity.X < 0f)
                {
                    npc.velocity.X = npc.velocity.X - speed;
                }
            }
            if (npc.velocity.Y < desiredVelocity.Y)
            {
                npc.velocity.Y = npc.velocity.Y + speed;
                if (npc.velocity.Y < 0f && desiredVelocity.Y > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y + speed;
                    return;
                }
            }
            else if (npc.velocity.Y > desiredVelocity.Y)
            {
                npc.velocity.Y = npc.velocity.Y - speed;
                if (npc.velocity.Y > 0f && desiredVelocity.Y < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y - speed;
                    return;
                }
            }
            float slowSpeed = Main.expertMode ? 0.93f : 0.95f;
            if (MyWorld.awakenedMode) slowSpeed = 0.92f;
            int xSign = Math.Sign(desiredVelocity.X);
            if ((npc.velocity.X < xSign && xSign == 1) || (npc.velocity.X > xSign && xSign == -1)) npc.velocity.X *= slowSpeed;

            int ySign = Math.Sign(desiredVelocity.Y);
            if (MathHelper.Distance(target.Y, npc.Center.Y) > 1000)
            {
                if ((npc.velocity.X < ySign && ySign == 1) || (npc.velocity.X > ySign && ySign == -1)) npc.velocity.Y *= slowSpeed;
            }
        }
    }
}
