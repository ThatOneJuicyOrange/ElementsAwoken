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

namespace ElementsAwoken.NPCs.Bosses.Volcanox
{
    [AutoloadBossHead]
    public class Volcanox : ModNPC
    {
        public int changeLocationTimer = 0;
        public float vectorX = 0f;
        public float vectorY = 0f;

        public float burstTimer = 10f;
        public float shootCooldown = 10f;
        public float shootTimer = 0f;
        public float minionTimer = 0f;

        public float strikeTimer = 0f;

        public int ringAttack = 0;

        public bool enraged = false;

        public int projectileBaseDamage = 100;
        public override void SetDefaults()
        {
            npc.width = 120;
            npc.height = 150;

            npc.lifeMax = 200000;
            npc.damage = 130;
            npc.defense = 75;
            npc.knockBackResist = 0f;

            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;

            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.value = Item.buyPrice(0, 15, 0, 0);
            npc.npcSlots = 1f;

            music = MusicID.Boss2;
            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/VolcanoxTheme");

            // all EA modded buffs (unless i forget to add new ones)
            npc.buffImmune[mod.BuffType("IceBound")] = true;
            npc.buffImmune[mod.BuffType("ExtinctionCurse")] = true;
            npc.buffImmune[mod.BuffType("HandsOfDespair")] = true;
            npc.buffImmune[mod.BuffType("EndlessTears")] = true;
            npc.buffImmune[mod.BuffType("AncientDecay")] = true;
            npc.buffImmune[mod.BuffType("SoulInferno")] = true;
            npc.buffImmune[mod.BuffType("DragonFire")] = true;
            npc.buffImmune[mod.BuffType("Discord")] = true;
            // all vanilla buffs
            for (int num2 = 0; num2 < 206; num2++)
            {
                npc.buffImmune[num2] = true;
            }

            bossBag = mod.ItemType("VolcanoxBag");
            npc.aiStyle = -1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Volcanox");
            Main.npcFrameCount[npc.type] = 10;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 175;
            npc.lifeMax = 350000;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 450000;
                npc.damage = 200;
                npc.defense = 90;
            }
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VolcanoxTrophy"));
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VolcanoxMask"));
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                int choice = Main.rand.Next(5);
                if (choice == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Combustia"));
                }
                if (choice == 1)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EmberBurst"));
                }
                if (choice == 2)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FatesFlame"));
                }
                if (choice == 3)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FirestarterStaff"));
                }
                if (choice == 4)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Hearth"));
                }
            }
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Pyroplasm"), Main.rand.Next(10, 60));
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VolcanicStone"), Main.rand.Next(10, 25));
            // box
            if (Main.netMode != 1)
            {
                int centerX = (int)npc.Center.X / 16;
                int centerY = (int)npc.Center.Y / 16;
                int boxWidth = npc.width / 2 / 16 + 1;
                for (int tileX = centerX - boxWidth; tileX <= centerX + boxWidth; tileX++)
                {
                    for (int tileY = centerY - boxWidth; tileY <= centerY + boxWidth; tileY++)
                    {
                        if ((tileX == centerX - boxWidth || tileX == centerX + boxWidth || tileY == centerY - boxWidth || tileY == centerY + boxWidth) && !Main.tile[tileX, tileY].active())
                        {
                            Main.tile[tileX, tileY].type = TileID.HellstoneBrick;
                            Main.tile[tileX, tileY].active(true);
                        }
                        Main.tile[tileX, tileY].lava(false);
                        Main.tile[tileX, tileY].liquid = 0;
                        if (Main.netMode == 2)
                        {
                            NetMessage.SendTileSquare(-1, tileX, tileY, 1, TileChangeType.None);
                        }
                        else
                        {
                            WorldGen.SquareTileFrame(tileX, tileY, true);
                        }
                    }
                }
            }


            MyWorld.downedVolcanox = true;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1;
            if (npc.life > npc.lifeMax * 0.5f)
            {
                if (npc.frameCounter > 6)
                {
                    npc.frame.Y = npc.frame.Y + frameHeight;
                    npc.frameCounter = 0.0;
                }
                if (npc.frame.Y > frameHeight * 4)  // so it doesnt go over
                {
                    npc.frame.Y = 0;
                }
            }
            if (npc.life < npc.lifeMax * 0.5f)
            {
                if (npc.frameCounter > 6)
                {
                    npc.frame.Y = npc.frame.Y + frameHeight;
                    npc.frameCounter = 0.0;
                }
                if (npc.frame.Y > frameHeight * 9)  // so it doesnt go over
                {
                    npc.frame.Y = frameHeight * 5;
                }
            }
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            if (!P.ZoneUnderworldHeight)
            {
                enraged = true;
            }
            if (P.ZoneUnderworldHeight)
            {
                enraged = false;
            }
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
            Lighting.AddLight(npc.Center, 2f, 2f, 2f);
            npc.TargetClosest(true);
            if (Main.netMode != 1)
            {
                int num728 = 6000;
                if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) + Math.Abs(npc.Center.Y - Main.player[npc.target].Center.Y) > (float)num728)
                {
                    npc.active = false;
                    npc.life = 0;
                    if (Main.netMode == 2)
                    {
                        NetMessage.SendData(23, -1, -1, null, npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                    }
                }
            }
            // Set the correct rotation for this NPC.
            npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;

            if (npc.localAI[0] == 0f && Main.netMode != 1)
            {
                npc.localAI[0] = 1f;
                int hook = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("VolcanoxHook"), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                hook = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("VolcanoxHook"), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                hook = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("VolcanoxHook"), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
            }
            if (npc.life <= npc.lifeMax * 0.25f && npc.localAI[2] == 0)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("SoulOfInfernace"));
                Main.NewText("You will pay for what you've done!", Color.Orange.R, Color.Orange.G, Color.Orange.B);
                npc.localAI[2]++;
            }

            #region movement
            int[] array2 = new int[3];
            float num730 = 0f;
            float num731 = 0f;
            int num732 = 0;
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                if (Main.npc[i].active && Main.npc[i].type == mod.NPCType("VolcanoxHook"))
                {
                    num730 += Main.npc[i].Center.X;
                    num731 += Main.npc[i].Center.Y;
                    array2[num732] = i;
                    num732++;
                    if (num732 > 2)
                    {
                        break;
                    }
                }
            }
            num730 /= (float)num732;
            num731 /= (float)num732;
            float num734 = 2.5f;
            float speed = 0.05f;
            float speedMultiplier = 4f;
            if (npc.life < npc.lifeMax / 2)
            {
                num734 = 5f;
                speed = 0.10f;
                npc.HitSound = SoundID.NPCHit1;
            }
            if (npc.life < npc.lifeMax / 4)
            {
                num734 = 7f;
            }
            if (Main.expertMode)
            {
                num734 += 1f;
                num734 *= 1.1f;
                speed *= 1.2f;
            }
            Vector2 vector91 = new Vector2(num730, num731);
            float targetX = Main.player[npc.target].Center.X - vector91.X;
            float targetY = Main.player[npc.target].Center.Y - vector91.Y;
            if (!P.active)
            {
                targetY *= -1f;
                targetX *= -1f;
                num734 += 8f;
            }
            float num738 = (float)Math.Sqrt((double)(targetX * targetX + targetY * targetY));
            int num739 = 500;
            if (Main.expertMode)
            {
                num739 += 150;
            }
            if (num738 >= (float)num739)
            {
                num738 = (float)num739 / num738;
                targetX *= num738;
                targetY *= num738;
            }
            num730 += targetX;
            num731 += targetY;
            vector91 = new Vector2(npc.Center.X, npc.Center.Y);
            targetX = num730 - vector91.X;
            targetY = num731 - vector91.Y;
            num738 = (float)Math.Sqrt((double)(targetX * targetX + targetY * targetY));
            if (enraged)
            {
                npc.damage += 50;
                npc.defense = 60;
                speedMultiplier += 3f;
            }
            if (num738 < num734)
            {
                targetX = npc.velocity.X;
                targetY = npc.velocity.Y;
            }
            else
            {
                num738 = num734 / num738;
                targetX *= num738 * speedMultiplier; // MULTIPLY SPEED HERE
                targetY *= num738 * speedMultiplier;
            }
            if (npc.velocity.X < targetX)
            {
                npc.velocity.X = npc.velocity.X + speed;
                if (npc.velocity.X < 0f && targetX > 0f)
                {
                    npc.velocity.X = npc.velocity.X + speed * 2f;
                }
            }
            else if (npc.velocity.X > targetX)
            {
                npc.velocity.X = npc.velocity.X - speed;
                if (npc.velocity.X > 0f && targetX < 0f)
                {
                    npc.velocity.X = npc.velocity.X - speed * 2f;
                }
            }
            if (npc.velocity.Y < targetY)
            {
                npc.velocity.Y = npc.velocity.Y + speed;
                if (npc.velocity.Y < 0f && targetY > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y + speed * 2f;
                }
            }
            else if (npc.velocity.Y > targetY)
            {
                npc.velocity.Y = npc.velocity.Y - speed;
                if (npc.velocity.Y > 0f && targetY < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y - speed * 2f;
                }
            }
            Vector2 vector92 = new Vector2(npc.Center.X, npc.Center.Y);
            float num740 = Main.player[npc.target].Center.X - vector92.X;
            float num741 = Main.player[npc.target].Center.Y - vector92.Y;
            npc.rotation = (float)Math.Atan2((double)num741, (double)num740) + 1.57f;
            #endregion

            npc.ai[2]++;
            if (npc.ai[2] >= 1500f)
            {
                npc.ai[2] = 0f;
            }
            strikeTimer--;
            if (strikeTimer <= 0)
            {
                int damage = projectileBaseDamage;
                float posX = P.Center.X + Main.rand.Next(-20, 20);
                float posY = P.Center.Y + 1000;
                Projectile.NewProjectile(posX, posY, 0f, -15f, mod.ProjectileType("VolcanicDemon"), damage, 0f);
                float posX2 = P.Center.X + Main.rand.Next(-20, 20);
                float posY2 = P.Center.Y - 1000;
                Projectile.NewProjectile(posX2, posY2, 0f, 15f, mod.ProjectileType("VolcanicDemon"), damage, 0f);
                strikeTimer = 100;
            }
            if (npc.life > npc.lifeMax / 2)
            {
                shootTimer += 1f;
                if (npc.life < npc.lifeMax * 0.8)
                {
                    shootTimer += 1f;
                }
                if (npc.life < npc.lifeMax * 0.6)
                {
                    shootTimer += 1f;
                }
                if (Main.expertMode)
                {
                    shootTimer += 1f;
                }
                if (Main.netMode != 1 && shootTimer > 80f) // firing
                {
                    if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                    {
                        float Speed = 15f;
                        int damage = projectileBaseDamage;
                        if (Main.expertMode)
                        {
                            Speed += 2f;
                        }
                        int type = mod.ProjectileType("VolcanoxBolt");
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                        float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                        int proj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                    }
                    shootTimer = 0f;
                }
            }
            else
            {
                shootCooldown--;
                burstTimer--;
                if (shootCooldown <= 0)
                {
                    shootCooldown = 80f;
                }
                if (Main.netMode != 1 && burstTimer <= 0f && shootCooldown <= 30)
                {
                    float projSpeed = 10f;
                    int damage = projectileBaseDamage - 20;
                    int type = mod.ProjectileType("VolcanoxBolt");
                    if (Main.expertMode)
                    {
                        projSpeed += 5f;
                    }
                    if (Main.rand.Next(6) == 0)
                    {
                        projSpeed += 5f;
                        damage += 25;
                        type = mod.ProjectileType("VolcanoxBlast");
                    }
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                    float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * projSpeed) * -1), (float)((Math.Sin(rotation) * projSpeed) * -1), type, damage, 0f, 0);

                    burstTimer = 6f;
                }
                if (Main.netMode != 1 && npc.ai[2] >= 750 && npc.ai[2] <= 1000)
                {
                    ringAttack--;
                    if (ringAttack <= 0)
                    {
                        int type = mod.ProjectileType("VolcanicDebris");
                        int projDamage = projectileBaseDamage;
                        float numberProjectiles = Main.expertMode ? 12 : 8;
                        float rotation = MathHelper.ToRadians(360);
                        float projSpeed = 4f;
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = new Vector2(projSpeed, projSpeed).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 2f;
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, type, projDamage, 2f, 0);
                        }
                        Main.PlaySound(29, (int)npc.position.X, (int)npc.position.Y, 93);
                        /*
                        float Speed = 8f;
                        int type = mod.ProjectileType("VolcanicDebris");
                        float spread = 45f * 0.0174f;
                        double startAngle = Math.Atan2(npc.velocity.X, npc.velocity.Y) - spread / 2;
                        double deltaAngle = spread / 8f;
                        double offsetAngle;
                        for (int i = 0; i < 4; i++)
                        {
                            offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(Math.Sin(offsetAngle) * Speed), (float)(Math.Cos(offsetAngle) * Speed), type, 60, 0f, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(-Math.Sin(offsetAngle) * Speed), (float)(-Math.Cos(offsetAngle) * Speed), type, 60, 0f, Main.myPlayer, 0f, 0f);
                        }*/
                        ringAttack = 60;
                    }
                }

                // increase stats when low health:
                npc.defense = 25;
                npc.damage = Main.expertMode ? 250 : 175;
                if (MyWorld.awakenedMode)
                {
                    npc.damage = 275;
                }
                int tentacleType = mod.NPCType("VolcanoxTentacle");
                int numTentacles = 10;
                if (Main.netMode != 1)
                {
                    if (npc.localAI[0] == 1f)
                    {
                        npc.localAI[0] = 2f;
                        for (int k = 0; k < numTentacles; k++)
                        {
                            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, tentacleType, npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                        }
                        // create extra tentacles
                        if (Main.expertMode)
                        {
                            for (int k = 0; k < 5; k++)
                            {
                                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, tentacleType, npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                            }
                        }
                    }
                }
                // SPORE TIME
                minionTimer += 1f;
                if ((double)npc.life < (double)npc.lifeMax * 0.2)
                {
                    minionTimer += 1f;
                }
                if (minionTimer >= 350f)
                {
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType("Firefly"));
                    minionTimer = 0f;
                }
            }
        }

        public override bool CheckActive()
        {
            return false;
        }
    }
}
