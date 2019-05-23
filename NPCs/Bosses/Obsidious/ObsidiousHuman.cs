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

namespace ElementsAwoken.NPCs.Bosses.Obsidious
{
    [AutoloadBossHead]
    public class ObsidiousHuman : ModNPC
    {
        int projectileBaseDamage = 40;

        bool reset = false;

        bool runTPAlphaChange = false;
        int tpAlphaChangeTimer = 0;
        float telePosX = 0;
        float telePosY = 0;

        float spinAI = 0f;

        public override void SetDefaults()
        {
            npc.width = 48;
            npc.height = 48;

            npc.lifeMax = 40000;
            npc.damage = 40;
            npc.defense = 30;
            npc.knockBackResist = 0f;

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;
            npc.boss = true;

            npc.scale = 1f;
            npc.HitSound = SoundID.NPCHit1;
            //npc.DeathSound = SoundID.NPCDeath1;

            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            music = MusicID.Boss1;
            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ObsidiousTheme");

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
            DisplayName.SetDefault("Obsidious");
            Main.npcFrameCount[npc.type] = 16;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 75;
            npc.lifeMax = 50000;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 75000;
                npc.damage = 90;
                npc.defense = 40;
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
            if (npc.ai[1] == 0)
            {
                if (npc.frame.Y > frameHeight * 3)
                {
                    npc.frame.Y = 0 * frameHeight;
                }
            }
            if (npc.ai[1] == 1)
            {
                if (npc.frame.Y > frameHeight * 7)
                {
                    npc.frame.Y = 4 * frameHeight;
                }
            }
            if (npc.ai[1] == 2)
            {
                if (npc.frame.Y > frameHeight * 11)
                {
                    npc.frame.Y = 8 * frameHeight;
                }
            }
            if (npc.ai[1] == 3)
            {
                if (npc.frame.Y > frameHeight * 15)
                {
                    npc.frame.Y = 12 * frameHeight;
                }
            }
            //harpy rotation
            npc.rotation = npc.velocity.X * 0.05f;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("ObsidiousTransition"));
            }
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            npc.spriteDirection = npc.direction;
            // despawn if no players
            if (!Main.player[npc.target].active || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (!Main.player[npc.target].active || Main.player[npc.target].dead)
                {
                    npc.ai[0]++;
                    npc.velocity.Y = npc.velocity.Y + 0.11f;
                    if (npc.ai[0] >= 300)
                    {
                        if (npc.life > npc.lifeMax * 0.50f)
                        {
                            Main.NewText("Easy, you do not deserve such power.", new Color(188, 58, 49));
                        }
                        else
                        {
                            Main.NewText("I tried to warn you, kid.", new Color(188, 58, 49));
                        }
                        npc.active = false;
                    }
                }
                else
                    npc.ai[0] = 0;
            }
            Lighting.AddLight(npc.Center, 0.5f, 0.5f, 0.5f);

            //teleport code- takes 20 ticks to return to full alpha
            if (runTPAlphaChange)
            {
                tpAlphaChangeTimer++;
                if (tpAlphaChangeTimer < 10)
                {
                    npc.alpha += 26;
                }
                if (tpAlphaChangeTimer == 10)
                {
                    npc.position.X = telePosX;
                    npc.position.Y = telePosY;
                }
                if (tpAlphaChangeTimer > 10)
                {
                    npc.alpha -= 26;
                    if (npc.alpha <= 0)
                    {
                        runTPAlphaChange = false;
                    }
                }
            }
            else
            {
                npc.alpha = 0;
            }
            if (npc.alpha < 0)
            {
                npc.alpha = 0;
            }
            if (npc.alpha > 255)
            {
                npc.alpha = 255;
            }
            // text and phase changes
            if (!reset)
            {
                Main.NewText("You have something I require...", new Color(188, 58, 49));
                reset = true;
            }
            if (npc.life <= npc.lifeMax * 0.75f && npc.ai[1] == 0)
            {
                npc.ai[1]++;
            }
            if (npc.life <= npc.lifeMax * 0.50f && npc.ai[1] == 1)
            {
                Main.NewText("Come on kid, hand it over and I'll spare you the pain", new Color(188, 58, 49));
                npc.ai[1]++;
            }
            if (npc.life <= npc.lifeMax * 0.25f && npc.ai[1] == 2)
            {
                npc.ai[1]++;
            }

