using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.Obsidious
{
    [AutoloadBossHead]
    public class Obsidious : ModNPC
    {
        public int phase = 1;

        /* public int dashTimer = 20;
         public int shootTimer1 = 0;
         public int shootTimer2 = 0;
         public int shootTimer3 = 0;
         public int minionTimer = 200;

         public float lasernum = 0f;
         public int ai = 1;*/
        bool spawnedHands = false;
        int aiTimer = 0;
        float spinAI = 0f;
        int shootTimer = 0;
        int shootCooldown = 0; // for a multiple burst

        int text = 0;
        bool reset = false;
        bool shielded = false;

        public int projectileBaseDamage = 50;
        public override void SetDefaults()
        {
            npc.width = 222;
            npc.height = 254;

            npc.lifeMax = 75000;
            npc.damage = 75;
            npc.defense = 55;
            npc.knockBackResist = 0f;

            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;

            //npc.scale = 1.2f;

            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;

            npc.value = Item.buyPrice(0, 20, 0, 0);
            music = MusicID.Plantera;
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

            bossBag = mod.ItemType("ObsidiousBag");
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 4;
            DisplayName.SetDefault("Obsidious");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 90;
            npc.lifeMax = 100000;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 150000;
                npc.damage = 110;
                npc.defense = 65;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (npc.ai[1] != 4)
            {
                if (npc.ai[1] == 0)
                {
                    npc.frame.Y = 0 * frameHeight;
                }
                if (npc.ai[1] == 1)
                {
                    npc.frame.Y = 1 * frameHeight;
                }
                if (npc.ai[1] == 2)
                {
                    npc.frame.Y = 2 * frameHeight;
                }
                if (npc.ai[1] == 3)
                {
                    npc.frame.Y = 3 * frameHeight;
                }
            }
            else
            {
                npc.frameCounter++;
                if (npc.frameCounter > 6)
                {
                    npc.frame.Y = npc.frame.Y + frameHeight;
                    npc.frameCounter = 0.0;
                }
                if (npc.frame.Y > frameHeight * 3)
                {
                    npc.frame.Y = 0 * frameHeight;
                }

            }
        }


        public override void NPCLoot()
        {
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ObsidiousTrophy"));
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CrystallineCluster"));
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ObsidiousWings"));
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ObsidiousMask"));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ObsidiousRobes"));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ObsidiousPants"));
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                int choice = Main.rand.Next(4);
                if (choice == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Magmarox"));
                }
                if (choice == 1)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TerreneScepter"));
                }
                if (choice == 2)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Ultramarine"));
                }
                if (choice == 3)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VioletEdge"));
                }
            }
            Main.NewText("Ah, my crystal... crumbling... You were stronger than I anticipated", new Color(188, 58, 49));
            MyWorld.downedObsidious = true;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
        public int notshieldstrengthbutok = 100;
        public override void AI()
        {
            Lighting.AddLight(npc.Center, 0.5f, 0.5f, 0.5f);
            Player P = Main.player[npc.target];
            if (!Main.player[npc.target].active || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (!Main.player[npc.target].active || Main.player[npc.target].dead)
                {
                    npc.ai[0]++;
                    npc.velocity.Y = npc.velocity.Y + 0.11f;
                    if (npc.ai[0] >= 300)
                    {
                        if (npc.ai[1] == 4)
                        {
                            Main.NewText("You put up a good fight, but alas, it had to be this way.", new Color(188, 58, 49));
                        }
                        else
                        {
                            Main.NewText("You forced me to do this.", new Color(188, 58, 49));
                        }
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
            if (shielded)
            {
                npc.immortal = true;
                npc.dontTakeDamage = true;
            }
            else
            {
                npc.immortal = false;
                npc.dontTakeDamage = false;
            }
            if (!reset)
            {
                reset = true;
                //npc.ai[1] = 1;
                //Main.NewText("At last, I am free!", new Color(93, 25, 43, 200));
                for (int k = 0; k < 50; k++)
                {
                    int dust = Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, 6, npc.oldVelocity.X * 0.5f, npc.oldVelocity.Y * 0.5f, 100, default(Color), 2f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = 1f + Main.rand.Next(10) * 0.1f;
                    int dust1 = Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, 75, npc.oldVelocity.X * 0.5f, npc.oldVelocity.Y * 0.5f, 100, default(Color), 2f);
                    Main.dust[dust1].noGravity = true;
                    Main.dust[dust1].scale = 1f + Main.rand.Next(10) * 0.1f;
                    int dust2 = Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, 135, npc.oldVelocity.X * 0.5f, npc.oldVelocity.Y * 0.5f, 100, default(Color), 2f);
                    Main.dust[dust2].noGravity = true;
                    Main.dust[dust2].scale = 1f + Main.rand.Next(10) * 0.1f;
                    int dust3 = Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, DustID.PinkFlame, npc.oldVelocity.X * 0.5f, npc.oldVelocity.Y * 0.5f, 100, default(Color), 2f);
                    Main.dust[dust3].noGravity = true;
                    Main.dust[dust3].scale = 1f + Main.rand.Next(10) * 0.1f;
                }
            }
            if (npc.life <= npc.lifeMax * 0.75f && text == 0)
            {
                Main.NewText("You put up a good fight for a mere Terrarian", new Color(188, 58, 49));
                text++;
            }
            if (npc.life <= npc.lifeMax * 0.50f && text == 1)
            {
                Main.NewText("Hah, lets see how you do against this!", new Color(188, 58, 49));
                text++;
            }
            if (npc.life <= npc.lifeMax * 0.25f && text == 2)
            {
                Main.NewText("I didnt sacrifice my life for nothing, I will obtain the core!", new Color(188, 58, 49));
                npc.ai[1] = 4;
                text++;
            }
            if (npc.life <= npc.lifeMax * 0.1f && text == 3)
            {
                Main.NewText("No... the crystal is too powerful! I can't die...", new Color(188, 58, 49));
                text++;
            }
            if (npc.life <= npc.lifeMax * 0.50f && npc.ai[1] != 4)
            {
                int dustType = 6;
                switch ((int)npc.ai[1])
                {
                    case 0:
                        dustType = 6;
                        break;
                    case 1:
                        dustType = 75;
                        break;
                    case 2:
                        dustType = 135;
                        break;
                    case 3:
                        dustType = DustID.PinkFlame;
                        break;
                    default: break;
                }
                Vector2 leftEye = new Vector2(npc.Center.X - 14, npc.Center.Y - 96);
                Vector2 rightEye = new Vector2(npc.Center.X + 14, npc.Center.Y - 96);
                int dust = Dust.NewDust(leftEye, 6, 6, dustType, npc.velocity.X * 0.5f, 12f, 100, default(Color), 2f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1f + Main.rand.Next(10) * 0.1f;
                dust = Dust.NewDust(rightEye, 6, 6, dustType, npc.velocity.X * 0.5f, 12f, 100, default(Color), 2f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1f + Main.rand.Next(10) * 0.1f;
            } 
            shootTimer--;
            shootCooldown--;
            if (shootCooldown <= 0)
            {
                shootCooldown = 80;
            }
            aiTimer++;
            //fire
            if (npc.ai[1] == 0)
            {
                if (aiTimer <= 120)
                {
                    npc.velocity = Vector2.Zero;
                    npc.ai[3] = 1;
                    if (aiTimer == 100)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y + 60, 0, 0, mod.ProjectileType("ObsidiousFirePortal"), projectileBaseDamage, 1, Main.myPlayer);
                    }
                }
                else
                {
                    Move(P, 4f);
                    npc.ai[3] = 0;
                    if (npc.life <= npc.lifeMax * 0.5f)
                    {
                        if (shootTimer == 150)
                        {
                            Projectile.NewProjectile(P.Center.X, P.Center.Y, 0, 0, mod.ProjectileType("ObsidiousTargetCrystalCenter"), 0, 0, 0, 0f, P.whoAmI); // lonng name
                        }
                        if (shootTimer <= 0)
                        {
                            for (int k = 0; k < Main.maxProjectiles; k++)
                            {
                                Projectile other = Main.projectile[k];
                                if (other.type == mod.ProjectileType("ObsidiousTargetCrystalCenter") && other.active)
                                {
                                    int numberProjectiles = 8 + Main.rand.Next(0,4);
                                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 12);
                                    float rotation = (float)Math.Atan2(npc.Center.Y - other.Center.Y, npc.Center.X - other.Center.X);
                                    for (int i = 0; i < numberProjectiles; i++)
                                    {
                                        Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * 12f) * -1), (float)((Math.Sin(rotation) * 12f) * -1)).RotatedByRandom(MathHelper.ToRadians(10));
                                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("ObsidiousFireBeam"), projectileBaseDamage + 30, 0f, Main.myPlayer, 0f, 0f);
                                    }
                                }
                            }
                            shootTimer = 300;
                        }
                    }
                }
                if (aiTimer >= 900)
                {
                    npc.ai[1]++;
                    aiTimer = 0;
                }
            }
            //earth
            if (npc.ai[1] == 1)
            {
                Move(P, 4f);
                if (aiTimer == 10)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 0, mod.ProjectileType("ObsidiousRockEffect"), projectileBaseDamage, 1, Main.myPlayer);
                }
                if (shootTimer <= 0)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 69);
                    float speed = 8f;
                    float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), mod.ProjectileType("ObsidiousRockLarge"), projectileBaseDamage + 20, 0f, 0);
                    shootTimer = Main.rand.Next(20, 60);
                }
                int rand = npc.life <= npc.lifeMax * 0.5f ? 12 : 14;
                if (Main.rand.Next(rand) == 0)
                {
                    int damage = 30;
                    float posX = npc.Center.X + Main.rand.Next(5000) - 3000;
                    float posY = npc.Center.Y + 1000;
                    Projectile.NewProjectile(posX, posY, 0f, -10f, mod.ProjectileType("ObsidiousRockNoCollide"), damage, 0f);
                }
                if (aiTimer >= 900)
                {
                    npc.ai[1]++;
                    aiTimer = 0;
                }
            }
            //ice
            if (npc.ai[1] == 2)
            {
                Move(P, 4f);
                if (Main.netMode != 1 && shootTimer <= 0 && shootCooldown <= 24)
                {
                    float speed = 6f;
                    int type = mod.ProjectileType("ObsidiousIceLaser");
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 33);

                    float rotation = (float)Math.Atan2(npc.Center.Y - 92 - P.Center.Y, npc.Center.X - 12 - P.Center.X);
                    Projectile.NewProjectile(npc.Center.X - 12, npc.Center.Y - 92, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), type, projectileBaseDamage, 0f, 0);

                    float rotation2 = (float)Math.Atan2(npc.Center.Y - 92 - P.Center.Y, npc.Center.X + 12 - P.Center.X);
                    Projectile.NewProjectile(npc.Center.X + 12, npc.Center.Y - 92, (float)((Math.Cos(rotation2) * speed) * -1), (float)((Math.Sin(rotation2) * speed) * -1), type, projectileBaseDamage, 0f, 0);
                    shootTimer = 6;
                }
                if (aiTimer >= 900)
                {
                    npc.ai[1]++;
                    aiTimer = 0;
                }
            }
            //shadowflame
            if (npc.ai[1] == 3)
            {
                Move(P, 4f);
                if (shootTimer <= 0)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 103);
                    float speed = 5f;
                    float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 80, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), mod.ProjectileType("ObsidiousHomingBall"), projectileBaseDamage + 20, 0f, 0);
                    shootTimer = Main.rand.Next(8, 40);
                }
                if (aiTimer >= 900)
                {
                    npc.ai[1] = 0;
                    aiTimer = 0;
                }
            }
            // all/beams
            if (npc.ai[1] == 4)
            {
                npc.velocity.X = 0f;
                npc.velocity.Y = 0f;
                if (aiTimer <= 300)
                {
                    shielded = true;
                    Vector2 offset = new Vector2(400, 0);
                    spinAI += 0.01f;
                    if (shootTimer <= 0)
                    {
                        int numProj = 4;
                        for (int i = 0; i < numProj; i++)
                        {
                            int damage = aiTimer <= 60 ? 0 : projectileBaseDamage;
                            float projOffset = 360 / numProj;
                            Vector2 shootTarget1 = npc.Center + offset.RotatedBy(spinAI + (projOffset * i) * (Math.PI * 2 / 8));
                            float rotation = (float)Math.Atan2(npc.Center.Y - shootTarget1.Y, npc.Center.X - shootTarget1.X);
                            int proj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * 10f) * -1), (float)((Math.Sin(rotation) * 10f) * -1), mod.ProjectileType("ObsidiousBeam"), damage, 0f, 0, 0, i);
                            Main.projectile[proj].timeLeft = aiTimer;
                        }
                        shootTimer = 4;
                    }
                }
                else
                {
                    shielded = false;
                }
                if (aiTimer >= (npc.life <= npc.lifeMax * 0.1f ? 400 : 600))
                {
                    aiTimer = 0;
                }
            }

            if (!spawnedHands && Main.netMode != 1)
            {
                npc.TargetClosest(true);
                spawnedHands = true;
                int num = NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, mod.NPCType("ObsidiousHand"), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                Main.npc[num].ai[0] = -1f;
                Main.npc[num].ai[1] = (float)npc.whoAmI;
                Main.npc[num].target = npc.target;
                Main.npc[num].netUpdate = true;
                num = NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, mod.NPCType("ObsidiousHand"), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                Main.npc[num].ai[0] = 1f;
                Main.npc[num].ai[1] = (float)npc.whoAmI;
                Main.npc[num].ai[3] = 150f; // ai timer offset so they arent exactly the same
                Main.npc[num].target = npc.target;
                Main.npc[num].netUpdate = true;
            }
            //ElementsAwoken.screenshakeAmount = 10;
        }
        private void Move(Player P, float moveSpeed)
        {
            Vector2 toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
            toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
            toTarget.Normalize();
            if (Vector2.Distance(P.Center, npc.Center) >= 30)
            {
                npc.velocity = toTarget * moveSpeed;
            }
        }
        public override bool CheckActive()
        {
            return false;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (shielded)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);

                var center = npc.Center - Main.screenPosition;
                float intensity = 0f;
                if (npc.ai[3] > 0f && npc.ai[3] <= 30f)
                {
                    intensity = 1f - npc.ai[3] / 30f;
                }
                //Filters.Scene["Nebula"].GetShader().UseIntensity(1f + intensity).UseProgress(0f);
                DrawData drawData = new DrawData(TextureManager.Load("Images/Misc/Perlin"), center - new Vector2(0, 10), new Rectangle(0, 0, 500, 500), Color.White, npc.rotation, new Vector2(250f, 250f), npc.scale * (1f + intensity * 0.05f), SpriteEffects.None, 0);
                GameShaders.Misc["ForceField"].UseColor(new Vector3(1f + intensity * 0.5f));
                GameShaders.Misc["ForceField"].Apply(drawData);
                drawData.Draw(Main.spriteBatch);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin();
                return;
            }
            /*if (npc.ai[3] > 0f)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
                var center = npc.Center - Main.screenPosition;
                float num90 = npc.ai[3] / 120f;
                float num91 = Math.Min(npc.ai[3] / 30f, 1f);
                Filters.Scene["Nebula"].GetShader().UseIntensity(Math.Min(5f, 15f * num90) + 1f).UseProgress(num90);
                DrawData drawData = new DrawData(TextureManager.Load("Images/Misc/Perlin"), center - new Vector2(0, 10), new Rectangle(0, 0, 600, 600), new Color(new Vector4(1f - (float)Math.Sqrt(num91))), npc.rotation, new Vector2(300f, 300f), npc.scale * (1f + num91), SpriteEffects.None, 0);
                GameShaders.Misc["ForceField"].UseColor(new Vector3(2f));
                GameShaders.Misc["ForceField"].Apply(drawData);
                drawData.Draw(Main.spriteBatch);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin();
                return;
            }*/
            Filters.Scene["Nebula"].GetShader().UseIntensity(0f).UseProgress(0f);
        }
    }
}
