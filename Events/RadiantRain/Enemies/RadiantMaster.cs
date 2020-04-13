using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using static Terraria.ModLoader.ModContent;
using System.IO;
using ElementsAwoken.Projectiles.NPCProj;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using ElementsAwoken.Items.ItemSets.Radia;
using ElementsAwoken.Buffs.Debuffs;
using ElementsAwoken.Projectiles.NPCProj.RadiantMaster;
using ElementsAwoken.Projectiles.Explosions;
using ElementsAwoken.Items.BossDrops.RadiantMaster;

namespace ElementsAwoken.Events.RadiantRain.Enemies
{
    
    [AutoloadBossHead]

    public class RadiantMaster : ModNPC
    {
        private float despawnTimer = 0;
        private float aiTimer2 = 0;
        private float deathTimerAI = 0;
        private int projectileBaseDamage = 100;
        private float aiTimer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float shootTimer
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float aiState
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float deathTimer
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(despawnTimer);
            writer.Write(deathTimer);
            writer.Write(deathTimerAI);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            despawnTimer = reader.ReadSingle();
            deathTimer = reader.ReadSingle();
            deathTimerAI = reader.ReadSingle();
        }
        public override void SetDefaults()
        {
            npc.width = 66;
            npc.height = 96;

            npc.aiStyle = -1;
            npc.lifeMax = 175000;
            npc.damage = 140;
            npc.defense = 40;

            npc.boss = true;
            npc.noTileCollide = true;
            npc.lavaImmune = true;
            npc.noGravity = true;

            npc.HitSound = SoundID.NPCHit54;
            npc.DeathSound = SoundID.NPCDeath6;

            npc.value = Item.buyPrice(0, 3, 0, 0);
            npc.knockBackResist = 0f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Radiant Master");
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffType<Starstruck>(), 300);
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 250000;
            npc.damage = 220;
            npc.defense = 50;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 450000;
                npc.damage = 300;
                npc.defense = 65;
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemType<Items.Consumable.Potions.EpicHealingPotion>();
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            npc.frameCounter++;
            if (npc.frameCounter > 4)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.frame.Y > frameHeight * 5)
            {
                npc.frame.Y = 0;
            }
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<Radia>(), Main.rand.Next(4, 21));
            if (MyWorld.awakenedMode) Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<RadiantCrown>());
            int choice = Main.rand.Next(4);
            if (choice == 0) Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<Majesty>());
            else if (choice == 1) Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<RadiantBomb>());
            else if (choice == 2) Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<RadiantBow>());
            else if (choice == 3) Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<RadiantSword>());
            MyWorld.downedRadiantMaster = true;
            for (int l = 0; l < 6; l++)
            {
                Projectile.NewProjectile(npc.Center + Main.rand.NextVector2Square(-npc.width, npc.width), new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(3)), ProjectileType<RadiantFireball>(), 0, 0, Main.myPlayer,1);
                Projectile.NewProjectile(npc.Center + Main.rand.NextVector2Square(-npc.width, npc.width), Vector2.Zero, ProjectileType<RadiantMasterDeathExplosion>(), 0, 0, Main.myPlayer);
                Main.PlaySound(SoundID.Item14, npc.Center);
            }
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData);
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override bool CheckDead()
        {
            if (deathTimer < 300)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<RadiantMasterDeath>());
                deathTimer = 1;
                npc.damage = 0;
                npc.life = npc.lifeMax;
                npc.dontTakeDamage = true;
                npc.netUpdate = true;
                return false;
            }
            return true;
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (npc.life <= 0) return false;
            return base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override void AI()
        {
            npc.TargetClosest(false);
            Player P = Main.player[npc.target];
            Lighting.AddLight(npc.Center, 1f, 0.2f, 0.5f);
            if (deathTimer > 0)
            {
                npc.rotation = 0;
                npc.velocity = Vector2.Zero;
                deathTimer++;
                deathTimerAI--;
                if (deathTimerAI <= 0)
               {
                    Teleport(P.Center + Main.rand.NextVector2Square(-300, 300));
                    if (deathTimer < 120) deathTimerAI = MathHelper.Lerp(60, 5, deathTimer / 300f);
                    else deathTimerAI = MathHelper.Lerp(30, 5, deathTimer / 300f);
                }
                if (deathTimer > 300)
                {
                    npc.life = 0;
                    npc.HitEffect(0, 0);
                    npc.checkDead();
                }
            }
            else if (!P.active || P.dead)
            {
                despawnTimer++;
                npc.velocity.Y = npc.velocity.Y + 0.11f;
                if (despawnTimer >= 300)  npc.active = false;
            }
            else
            {
                despawnTimer = 0;
                if (Main.rainTime < 600)
                {
                    Main.rainTime = 600;
                }
                if (aiState == 0)
                {
                    FlyTo(P.Center, 0.1f, 13f);
                    npc.direction = Math.Sign(P.Center.X - npc.Center.X);

                    aiTimer++;
                    shootTimer--;
                    if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient && Vector2.Distance(npc.Center, P.Center) > 150)
                    {
                        float Speed = 14f;
                        float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                        Vector2 projSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                        projSpeed = projSpeed.RotatedByRandom(MathHelper.ToRadians(10));
                        Projectile.NewProjectile(npc.Center, projSpeed, ProjectileType<RadiantFireball>(), 0, 0f, Main.myPlayer);
                        shootTimer = 30;
                    }
                    if (aiTimer > 600)
                    {
                        aiState++;
                        aiTimer = 0;
                        shootTimer = 0;
                    }
                }
                else if (aiState == 1)
                {
                    npc.rotation = npc.velocity.X * 0.02f;
                    int dashDelay = 15;
                    int dashDuration = 120;
                    int totalDur = dashDelay + dashDuration;
                    aiTimer++;
                    if (aiTimer == 1)
                    {
                        Teleport(P.Center - new Vector2(-500, 0));
                        npc.direction = Math.Sign(P.Center.X - npc.Center.X);
                        npc.velocity = Vector2.Zero;
                    }
                    else if (aiTimer > dashDelay && aiTimer < totalDur)
                    {
                        npc.velocity.X = -12;
                    }
                    else if (aiTimer == totalDur)
                    {
                        Teleport(P.Center - new Vector2(500, 0));
                        npc.direction = Math.Sign(P.Center.X - npc.Center.X);
                        npc.velocity = Vector2.Zero;
                    }
                    else if (aiTimer > totalDur + dashDelay && aiTimer < totalDur * 2)
                    {
                        npc.velocity.X = 12;
                    }
                    else if (aiTimer == totalDur * 2)
                    {
                        Teleport(P.Center - new Vector2(0, 500));
                        npc.velocity = Vector2.Zero;
                    }
                    else if (aiTimer > totalDur * 2)
                    {
                        npc.velocity.Y = 12;
                    }
                    if ((aiTimer > totalDur * 2 && npc.Center.Y > P.Center.Y) || aiTimer > totalDur * 3)
                    {
                        aiState++;
                        aiTimer = 0;
                        shootTimer = 120;
                        aiTimer2 = 300;
                        npc.rotation = 0;
                    }
                }
                else if (aiState == 2)
                {
                    npc.direction = Math.Sign(P.Center.X - npc.Center.X);
                    aiTimer++;
                    aiTimer2--;
                    if (aiTimer2 > 0) FlyTo(P.Center - new Vector2(npc.direction * 400, 300), 0.2f, 13f);
                    shootTimer--;
                    if (aiTimer2 == 0)
                    {
                        npc.velocity.Y = 0;
                        npc.velocity.X = npc.direction * 20;
                    }
                    else if (aiTimer2 < -20)
                    {
                        aiTimer2 = 300;
                    }

                    if (shootTimer < 120)
                    {

                        Dust dust = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, DustID.PinkFlame, 0f, 0f, 200, default(Color), 0.5f)];
                        dust.noGravity = true;
                        dust.fadeIn = 1.3f;
                        Vector2 vector = Main.rand.NextVector2Square(-1, 1f);
                        vector.Normalize();
                        vector *= 12f;
                        dust.velocity = vector;
                        dust.position = npc.Center - vector * 15;
                    }
                    if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        float Speed = 5f;
                        float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                        Vector2 projSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                        projSpeed = projSpeed.RotatedByRandom(MathHelper.ToRadians(10));
                        Projectile.NewProjectile(npc.Center, projSpeed, ProjectileType<RadiantWhirlwind>(), projectileBaseDamage * 2, 0f, Main.myPlayer);
                        shootTimer = 360;
                    }
                    if (aiTimer > 1100)
                    {
                        aiState++;
                        aiTimer = 0;
                        shootTimer = 0;
                    }
                }
                else if (aiState == 3)
                {
                    npc.direction = Math.Sign(P.Center.X - npc.Center.X);
                    aiTimer++;
                    shootTimer--;
                    FlyTo(P.Center, 0.1f, 3f);
                    if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int orbitalCount = Main.expertMode ? MyWorld.awakenedMode ? 12 : 8 : 6;
                        for (int l = 0; l < orbitalCount; l++)
                        {
                            int distance = 360 / orbitalCount;
                            Projectile orbital = Main.projectile[Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, ProjectileType<RadiantMasterStar>(), projectileBaseDamage, 0f, Main.myPlayer, l * distance, npc.whoAmI)];
                            RadiantMasterStar radStar = (RadiantMasterStar)orbital.modProjectile;
                            radStar.aiTimer = l * -10;
                        }
                        shootTimer = 240;
                    }
                    if (aiTimer > 600)
                    {
                        aiState = 0;
                        aiTimer = 0;
                        shootTimer = 0;
                    }
                }
            }
        }
        private void Teleport(Vector2 toPos)
        {
            Main.PlaySound(SoundID.Item46, npc.Center); // 46 // 77 // 104
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int numProj = 5;
                Vector2 distance = (toPos - npc.Center) / numProj;
                for (int k = 0; k < numProj; k++)
                {
                    Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center + distance - new Vector2(0, 23), Vector2.Zero, ProjectileType<RadiantTeleport>(), 0, 0f, Main.myPlayer)];
                    proj.spriteDirection = -npc.spriteDirection;
                    distance += (toPos - npc.Center) / numProj;
                }
                npc.Center = toPos;
                npc.netUpdate = true;
            }
        }
        private void FlyTo(Vector2 location, float acceleration, float speed)
        {
            float targetX = location.X - npc.Center.X;
            float targetY = location.Y - npc.Center.Y;
            float targetPos = (float)Math.Sqrt((double)(targetX * targetX + targetY * targetY));
            targetPos = speed / targetPos;
            targetX *= targetPos;
            targetY *= targetPos;
            if (npc.velocity.X < targetX)
            {
                npc.velocity.X = npc.velocity.X + acceleration;
                if (npc.velocity.X < 0f && targetX > 0f)
                {
                    npc.velocity.X = npc.velocity.X + acceleration;
                }
            }
            else if (npc.velocity.X > targetX)
            {
                npc.velocity.X = npc.velocity.X - acceleration;
                if (npc.velocity.X > 0f && targetX < 0f)
                {
                    npc.velocity.X = npc.velocity.X - acceleration;
                }
            }
            if (npc.velocity.Y < targetY)
            {
                npc.velocity.Y = npc.velocity.Y + acceleration;
                if (npc.velocity.Y < 0f && targetY > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y + acceleration;
                }
            }
            else if (npc.velocity.Y > targetY)
            {
                npc.velocity.Y = npc.velocity.Y - acceleration;
                if (npc.velocity.Y > 0f && targetY < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y - acceleration;
                }
            }
        }
    }
}
