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

namespace ElementsAwoken.NPCs.Bosses.CosmicObserver
{
    [AutoloadBossHead]
    public class CosmicObserver : ModNPC
    {
        private int projectileBaseDamage = 20;


        private const float beamChargeMax = 300f;

        private int targetLaserFrame = 0;
        private float desiredX = 0;
        private float desiredY = 0;
        private int moveAI = 1;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(desiredX);
            writer.Write(desiredY);
            writer.Write(targetLaserFrame);
            writer.Write(moveAI);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            desiredX = reader.ReadInt32();
            desiredY = reader.ReadInt32();
            targetLaserFrame = reader.ReadInt32();
            moveAI = reader.ReadInt32();
        }
        private float shootTimer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float aiTimer
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float storeRot
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float beamCharge
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public override void SetDefaults()
        {
            npc.width = 104;
            npc.height = 104;

            npc.aiStyle = -1;

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

            // used for the hands
            NPCID.Sets.TrailCacheLength[npc.type] = 15;
            NPCID.Sets.TrailingMode[npc.type] = 1;

            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("A Cosmic Observer");
            Main.npcFrameCount[npc.type] = 4;
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
            if (npc.life >= npc.lifeMax * 0.8f)
            {
                npc.frame.Y = 0;
            }
            else if (npc.life >= npc.lifeMax * 0.65f)
            {
                npc.frame.Y = 1 * frameHeight;
            }
            else if (npc.life >= npc.lifeMax * 0.45f)
            {
                npc.frame.Y = 2 * frameHeight;
            }
            else if (npc.life >= npc.lifeMax * 0.3f && MyWorld.awakenedMode)
            {
                npc.frame.Y = 3 * frameHeight;
            }
            if (aiTimer % 9 == 0)
            {
                targetLaserFrame++;
                if (targetLaserFrame > 3)
                {
                    targetLaserFrame = 0;
                }
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override void NPCLoot()
        {
            int numShards = 0;
            for (int i = 0; i < Main.player.Length; i++)
            {
                if (Main.player[i].active)
                {
                    if (MyWorld.awakenedMode) numShards += Main.rand.Next(16, 26);
                    else if (Main.expertMode) numShards = Main.rand.Next(12, 19); 
                    else numShards += Main.rand.Next(8, 12);
                }
            }
            
            if (MyWorld.awakenedMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CosmicGlass"), 1);
            }
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CosmicShard"), numShards); //Item spawn
            MyWorld.downedCosmicObserver = true;
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/CosmicObserver" + i), npc.scale);
                }
            }
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (aiTimer >= 1400)
            {
                Texture2D texture = ModContent.GetTexture("ElementsAwoken/Projectiles/NPCProj/CosmicObserver/ObserverTarget");
                Texture2D backTexture = ModContent.GetTexture("ElementsAwoken/Projectiles/NPCProj/CosmicObserver/ObserverTarget1");

                Vector2 position = npc.Center;
                Player P = Main.player[npc.target];
                Vector2 mountedCenter = P.MountedCenter;
                int height = 34;
                Rectangle? sourceRectangle = new Rectangle(0, height * targetLaserFrame, texture.Width, height);
                Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)height * 0.5f);
                float num1 = (float)height;
                Vector2 vector2_4 = mountedCenter - position;
                float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
                bool flag = true;
                if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                    flag = false;
                if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
                    flag = false;
                while (flag)
                {
                    if ((double)vector2_4.Length() < (double)num1 + 1.0)
                    {
                        flag = false;
                    }
                    else
                    {
                        Vector2 vector2_1 = vector2_4;
                        vector2_1.Normalize();
                        position += vector2_1 * num1;
                        vector2_4 = mountedCenter - position;
                        Tile t = Main.tile[position.ToTileCoordinates().X, position.ToTileCoordinates().Y];
                        if (Main.tileSolid[t.type] && t.active())
                        {
                            return;
                        }
                        Main.spriteBatch.Draw(backTexture, position - Main.screenPosition, new Rectangle?(), Color.White * (beamCharge / beamChargeMax), rotation, origin, 1f, SpriteEffects.None, 0.0f);
                        Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, Color.White, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                    }
                }
            }
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
            shootTimer--;
            aiTimer++;
            if (MyWorld.awakenedMode && npc.life < npc.lifeMax * 0.25f)
            {
                aiTimer++;
            }
            if (MyWorld.awakenedMode)
            {
                if (npc.life > npc.lifeMax * 0.45f)
                {
                    if (aiTimer >= 1200)
                    {
                        aiTimer = 0;
                    }
                }
                else if (npc.life > npc.lifeMax * 0.3f)
                {
                    if (aiTimer >= 1400)
                    {
                        aiTimer = 0;
                    }
                }
                else
                {
                    // set in the beam
                }
            }
            else if (Main.expertMode)
            {
                if (npc.life > npc.lifeMax / 2)
                {
                    if (aiTimer >= 1200)
                    {
                        aiTimer = 0;
                    }
                }
                else
                {
                    if (aiTimer >= 1400)
                    {
                        aiTimer = 0;
                    }
                }
            }
            else
            {
                if (aiTimer >= 1200)
                {
                    aiTimer = 0;
                }
            }

            if (aiTimer <= 600)
            {
                Vector2 floatCenter = new Vector2(desiredX, desiredY);
                 if (Vector2.Distance(P.Center, npc.Center) > 700)
                {
                    Vector2 toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
                    toTarget.Normalize();
                    npc.velocity = toTarget * 5f;
                }
                else if (Vector2.Distance(P.Center, npc.Center) > 500)
                {
                    Move(P, 0.02f, P.Center);
                }
                else
                {
                    float moveSpeed = 0.2f;
                    Vector2 vector149 = floatCenter - npc.Center;
                    if (vector149.Length() < 60f)
                    {
                        moveSpeed = 0.12f;
                    }
                    if (vector149.Length() < 40f)
                    {
                        moveSpeed = 0.06f;
                    }
                    if (vector149.Length() > 20f)
                    {
                        if (Math.Abs(floatCenter.X - npc.Center.X) > 20f)
                        {
                            npc.velocity.X = npc.velocity.X + moveSpeed * (float)Math.Sign(floatCenter.X - npc.Center.X);
                        }
                        if (Math.Abs(floatCenter.Y - npc.Center.Y) > 10f)
                        {
                            npc.velocity.Y = npc.velocity.Y + moveSpeed * (float)Math.Sign(floatCenter.Y - npc.Center.Y);
                        }
                    }
                    else if (npc.velocity.Length() > 2f)
                    {
                        npc.velocity *= 0.96f;
                    }
                    if (Math.Abs(npc.velocity.Y) < 1f)
                    {
                        npc.velocity.Y = npc.velocity.Y - 0.1f;
                    }
                    float maxSpeed = 15f;
                    if (npc.velocity.Length() > maxSpeed)
                    {
                        npc.velocity = Vector2.Normalize(npc.velocity) * maxSpeed;
                    }
                }
                if (Vector2.Distance(P.Center, npc.Center) > 300)
                {
                    if (Vector2.Distance(floatCenter, npc.Center) > 150) 
                    {
                        desiredX = npc.Center.X;
                        desiredY = npc.Center.Y;
                    }
                }

                if (ModContent.GetInstance<Config>().debugMode)
                {
                    Dust dust = Main.dust[Dust.NewDust(floatCenter, 2, 2, 6)];
                    dust.noGravity = true;
                }
                
            }
            if (aiTimer == 600)
            {
                npc.localAI[0] = 0;
            }
            if (aiTimer > 600 && aiTimer <= 1200)
            {

                int numProj = 8;
                int shootDelay = 5;
                if (shootTimer == numProj * shootDelay + 1)
                {
                    storeRot = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                }               
                //movement
                if (shootTimer > numProj * shootDelay)
                {
                    Vector2 target = P.Center + new Vector2(600f * moveAI, -75);
                    if (moveAI == 0) moveAI = -1;
                    if (MathHelper.Distance(target.X, npc.Center.X) <= 20)
                    {
                        moveAI *= -1;
                    }

                    Move(P, 0.1f, target);
                }
                if (shootTimer <= numProj * shootDelay)
                {
                    npc.velocity.X *= 0.9f;
                    npc.velocity.Y *= 0.9f;

                    if (npc.localAI[0] < numProj && shootTimer % shootDelay == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);

                        Vector2 projSpeed = new Vector2((float)((Math.Cos(storeRot) * 10) * -1), (float)((Math.Sin(storeRot) * 10) * -1));
                        float rotation = MathHelper.ToRadians(5);
                        float amount = npc.direction == -1 ? npc.localAI[0] - numProj / 2 : -(npc.localAI[0] - numProj / 2); // to make it from down to up
                        Vector2 perturbedSpeed = new Vector2(projSpeed.X, projSpeed.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, amount));
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("ObserverShard"), projectileBaseDamage, 3f, Main.myPlayer);
                        npc.localAI[0]++;
                    }
                }
                if (npc.localAI[0] >= numProj)
                {
                    npc.localAI[0] = 0;
                    shootTimer = 120f + numProj * shootDelay;
                }
            }
            if (aiTimer > 1200 && aiTimer < 1400) // spin
            {
                npc.rotation += 0.2f;

                npc.velocity.X = 0f;
                npc.velocity.Y = 0f;
                if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 perturbedSpeed = new Vector2(7f, 7f).RotatedByRandom(MathHelper.ToRadians(360));
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("ObserverShard"), projectileBaseDamage, 0f, Main.myPlayer);
                    shootTimer = 5;
                }
                if (MyWorld.awakenedMode)
                {
                    for (int i = 0; i < Main.maxProjectiles; i++)
                    {
                        Projectile proj = Main.projectile[i];
                        if (proj.active && proj.friendly && Vector2.Distance(proj.Center, npc.Center) < 150)
                        {
                            proj.Kill();
                            for (int d = 0; d < 10; d++)
                            {
                                Dust dust = Main.dust[Dust.NewDust(proj.position, proj.width, proj.height, 220, proj.oldVelocity.X, proj.oldVelocity.Y, 100, default(Color), 1.8f)];
                                dust.noGravity = true;
                                dust.velocity *= 0.5f;
                            }
                        }
                    }
                }
            }
            else
            {
                npc.rotation = npc.velocity.X * 0.1f;
            }
            if (aiTimer >= 1400)
            {
                beamCharge++;
                if (beamCharge >= beamChargeMax && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 113);
                    float rotation = (float)Math.Atan2(npc.Center.Y - (P.Center.Y + P.velocity.Y * 2), npc.Center.X - (P.Center.X + P.velocity.X * 2));
                    if (Collision.CanHit(npc.Center, 2, 2, P.Center, 2, 2))
                    {
                        P.immune = false;
                    }
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * 4f) * -1), (float)((Math.Sin(rotation) * 4f) * -1), mod.ProjectileType("ObserverBeam"), projectileBaseDamage * 5, 0f, Main.myPlayer, 0, npc.whoAmI);
                    beamCharge = 0;
                    aiTimer = 0;
                }
            }
            if (npc.localAI[1] == 0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                npc.TargetClosest(true);
                npc.localAI[1] = 1;
                NPC hand = Main.npc[NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, mod.NPCType("CosmicObserverHand"), 0, 0f, 0f, 0f, 0f, 255)];
                hand.ai[0] = -1f;
                hand.ai[1] = (float)npc.whoAmI;
                hand.target = npc.target;
                hand.netUpdate = true;
                hand = Main.npc[NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, mod.NPCType("CosmicObserverHand"), 0, 0f, 0f, 0f, 0f, 255)];
                hand.ai[0] = 1f;
                hand.ai[1] = (float)npc.whoAmI;
                hand.target = npc.target;
                hand.netUpdate = true;
            }
        }

        private void Move(Player P, float speed, Vector2 target)
        {
            Vector2 desiredVelocity = target - npc.Center;
            if (Main.expertMode) speed *= 1.05f;
            if (MyWorld.awakenedMode) speed *= 1.05f;

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
            float slowSpeed = Main.expertMode ? 0.96f : 0.98f;
            if (MyWorld.awakenedMode) slowSpeed = 0.94f;
            int xSign = Math.Sign(desiredVelocity.X);
            if ((npc.velocity.X < xSign && xSign == 1) || (npc.velocity.X > xSign && xSign == -1)) npc.velocity.X *= slowSpeed;

            int ySign = Math.Sign(desiredVelocity.Y);
            if (MathHelper.Distance(target.Y, npc.Center.Y) > 1000)
            {
                if ((npc.velocity.X < ySign && ySign == 1) || (npc.velocity.X > ySign && ySign == -1)) npc.velocity.Y *= slowSpeed;
            }
        }

        public override bool CheckActive()
        {
            return false;
        }
    }
}
