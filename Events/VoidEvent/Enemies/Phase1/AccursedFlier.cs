using ElementsAwoken.NPCs;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Events.VoidEvent.Enemies.Phase1
{
    public class AccursedFlier : ModNPC
    {
        private float aiTimer = 0;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(aiTimer);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            aiTimer = reader.ReadSingle();
        }
        private float changeLocationTimer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float vectorX
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float vectorY
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float aiState
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public override void SetDefaults()
        {
            npc.width = 28;
            npc.height = 26;

            npc.aiStyle = -1;

            npc.damage = 150;
            npc.defense = 35;
            npc.lifeMax = 1000;
            npc.knockBackResist = 0.25f;

            npc.value = Item.buyPrice(0, 0, 20, 0);
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath30;

            npc.noGravity = true;

            npc.buffImmune[24] = true;

            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = NPCsGLOBAL.ReducePierceDamage(damage,projectile);
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Accursed Flier");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 2000;
            npc.defense = 50;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 3000;
                npc.defense = 65;
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
            if (npc.frame.Y > frameHeight * 3)  // so it doesnt go over
            {
                npc.frame.Y = 0;
            }
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidEssence"), 1);
            }
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidStone"), Main.rand.Next(3, 5));
            }
        }

        public override void AI()
        {
            npc.TargetClosest(true);
            Player P = Main.player[npc.target];

            if (aiState == 0)
            {
                changeLocationTimer--;
                if ((vectorX == 0 || vectorY == 0) || changeLocationTimer <= 0 || MathHelper.Distance(vectorX, P.Center.X) > 2000 || Vector2.Distance(npc.Center, new Vector2(vectorX, vectorY)) < 30)
                {
                    float midX = (P.Center.X + npc.Center.X) / 2;
                    vectorX = midX + Main.rand.Next(-200, 200);
                    if (Main.rand.Next(6) == 0) vectorX = npc.Center.X + Main.rand.Next(-200, 200);
                    vectorY = P.Center.Y + Main.rand.Next(-100, 100);
                    changeLocationTimer = 190;
                    npc.netUpdate = true;
                }
                Vector2 targetLoc = new Vector2(vectorX, vectorY);
                Move(P, 0.015f, targetLoc);
                if (ModContent.GetInstance<Config>().debugMode)
                {
                    Dust dust = Main.dust[Dust.NewDust(targetLoc, 2, 2, 6)];
                    dust.noGravity = true;
                }

                aiTimer++;
                if (aiTimer > 240 && Vector2.Distance(P.Center, npc.Center) < 400)
                {
                    aiState = 1;
                    aiTimer = 0;
                }
                npc.direction = Math.Sign(npc.velocity.X);
            }
            else if (aiState == 1)
            {
                Vector2 targetLoc = new Vector2(P.Center.X, P.Center.Y - 120);
                Vector2 toTarget = new Vector2(targetLoc.X - npc.Center.X, targetLoc.Y - npc.Center.Y);
                toTarget.Normalize();
                npc.velocity = toTarget * 4;
                if (Vector2.Distance(targetLoc, npc.Center) < 30)
                {
                    aiState = 2;
                }
                npc.direction = Math.Sign(npc.velocity.X);
            }
            else if (aiState == 2)
            {
                npc.direction = Math.Sign(P.Center.X - npc.Center.X);

                npc.velocity *= 0.96f;
                aiTimer++;
                if (aiTimer % 6 == 0)
                {
                    float Speed = 6f;
                    float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("AccursedBreath"), 30, 0f, 0);
                }
                if (aiTimer % 20==0) Main.PlaySound(SoundID.DD2_BetsyFlameBreath, (int)npc.position.X, (int)npc.position.Y);
                if (aiTimer >= 90)
                {
                    aiState = 3;
                    aiTimer = 0;
                }
            }
            else
            {
                aiTimer++;
                Vector2 targetLoc = new Vector2(P.Center.X, P.Center.Y - 120);
                Vector2 toTarget = new Vector2(targetLoc.X - npc.Center.X, targetLoc.Y - npc.Center.Y);
                toTarget.Normalize();
                npc.velocity.X = -toTarget.X * 10;
                npc.velocity.Y = -toTarget.Y * 1;
                if (aiTimer > 20)
                {
                    aiState = 0;
                    aiTimer = 0;
                }
                npc.direction = Math.Sign(npc.velocity.X);
            }

            NPCsGLOBAL.GoThroughPlatforms(npc);
        }
        private void Move(Player P, float speed, Vector2 target)
        {
            Vector2 desiredVelocity = target - npc.Center;
            if (Main.expertMode) speed *= 1.1f;
            if (MyWorld.awakenedMode) speed *= 1.1f;

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
                npc.velocity.Y = npc.velocity.Y + speed * 0.5f;
                if (npc.velocity.Y < 0f && desiredVelocity.Y > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y + speed * 0.5f;
                    return;
                }
            }
            else if (npc.velocity.Y > desiredVelocity.Y)
            {
                npc.velocity.Y = npc.velocity.Y - speed * 0.5f;
                if (npc.velocity.Y > 0f && desiredVelocity.Y < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y - speed * 0.5f;
                    return;
                }
            }
            float slowSpeed = Main.expertMode ? 0.97f : 0.99f;
            if (MyWorld.awakenedMode) slowSpeed = 0.96f;
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