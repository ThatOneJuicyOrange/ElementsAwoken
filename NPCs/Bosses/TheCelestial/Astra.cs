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

namespace ElementsAwoken.NPCs.Bosses.TheCelestial
{
    [AutoloadBossHead]
    public class Astra : ModNPC
    {
        private int projectileBaseDamage = NPC.downedMoonlord ? 60 : 45;
        private float despawnTimer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float moveAI
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float shootTimer
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float attackCool
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }

        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 184;

            npc.lifeMax = NPC.downedMoonlord ? 45000 : 7500;
            npc.damage = NPC.downedMoonlord ? 90 : 60;
            npc.defense = 30;
            npc.knockBackResist = 0f;

            npc.aiStyle = -1;

            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;

            npc.HitSound = SoundID.NPCHit55;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.value = Item.buyPrice(0, 30, 0, 0);
            music = MusicID.Boss5;
            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/CelestialThemeSolar");
            bossBag = mod.ItemType("TheCelestialBag");

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
            DisplayName.SetDefault("Astra");
            Main.npcFrameCount[npc.type] = 8;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = NPC.downedMoonlord ? 55000 : 12000;
            npc.damage = NPC.downedMoonlord ? 120 : 90;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = NPC.downedMoonlord ? 90000 : 15000;
                npc.damage = NPC.downedMoonlord ? 150 : 110;
                npc.defense = NPC.downedMoonlord ? 60 : 40;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1;
            if (npc.frameCounter > 6)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.frame.Y > frameHeight * 3)
            {
                npc.frame.Y = 0;
            }
        }


        public override void NPCLoot()
        {
            npc.DropBossBags();

            MyWorld.downedCelestial = true;
            if (!NPC.downedMoonlord)
            {
                Main.NewText("You cant stop whats already been started...", new Color(255, 101, 73));
                Main.NewText("You cant stop whats already been started...", new Color(89, 110, 230));
                Main.NewText("You cant stop whats already been started...", new Color(51, 247, 165));
                Main.NewText("You cant stop whats already been started...", new Color(223, 146, 244));
                Main.PlaySound(29, (int)npc.position.X, (int)npc.position.Y, 105);
            }
            else
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.FragmentNebula, Main.rand.Next(5, 20));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.FragmentSolar, Main.rand.Next(5, 20));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.FragmentStardust, Main.rand.Next(5, 20));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.FragmentVortex, Main.rand.Next(5, 20));
            }
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            npc.direction = Math.Sign(P.Center.X - npc.Center.X);
            npc.spriteDirection = npc.direction;
            Lighting.AddLight(npc.Center, 1f, 1f, 1f);
            #region despawning
            if (!Main.player[npc.target].active || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (!Main.player[npc.target].active || Main.player[npc.target].dead)
                {
                    despawnTimer++;
                    npc.velocity.Y = npc.velocity.Y + 0.11f;
                    if (despawnTimer >= 300)
                    {
                        npc.active = false;
                    }
                }
                else
                    despawnTimer = 0;
            }
            if (Main.dayTime)
            {
                despawnTimer++;
                npc.velocity.Y = npc.velocity.Y + 0.11f;
                if (despawnTimer >= 300)
                {
                    npc.active = false;
                }
            }
            #endregion
            // illusions & orbitals
            if (npc.localAI[1] == 0)
            {
                if (Main.netMode == 0)
                {
                    if (P.ownedProjectileCounts[mod.ProjectileType("CelestialIllusions")] == 0)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            Projectile.NewProjectile(P.Center.X, P.Center.Y, 0f, 0f, mod.ProjectileType("CelestialIllusions"), 0, 0f, Main.myPlayer, i, 0f);
                        }
                    }
                }
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int orbitalcount = MyWorld.awakenedMode ? 9 : 7;
                    for (int l = 0; l < orbitalcount; l++)
                    {
                        //cos = y, sin = x
                        int distance = 360 / orbitalcount;
                        NPC orbital = Main.npc[NPC.NewNPC((int)(npc.Center.X + (Math.Sin(l * distance) * 150)), (int)(npc.Center.Y + (Math.Cos(l * distance) * 150)), mod.NPCType("AstraMinion"), npc.whoAmI, 0, Main.rand.Next(0, 300))];
                        orbital.netUpdate = true;
                    }
                }
                npc.localAI[1]++;
                npc.netUpdate = true;
            }
            //movement
            Vector2 target = P.Center + new Vector2(700f * moveAI, 0);
            if (moveAI == 0) moveAI = -1;
            if (MathHelper.Distance(target.X, npc.Center.X) <= 20)
            {
                moveAI *= -1;
            }
            Move(P, 0.15f, target);


            shootTimer--;
            if (NPC.AnyNPCs(mod.NPCType("AstraMinion")))
            {
                if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    AstraShots(P, 12f, projectileBaseDamage + 35, Main.rand.Next(3, 5));
                    shootTimer = Main.rand.Next(60, 180);
                    npc.netUpdate = true;
                }
                npc.immortal = true;
                npc.dontTakeDamage = true;
            }
            else
            {
                if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    AstraShots(P, 12f, projectileBaseDamage, Main.rand.Next(3, 5));
                    shootTimer = Main.rand.Next(20, 140);
                    npc.netUpdate = true;
                }
                npc.immortal = false;
                npc.dontTakeDamage = false;
            } 
            attackCool--;
            if (attackCool <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                int attackType = Main.rand.Next(4);
                // solar
                if(attackType == 0)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 69);
                    MeteorShower(P, 11, projectileBaseDamage, Main.rand.Next(5, 8));
                }
                // stardust
                else if(attackType == 1)
                {
                    Main.PlaySound(4, (int)npc.position.X, (int)npc.position.Y, 7, 1f, 0f);
                    StarShower(P, 8, projectileBaseDamage + 30, Main.rand.Next(3, 4));
                }
                // nebula
                else if(attackType == 2)
                {
                    int numberProjectiles = 3;
                    float Speed = 5f;
                    float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1)).RotatedByRandom(MathHelper.ToRadians(50));
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("CelestialNebulaPortal"), projectileBaseDamage, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
                else if (attackType == 3)
                {
                    int type = mod.ProjectileType("CelestialVortexPortal");
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 21);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), type, projectileBaseDamage, 0f, Main.myPlayer);
                }
                attackCool = 240;
                npc.netUpdate = true;
            }
            if (!ModContent.GetInstance<Config>().lowDust)
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

                for (int i = 0; i < 3; i++)
                {
                    int dust = Dust.NewDust(npc.position, npc.width, npc.height, dustType);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = 1f;
                    Main.dust[dust].velocity *= 0.1f;
                }
            }
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

        private void MeteorShower(Player P, float speed, int damage, int numberProj)
        {
            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 12);
            float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
            for (int i = 0; i < numberProj; i++)
            {
                speed += Main.rand.NextFloat(-2, 2);
                Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1) - 2).RotatedByRandom(MathHelper.ToRadians(20));
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("SolarFragmentProj"), damage, 0f, Main.myPlayer);
            }
        }

        private void StarShower(Player P, float speed, int damage, int numberProj)
        {
            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 12);
            float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
            for (int i = 0; i < numberProj; i++)
            {
                speed += Main.rand.NextFloat();
                Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1)).RotatedByRandom(MathHelper.ToRadians(10));
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("CelestialStarShot"), damage, 0f, Main.myPlayer);
            }
        }

        private void AstraShots(Player P, float speed, int damage, int numberProj)
        {
            Main.PlaySound(4, (int)npc.position.X, (int)npc.position.Y, 7, 1f, 0f);
            float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
            for (int i = 0; i < numberProj; i++)
            {
                Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1)).RotatedByRandom(MathHelper.ToRadians(5));
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("AstraShot"), damage, 0f, Main.myPlayer, 0f, 0f);
            }
        }

        public override bool CheckActive()
        {
            return false;
        }
    }
}