            npc.ai[2]--; // shoot timer
            npc.ai[3]++; // ai timer
            // fire
            if (npc.ai[1] == 0f)
            {
                if (npc.ai[3] < 600f)
                {
                    Move(P, 0.1f);
                    if (npc.ai[2] <= 0)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("ObsidiousFireCrystal"), projectileBaseDamage, 0f, 0, 0f, npc.whoAmI);
                        npc.ai[2] = 45;
                    }
                }
                else
                {
                    npc.velocity.X = 0f;
                    npc.velocity.Y = 0f;
                    // shoot in circle
                    Vector2 offset = new Vector2(300, 0);
                    float rotateSpeed = 0.05f;
                    spinAI += rotateSpeed;

                    Vector2 vector = new Vector2(npc.Center.X, npc.Center.Y - 102);

                    if (npc.ai[2] <= 0)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 8);
                        Vector2 tpTarget = P.Center + offset.RotatedBy(spinAI * (Math.PI * 2 / 8));
                        Teleport(tpTarget.X, tpTarget.Y);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("ObsidiousFireCrystalStationary"), projectileBaseDamage - 10, 0f, 0, 0f, npc.whoAmI);
                        npc.ai[2] = 25;
                    }
                }
                if (npc.ai[3] > 900f)
                {
                    npc.ai[3] = 0f;
                }
            }
            // earth
            if (npc.ai[1] == 1)
            {
                Move(P, 0.1f);
                if (npc.localAI[1] == 0)
                {
                    int orbitalCount = 3;
                    for (int l = 0; l < orbitalCount; l++)
                    {
                        int distance = 360 / orbitalCount;
                        int orbital = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("ObsidiousHumanRockOrbital"), npc.damage, 0f, 0, l * distance, npc.whoAmI);
                    }
                    npc.localAI[1]++;
                }
                if (npc.ai[2] <= 0)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 69);
                    float speed = 8f;
                    float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), mod.ProjectileType("ObsidiousRockLarge"), projectileBaseDamage + 20, 0f, 0);
                    npc.ai[2] = 40;
                }
            }
            // ice
            if (npc.ai[1] == 2)
            {
                if (npc.ai[3] < 600f)
                {
                    Move(P, 0.1f);
                    if (npc.ai[2] <= 0)
                    {
                        int orbitalCount = 3;
                        for (int l = 0; l < orbitalCount; l++)
                        {
                            int distance = 360 / orbitalCount;
                            int orbital = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("ObsidiousIceCrystalSpin"), npc.damage, 0f, 0, l * distance, npc.whoAmI);
                        }
                        npc.ai[2] = 45;
                    }
                }
                else
                {
                    npc.velocity.X = 0f;
                    npc.velocity.Y = 0f;
                    if (npc.ai[3] == 600f)
                    {
                        spinAI = 0f;
                        int crystalCount = 2;
                        for (int l = 0; l < crystalCount; l++)
                        {
                            int distance = 360 / crystalCount;
                            int orbital = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("ObsidiousIceBeamCrystal"), 0, 0f, 0, l * distance, npc.whoAmI);
                        }
                    }
                    Vector2 offset = new Vector2(400, 0);
                    spinAI += 0.015f;

                    if (npc.ai[2] <= 0)
                    {
                        int numProj = 2;
                        for (int i = 0; i < numProj; i++)
                        {
                            int damage = npc.ai[3] <= 660 ? 0 : projectileBaseDamage;
                            float projOffset = 360 / numProj;
                            Vector2 shootTarget1 = npc.Center + offset.RotatedBy(spinAI + (projOffset * i) * (Math.PI * 2 / 8));
                            float rotation = (float)Math.Atan2(npc.Center.Y - shootTarget1.Y, npc.Center.X - shootTarget1.X);
                            int proj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * 10f) * -1), (float)((Math.Sin(rotation) * 10f) * -1), mod.ProjectileType("ObsidiousIceBeam"), projectileBaseDamage, 0f, 0);
                            Main.projectile[proj].timeLeft = (int)((npc.ai[3] - 600));
                        }
                        npc.ai[2] = 3;
                    }
                }
                if (npc.ai[3] > 900f)
                {
                    npc.ai[3] = 0f;
                }
            }
            // purpleness
            if (npc.ai[1] == 3)
            {
                npc.velocity.X = 0f;
                npc.velocity.Y = 0f;
                if (npc.localAI[2] == 0)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("ObsidiousRitual"), npc.damage, 0f, 0, 0f, npc.whoAmI);
                    npc.localAI[2]++;
                }
                npc.ai[2]--;
                if (npc.life <= npc.lifeMax * 0.15f)
                {
                    npc.ai[2]--;
                }
                if (npc.life <= npc.lifeMax * 0.05f)
                {
                    npc.ai[2]--;
                }
                if (npc.ai[2] <= 0)
                {
                    int distance = Main.rand.Next(250, 400);
                    int choice = Main.rand.Next(4);
                    if (choice == 0)
                    {
                        Teleport(Main.player[npc.target].position.X + distance, Main.player[npc.target].position.Y - distance);
                    }
                    if (choice == 1)
                    {
                        Teleport(Main.player[npc.target].position.X - distance, Main.player[npc.target].position.Y - distance);
                    }
                    if (choice == 2)
                    {
                        Teleport(Main.player[npc.target].position.X + distance, Main.player[npc.target].position.Y + distance);
                    }
                    if (choice == 3)
                    {
                        Teleport(Main.player[npc.target].position.X - distance, Main.player[npc.target].position.Y + distance);
                    }
                    npc.ai[2] = Main.rand.Next(140, 220);
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 8);
                    float speed = 8f;
                    float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), mod.ProjectileType("ObsidiousHomingBall"), projectileBaseDamage - 10, 0f, 0);
                }
            }
        }

        private void Move(Player P, float speed)
        {
            int maxDist = 1000;
            if (Vector2.Distance(P.Center, npc.Center) >= maxDist)
            {
                float moveSpeed = 8f;
                Vector2 toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
                toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
                toTarget.Normalize();
                npc.velocity = toTarget * moveSpeed;
            }
            else
            {
                npc.spriteDirection = npc.direction;

                if (Main.expertMode)
                {
                    speed += 0.05f;
                }
                Vector2 vector75 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                float playerX = P.position.X + (float)(P.width / 2) - vector75.X;
                float playerY = P.position.Y + (float)(P.height / 2) - vector75.Y;
                if (npc.velocity.X < playerX)
                {
                    npc.velocity.X = npc.velocity.X + speed;
                    // if (npc.velocity.X < 0f && playerY > 0f)
                    {
                        npc.velocity.X = npc.velocity.X + speed;
                    }
                }
                else if (npc.velocity.X > playerX)
                {
                    npc.velocity.X = npc.velocity.X - speed;
                    //if (npc.velocity.X > 0f && playerX < 0f) // this breaks it for some reason :(
                    {
                        npc.velocity.X = npc.velocity.X - speed;
                    }
                }
                if (npc.velocity.Y < playerY)
                {
                    npc.velocity.Y = npc.velocity.Y + speed;
                    if (npc.velocity.Y < 0f && playerY > 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y + speed;
                        return;
                    }
                }
                else if (npc.velocity.Y > playerY)
                {
                    npc.velocity.Y = npc.velocity.Y - speed;
                    if (npc.velocity.Y > 0f && playerY < 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y - speed;
                        return;
                    }
                }
            }
        }

        private void Teleport(float posX, float posY)
        {
            runTPAlphaChange = true;
            tpAlphaChangeTimer = 0;
            telePosX = posX;
            telePosY = posY;
        }

        public override bool CheckActive()
        {
            return false;
        }
    }
}
