using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.TheGuardian
{
    [AutoloadBossHead]
    public class TheGuardianFly : ModNPC
    {
        private int projectileBaseDamage = 65;
        private int moveAI = 1;
        private float shootTimer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float minionTimer
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float aiTimer
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float shootTimer2
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(moveAI);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            moveAI = reader.ReadInt32();
        }
        public override void SetDefaults()
        {
            npc.width = 252;
            npc.height = 152;

            npc.aiStyle = -1;

            npc.lifeMax = 115000;
            npc.damage = 120;
            npc.defense = 45;
            npc.knockBackResist = 0f;

            npc.scale = 1.2f;

            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;

            npc.HitSound = SoundID.NPCHit2;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.value = Item.buyPrice(0, 55, 0, 0);

            music = MusicID.Boss4;
            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/GuardianTheme");

            for (int num2 = 0; num2 < 206; num2++)
            {
                npc.buffImmune[num2] = true;
            }
            npc.buffImmune[mod.BuffType("IceBound")] = true;
            npc.buffImmune[mod.BuffType("EndlessTears")] = true;

            bossBag = mod.ItemType("GuardianBag");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Guardian");
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 200000;
            npc.damage = 150;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 250000;
                npc.damage = 200;
                npc.defense = 60;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1;
            if (npc.frameCounter > 5)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.frame.Y > frameHeight * 5)
            {
                npc.frame.Y = 0;
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TheGuardianTrophy"));
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TheGuardianMask"));
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                int choice = Main.rand.Next(3);
                if (choice == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Godslayer"));
                }
                if (choice == 1)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("InfernoStorm"));
                }
                if (choice == 2)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TemplesWrath"));
                }
            }
            MyWorld.downedGuardian = true;
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
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
                    npc.localAI[0]++;
                }
            }
            if (Main.dayTime)
            {
                npc.localAI[0]++;
            }
            if (npc.localAI[0] >= 300)
            {
                npc.active = false;
            }
            #endregion
            shootTimer--; 
            minionTimer--;
            aiTimer++;
            shootTimer2--; // portals & infernoballs
            if (npc.life >= npc.lifeMax * 0.5f)
            {
                if (aiTimer > 1700f)
                {
                    aiTimer = 0f;
                }
            }
            if (npc.life <= npc.lifeMax * 0.5f)
            {
                if (aiTimer > 2500f)
                {
                    aiTimer = 0f;
                }
            }
            // minions
            if (minionTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("GuardianProbe"));
                minionTimer = Main.rand.Next(300, 1200);
                npc.netUpdate = true;
            }

            //attack 1- flys left and right of the player and leaves shooting orbs 
            if (aiTimer <= 500)
            {
                Vector2 target = P.Center + new Vector2(400 * moveAI, -300);
                if (Vector2.Distance(target,npc.Center) <= 20)
                {
                    moveAI *= -1;
                }
                Move(P, 0.2f, target);

                if (Main.netMode != NetmodeID.MultiplayerClient && shootTimer <= 25 && shootTimer % 5 == 0)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("GuardianOrb"), projectileBaseDamage, 0f, Main.myPlayer);
                }
                if (shootTimer <= 0) shootTimer = 75;
            }

            //attack 2- flys above the player and drops fire/lasers
            if (aiTimer >= 500 && aiTimer <= 800)
            {
                Move(P, 0.09f, P.Center - new Vector2(0, 400), 1f);
                if (npc.life >= npc.lifeMax * 0.5f)
                {
                    if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        //Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 1);
                        int proj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.NextFloat(-2, 2), 8f, mod.ProjectileType("GuardianFire"), projectileBaseDamage, 0f, Main.myPlayer);
                        Projectile fire = Main.projectile[proj];
                        fire.timeLeft = 120;
                        shootTimer = 12;
                    }
                }
                if (npc.life <= npc.lifeMax * 0.5f)
                {
                    if (shootTimer <= 30 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int maxdusts = 10;
                        for (int i = 0; i < maxdusts; i++)
                        {
                            float dustDistance = 100;
                            float dustSpeed = 15;
                            Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                            Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                            Dust vortex = Dust.NewDustPerfect(new Vector2(npc.Center.X, npc.Center.Y + 40) + offset, 6, velocity, 0, default(Color), 1.5f);
                            vortex.noGravity = true;
                        }
                    }
                    if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 122);
                        for (int i = 0; i < 6; i++)
                        {
                            int proj = Projectile.NewProjectile(npc.position.X + 140 + 6.6f * i, npc.Bottom.Y, 0f, 9f, mod.ProjectileType("GuardianBeam"), projectileBaseDamage, 0f, Main.myPlayer);
                            Projectile Beam = Main.projectile[proj];
                            Beam.timeLeft = 75;
                        }
                        shootTimer = 50;
                    }

                }
            }
            // so the guardian wont instantly shoot the player in the face 
            if (aiTimer == 800)
            {
                shootTimer = 100;
            }
            //attack 3 - throws multiple sticky grenades at the player
            if (aiTimer >= 800 && aiTimer <= 1200)
            {
                Move(P, 0.1f, P.Center - new Vector2(0, 300));

                if (shootTimer <= 0)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                    Bolts(P, 18f, projectileBaseDamage - 20, Main.rand.Next(4, 6), 13);
                    shootTimer = Main.rand.Next(30, 80);
                }
            }
            //attack 4 - fireball cluster
            if (aiTimer >= 1200 && aiTimer <= 1700)
            {
                npc.velocity.X = 0f;
                npc.velocity.Y = 0f;
                if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                    float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                    for (int i = 0; i < 6; i++)
                    {
                        float speed = 16 + Main.rand.NextFloat(-2, 2);
                        Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1)).RotatedByRandom(MathHelper.ToRadians(5));
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("GuardianFireball"), projectileBaseDamage - 10, 0f, 0);
                    }
                    shootTimer = 50;
                }
            }

            //attack 5 - shoots a fast exploding bolt at the player 
            if (npc.life <= npc.lifeMax * 0.5f && aiTimer >= 1700f && aiTimer <= 2000f)
            {
                Move(P, 0.1f, P.Center - new Vector2(0, 300));
                if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    float Speed = 20f;
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                    float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("GuardianStrike"), projectileBaseDamage + 20, 0f, Main.myPlayer, 0, 1);
                    shootTimer = 100;
                }
            }

            //attack 6 - releases shots in a circle
            if (npc.life <= npc.lifeMax * 0.5f && aiTimer >= 2000f && aiTimer <= 2500f)
            {
                npc.velocity.X = 0f;
                npc.velocity.Y = 0f;
                if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 perturbedSpeed = new Vector2(4f, 4f).RotatedByRandom(MathHelper.ToRadians(360));
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("GuardianShot"), projectileBaseDamage, 0f, Main.myPlayer);
                    shootTimer = 5;
                }
            }

            // portals and infernoballs
            if (npc.life <= npc.lifeMax * 0.5f)
            {
                // infernoballs
                if (shootTimer2 == 200 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int direction = 1;
                    switch (Main.rand.Next(2))
                    {
                        case 0:
                            direction = 1;
                            break;
                        case 1:
                            direction = -1;
                            break;
                        default: break;
                    }
                    float speed = 12f;
                    int damage = projectileBaseDamage - 20;
                    for (int i = 0; i < 3; i++)
                    {
                        Projectile.NewProjectile(P.Center.X - 1300 * direction + i * 50, P.Center.Y - i * 200, speed * direction, 0f, mod.ProjectileType("GuardianInfernoball"), damage, 0f, Main.myPlayer);
                        Projectile.NewProjectile(P.Center.X - 1300 * direction + i * 50, P.Center.Y + i * 200, speed * direction, 0f, mod.ProjectileType("GuardianInfernoball"), damage, 0f, Main.myPlayer);
                    }
                }
                // portal
                if (shootTimer2 <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int minDist = 400;
                    int vectorX = Main.rand.Next(-800, 800);
                    int vectorY = Main.rand.Next(-800, 800);
                    if (vectorX < minDist && vectorX > 0)
                    {
                        vectorX = minDist;
                    }
                    else if (vectorX > -minDist && vectorX < 0)
                    {
                        vectorX = -minDist;
                    }
                    if (vectorY < minDist && vectorY > 0)
                    {
                        vectorY = minDist;
                    }
                    else if (vectorY > -minDist && vectorX < 0)
                    {
                        vectorY = -minDist;
                    }
                    Projectile.NewProjectile(P.Center.X + vectorX, P.Center.Y + vectorY, 0f, 0f, mod.ProjectileType("GuardianPortal"), projectileBaseDamage * 2, 0f, Main.myPlayer);
                    shootTimer2 = 400;
                }
            }

            // strikes coming up from underneath
            if (npc.life <= npc.lifeMax * 0.2f)
            {
                if (Main.rand.Next(14) == 0)
                {
                    float posX = P.Center.X + Main.rand.Next(5000) - 3000;
                    float posY = P.Center.Y + 1000;
                    Projectile.NewProjectile(posX, posY, 0f, -10f, mod.ProjectileType("GuardianStrike"), projectileBaseDamage, 0f, Main.myPlayer);
                }
            }

            // create circle
            if (npc.life <= npc.lifeMax * 0.2f && npc.localAI[1] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                npc.localAI[1]++;
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("GuardianCircle"), npc.damage, 0f, Main.myPlayer, 0, npc.whoAmI);
            }
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = mod.GetTexture("NPCs/Bosses/TheGuardian/" + GetType().Name + "_Glow");
            Rectangle frame = new Rectangle(0, npc.frame.Y, texture.Width, texture.Height / Main.npcFrameCount[npc.type]);
            Vector2 origin = new Vector2(texture.Width * 0.5f, (texture.Height / Main.npcFrameCount[npc.type]) * 0.5f);
            SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY) + new Vector2(0, 11), frame, new Color(255, 255, 255, 0), npc.rotation, origin, npc.scale, effects, 0.0f);
        }
        private void Move(Player P, float speed, Vector2 target, float slowScale = 0.99f)
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
            float slowSpeed = Main.expertMode ? slowScale * 0.97f : slowScale;
            if (MyWorld.awakenedMode) slowSpeed = slowScale * 0.95f;
            int xSign = Math.Sign(desiredVelocity.X);
            if ((npc.velocity.X < xSign && xSign == 1) || (npc.velocity.X > xSign && xSign == -1)) npc.velocity.X *= slowSpeed;

            int ySign = Math.Sign(desiredVelocity.Y);
            if (MathHelper.Distance(target.Y, npc.Center.Y) > 1000)
            {
                if ((npc.velocity.X < ySign && ySign == 1) || (npc.velocity.X > ySign && ySign == -1)) npc.velocity.Y *= slowSpeed;
            }
        }

        private void Bolts(Player P, float speed, int damage, int numberProj, int angle)
        {
            float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
            for (int i = 0; i < numberProj; i++)
            {
                Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1)).RotatedByRandom(MathHelper.ToRadians(angle));
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("GuardianStickyBolt"), damage, 0f, 0);
            }
        }

        public override bool CheckActive()
        {
            return false;
        }
    }
}
