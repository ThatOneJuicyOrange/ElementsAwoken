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

namespace ElementsAwoken.NPCs.Bosses.TheCelestial
{
    [AutoloadBossHead]
    public class TheCelestial : ModNPC
    {
        bool reset = true;
        int moveAi = 0;
        int text = 0;

        int projectileBaseDamage = NPC.downedMoonlord ? 60 : 45;
        int baseDefense = NPC.downedMoonlord ? 50 : 30;

        float moveSpeed = 0.15f;
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 184;

            npc.lifeMax = NPC.downedMoonlord ? 225000 : 40000;
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
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Celestials");
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = NPC.downedMoonlord ? 300000 : 60000;
            npc.damage = NPC.downedMoonlord ? 120 : 90;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = NPC.downedMoonlord ? 450000 : 75000;
                npc.damage = NPC.downedMoonlord ? 150 : 110;
                npc.defense = NPC.downedMoonlord ? 60 : 40;
            }
        }
        public override void BossHeadSlot(ref int index)
        {
            if (npc.ai[1] == 0)
            {
                index = NPCHeadLoader.GetBossHeadSlot("ElementsAwoken/NPCs/Bosses/TheCelestial/TheCelestial_Head_Boss");
            }
            if (npc.ai[1] == 1)
            {
                index = NPCHeadLoader.GetBossHeadSlot("ElementsAwoken/NPCs/Bosses/TheCelestial/TheCelestial_Head_Boss_1");
            }
            if (npc.ai[1] == 2)
            {
                index = NPCHeadLoader.GetBossHeadSlot("ElementsAwoken/NPCs/Bosses/TheCelestial/TheCelestial_Head_Boss_2");
            }
            if (npc.ai[1] == 3)
            {
                index = NPCHeadLoader.GetBossHeadSlot("ElementsAwoken/NPCs/Bosses/TheCelestial/TheCelestial_Head_Boss_3");
            }
            if (npc.ai[1] == 4)
            {
                index = NPCHeadLoader.GetBossHeadSlot("ElementsAwoken/NPCs/Bosses/TheCelestial/TheCelestial_Head_Boss_4");
            }
        }

        public override void FindFrame(int frameHeight)
        {
            int frameWidth = 120;
            npc.frame.Width = frameWidth;
            npc.frameCounter += 1;

            if (npc.frameCounter > 9)
            {
                npc.frame.X = npc.frame.X + frameWidth;
                npc.frameCounter = 0.0;
            }

            if (npc.ai[1] != 4)
            {
                if (npc.frame.X > frameWidth * 3)
                {
                    npc.frame.X = 0;
                }
            }
            else
            {
                if (npc.frame.X > frameWidth * 4)
                {
                    npc.frame.X = 0;
                }
            }

            if (npc.ai[1] == 0) // solar
            {
                npc.frame.Y = 0;
            }
            else if (npc.ai[1] == 1) // stardust
            {
                npc.frame.Y = frameHeight * 1;
            }
            else if (npc.ai[1] == 2) // vortex
            {
                npc.frame.Y = frameHeight * 2;
            }
            else if (npc.ai[1] == 3) // nebula
            {
                npc.frame.Y = frameHeight * 3;
            }
            else if (npc.ai[1] == 4) // astra
            {
                npc.frame.Y = frameHeight * 4;
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
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TheCelestialTrophy"));
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CelestialCrown"));
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
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CelestialInferno"));
                }
                if (choice == 1)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EyeOfTheCelestial"));
                }
                if (choice == 2)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EyeballStaff"));
                }
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
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.FragmentNebula, Main.rand.Next(5, 20));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.FragmentSolar, Main.rand.Next(5, 20));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.FragmentStardust, Main.rand.Next(5, 20));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.FragmentVortex, Main.rand.Next(5, 20));
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            Lighting.AddLight(npc.Center, 1f, 1f, 1f);
            if (!Main.player[npc.target].active || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (!Main.player[npc.target].active || Main.player[npc.target].dead)
                {
                    npc.ai[0]++;
                    npc.velocity.Y = npc.velocity.Y + 0.11f;
                    if (npc.ai[0] >= 300)
                    {
                        npc.active = false;
                    }
                }
                else
                    npc.ai[0] = 0;
            }
            if (Main.dayTime)
            {
                npc.ai[0]++;
                npc.velocity.Y = npc.velocity.Y + 0.11f;
                if (npc.ai[0] >= 300)
                {
                    npc.active = false;
                }
            }
            #region orbitals, names & ai changes
            int moonlordBonus = NPC.downedMoonlord ? 2 : 1;
            if (reset)
            {
                moveSpeed = NPC.downedMoonlord ? 0.1f : 0.06f;
                npc.defense = baseDefense + 10 * moonlordBonus;
                npc.GivenName = "Ember";

                reset = false;
            }
            // orbitals
            if (npc.localAI[0] == 0)
            {
                int orbitalcount = Main.expertMode ? 9 : 7;
                for (int l = 0; l < orbitalcount; l++)
                {
                    //cos = y, sin = x
                    int distance = 360 / orbitalcount;
                    int orbital = NPC.NewNPC((int)(npc.Center.X + (Math.Sin(l * distance) * 150)), (int)(npc.Center.Y + (Math.Cos(l * distance) * 150)), mod.NPCType("TheCelestialMinion"), npc.whoAmI, l * distance, npc.whoAmI, npc.ai[1]);
                }
                npc.localAI[0] = 1;
            }
            // astra minions
            if (npc.localAI[1] == 1)
            {
                int orbitalcount = Main.expertMode ? 9 : 7;
                for (int l = 0; l < orbitalcount; l++)
                {
                    //cos = y, sin = x
                    int distance = 360 / orbitalcount;
                    int orbital = NPC.NewNPC((int)(npc.Center.X + (Math.Sin(l * distance) * 150)), (int)(npc.Center.Y + (Math.Cos(l * distance) * 150)), mod.NPCType("AstraMinion"), npc.whoAmI, 0, Main.rand.Next(0, 300));
                }
                npc.localAI[1] = 0;
            }
            if (npc.life <= npc.lifeMax * 0.9f && text == 0)
            {
                //Main.NewText("", new Color(255, 101, 73));
                text++;
            }
            if (npc.life <= npc.lifeMax * 0.8f && npc.ai[1] == 0)
            {
                moveSpeed = NPC.downedMoonlord ? 0.25f : 0.2f;
                npc.defense = baseDefense + 5 * moonlordBonus;
                npc.GivenName = "Nova";

                npc.ai[1]++;
                npc.localAI[0] = 0;
            }
            if (npc.life <= npc.lifeMax * 0.7f && text == 1)
            {
               // Main.NewText("", new Color(89, 110, 230));
                text++;
            }
            if (npc.life <= npc.lifeMax * 0.6f && npc.ai[1] == 1)
            {
                moveSpeed = NPC.downedMoonlord ? 0.2f : 0.15f;
                npc.defense = baseDefense;
                npc.GivenName = "Aquila";

                npc.ai[1]++;
                npc.localAI[0] = 0;
            }
            if (npc.life <= npc.lifeMax * 0.5f && text == 2)
            {
                //Main.NewText("", new Color(51, 247, 165));
                text++;
            }
            if (npc.life <= npc.lifeMax * 0.4f && npc.ai[1] == 2)
            {
                moveSpeed = NPC.downedMoonlord ? 0.2f : 0.15f;
                npc.defense = baseDefense -10 * moonlordBonus;
                projectileBaseDamage += 20 * moonlordBonus;
                npc.damage += 20;
                npc.GivenName = "Carina";

                npc.ai[1]++;
                npc.localAI[0] = 0;
            }
            if (npc.life <= npc.lifeMax * 0.2f && text == 3)
            {
                //Main.NewText("", new Color(223, 146, 244));
                text++;
            }
            if (npc.life <= npc.lifeMax * 0.2f && npc.ai[1] == 3)
            {
                moveSpeed = NPC.downedMoonlord ? 0.25f : 0.2f;
                npc.defense = baseDefense + 5 * moonlordBonus;
                projectileBaseDamage -= 20 * moonlordBonus; // return to normal
                npc.GivenName = "Astra";

                npc.ai[1]++;
                npc.localAI[1] = 1;
            }
            #endregion

            //movement
            float playerX = P.Center.X - npc.Center.X;
            float playerY = P.Center.Y - npc.Center.Y;
            if (moveAi == 0)
            {
                playerX = P.Center.X - 700f - npc.Center.X;
                if (Math.Abs(playerX) <= 20)
                {
                    moveAi = 1;
                }
            }
            if (moveAi == 1)
            {
                playerX = P.Center.X + 700f - npc.Center.X;
                if (Math.Abs(playerX) <= 20)
                {
                    moveAi = 0;
                }
            }
            Move(P, moveSpeed, playerX, playerY);
        
            npc.ai[2]--;
            // solar
            if (npc.ai[1] == 0)
            {
                if (npc.ai[2] <= 0)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 69);
                    MeteorShower(P, 11, projectileBaseDamage, Main.rand.Next(5,8));
                    npc.ai[2] = 240;
                }
            }
            // stardust
            if (npc.ai[1] == 1)
            {
                if (npc.ai[2] <= 0)
                {
                    Main.PlaySound(4, (int)npc.position.X, (int)npc.position.Y, 7, 1f, 0f);
                    StarShower(P, 8, projectileBaseDamage + 30, Main.rand.Next(3, 4));
                    npc.ai[2] = 240;
                }
            }
            // vortex
            if (npc.ai[1] == 2)
            {
                if (Main.rand.Next(150) == 0)
                {
                    int type = mod.ProjectileType("CelestialVortexPortal");
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 21);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 3), type, projectileBaseDamage, 0f, 0);
                }
            }
            // nebula
            if (npc.ai[1] == 3)
            {
                if (npc.ai[2] <= 0)
                {
                    int numberProjectiles = 3;
                    float Speed = 5f;
                    float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1)).RotatedByRandom(MathHelper.ToRadians(50));
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("CelestialNebulaPortal"), projectileBaseDamage, 0f, Main.myPlayer, 0f, 0f);
                    }
                    npc.ai[2] = 240;
                }
            }
            // astral
            if (npc.ai[1] == 4)
            {
                if (NPC.AnyNPCs(mod.NPCType("AstraMinion")))
                {
                    if (npc.ai[2] <= 0)
                    {
                        AstraShots(P, 12f, projectileBaseDamage + 35, Main.rand.Next(3, 4));
                        npc.ai[2] = Main.rand.Next(60, 180);
                    }
                    npc.immortal = true;
                    npc.dontTakeDamage = true;
                }
                else
                {
                    if (npc.ai[2] <= 0)
                    {
                        AstraShots(P, 12f, projectileBaseDamage, Main.rand.Next(3, 4));
                        npc.ai[2] =  Main.rand.Next(20, 140);
                    }
                    npc.immortal = false;
                    npc.dontTakeDamage = false;
                }
                npc.ai[3]--;
                if (npc.ai[3] <= 0)
                {
                    int attackType = Main.rand.Next(3);
                    // solar
                    if (attackType == 0)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 69);
                        MeteorShower(P, 11, projectileBaseDamage, Main.rand.Next(5, 8));
                    }
                    // stardust
                    if (attackType == 1)
                    {
                        Main.PlaySound(4, (int)npc.position.X, (int)npc.position.Y, 7, 1f, 0f);
                        StarShower(P, 8, projectileBaseDamage + 30, Main.rand.Next(3, 4));
                    }
                    // nebula
                    if (attackType == 2)
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
                    if (Main.rand.Next(150) == 0)
                    {
                        int type = mod.ProjectileType("CelestialVortexPortal");
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 21);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 3), type, projectileBaseDamage, 0f, 0);
                    }
                    npc.ai[3] = 240;
                }
                if (npc.localAI[2] == 0)
                {
                    if (P.ownedProjectileCounts[mod.ProjectileType("CelestialIllusions")] == 0)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            Projectile.NewProjectile(P.Center.X, P.Center.Y, 0f, 0f, mod.ProjectileType("CelestialIllusions"), 0, 0f, Main.myPlayer, i, 0f);
                        }
                    }
                }
            }
            int dustType = 6;
            if (npc.ai[1] == 0)
            {
                dustType = 6;
            }
            else if (npc.ai[1] == 1)
            {
                dustType = 197;
            }
            else if (npc.ai[1] == 2)
            {
                dustType = 229;
            }
            else if (npc.ai[1] == 3)
            {
                dustType = 242;
            }
            else if (npc.ai[1] == 4)
            {
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
            }
            for (int i = 0; i < 3; i++)
            {
                int dust = Dust.NewDust(npc.position, npc.width, npc.height, dustType);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1f;
                Main.dust[dust].velocity *= 0.1f;
            }
        }

        private void Move(Player P, float speed, float playerX, float playerY)
        {
            float moonlordYSpeedScale = NPC.downedMoonlord ? 0.6f : 1f;

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
                    npc.velocity.Y = npc.velocity.Y + speed;
                    if (npc.velocity.Y < 0f && playerY > 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y + speed;
                        return;
                    }
                }
                else if (npc.velocity.Y > playerY)
                {
                    npc.velocity.Y = npc.velocity.Y - speed * moonlordYSpeedScale;
                    if (npc.velocity.Y > 0f && playerY < 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y - speed * moonlordYSpeedScale;
                        return;
                    }
                }
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
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("SolarFragmentProj"), damage, 0f, 0);
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
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("CelestialStarShot"), damage, 0f, 0);
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
