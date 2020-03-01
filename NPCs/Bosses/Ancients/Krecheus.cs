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
    public class Krecheus : ModNPC
    {
        public float originX = 0;
        public float originY = 0;
        public int attackType = 0;
        public bool playerUp = false;
        public bool playerLeft = false;
        public float shootTimer = 0;

        public int projectileBaseDamage = 100;

        public float[] dash = new float[3];
        public float spinTimer = 0f;
        public float spinDetectDelay = 0f;
        public Vector2 spinOrigin = new Vector2(0, 0);
        public override void SetDefaults()
        {
            npc.width = 88;
            npc.height = 88;

            npc.aiStyle = -1;

            npc.lifeMax = 250000;
            npc.damage = 150;
            npc.defense = 80;
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
            DisplayName.SetDefault("Krecheus");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 300000;
            npc.damage = 175;
            npc.defense = 90;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 450000;
                npc.damage = 200;
                npc.defense = 95;
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
            Lighting.AddLight(npc.Center, 1.5f, 0f, 0.5f);

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
                    npc.Center = P.Center;
                    npc.netUpdate = true;
                }
                if (npc.ai[0] < 60)
                {
                    npc.alpha = 255;
                }
                else
                {
                    npc.alpha = 0;
                    Vector2 target = new Vector2(originX - 75, originY - 300);
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
                if (npc.localAI[2] == 0)
                {
                    int orbitalCount = 6;
                    for (int l = 0; l < orbitalCount; l++)
                    {
                        int distance = 360 / orbitalCount;
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("KrecheusBlade"), npc.damage / 2, 0f, Main.myPlayer, l * distance, npc.whoAmI);
                    }
                    npc.localAI[2]++;
                }
                npc.ai[1]++;
                if (npc.ai[1] < 900)
                {
                    dash[0]--;
                    if (dash[0] <= 0)
                    {
                        dash[1]--;
                        if (dash[1] >= 0)
                        {
                            float speed = 10f;
                            float num25 = P.Center.X - npc.Center.X;
                            float num26 = P.Center.Y - npc.Center.Y;
                            float num27 = (float)Math.Sqrt(num25 * num25 + num26 * num26);
                            num27 = speed / num27;
                            npc.velocity.X = num25 * num27;
                            npc.velocity.Y = num26 * num27;
                        }
                        else
                        {
                            dash[0] = 45;
                            dash[1] = 30;
                        }
                        if (Vector2.Distance(P.Center, npc.Center) < 30)
                        {
                            dash[0] = 45;
                            dash[1] = 30;
                        }
                    }
                    else
                    {
                        npc.velocity *= 0.94f;
                    }
                }
                else
                {
                    if (npc.ai[1] == 900)
                    {
                        attackType =  npc.life < npc.lifeMax / 3 ? Main.rand.Next(5) : Main.rand.Next(4);

                        playerUp = P.velocity.Y < 0;
                        playerLeft = P.Center.X < npc.Center.X;

                        originX = P.Center.X;
                        originY = P.Center.Y;

                        spinDetectDelay = 30;
                        npc.ai[2] = 0;
                        npc.ai[3] = 0;
                    }
                    if (attackType == 0)
                    {
                        if (npc.ai[2] == 0)
                        {
                            float targetX = playerLeft ? P.Center.X + 300 : P.Center.X - 300;
                            float targetY = playerUp ? P.Center.Y - 400 : P.Center.Y + 400;
                            Vector2 target = new Vector2(targetX, targetY);

                            Vector2 toTarget = new Vector2(target.X - npc.Center.X, target.Y - npc.Center.Y);
                            toTarget.Normalize();
                            if (Vector2.Distance(target, npc.Center) > 20)
                            {
                                npc.velocity = toTarget * 16;
                            }
                            else
                            {
                                npc.ai[2] = 1;
                            }
                        }
                        else
                        {
                            npc.velocity.X = 0;
                            npc.velocity.Y = playerUp ? 12 : -12;

                            npc.ai[3]++;
                            shootTimer--;
                            if (shootTimer <= 0)
                            {
                                float speed = 8f;
                                if (playerLeft)
                                {
                                    speed = -8f;
                                }
                                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 21);
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, speed, 0f, mod.ProjectileType("KrecheusBolt"), projectileBaseDamage, 0f, Main.myPlayer);
                                shootTimer = 5;
                            }
                            if (npc.ai[3] >= 60)
                            {
                                npc.ai[1] = 0;
                                npc.ai[2] = 0;
                                npc.ai[3] = 0;
                            }
                        }
                    }
                    else if (attackType == 1)
                    {
                        if (npc.ai[3] > 0)
                        {
                            spinTimer += 2f; // speed
                        }
                        Vector2 center = new Vector2(originX, originY);

                        int distance = 400;
                        double rad = spinTimer * (Math.PI / 180); // angle to radians
                        float spinX = originX - (int)(Math.Cos(rad) * distance) - npc.width / 2;
                        float spinY = originY - (int)(Math.Sin(rad) * distance) - npc.height / 2;
                        Vector2 target = new Vector2(spinX, spinY);

                        if (npc.ai[2] == 0)
                        {
                            spinOrigin = target;
                            npc.ai[2]++;
                        }

                        if (npc.ai[3] == 0)
                        {
                            Vector2 toTarget = new Vector2(target.X - npc.Center.X, target.Y - npc.Center.Y);
                            toTarget.Normalize();
                            if (Vector2.Distance(target, npc.Center) > 20)
                            {
                                npc.velocity = toTarget * 16;
                            }
                            else
                            {
                                npc.ai[3] = 1;
                            }
                        }
                        else
                        {
                            npc.velocity *= 0f;

                            npc.position.X = spinX;
                            npc.position.Y = spinY;

                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("KrecheusCircle"), projectileBaseDamage, 0f, Main.myPlayer);
                            spinDetectDelay--;
                            if (spinDetectDelay <= 0)
                            {
                                if (Vector2.Distance(spinOrigin, npc.Center) < 75)
                                {
                                    npc.ai[1] = 0;
                                    npc.ai[2] = 0;
                                    npc.ai[3] = 0;
                                }
                            }
                        }
                    }
                    else if (attackType == 2)
                    {
                        if (npc.ai[2] == 0)
                        {
                            float targetX = playerLeft ? P.Center.X + 300 : P.Center.X - 300;
                            float targetY = P.Center.Y;
                            Vector2 target = new Vector2(targetX, targetY);

                            Vector2 toTarget = new Vector2(target.X - npc.Center.X, target.Y - npc.Center.Y);
                            toTarget.Normalize();
                            if (Vector2.Distance(target, npc.Center) > 20)
                            {
                                npc.velocity = toTarget * 16;
                            }
                            else
                            {
                                npc.ai[2] = 1;
                            }
                        }
                        else
                        {
                            if (npc.ai[3] == 0)
                            {
                                int numDusts = 40;
                                for (int i = 0; i < numDusts; i++)
                                {
                                    Vector2 position = (Vector2.Normalize(new Vector2(2,2)) * new Vector2((float)npc.width / 2f, (float)npc.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + npc.Center;
                                    Vector2 velocity = position - npc.Center;
                                    int dust = Dust.NewDust(position + velocity, 0, 0, mod.DustType("AncientRed"), velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                                    Main.dust[dust].noGravity = true;
                                    Main.dust[dust].noLight = true;
                                    Main.dust[dust].velocity = Vector2.Normalize(velocity) * 4f;
                                }
                            }
                            npc.ai[3]++;
                            if (npc.ai[3] > 20)
                            {
                                npc.velocity.X = playerLeft ? -14 : 14;
                                npc.velocity.Y = 0;

                                npc.direction = Math.Sign(npc.velocity.X);
                                npc.spriteDirection = Math.Sign(npc.velocity.X);

                                Vector2 position = npc.Center + Vector2.Normalize(npc.velocity) * 10f;
                                Dust dust = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType("AncientRed"), 0f, 0f, 0, default(Color), 1.5f)];
                                dust.position = position;
                                dust.velocity = npc.velocity.RotatedBy(1.5707963705062866, default(Vector2)) * 0.33f + npc.velocity / 4f;
                                dust.velocity.X -= npc.velocity.X / 10f;
                                dust.position += npc.velocity.RotatedBy(1.5707963705062866, default(Vector2));
                                dust.fadeIn = 0.5f;
                                dust.noGravity = true;
                                Dust dust1 = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType("AncientRed"), 0f, 0f, 0, default(Color), 1.5f)];
                                dust1.position = position;
                                dust1.velocity = npc.velocity.RotatedBy(-1.5707963705062866, default(Vector2)) * 0.33f + npc.velocity / 4f;
                                dust1.velocity.X -= npc.velocity.X / 10f;
                                dust1.position += npc.velocity.RotatedBy(-1.5707963705062866, default(Vector2));
                                dust1.fadeIn = 0.5f;
                                dust1.noGravity = true;
                            }
                            else
                            {
                                npc.velocity *= 0f;
                            }
                            if (npc.ai[3] >= 120)
                            {
                                npc.ai[1] = 0;
                                npc.ai[2] = 0;
                                npc.ai[3] = 0;
                            }
                        }
                    }
                    else if (attackType == 3)
                    {
                        npc.ai[2]++;
                        if (npc.ai[2] > 200)
                        {
                            npc.ai[2] = 0;
                            npc.ai[1] = 0;
                        }
                        npc.velocity *= 0;
                        if (Main.rand.Next(30) == 0)
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                float posX = i == 1 ? P.Center.X + 1000 : P.Center.X - 1000;
                                float posY = P.Center.Y + Main.rand.Next(-700, 700);
                                Projectile.NewProjectile(posX, posY, i % 2 == 0 ? 16f : -16f, 0f, mod.ProjectileType("KrecheusSide"), projectileBaseDamage, 0f);
                            }
                        }

                        int numDusts = 2;
                        for (int i = 0; i < numDusts; i++)
                        {
                            float dustDistance = Main.rand.Next(40, 80);
                            float dustSpeed = 4;
                            Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                            Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                            Dust vortex = Dust.NewDustPerfect(new Vector2(npc.Center.X, npc.Center.Y - 30) + offset, mod.DustType("AncientRed"), velocity, 0, default(Color), 1.5f);
                            vortex.noGravity = true;
                            vortex.fadeIn = Main.rand.NextFloat(0.3f, 0.6f);
                        }
                    }
                    else if (attackType == 4)
                    {
                        npc.ai[2]++;
                        if (npc.ai[2] > 120)
                        {
                            npc.ai[2] = 0;
                            npc.ai[1] = 0;

                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("KrecheusPortal"), projectileBaseDamage, 0f);
                        }
                        else
                        {
                            int numDusts = 2;
                            for (int i = 0; i < numDusts; i++)
                            {
                                float dustDistance = Main.rand.Next(40, 80);
                                float dustSpeed = 4;
                                Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                                Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                                Dust vortex = Dust.NewDustPerfect(new Vector2(npc.Center.X, npc.Center.Y - 30) + offset, mod.DustType("AncientRed"), velocity, 0, default(Color), 1.5f);
                                vortex.noGravity = true;
                                vortex.fadeIn = Main.rand.NextFloat(0.3f, 0.6f);
                            }
                        }
                    }
                }
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("KrecheusShard"), 0, 0f, Main.myPlayer, i);
                }
                for (int k = 0; k < 80; k++)
                {
                    int dust = Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, mod.DustType("AncientRed"), npc.oldVelocity.X * 0.5f, npc.oldVelocity.Y * 0.5f, 100, default(Color), 2f);
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
