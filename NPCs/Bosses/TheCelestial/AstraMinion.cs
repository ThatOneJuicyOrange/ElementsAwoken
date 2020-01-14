using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.TheCelestial
{
    public class AstraMinion : ModNPC
    {
        public float vectorX = 0f;
        public float vectorY = 0f;
        private float dashTimer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float changePosTimer
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float moveSpeed
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 40;

            npc.aiStyle = -1;

            npc.lifeMax = NPC.downedMoonlord ? 7500 : 1500;
            npc.damage = NPC.downedMoonlord ? 75 : 40;
            npc.defense = NPC.downedMoonlord ? 50 : 30;
            npc.knockBackResist = 0f;

            npc.npcSlots = 0f;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.buffImmune[24] = true;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astra's Gazer");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = NPC.downedMoonlord ? 12500 : 3000;
            npc.damage = NPC.downedMoonlord ? 125 : 75;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = NPC.downedMoonlord ? 17500 : 7000;
                npc.damage = NPC.downedMoonlord ? 140 : 90;
                npc.defense = NPC.downedMoonlord ? 60 : 35;
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
            if (npc.frame.Y >= frameHeight * 4)  // so it doesnt go over
            {
                npc.frame.Y = 0;
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void AI()
        {
            npc.TargetClosest(true);

            Player P = Main.player[npc.target];
            Vector2 direction = P.Center - npc.Center;
            if (direction.X > 0f)
            {
                npc.spriteDirection = 1;
                npc.rotation = direction.ToRotation();
            }
            if (direction.X < 0f)
            {
                npc.spriteDirection = -1;
                npc.rotation = direction.ToRotation() - 3.14f;
            }
            if (npc.ai[3] == 0)
            {
                FindLoc();
                npc.ai[3]++;
                npc.netUpdate = true;
            }
            changePosTimer--;
            if (changePosTimer > 0)
            {
                Vector2 target = P.Center + new Vector2 (vectorX,vectorY);
                Move(P, 0.02f, target);
            }
            else
            {
                dashTimer++;
                // dash
                float speed = 10f;
                Vector2 target = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
                float num = (float)Math.Sqrt(target.X * target.X + target.Y * target.Y);
                num = speed / num;
                npc.velocity.X = target.X * num;
                npc.velocity.Y = target.Y * num;

                if (dashTimer >= 20 || Vector2.Distance(P.Center, npc.Center) < 10)
                {
                    if (MyWorld.awakenedMode) changePosTimer = 240;
                    else changePosTimer = 450;
                }

                FindLoc();
            }
            if (!ModContent.GetInstance<Config>().lowDust)
            {
                for (int i = 0; i < 2; i++)
                {
                    int dustType = 6;
                    switch (Main.rand.Next(4))
                    {
                        case 0:
                            dustType = 6;
                            break;
                        case 1:
                            dustType = 197;
                            break;
                        case 2:
                            dustType = 229;
                            break;
                        case 3:
                            dustType = 242;
                            break;
                        default: break;
                    }
                    int dust = Dust.NewDust(npc.position, npc.width, npc.height, dustType);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 0.1f;
                }
            }
            if (!NPC.AnyNPCs(mod.NPCType("Astra"))) npc.active = false;
        }
        private void Move(Player P, float speed, Vector2 target)
        {
            Vector2 desiredVelocity = target - npc.Center;
            if (Main.expertMode) speed *= 1.1f;
            if (MyWorld.awakenedMode) speed *= 1.1f;
            if (NPC.downedMoonlord) speed *= 1.25f;
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
        private void FindLoc()
        {
            int dist = Main.rand.Next(150, 500);
            double angle = Main.rand.NextDouble() * 2d * Math.PI;
            Vector2 offset = new Vector2((float)Math.Sin(angle) * dist, (float)Math.Cos(angle) * dist);
            vectorX = offset.X;
            vectorY = offset.Y;
            moveSpeed = Main.rand.NextFloat(0.15f, 0.25f);
            npc.netUpdate = true;
        }
    }
}