using ElementsAwoken.Items.BossDrops.Azana;
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

namespace ElementsAwoken.NPCs.Bosses.Azana
{
    [AutoloadBossHead]
    public class Azana : ModNPC
    {
        private int projectileBaseDamage = 100;

        private float circleNum = 0;

        private float targetPosX = 0;
        private float targetPosY = 0;

        int text = 0;
        int dustTimer = 0;
        int stopHitTimer = 0;

        float spinAI = 0f;

        int lightTimer = 0;

        private float aiState
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float aiTimer
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float moveAI
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float shootTimer
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 1000000;
            npc.damage = 200;
            npc.defense = 100;
            npc.knockBackResist = 0f;

            npc.aiStyle = -1;

            npc.width = 150;
            npc.height = 270;

            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;

            npc.scale = 1f;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(0, 75, 0, 0);
            npc.npcSlots = 1f;
            music = MusicID.Boss4;

            NPCsGLOBAL.ImmuneAllEABuffs(npc);
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }

            bossBag = mod.ItemType("AzanaBag");

            NPCID.Sets.TrailCacheLength[npc.type] = 3;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Azana");
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 1200000;
            npc.damage = 300;
            npc.defense = 120;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 1500000;
                npc.damage = 350;
                npc.defense = 130;
            }
        }
        public override bool PreDraw(SpriteBatch spritebatch, Color lightColor)
        {
            if (npc.alpha == 0)
            {
                Texture2D tex = Main.npcTexture[npc.type];
                Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, npc.height * 0.5f);
                for (int k = 0; k < npc.oldPos.Length; k++)
                {
                    Vector2 drawPos = npc.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
                    SpriteEffects spriteEffects = npc.direction != -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                    float alpha = 1 - ((float)k / (float)npc.oldPos.Length);
                    Color color = Color.Lerp(npc.GetAlpha(lightColor), new Color(196, 58, 76), (float)k / (float)npc.oldPos.Length) * alpha;
                    spritebatch.Draw(tex, drawPos, npc.frame, color, npc.rotation, drawOrigin, npc.scale, spriteEffects, 0f);
                }
            }
            return true;
        }
        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (damage > npc.lifeMax / 2)
            {
                Main.NewText("Ah, the spores cannot withstand these godly powers...", new Color(235, 70, 106));
            }
            return base.StrikeNPC(ref damage, defense, ref knockback, hitDirection, ref crit);
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            if (aiState != 57)
            {
                npc.frameCounter += 1;
                if (npc.frameCounter > 10)
                {
                    npc.frame.Y = npc.frame.Y + frameHeight;
                    npc.frameCounter = 0.0;
                }
                if (npc.frame.Y > frameHeight * 4)
                {
                    npc.frame.Y = 0;
                }
            }
            else npc.frame.Y = frameHeight * 5;
        }
        public override void NPCLoot()
        {
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                int choice = Main.rand.Next(6);
                if (choice == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Anarchy>());
                }
                if (choice == 1)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<PurgeRifle>());
                }
                if (choice == 2)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ChaoticImpaler>());
                }
                if (choice == 3)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<GleamOfAnnhialation>());
                }
                if (choice == 4)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Pandemonium>());
                }
                if (choice == 5)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<AzanaMinionStaff>());
                }
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<EntropicCoating>());
                }
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<AzanaTrophy>());
                }
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<AzanaMask>());
                }
                //Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DiscordantOre"), Main.rand.Next(35, 80));
            }
            MyWorld.downedAzana = true;
            MyWorld.sparedAzana = false;
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = mod.ItemType("EpicHealingPotion");
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            stopHitTimer = 0;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 0, mod.ProjectileType("InfectionHeart"), 0, 0f, Main.myPlayer);
                Main.NewText("But nothing satisfies your bloodlust...", new Color(235, 70, 106));
            }
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            npc.TargetClosest(true);
            Lighting.AddLight(npc.Center, ((255 - npc.alpha) * 0.9f) / 255f, ((255 - npc.alpha) * 0.1f) / 255f, ((255 - npc.alpha) * 0f) / 255f);

            if (!P.active || P.dead)
            {
                npc.TargetClosest(true);
                if (!P.active || P.dead)
                {
                    npc.localAI[0]++;
                    npc.velocity.Y = npc.velocity.Y + 0.11f;
                    if (npc.localAI[0] >= 300) npc.active = false;
                }
                else npc.localAI[0] = 0;
            }

            if (npc.immortal)
            {
                npc.color = new Color(196, 58, 76);
                npc.alpha = 100;
            }
            else
            {
                npc.color = Color.White;
                npc.alpha = 0;
            }
            Vector2 maskCenter = new Vector2(npc.Center.X, npc.Center.Y - 73);

            bool canBeSpared = npc.life <= npc.lifeMax * 0.05f;
            if (canBeSpared) stopHitTimer++;
            bool halfLife = npc.life <= npc.lifeMax * 0.50f;
            bool lowLife = npc.life <= npc.lifeMax * 0.25f;
            if (lightTimer <= 60)
            {
                Terraria.GameContent.Events.MoonlordDeathDrama.RequestLight(1f, npc.Center);

                lightTimer++;
            }
            else
            {
                #region text & stophit
                if (Main.netMode != 2)
                {
                    MyPlayer modPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();

                    string speech = "";
                    if (npc.life <= npc.lifeMax * 0.90f && text == 0)
                    {
                        if (modPlayer.talkToAzana) speech = "NEW VESSEL";
                        else speech = "NEKTOZ AXLKA";
                        Main.NewText(speech, new Color(235, 70, 106));
                        text++;
                    }
                    if (npc.life <= npc.lifeMax * 0.75f && text == 1)
                    {
                        if (modPlayer.talkToAzana) speech = "WE HUNGER";
                        else speech = "KROLP UMULEK";
                        Main.NewText(speech, new Color(235, 70, 106));
                        text++;
                    }
                    if (npc.life <= npc.lifeMax * 0.50f && text == 2)
                    {
                        if (modPlayer.talkToAzana) speech = "YOU CAN'T RESIST";
                        else speech = "VAKEZ WAKTI";
                        Main.NewText(speech, new Color(235, 70, 106)); 
                        text++;
                    }
                    if (npc.life <= npc.lifeMax * 0.35f && text == 3)
                    {
                        if (modPlayer.talkToAzana) speech = "PAIN IS MORTAL";
                        else speech = "XANZE MOAKZ";
                        Main.NewText(speech, new Color(235, 70, 106)); 
                        text++;
                    }
                    if (npc.life <= npc.lifeMax * 0.2f && text == 4)
                    {
                        if (modPlayer.talkToAzana) speech = "DON'T Hel-p me";
                        else speech = "NOU HEHLP";
                        Main.NewText(speech, new Color(235, 70, 106));
                        text++;
                    }
                    if (npc.life <= npc.lifeMax * 0.1f && text == 5)
                    {
                        if (modPlayer.talkToAzana) speech = "End this...";
                        else speech = "ECHnd it";
                        Main.NewText(speech, new Color(235, 70, 106)); 
                        text++;
                    }
                    if (npc.life <= npc.lifeMax * 0.05f && text == 6)
                    {
                        speech = "PLEase stop...";
                        Main.NewText(speech, new Color(235, 70, 106));
                        text++;
                        stopHitTimer = 0;
                    }
                }
                if (stopHitTimer == 900)
                {
                    Main.NewText("You're... You're not attacking me?", new Color(235, 70, 106));
                }
                else if (stopHitTimer == 1800)
                {
                    Main.NewText("Maybe there is good in you...", new Color(235, 70, 106));
                }
                else if (stopHitTimer >= 2700)
                {
                    NPCLoot();
                    Main.NewText("Salvation...", new Color(235, 70, 106));
                    if (Main.netMode != NetmodeID.MultiplayerClient)Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 0, mod.ProjectileType("InfectionHeart"), 0, 0f, Main.myPlayer);

                    MyWorld.sparedAzana = true;
                    MyWorld.downedAzana = false;
                    npc.active = false;
                    if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData);
                }
                #endregion
                #region escape
                if (Vector2.Distance(P.Center, npc.Center) >= 2500)
                {
                    int dist = 500;
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    Vector2 offset = new Vector2((float)Math.Sin(angle) * dist, (float)Math.Cos(angle) * dist);
                    Teleport(P.Center.X + offset.X, P.Center.Y + offset.Y);
                    npc.netUpdate = true;
                }
                #endregion
                if (aiState == 0)
                {
                    aiTimer++;
                    Vector2 target = P.Center + new Vector2(600f * moveAI, -400);
                    if (moveAI == 0) moveAI = -1;
                    if (MathHelper.Distance(target.X, npc.Center.X) <= 20)
                    {
                        moveAI *= -1;
                    }
                    Move(P, 0.1f, target);
                    shootTimer--;
                    if (npc.life <= npc.lifeMax * 0.75f) shootTimer -= 0.5f;
                    if (npc.life <= npc.lifeMax * 0.5f) shootTimer -= 0.5f;
                    if (npc.life <= npc.lifeMax * 0.25f) shootTimer -= 0.5f;
                    if (npc.life <= npc.lifeMax * 0.1f) shootTimer -= 0.5f;
                    if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 21);
                        float speed = 18f;
                        float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), mod.ProjectileType("AzanaMiniBlast"), projectileBaseDamage, 0f, Main.myPlayer, 1);
                        shootTimer = 60;
                    }
                    if (aiTimer >= 600)
                    {
                        aiTimer = 0;
                        aiState = 1;
                        shootTimer = 0;
                    }
                }
                else if (aiState == 1)
                {
                    aiTimer++;
                    Move(P, 0.2f, P.Center);
                    shootTimer--;
                    if (npc.life <= npc.lifeMax * 0.75f) shootTimer -= 0.5f;
                    if (npc.life <= npc.lifeMax * 0.5f) shootTimer -= 0.5f;
                    if (npc.life <= npc.lifeMax * 0.25f) shootTimer -= 0.5f;
                    if (npc.life <= npc.lifeMax * 0.1f) shootTimer -= 0.5f;
                    if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/GiantLaser"));

                        float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                        float projSpeed = 18f;
                        Vector2 speed = new Vector2((float)((Math.Cos(rotation) * projSpeed) * -1), (float)((Math.Sin(rotation) * projSpeed) * -1));
                        Vector2 vector14 = speed;
                        vector14.Normalize();
                        vector14 *= 124f;

                        int numProjectiles = Main.expertMode ? MyWorld.awakenedMode ? 5 : 3 : 2;
                        for (int i = 0; i < numProjectiles; i++)
                        {
                            float num124 = (float)i - ((float)numProjectiles - 1f) / 2f;
                            Vector2 vector15 = vector14.RotatedBy((double)((Math.PI / 10) * num124), default(Vector2));

                            int num125 = Projectile.NewProjectile(npc.Center.X + vector15.X, npc.Center.Y + vector15.Y, speed.X, speed.Y, mod.ProjectileType("AzanaBeam"), projectileBaseDamage, 0f, Main.myPlayer);
                            Main.projectile[num125].noDropItem = true;
                        }
                        shootTimer = 180;
                    }
                    if (aiTimer >= 600)
                    {
                        aiTimer = 0;
                        aiState = 2;
                        shootTimer = 0;
                    }
                }
                else if (aiState == 2)
                {
                    aiTimer++;
                    shootTimer--;
                    npc.velocity *= 0.96f;
                    if (Main.expertMode)
                    {
                        npc.immortal = true;
                        npc.dontTakeDamage = true;
                    }

                    if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);

                        float speed = 18f;
                        float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), mod.ProjectileType("AzanaGiantCloud"), projectileBaseDamage, 0f, Main.myPlayer);
                        shootTimer = 3000;
                    }

                    if (aiTimer >= 600)
                    {
                        aiTimer = 0;
                        aiState = 3;
                        shootTimer = 0;
                        npc.immortal = false;
                        npc.dontTakeDamage = false;
                    }
                }
                else if (aiState == 3)
                {
                    aiTimer++;
                    Vector2 target = P.Center + new Vector2(400f * moveAI, 400 * moveAI);
                    if (moveAI == 0) moveAI = -1;
                    if (MathHelper.Distance(target.X, npc.Center.X) <= 20)
                    {
                        moveAI *= -1;
                    }
                    Move(P, 0.1f, target);
                    shootTimer++;
                    bool shoot = shootTimer % 20 == 0 && shootTimer > 0;
                    if (lowLife) shoot = shootTimer % 90 == 0;
                    if (shootTimer > 80 && !lowLife) shootTimer = -60;
                    if (shoot && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                        float speed = 8;
                        if (lowLife) speed = 2;
                        float numberProjectiles = Main.expertMode ? MyWorld.awakenedMode ? 8 : 6 : 4;
                        float rotation = MathHelper.ToRadians(360);
                        int dir = Main.rand.NextBool() ? -1 : 1;
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = Vector2.One.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * speed;

                            if (lowLife)
                            {
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("AzanaInfection"), projectileBaseDamage, 2f, Main.myPlayer, P.whoAmI);
                            }
                            else
                            {
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("AzanaMiniBlastSpiral"), projectileBaseDamage, 2f, Main.myPlayer, dir);
                            }
                        }                      
                    }
                    if (aiTimer >= 420)
                    {
                        aiTimer = 0;
                        aiState = 4;
                        shootTimer = 0;
                    }
                }
                else if (aiState == 4)
                {
                    aiTimer++;
                    int numPoints = 180;
                    Vector2 target = P.Center + new Vector2(0, 400).RotatedBy(circleNum * (Math.PI * 2f / numPoints));
                    if (Vector2.Distance(target, npc.Center) < 100) circleNum++;
                    Move(P, 0.1f, target);
                    shootTimer--;
                    if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        float numProj = 10;
                        for (int i = 0; i < numProj; i++)
                        {
                            float width = 1800;
                            float distBetween = width / numProj;
                            Vector2 pos = new Vector2(P.Center.X - width / 2 + distBetween * i, P.Center.Y + 500);
                            Projectile proj = Main.projectile[Projectile.NewProjectile(pos.X, pos.Y, 0, -24, mod.ProjectileType("AzanaInfectionPillar"), projectileBaseDamage, 0f, Main.myPlayer,0,40)];
                            proj.localAI[1] = 125;
                        }
                        shootTimer = 180;
                    }
                    if (aiTimer >= 600)
                    {
                        aiTimer = 0;
                        aiState = 5;
                        shootTimer = 0;
                    }
                }
                else if (aiState == 5)
                {
                    aiTimer++;
                    Vector2 target = P.Center + new Vector2(400f * moveAI, 400 * moveAI);
                    if (moveAI == 0) moveAI = -1;
                    if (MathHelper.Distance(target.X, npc.Center.X) <= 20)
                    {
                        moveAI *= -1;
                    }
                    Move(P, 0.1f, target);
                    shootTimer--;
                    if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        for (int i = -1; i < 2; i += 2)
                        {
                            float posX = P.Center.X + 1000 * i;
                            float posY = P.Center.Y + Main.rand.Next(-1000, 1000);
                            Projectile.NewProjectile(posX, posY, -18f * i, 0f, mod.ProjectileType("AzanaMiniBlastWave"), projectileBaseDamage, 0f, Main.myPlayer, 1f);
                        }
                        shootTimer = 20;
                    }
                    if (aiTimer >= 600)
                    {
                        aiTimer = 0;
                        aiState = 6;
                        shootTimer = 0;
                    }
                }
                else if (aiState == 6)
                {
                    aiTimer++;
                    shootTimer--;
                    Vector2 target = P.Center + new Vector2(400f * moveAI, 400 * moveAI);
                    if (moveAI == 0) moveAI = -1;
                    if (MathHelper.Distance(target.X, npc.Center.X) <= 20)
                    {
                        moveAI *= -1;
                    }
                    Move(P, 0.1f, target);
                    if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 8);
                        Projectile.NewProjectile(P.Center, Vector2.Zero, mod.ProjectileType("AzanaTarget"), projectileBaseDamage, 0f, Main.myPlayer);
                        shootTimer = 120;
                    }
                    if (aiTimer >= 600)
                    {
                        aiTimer = 0;
                        aiState = 7;
                        shootTimer = 30;

                        if (!halfLife) aiState = 50;
                        else
                        {
                            int dist = 500;
                            double angle = Main.rand.NextDouble() * 2d * Math.PI;
                            Vector2 offset = new Vector2((float)Math.Sin(angle) * dist, (float)Math.Cos(angle) * dist);
                            Teleport(P.Center.X + offset.X, P.Center.Y + offset.Y);
                        }
                    }
                }
                else if (aiState == 7)
                {
                    aiTimer++;
                    if (Main.expertMode)
                    {
                        npc.immortal = true;
                        npc.dontTakeDamage = true;
                    }
                    npc.velocity = Vector2.Zero;
                    shootTimer--;
                    if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Main.PlaySound(SoundID.Item78, npc.Center);

                        float speed = 24f;
                        float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                        Vector2 projSpeed = new Vector2((float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1));
                        projSpeed = projSpeed.RotatedByRandom(MathHelper.ToRadians(30));
                        Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center, projSpeed, mod.ProjectileType("AzanaInfectionPillar"), projectileBaseDamage, 0f, Main.myPlayer, 0f, 0f)];
                        proj.localAI[1] = 200;

                        shootTimer = 30;
                    }
                    if (aiTimer >= 600)
                    {
                        aiTimer = 0;
                        aiState = 8;
                        shootTimer = 0;
                        npc.immortal = false;
                        npc.dontTakeDamage = false;
                    }
                }
                else if (aiState == 8)
                {
                    aiTimer++;
                    Move(P, 0.2f, P.Center - new Vector2(0, 400));
                    shootTimer--;
                    if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Main.PlaySound(SoundID.Item95, npc.Center);
                        int numProj = Main.expertMode ? MyWorld.awakenedMode ? 10 : 8 : 6;
                        for (int i = 0; i < numProj; i++)
                        {
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(14, 22), mod.ProjectileType("AzanaGlob"), projectileBaseDamage, 0f, Main.myPlayer);
                        }
                        shootTimer = 60;
                    }
                    if (aiTimer >= 600)
                    {
                        aiTimer = 0;
                        aiState = 9;
                        shootTimer = 0;
                    }
                }
                else if (aiState == 9)
                {
                    aiTimer++;
                    Vector2 target = P.Center + new Vector2(600f * moveAI, -400);
                    if (moveAI == 0) moveAI = -1;
                    if (MathHelper.Distance(target.X, npc.Center.X) <= 20)
                    {
                        moveAI *= -1;
                    }
                    Move(P, 0.1f, target);
                    shootTimer++;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        if (shootTimer == 90)
                        {
                            Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/GiantLaser"));

                            Vector2 speed = new Vector2(0, 1);
                            for (int i = 0; i < 4; i++)
                            {
                                speed = speed.RotatedBy(MathHelper.ToRadians(90));
                                Projectile.NewProjectile(npc.Center + speed * 50, speed, mod.ProjectileType("AzanaBeam"), projectileBaseDamage, 0f, Main.myPlayer);
                            }
                        }
                        else if (shootTimer == 180)
                        {
                            Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/GiantLaser"));

                            Vector2 speed = new Vector2(1, 1);
                            for (int i = 0; i < 4; i++)
                            {
                                speed = speed.RotatedBy(MathHelper.ToRadians(90));
                                Projectile.NewProjectile(npc.Center + speed * 50, speed, mod.ProjectileType("AzanaBeam"), projectileBaseDamage, 0f, Main.myPlayer);
                            }
                        }
                        else if (shootTimer == 270)
                        {
                            Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/GiantLaser"));

                            Vector2 speed = new Vector2(0, 1);
                            for (int i = 0; i < 8; i++)
                            {
                                speed = speed.RotatedBy(MathHelper.ToRadians(45));
                                Projectile.NewProjectile(npc.Center + speed * 50, speed, mod.ProjectileType("AzanaBeam"), projectileBaseDamage, 0f, Main.myPlayer);
                            }
                            shootTimer = 0;
                        }
                    }
                    if (aiTimer >= 350)
                    {
                        aiTimer = 0;
                        aiState = 10;
                        shootTimer = 0;
                        if (!lowLife) aiState = 50;
                    }
                }
                else if (aiState == 10)
                {
                    aiTimer++;
                    npc.velocity = Vector2.Zero;
                    if (Main.expertMode)
                    {
                        npc.immortal = true;
                        npc.dontTakeDamage = true;
                    }
                    shootTimer++;
                    if (shootTimer > 80) shootTimer = -60;
                    if (shootTimer % 20 == 0 && shootTimer > 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Main.PlaySound(SoundID.NPCDeath13, npc.Center);

                        float shootSpeed = 8;
                        float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                        Vector2 speed = new Vector2((float)((Math.Cos(rotation) * shootSpeed) * -1), (float)((Math.Sin(rotation) * shootSpeed) * -1)).RotatedByRandom(MathHelper.ToRadians(30));

                        NPC monster = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("InfectionMouth"))];
                        monster.velocity = speed;
                    }
                    if (aiTimer >= 200 || canBeSpared)
                    {
                        aiTimer = 0;
                        aiState = 50;
                        shootTimer = 0;
                        npc.immortal = false;
                        npc.dontTakeDamage = false;
                    }

                }
                else if (aiState == 50) // tp spam
                {
                    npc.velocity = Vector2.Zero;
                    Dust dust = Main.dust[Dust.NewDust(maskCenter, npc.width, npc.height, 127, 0f, 0f, 200, default(Color), 0.5f)];
                    dust.noGravity = true;
                    dust.fadeIn = 1.3f;
                    Vector2 vector = Main.rand.NextVector2Square(-1, 1f);
                    vector.Normalize();
                    vector *= 5f;
                    dust.velocity = vector;
                    dust.position = maskCenter - vector * 25;
                    aiTimer++;

                    if (npc.life <= npc.lifeMax * 0.5f) aiTimer++;
                    if (aiTimer >= 180)
                    {
                        aiTimer = 0;
                        aiState = 51;
                        shootTimer = 0;
                    }
                }
                else if (aiState == 51)
                {
                    npc.velocity = Vector2.Zero;
                    aiTimer++;

                    shootTimer--;
                    int shootCD = 30;
                    if (npc.life <= npc.lifeMax * 0.5f) shootCD = 20;
                    if (npc.life <= npc.lifeMax * 0.1f) shootCD = 15;
                    if (shootTimer == (int)(shootCD * 0.75f))
                    {
                        int dist = 700;
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        Vector2 offset = new Vector2((float)Math.Sin(angle) * dist, (float)Math.Cos(angle) * dist);
                        Teleport(P.Center.X + offset.X, P.Center.Y + offset.Y);
                    }
                    if (shootTimer <= 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                            float speed = Main.expertMode ? MyWorld.awakenedMode ? 8 : 7 : 5;
                            float numberProjectiles = Main.expertMode ? MyWorld.awakenedMode ? 8 : 6 : 4;
                            float rotation = MathHelper.ToRadians(360);
                            int dir = Main.rand.NextBool() ? -1 : 1;
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = Vector2.One.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * speed;
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("AzanaWave"), projectileBaseDamage, 2f, Main.myPlayer, dir);
                            }
                            shootTimer = shootCD;
                        }
                    }

                    if (aiTimer >= 300)
                    {
                        aiTimer = 0;
                        aiState = 52;
                        shootTimer = 30;
                    }
                }
                else if (aiState == 52)
                {
                    npc.velocity = Vector2.Zero;
                    aiTimer++;

                    shootTimer--;
                    if (shootTimer == 20)
                    {
                        int dist = 700;
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        Vector2 offset = new Vector2((float)Math.Sin(angle) * dist, (float)Math.Cos(angle) * dist);
                        Teleport(P.Center.X + offset.X, P.Center.Y + offset.Y);
                    }
                    if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                        float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                        for (int i = 0; i < 6; i++)
                        {
                            float speed = 8 + Main.rand.NextFloat(-2, 2);
                            Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1)).RotatedByRandom(MathHelper.ToRadians(5));
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("AzanaMiniBlastAccelerate"), projectileBaseDamage - 10, 0f, 0);
                        }
                        shootTimer = 50;
                    }

                    if (aiTimer >= 300)
                    {
                        aiTimer = 0;
                        aiState = 53;
                        shootTimer = 0;
                    }
                }
                else if (aiState == 53)
                {
                    npc.velocity = Vector2.Zero;
                    aiTimer++;

                    float shootDelay = Main.expertMode ? MyWorld.awakenedMode ? 8f : 9f : 12f;
                    float moveSpeed = 10f;
                    if (halfLife) shootDelay *= 0.9f;
                    Vector2 diagSpeed = new Vector2();
                    int distance = 500;
                    //moving to top left
                    if (aiTimer == 1) Teleport(P.position.X + distance, P.position.Y + distance);
                    if (aiTimer >= 1 && aiTimer < 120)
                    {
                        npc.velocity = new Vector2(-8f, -8f);
                        diagSpeed = new Vector2(moveSpeed, -moveSpeed);
                    }
                    //moving to top right
                    if (aiTimer == 120) Teleport(P.position.X - distance, P.position.Y + distance);
                    if (aiTimer >= 120 && aiTimer < 240)
                    {
                        npc.velocity = new Vector2(8f, -8f);
                        diagSpeed = new Vector2(-moveSpeed, -moveSpeed);
                    }
                    //moving to bottom right
                    if (aiTimer == 240) Teleport(P.position.X - distance, P.position.Y - distance);
                    if (aiTimer >= 240 && aiTimer < 360)
                    {
                        npc.velocity = new Vector2(8f, 8f);
                        diagSpeed = new Vector2(moveSpeed, -moveSpeed);
                    }                    //moving to bottom left
                    if (aiTimer == 360) Teleport(P.position.X + distance, P.position.Y - distance);
                    if (aiTimer >= 360)
                    {
                        npc.velocity = new Vector2(-8f, 8f);
                        diagSpeed = new Vector2(-moveSpeed, -moveSpeed);
                    }
                    shootTimer--;
                    if (shootTimer <= 0)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 21);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, diagSpeed.X, diagSpeed.Y, mod.ProjectileType("AzanaMiniBlast"), projectileBaseDamage, 0f, Main.myPlayer);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -diagSpeed.X, -diagSpeed.Y, mod.ProjectileType("AzanaMiniBlast"), projectileBaseDamage, 0f, Main.myPlayer);
                        shootTimer = shootDelay;
                    }
                    if (aiTimer >= 480)
                    {
                        aiTimer = 0;
                        aiState = 54;
                        shootTimer = 0;
                    }
                }
                else if (aiState == 54)
                {
                    npc.velocity = Vector2.Zero;
                    aiTimer++;


                    int distance = 700;
                    double rad = aiTimer * (Math.PI / 180);
                    npc.Center = new Vector2(P.Center.X - (int)(Math.Cos(rad) * distance), P.Center.Y - (int)(Math.Sin(rad) * distance));

                    shootTimer--;
                    if (npc.life <= npc.lifeMax * 0.5f) shootTimer -= 0.5f;
                    if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                        float speed = Main.rand.NextFloat(19, 30);
                        float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), mod.ProjectileType("AzanaCloud"), projectileBaseDamage, 0f, Main.myPlayer);
                        shootTimer = 10;
                    }
                    if (aiTimer >= 180)
                    {
                        aiTimer = 0;
                        aiState = 55;
                        shootTimer = 0;
                    }
                }
                else if (aiState == 55)
                {
                    aiTimer++;
                    shootTimer++;
                    if (shootTimer == 1)
                    {
                        npc.velocity = Vector2.Zero;
                        int dist = 400;
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        Vector2 offset = new Vector2((float)Math.Sin(angle) * dist, (float)Math.Cos(angle) * dist);
                        Teleport(P.position.X + offset.X, P.position.Y + offset.Y);
                    }
                    if (shootTimer == 30)
                    {
                        float speed = 18f;
                        float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                        npc.velocity = new Vector2((float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1));
                    }
                    if (shootTimer == 90)
                    {
                        shootTimer = 0;
                    }
                
                    if (aiTimer >= 360)
                    {
                        aiTimer = 0;
                        aiState = 56;
                        shootTimer = 0;
                    }
                }
                else if (aiState == 56)
                {
                    aiTimer++;
                    int aiLength = 360;
                    int aiHalf = aiLength / 2;
                    float moveSpeed = Main.expertMode ? MyWorld.awakenedMode ? 12f : 9f : 6f;
                    float shootDelay = Main.expertMode ? MyWorld.awakenedMode ? 2f : 4f : 6f;
                    if (aiTimer == 1)
                    {
                        Teleport(P.Center.X + 900, P.Center.Y - 900);
                        targetPosX = P.Center.X - 600;
                        targetPosY = P.Center.Y - 4000;
                    }
                    if (aiTimer >= 1 && aiTimer < aiHalf)
                    {
                        npc.velocity = new Vector2(-moveSpeed, 0);
                    }
                    if (aiTimer == aiHalf)
                    {
                        Teleport(P.Center.X - 900, P.Center.Y + 900);
                        targetPosX = P.Center.X + 600;
                        targetPosY = P.Center.Y - 4000;
                    }
                    if (aiTimer >= aiHalf && aiTimer < aiLength)
                    {
                        npc.velocity = new Vector2(moveSpeed, 0);
                    }
                    shootTimer--;
                    if (shootTimer <= 0)
                    {
                        //Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 33);
                        //Main.PlaySound(SoundID.Item66, npc.Center);
                        //Main.PlaySound(SoundID.Item67, npc.Center);
                        Main.PlaySound(SoundID.Item90, npc.Center);
                        float speed = aiTimer >= aiHalf ? -5 : 5;
                        Projectile.NewProjectile(npc.Center, new Vector2(0, speed), mod.ProjectileType("AzanaBeamQuick"), projectileBaseDamage, 0f, Main.myPlayer);
                        Projectile.NewProjectile(new Vector2(targetPosX, targetPosY), new Vector2(0, 5), mod.ProjectileType("AzanaBeamQuick"), (int)( projectileBaseDamage * 1.5f), 0f, Main.myPlayer);
                        if ((P.Center.X < targetPosX && aiTimer < aiHalf) || (P.Center.X > targetPosX && aiTimer >= aiHalf))
                        {
                            Projectile.NewProjectile(new Vector2(P.Center.X, targetPosY), new Vector2(0, 5), mod.ProjectileType("AzanaBeamQuick"), (int)(projectileBaseDamage * 2f), 0f, Main.myPlayer);
                        }
                        shootTimer = shootDelay;
                    }

                    if (aiTimer >= aiLength)
                    {
                        aiTimer = 0;
                        aiState = 57;
                        shootTimer = 0;

                        int dist = 700;
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        Vector2 offset = new Vector2((float)Math.Sin(angle) * dist, (float)Math.Cos(angle) * dist);
                        Teleport(P.position.X + offset.X, P.position.Y + offset.Y);
                        Main.PlaySound(SoundLoader.customSoundType, (int)npc.position.X, (int)npc.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/NPC/AzanaPowerDown"));
                    }
                }
                else if (aiState == 57)
                {
                    aiTimer++;
                    if (npc.life <= npc.lifeMax * 0.5f) aiTimer++;
                    if (npc.life <= npc.lifeMax * 0.25f) aiTimer++;
                    npc.velocity = Vector2.Zero;
                    if (aiTimer >= 300)
                    {
                        aiTimer = 0;
                        aiState = 0;
                        shootTimer = 0;
                    }
                }
                    /*
                        npc.ai[1]++;
                        npc.ai[2]--;
                        npc.ai[3]++;

                        dustTimer--;

                    if (npc.ai[1] > 5000)
                    {
                        npc.ai[1] = 0;
                    }

                    if (stopHitTimer < 2700)
                    {
                        #region attack 1
                        if (npc.ai[1] < 1000)
                        {

                            if (halfLife && Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                npc.ai[2] -= 0.5f;
                            }

                            //movement
                            float speed = 0.2f;
                            float playerX = P.Center.X - npc.Center.X;
                            float playerY = P.Center.Y - 400f - npc.Center.Y;
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
                            Move(P, speed, playerX, playerY);

                            if (npc.ai[2] <= 0)
                            {
                                float projSpeed = 18f;
                                if (halfLife) projSpeed += 2f;
                                if (lowLife) projSpeed += 2f;
                                Blasts(P, projSpeed, projectileBaseDamage + 25);
                                npc.ai[2] = 35;
                            }

                            if (halfLife)
                            {
                                npc.ai[2]--;
                                if (lowLife)
                                {
                                    npc.ai[2]--;
                                }
                                if (npc.ai[2] <= 0)
                                {
                                    if (Main.netMode != NetmodeID.MultiplayerClient)
                                    {
                                        int distance = Main.rand.Next(200, 800);
                                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                                        Vector2 offset = new Vector2((float)Math.Sin(angle) * distance, (float)Math.Cos(angle) * distance);

                                        Teleport(P.Center.X + offset.X, P.Center.Y + offset.Y);
                                        npc.ai[2] = 180 + Main.rand.Next(0, 120);
                                        npc.netUpdate = true;
                                    }
                                }
                            }
                        }
                        #endregion
                        #region attack 2 - waves
                        if (npc.ai[1] == 1000)
                        {
                            npc.ai[3] = 0;
                        }
                        if (npc.ai[1] >= 1000 && npc.ai[1] < 1600)
                        {

                            npc.velocity.X = 0f;
                            npc.velocity.Y = 0f;

                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                if (halfLife) npc.ai[3] += 0.5f;
                                if (lowLife) npc.ai[3] += 0.5f;
                                if (MyWorld.awakenedMode) npc.ai[3] += 0.5f;
                            }
                            //teleport
                            if (npc.ai[3] >= 20 && npc.ai[0] == 0)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                {
                                    int distance = Main.rand.Next(200, 800);
                                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                                    Vector2 offset = new Vector2((float)Math.Sin(angle) * distance, (float)Math.Cos(angle) * distance);

                                    Teleport(P.Center.X + offset.X, P.Center.Y + offset.Y);
                                    npc.ai[0]++;
                                    npc.netUpdate = true;
                                }
                            }
                            if (npc.ai[3] >= 45)
                            {
                                int damage = Main.expertMode ? projectileBaseDamage + 50 : projectileBaseDamage + 25;
                                float speed = 14f;
                                if (halfLife && !lowLife)
                                {
                                    speed += 1f;
                                }
                                if (lowLife)
                                {
                                    speed -= 1f; // make them slower cuz she shoots faster
                                }
                                WaveRing(P, speed, damage);
                                npc.ai[3] = 0;
                                npc.ai[0] = 0;
                            }
                        }
                        #endregion
                        #region attack 3 - tp to the sides and shoot at player while moving up
                        if (npc.ai[1] >= 1600 && npc.ai[1] < 2000)
                        {
                            int type = mod.ProjectileType("AzanaMiniBlast");
                            float movespeed = 10f;
                            if (halfLife) movespeed += 2f;
                            if (lowLife) movespeed += 2;
                            float speed = 22f;

                            if (npc.ai[1] == 1620)
                            {
                                npc.Center = new Vector2(P.Center.X - 500, P.Center.Y + 800);
                            }
                            if (npc.ai[1] >= 1620 && npc.ai[1] < 1800)
                            {
                                npc.velocity.Y = -movespeed;
                                if (npc.ai[2] <= 0)
                                {
                                    if (Main.netMode != NetmodeID.MultiplayerClient)
                                    {
                                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 21);
                                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, speed, 0f, type, projectileBaseDamage, 0f, Main.myPlayer);
                                        npc.ai[2] = 5 + Main.rand.Next(0, 10);
                                        npc.netUpdate = true;
                                    }
                                }
                            }
                            if (npc.ai[1] == 1820)
                            {
                                npc.Center = new Vector2(P.Center.X + 500, P.Center.Y + 800);
                            }
                            if (npc.ai[1] >= 1820 && npc.ai[1] < 2000)
                            {
                                npc.velocity.Y = -movespeed;
                                if (npc.ai[2] <= 0)
                                {
                                    if (Main.netMode != NetmodeID.MultiplayerClient)
                                    {
                                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 21);
                                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -speed, 0f, type, projectileBaseDamage, 0f, Main.myPlayer);
                                        npc.ai[2] = Main.rand.Next(5, 15);
                                        if (lowLife) npc.ai[2] -= 2;
                                        npc.netUpdate = true;
                                    }
                                }
                            }
                        }
                        #endregion
                        #region attack 4 - fly around and create spikes from the sides
                        if (npc.ai[1] >= 2000 && npc.ai[1] < 2600)
                        {

                            float speed = 0.2f;
                            float playerX = P.Center.X - npc.Center.X;
                            float playerY = P.Center.Y - 500f - npc.Center.Y;
                            Move(P, speed, playerX, playerY);

                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                int randValue = lowLife ? 6 : 8;
                                if (Main.rand.Next(randValue) == 0)
                                {
                                    int damage = projectileBaseDamage - 25;
                                    EdgeSpikes(P, damage);
                                }
                                npc.netUpdate = true;
                            }
                        }
                        #endregion
                        #region attack 5 - shoot blades
                        if (npc.ai[1] >= 2600 && npc.ai[1] < 3000)
                        {
                            float speed = 0.2f;
                            float playerX = P.Center.X + 400 - npc.Center.X;
                            float playerY = P.Center.Y - npc.Center.Y;
                            Move(P, speed, playerX, playerY);

                            if (npc.ai[2] <= 0)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                {
                                    float projSpeed = halfLife ? 20f : 18f;
                                    int damage = projectileBaseDamage + 25;
                                    Blades(P, projSpeed, damage);
                                    npc.ai[2] = 10 + Main.rand.Next(0, 20);
                                    npc.netUpdate = true;
                                }
                            }
                        }
                        #endregion
                        #region attack 6 - fire in circle
                        if (npc.ai[1] >= 3000 && npc.ai[1] < 3600)
                        {
                            npc.velocity.X = 0f;
                            npc.velocity.Y = 0f;

                            if (npc.ai[1] >= 3000 && npc.ai[1] < 3120 && dustTimer <= 0)
                            {
                                int maxdusts = 20;
                                for (int i = 0; i < maxdusts; i++)
                                {
                                    float dustDistance = 100;
                                    float dustSpeed = 15;
                                    Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                                    Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                                    Dust vortex = Dust.NewDustPerfect(new Vector2(npc.Center.X, npc.Center.Y - 102) + offset, 127, velocity, 0, default(Color), 1.5f);
                                    vortex.noGravity = true;
                                }
                                dustTimer = 5;
                            }
                            if (npc.ai[1] == 3120)spinAI = 0f;
                            if (npc.ai[1] >= 3120 && npc.ai[1] < 3600)
                            {
                                // shoot in circle
                                Vector2 offset = new Vector2(400, 0);
                                float rotateSpeed = 0.027f;
                                spinAI += rotateSpeed;

                                float projSpeed = halfLife ? 17f : 14f;
                                int type = mod.ProjectileType("AzanaMiniBlast");
                                int damage = halfLife ? projectileBaseDamage : projectileBaseDamage - 20;

                                if (npc.ai[2] <= 0)
                                {
                                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                                    float numProj = 4f;
                                    float projOffset = MathHelper.ToRadians(360f) / numProj;
                                    for (int i = 0; i < numProj; i++)
                                    {
                                        Vector2 shootTarget1 = maskCenter + offset.RotatedBy(spinAI + (projOffset * (float)i));
                                        float rotation = (float)Math.Atan2(maskCenter.Y - shootTarget1.Y, maskCenter.X - shootTarget1.X);
                                        Projectile.NewProjectile(maskCenter.X, maskCenter.Y, (float)((Math.Cos(rotation) * projSpeed) * -1), (float)((Math.Sin(rotation) * projSpeed) * -1), type, damage, 0f, Main.myPlayer);
                                    }
                                    npc.ai[2] = 5;
                                }
                            }
                        }
                        #endregion
                        #region attack 7 - fly diagonally shooting blasts
                        if (npc.ai[1] == 3600)
                        {
                            npc.ai[2] = 0;
                        }
                        if (npc.ai[1] >= 3600 && npc.ai[1] < 4400)
                        {
                            float speed = halfLife ? 10f : 8f;
                            Vector2 diagSpeed = new Vector2();

                            //top left
                            if (npc.ai[3] == 1) Teleport(P.position.X + 700, P.position.Y + 700);
                            if (npc.ai[3] >= 1 && npc.ai[3] < 120)
                            {
                                npc.velocity = new Vector2(-8f,-8f);
                                diagSpeed = new Vector2(speed, -speed);
                            }
                            // top right
                            if (npc.ai[3] == 120) Teleport(P.position.X - 700, P.position.Y + 700);
                            if (npc.ai[3] >= 120 && npc.ai[3] < 240)
                            {
                                npc.velocity = new Vector2(8f, -8f);
                                diagSpeed = new Vector2(-speed, -speed);
                            }
                            // bottom left
                            if (npc.ai[3] == 240) Teleport(P.position.X + 700, P.position.Y - 700);
                            if (npc.ai[3] >= 240 && npc.ai[3] < 360)
                            {
                                npc.velocity = new Vector2(-8f, 8f);
                                diagSpeed = new Vector2(-speed, -speed);
                            }
                            // bottom right
                            if (npc.ai[3] == 360) Teleport(P.position.X - 700, P.position.Y - 700);
                            if (npc.ai[3] >= 360)
                            {
                                npc.velocity = new Vector2(8f, 8f);
                                diagSpeed = new Vector2(speed, -speed);
                            }

                            if (npc.ai[3] >= 480) npc.ai[3] = 0;
                            if (npc.ai[2] <= 0)
                            {
                                int damage = projectileBaseDamage - 60;
                                int type = mod.ProjectileType("AzanaMiniBlast");
                                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 21);
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, diagSpeed.X, diagSpeed.Y, type, damage, 0f, Main.myPlayer);
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -diagSpeed.X, -diagSpeed.Y, type, damage, 0f, Main.myPlayer);
                                npc.ai[2] = 9;
                            }
                        }
                        #endregion
                        #region attack 8 - homing fire
                        if (npc.ai[1] >= 4400 && npc.ai[1] < 5000)
                        {
                            int dust = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y + 150), 20, 20, 127, npc.oldVelocity.X * 0.5f, npc.oldVelocity.Y * 0.5f, 100, default(Color), 2f);
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].scale = 1f + Main.rand.Next(10) * 0.1f;
                            if (npc.ai[1] == 4400)
                            {
                                TeleportDust();

                                npc.position.X = P.position.X;
                                npc.position.Y = P.position.Y - 400;
                            }
                            npc.velocity.X = 0f;
                            npc.velocity.Y = 0f;
                            if (npc.ai[1] >= 4060)
                            {
                                if (npc.ai[2] <= 0)
                                {
                                    HomingFire(projectileBaseDamage - 25);
                                    npc.ai[2] = 12;
                                    if (halfLife)
                                    {
                                        npc.ai[2] -= 3;
                                    }
                                    if (lowLife)
                                    {
                                        npc.ai[2] -= 2;
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    else // not been attacked for 45 seconds
                    {
                        for (int k = 0; k < Main.npc.Length; k++)
                        {
                            NPC other = Main.npc[k];
                            if (other.type == mod.NPCType("AzanaWormHead") ||
                                other.type == mod.NPCType("AzanaWormBody") ||
                                other.type == mod.NPCType("AzanaWormTail"))
                            {
                                other.alpha += 5;
                                if (other.alpha >= 255)
                                {
                                    other.active = false;
                                }
                            }
                        }

                        float speed = 0.2f;
                        float playerX = P.Center.X - npc.Center.X;
                        float playerY = P.Center.Y - 400f - npc.Center.Y;
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
                        Move(P, speed, playerX, playerY);
                    }*/
                }
        }
        public override bool CheckActive()
        {
            return false;
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
                npc.velocity.Y = npc.velocity.Y + speed;
                if (npc.velocity.Y < 0f && desiredVelocity.Y > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y + speed;
                }
            }
            else if (npc.velocity.Y > desiredVelocity.Y)
            {
                npc.velocity.Y = npc.velocity.Y - speed;
                if (npc.velocity.Y > 0f && desiredVelocity.Y < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y - speed;
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



        private void Teleport(float posX, float posY)
        {
            // Main.PlaySound(SoundID.Item6, npc.Center.X, npc.Center.Y);
            Main.PlaySound(SoundID.Item8, npc.Center);
            npc.position.X = posX;
            npc.position.Y = posY;
            /*for (int k = 0; k < 80; k++)
            {
                int dust = Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, 127, npc.oldVelocity.X * 0.5f, npc.oldVelocity.Y * 0.5f, 100, default(Color), 2f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1f + Main.rand.Next(10) * 0.1f;
            }*/
            int numDusts = 36;
            for (int i = 0; i < numDusts; i++)
            {
                Vector2 position = Vector2.Normalize(Vector2.One * new Vector2((float)npc.width / 2f, (float)npc.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + npc.Center;
                Vector2 velocity = position - npc.Center;
                int dust = Dust.NewDust(position + velocity, 0, 0, 127, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].noLight = true;
                Main.dust[dust].velocity = Vector2.Normalize(velocity) * 9f;
            }
            npc.netUpdate = true;
        }
    }
}

