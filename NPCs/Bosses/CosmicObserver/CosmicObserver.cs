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
        public int projectileBaseDamage = 20;
        public bool reset = false;
        public int moveAi = 0;

        public float storeRot = 0;

        public bool spawnedHands = false;

        public Vector2 floatCenter = new Vector2();

        public float beamCharge = 0f;
        public const float beamChargeMax = 300f;

        public int targetLaserFrame = 0;
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

            // used for the hands
            NPCID.Sets.TrailCacheLength[npc.type] = 15;
            NPCID.Sets.TrailingMode[npc.type] = 1;
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
            if (npc.life >= npc.lifeMax * 0.75f)
            {
                npc.frame.Y = 0;
            }
            else if (npc.life >= npc.lifeMax * 0.6f)
            {
                npc.frame.Y = 1 * frameHeight;
            }
            else if (npc.life >= npc.lifeMax * 0.4f)
            {
                npc.frame.Y = 2 * frameHeight;
            }
            else if (npc.life >= npc.lifeMax * 0.25f && MyWorld.awakenedMode)
            {
                npc.frame.Y = 3 * frameHeight;
            }
            if (npc.ai[1] % 9 == 0)
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
            int numShards = Main.rand.Next(8, 12);
            if (Main.expertMode) numShards = Main.rand.Next(12, 19);
            if (MyWorld.awakenedMode)
            {
                numShards = Main.rand.Next(16, 26);
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CosmicGlass"), 1);
            }
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CosmicShard"), numShards); //Item spawn
            MyWorld.downedCosmicObserver = true;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (npc.ai[1] >= 1400)
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
            if (!reset)
            {
                Main.PlaySound(15, (int)P.position.X, (int)P.position.Y, 0);
                Main.NewText("A Cosmic Observer roams the skies", 175, 75, 255, false);
                reset = true;
            }
            npc.ai[0]--;
            npc.ai[1]++;
            if (MyWorld.awakenedMode && npc.life < npc.lifeMax * 0.25f)
            {
                npc.ai[1]++;
            }
            npc.ai[2]--; // multiple laser burst
            if (MyWorld.awakenedMode)
            {
                if (npc.life > npc.lifeMax / 2)
                {
                    if (npc.ai[1] >= 1200)
                    {
                        npc.ai[1] = 0;
                    }
                }
                else if (npc.life > npc.lifeMax * 0.25f)
                {
                    if (npc.ai[1] >= 1400)
                    {
                        npc.ai[1] = 0;
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
                    if (npc.ai[1] >= 1200)
                    {
                        npc.ai[1] = 0;
                    }
                }
                else
                {
                    if (npc.ai[1] >= 1400)
                    {
                        npc.ai[1] = 0;
                    }
                }
            }
            else
            {
                if (npc.ai[1] >= 1200)
                {
                    npc.ai[1] = 0;
                }
            }

            if (npc.ai[2] <= 0)
            {
                npc.ai[2] = Main.rand.Next(60, 90);
            }
            if (npc.ai[1] <= 600)
            {
                /*if (npc.life > npc.lifeMax / 2)
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
                    if (Main.netMode != NetmodeID.MultiplayerClient && npc.ai[0] <= 0 && npc.ai[2] <= 18)
                    {
                        Laser(P, 4f, projectileBaseDamage);
                        npc.ai[0] = 6;
                    }
                }*/
                //Move(P, 0.075f, P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
                if (Vector2.Distance(P.Center, npc.Center) > 700)
                {
                    Vector2 toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
                    toTarget.Normalize();
                    npc.velocity = toTarget * 5f;
                }
                else if (Vector2.Distance(P.Center, npc.Center) > 500)
                {
                    Move(P, 0.02f, P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
                }
                else
                {
                    float num1060 = 0.2f;
                    Vector2 vector149 = floatCenter - npc.Center;
                    if (vector149.Length() < 60f)
                    {
                        num1060 = 0.12f;
                    }
                    if (vector149.Length() < 40f)
                    {
                        num1060 = 0.06f;
                    }
                    if (vector149.Length() > 20f)
                    {
                        if (Math.Abs(floatCenter.X - npc.Center.X) > 20f)
                        {
                            npc.velocity.X = npc.velocity.X + num1060 * (float)Math.Sign(floatCenter.X - npc.Center.X);
                        }
                        if (Math.Abs(floatCenter.Y - npc.Center.Y) > 10f)
                        {
                            npc.velocity.Y = npc.velocity.Y + num1060 * (float)Math.Sign(floatCenter.Y - npc.Center.Y);
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
                    float num1061 = 15f;
                    if (npc.velocity.Length() > num1061)
                    {
                        npc.velocity = Vector2.Normalize(npc.velocity) * num1061;
                    }
                }
                if (Vector2.Distance(P.Center, npc.Center) > 300)
                {
                    if (Vector2.Distance(floatCenter, npc.Center) > 150) floatCenter = npc.Center;
                }

                if (ModContent.GetInstance<Config>().debugMode)
                {
                    Dust dust = Main.dust[Dust.NewDust(floatCenter, 2, 2, 6)];
                    dust.noGravity = true;
                }
                
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
            if (npc.ai[1] > 1200 && npc.ai[1] < 1400) // spin
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
                if (MyWorld.awakenedMode)
                {
                    for (int i = 0; i < Main.projectile.Length; i++)
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
            if (npc.ai[1] >= 1400)
            {
                beamCharge++;
                if (beamCharge >= beamChargeMax)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 113);
                    float rotation = (float)Math.Atan2(npc.Center.Y - (P.Center.Y + P.velocity.Y * 2), npc.Center.X - (P.Center.X + P.velocity.X * 2));
                    if (Collision.CanHit(npc.Center, 2, 2, P.Center, 2, 2))
                    {
                        P.immune = false;
                    }
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * 4f) * -1), (float)((Math.Sin(rotation) * 4f) * -1), mod.ProjectileType("ObserverBeam"), projectileBaseDamage * 5, 0f, 0, 0, npc.whoAmI);
                    beamCharge = 0;
                    npc.ai[1] = 0;
                }
            }
            if (!spawnedHands && Main.netMode != NetmodeID.MultiplayerClient)
            {
                npc.TargetClosest(true);
                spawnedHands = true;
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
