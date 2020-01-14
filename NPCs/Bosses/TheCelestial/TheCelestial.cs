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
    public class TheCelestial : ModNPC
    {
        private int projectileBaseDamage = NPC.downedMoonlord ? 60 : 45;
        private int baseDefense = NPC.downedMoonlord ? 50 : 30;

        private float moveSpeed = 0.15f;

        private float awakenedAttacks = 0f;
        private float despawnTimer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float phase
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float shootTimer
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float moveAI
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(moveSpeed);
            writer.Write(awakenedAttacks);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            moveSpeed = reader.ReadSingle();
            awakenedAttacks = reader.ReadSingle();
        }
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 202;

            npc.aiStyle = -1;

            npc.lifeMax = NPC.downedMoonlord ? 180000 : 32000;
            npc.damage = NPC.downedMoonlord ? 90 : 60;
            npc.defense = 30;
            npc.knockBackResist = 0f;

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

            moveSpeed =  0.06f;
            npc.defense = baseDefense + 10;
            npc.GivenName = "Ember";
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Celestials");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = NPC.downedMoonlord ? 240000 : 48000;
            npc.damage = NPC.downedMoonlord ? 120 : 90;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = NPC.downedMoonlord ? 460000 : 60000;
                npc.damage = NPC.downedMoonlord ? 150 : 110;
                npc.defense = NPC.downedMoonlord ? 60 : 40;
            }
        }
        public override void BossHeadSlot(ref int index)
        {
            if (phase == 0)index = NPCHeadLoader.GetBossHeadSlot("ElementsAwoken/NPCs/Bosses/TheCelestial/TheCelestial_Head_Boss");
            else if (phase > 0) index = NPCHeadLoader.GetBossHeadSlot("ElementsAwoken/NPCs/Bosses/TheCelestial/TheCelestial_Head_Boss_" + phase);
        }
        public override void FindFrame(int frameHeight)
        {
            int frameWidth = 100;
            npc.frame.Width = frameWidth;
            npc.frameCounter += 1;

            if (npc.frameCounter > 9)
            {
                npc.frame.X = npc.frame.X + frameWidth;
                npc.frameCounter = 0.0;
            }

                if (npc.frame.X > frameWidth * 3)
                {
                    npc.frame.X = 0;
                }

            if (phase == 0) // solar
            {
                npc.frame.Y = 0;
            }
            else if (phase == 1) // stardust
            {
                npc.frame.Y = frameHeight * 1;
            }
            else if (phase == 2) // vortex
            {
                npc.frame.Y = frameHeight * 2;
            }
            else if (phase == 3) // nebula
            {
                npc.frame.Y = frameHeight * 3;
            }
        }

        public override bool PreDraw(SpriteBatch spritebatch, Color drawColor)
        {
            Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, Main.npcTexture[npc.type].Height * 0.5f);
            SpriteEffects spriteEffects = npc.direction != 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            var origin = npc.frame.Size() * 0.5f;
            Color color = npc.GetAlpha(drawColor);
            spritebatch.Draw(Main.npcTexture[npc.type], npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), npc.frame, color, npc.rotation, origin, npc.scale, spriteEffects, 0);
            return false;
        }

        public override void NPCLoot()
        {
            if (!Main.expertMode)
            {
                int choice = Main.rand.Next(4);
                if (choice == 0) choice = mod.ItemType("Celestia");
                else if (choice == 1) choice = mod.ItemType("EyeballStaff");
                else if(choice == 2) choice = mod.ItemType("CelestialInferno");
                else if(choice == 3) choice = mod.ItemType("Solus");
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, choice);

                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CelestialsMask"));
                }
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TheCelestialTrophy"));
                }
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CelestialCrown"));
                }
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
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.FragmentNebula, Main.rand.Next(5, 10));
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.FragmentSolar, Main.rand.Next(5, 10));
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.FragmentStardust, Main.rand.Next(5, 10));
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.FragmentVortex, Main.rand.Next(5, 10));
                }
            }
            else NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Astra"), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            if (!Main.expertMode) potionType = ItemID.GreaterHealingPotion;
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            npc.direction = Math.Sign(P.Center.X - npc.Center.X);
            Lighting.AddLight(npc.Center, 1f, 1f, 1f);
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
            #region orbitals, names & ai changes
            int moonlordBonus = NPC.downedMoonlord ? 2 : 1;
            bool createOrbitals = false;

            if (npc.localAI[0] == 0)
            {
                createOrbitals = true;
                npc.localAI[0]++;
                //npc.netUpdate = true;
            }
            if (npc.life <= npc.lifeMax * 0.75f && phase == 0)
            {
                moveSpeed = 0.2f;
                npc.defense = baseDefense + 5 * moonlordBonus;
                npc.GivenName = "Nova";
                if (Main.netMode != NetmodeID.MultiplayerClient) phase++;
                npc.netUpdate = true;
                createOrbitals = true;
            }
            if (npc.life <= npc.lifeMax * 0.5f && phase == 1)
            {
                moveSpeed =  0.15f;
                npc.defense = baseDefense;
                npc.GivenName = "Aquila";

                if (Main.netMode != NetmodeID.MultiplayerClient) phase++;
                npc.netUpdate = true;
                createOrbitals = true;
            }
            if (npc.life <= npc.lifeMax * 0.25f && phase == 2)
            {
                moveSpeed = 0.15f;
                npc.defense = baseDefense - 10 * moonlordBonus;
                projectileBaseDamage += 20 * moonlordBonus;
                npc.damage += 20;
                npc.GivenName = "Carina";

                if (Main.netMode != NetmodeID.MultiplayerClient) phase++;
                npc.netUpdate = true;
                createOrbitals = true;
            }
            if (createOrbitals)
            {
                int orbitalcount = Main.expertMode ? 9 : 7;
                for (int l = 0; l < orbitalcount; l++)
                {
                    //cos = y, sin = x
                    int distance = 360 / orbitalcount;
                    NPC orbital = Main.npc[NPC.NewNPC((int)(npc.Center.X + (Math.Sin(l * distance) * 150)), (int)(npc.Center.Y + (Math.Cos(l * distance) * 150)), mod.NPCType("TheCelestialMinion"), npc.whoAmI, l * distance, npc.whoAmI, phase, Main.rand.Next(0,200))];
                    orbital.netUpdate = true;
                    //npc.netUpdate = true;
                }
            }
            #endregion

            //movement
            Vector2 target = P.Center + new Vector2(700f * moveAI, 0);
            if (moveAI == 0) moveAI = -1;
            if (MathHelper.Distance(target.X,npc.Center.X) <= 20)
            {
                moveAI *= -1;
            }
            Move(P, moveSpeed, target);

            if (ModContent.GetInstance<Config>().debugMode)
            {
                int dust = Dust.NewDust(P.Center + new Vector2(700f * moveAI, 0) - new Vector2(8,8), 16, 16, 6);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1f;
                Main.dust[dust].velocity *= 0.1f;
            }

            shootTimer--;
            awakenedAttacks--;
            // solar
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (shootTimer <= 0)
                {
                    if (phase == 0)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 69);
                        MeteorShower(P, 11, projectileBaseDamage, Main.rand.Next(5, 8));
                        shootTimer = 240;
                    }
                    // stardust
                    else if (phase == 1)
                    {
                        Main.PlaySound(4, (int)npc.position.X, (int)npc.position.Y, 7, 1f, 0f);
                        StarShower(P, 8, projectileBaseDamage + 30, Main.rand.Next(3, 4));
                        shootTimer = 240;
                    }
                    // vortex
                    else if(phase == 2)
                    {
                        int type = mod.ProjectileType("CelestialVortexPortal");
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 21);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 3), type, projectileBaseDamage, 0f, 0);
                        shootTimer = Main.rand.Next(120, 360);
                        if (MyWorld.awakenedMode) shootTimer = Main.rand.Next(60, 180);
                        //npc.netUpdate = true; // apprently doesnt need to be done, spawning of projectiles & timers related to that dont need to be synced?
                    }
                    // nebula
                    else if(phase == 3)
                    {
                        int numberProjectiles = 3;
                        float Speed = 5f;
                        float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1)).RotatedByRandom(MathHelper.ToRadians(50));
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("CelestialNebulaPortal"), projectileBaseDamage, 0f, Main.myPlayer, 0f, 0f);
                        }
                        shootTimer = 240;
                    }
                    if (Main.expertMode) shootTimer -= 25;
                    if (MyWorld.awakenedMode) shootTimer -= 45;
                }
                if (awakenedAttacks <= 0 && MyWorld.awakenedMode)
                {
                    if (phase == 0)
                    {
                        Projectile proj = Main.projectile[Projectile.NewProjectile(P.Center.X + Main.rand.Next(-750,750), P.Center.Y - 1500, Main.rand.NextFloat(-2f,2f), -6f, mod.ProjectileType("SolarFragmentProj"), projectileBaseDamage, 0f, 0)];
                        awakenedAttacks = 120;
                    }
                    // stardust
                    else if (phase == 1)
                    {
                        Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-1500, 1500), npc.Center.Y + Main.rand.Next(-700, 700), Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f), mod.ProjectileType("CelestialStar"), projectileBaseDamage / 2, 0f, 0)];
                        awakenedAttacks = 180;
                    }
                    // nebula
                    else if (phase == 3)
                    {

                    }
                }
            }
            if (!ModContent.GetInstance<Config>().lowDust)
            {
                int dustType = 6;
                if (phase == 0) dustType = 6;
                else if (phase == 1) dustType = 197;
                else if (phase == 2) dustType = 229;
                else if (phase == 3) dustType = 242;
                for (int i = 0; i < 2; i++)
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
            float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
            for (int i = 0; i < numberProj; i++)
            {
                speed += Main.rand.NextFloat(-2, 2);
                Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1) - 2).RotatedByRandom(MathHelper.ToRadians(20));
                Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("SolarFragmentProj"), damage, 0f, 0)];
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
                Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("CelestialStarShot"), damage, 0f, 0)];
            }
        }

        public override bool CheckActive()
        {
            return false;
        }
    }
}
