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

namespace ElementsAwoken.NPCs.Bosses.Infernace
{
    [AutoloadBossHead]
    public class Infernace : ModNPC
    {
        public bool summonedFurosia = false;
        public bool deadFurosia = false;

        public bool droppingMonsters = false;
        public int doneDrop = 0;
        public float[] monsterDropAI = new float[2];
        public float shadowScale = 1f;
        public int shadowAlpha = 100;
        public int colourPulsate = 0;
        public bool colourIncrease = false;

        public bool createdHealer = false;

        public float shootTimer1 = 0f;
        public float shootTimer2 = 0f;
        public float monolithTimer = 0f;
        public float fireTimer = 0f;
        public float fireAITimer = 0f;
        public float tpCooldown1 = 300f;
        public float tpDustCooldown = 10f;

        public float invinceTimer = 0f;

        int moveAi = 0;
        int projectileBaseDamage = 37;

        bool runTPAlphaChange = false;
        int tpAlphaChangeTimer = 0;
        float telePosX = 0;
        float telePosY = 0;

        int contactDamage = 25;
        public override void SetDefaults()
        {
            npc.width = 120;
            npc.height = 90;

            npc.lifeMax = 8500;
            npc.damage = 25;
            npc.defense = 20;
            npc.knockBackResist = 0f;

            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;

            npc.scale = 1.3f;
            npc.HitSound = SoundID.NPCHit52;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.value = Item.buyPrice(0, 3, 0, 0);
            npc.npcSlots = 1f;

            music = MusicID.Plantera;
            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/InfernaceTheme");

            bossBag = mod.ItemType("InfernaceBag");

            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[mod.BuffType("IceBound")] = true;
            npc.buffImmune[mod.BuffType("EndlessTears")] = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infernace");
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 30;
            npc.lifeMax = 12000;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 15000;
                npc.damage = 45;
                npc.defense = 30;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (droppingMonsters)
            {
                Vector2 drawPos = npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY);
                Rectangle frame = new Rectangle(0, Main.npcTexture[npc.type].Height * npc.frame.Y, Main.npcTexture[npc.type].Width, Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type]);
                Vector2 origin = frame.Size() * 0.5f;
                SpriteEffects effects = npc.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                Color color = npc.GetAlpha(drawColor) * MathHelper.Lerp(1, 0, (float)shadowAlpha / 255f);
                spriteBatch.Draw(Main.npcTexture[npc.type], drawPos, frame, color, npc.rotation, origin, shadowScale, effects, 0f);
            }

            NPC healer = null;
            for (int k = 0; k < Main.npc.Length; k++)
            {
                NPC other = Main.npc[k];
                if (other.ai[1] == npc.whoAmI && other.active && other.type == mod.NPCType("HealingHearth"))
                {
                    healer = other;
                }
            }
            if (healer != null)
            {
                if (healer.active)
                {
                    if (Vector2.Distance(npc.Center, healer.Center) < 600)
                    {
                        Texture2D texture = ModContent.GetTexture("ElementsAwoken/NPCs/Bosses/Infernace/InfernaceHealer");

                        Vector2 position = npc.Center;
                        Vector2 mountedCenter = healer.Center;
                        Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
                        Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
                        float num1 = (float)texture.Height;
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
                                Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, Color.White, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                            }
                        }
                    }
                }
            }

            return true;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            ++npc.frameCounter;
            if (npc.frameCounter >= 30.0)
                npc.frameCounter = 0.0;
            npc.frame.Y = frameHeight * (int)(npc.frameCounter / 6.0);

            //harpy rotation
            npc.rotation = npc.velocity.X * 0.1f;
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.OnFire, 180, false);
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("InfernaceTrophy"));
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("InfernaceMask"));
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
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FireBlaster"));
                }
                if (choice == 1)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FlareSword"));
                }
                if (choice == 2)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("InfernoVortex"));
                }
                if (choice == 3)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FireHarpyStaff"));
                }
            }
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FireEssence"), Main.rand.Next(5, 22));
            if (!MyWorld.downedInfernace)
            {
                MyWorld.encounter1 = true;
                MyWorld.encounterTimer = 3600;
            }

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

            MyWorld.downedInfernace = true;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;
        }

        public override bool CheckActive()
        {
            return false;
        }

        public override void AI()
        {
            Player P = Main.player[npc.target];
            #region despawning
            if (!P.active || P.dead)
            {
                npc.TargetClosest(true);
                if (!P.active || P.dead)
                {
                    npc.localAI[0]++;
                }
            }
            if (!P.ZoneUnderworldHeight)
            {
                npc.TargetClosest(true);
                if (!P.ZoneUnderworldHeight)
                {
                    npc.localAI[0]++;
                }
            }
            if (P.active && !P.dead && P.ZoneUnderworldHeight)
            {
                npc.localAI[0] = 0;
            }
            if (npc.localAI[0] >= 300)
            {
                npc.active = false;
            }
            #endregion

            if (MyWorld.awakenedMode)
            {
                if (NPC.AnyNPCs(mod.NPCType("Furosia")))
                {
                    npc.immortal = true;
                    npc.dontTakeDamage = true;
                }
                else
                {
                    npc.immortal = false;
                    npc.dontTakeDamage = false;
                }
            }
            if (npc.localAI[1] == 0)
            {
                contactDamage = npc.damage;
                npc.localAI[1]++;
            }
            if (npc.alpha > 100)
            {
                npc.damage = 0;
            }
            else
            {
                npc.damage = contactDamage;
            }

            npc.ai[1] += 1f;
            fireTimer--;
            tpCooldown1--;
            tpDustCooldown--;
            if (shootTimer1 > 0f)
            {
                shootTimer1 -= 1f;
            }
            if (shootTimer2 > 0f)
            {
                shootTimer2 -= 1f;
            }

            if (npc.life > npc.lifeMax * 0.75f)
            {
                if (npc.ai[1] > 1060f)
                {
                    npc.ai[1] = 0f;
                }
            }
            else if (npc.life <= npc.lifeMax * 0.75f && npc.life > npc.lifeMax * 0.5f)
            {
                if (npc.ai[1] > 1660f)
                {
                    npc.ai[1] = 0f;
                }
            }
            else if (npc.life <= npc.lifeMax * 0.45f)
            {
                if (npc.ai[1] > 1900f)
                {
                    npc.ai[1] = 0f;
                }
            }
            if (npc.life <= npc.lifeMax * 0.25f && MyWorld.awakenedMode)
            {
                monolithTimer--;
                if (monolithTimer <= 0)
                {
                    Vector2 monolithPos = P.Bottom;

                    Point monolithPoint = monolithPos.ToTileCoordinates();
                    Tile monolithTile = Framing.GetTileSafely((int)monolithPoint.X, (int)monolithPoint.Y);
                    for (int j = monolithPoint.Y; j < Main.maxTilesY; j++)
                    {
                        Tile newTile = Framing.GetTileSafely(monolithPoint.X, j);
                        if (newTile.active() && Main.tileSolid[newTile.type] && !TileID.Sets.Platforms[newTile.type])
                        {
                            monolithTile = newTile;
                            monolithPoint = new Point(monolithPoint.X, j);
                            monolithPos = new Vector2(monolithPoint.X * 16, monolithPoint.Y * 16);
                            break;
                        }
                    }
                    Projectile.NewProjectile(monolithPos.X, monolithPos.Y, 0f, 0f, mod.ProjectileType("InfernalMonolithSpawn"), projectileBaseDamage + 20, 0f, 0);
                    monolithTimer = (int)(MathHelper.Lerp(120, 500, (float)npc.life / (float)(npc.lifeMax * 0.25f)));
                }
            }
            if (npc.life <= npc.lifeMax * 0.65f && MyWorld.awakenedMode && doneDrop == 0)
            {
                doneDrop++;

                droppingMonsters = true;
                monsterDropAI[0] = 0;
                monsterDropAI[1] = 0;
            }
            if (npc.life <= npc.lifeMax * 0.15f)
            {
                bool canDrop = doneDrop == 0;
                if (MyWorld.awakenedMode) canDrop = doneDrop == 1;
                if (canDrop)
                {
                    Main.NewText("I will kill you in my dying breaths!", Color.Orange.R, Color.Orange.G, Color.Orange.B);
                    doneDrop++;

                    droppingMonsters = true;
                    monsterDropAI[0] = 0;
                    monsterDropAI[1] = 0;
                }
            }
            #region furosia summoning
            if (P.active && Main.expertMode)
            {
                bool validLife = npc.life <= npc.lifeMax * 0.5f;
                if (MyWorld.awakenedMode) validLife = npc.life <= npc.lifeMax * 0.75f;
                if (validLife && !summonedFurosia)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Furosia"));
                    Main.NewText("Furosia, help me rid of this pest!", Color.Orange.R, Color.Orange.G, Color.Orange.B);
                    summonedFurosia = true;
                }
                if (!deadFurosia && summonedFurosia && !NPC.AnyNPCs(mod.NPCType("Furosia")))
                {
                    Main.NewText("My daughter... I will be your downfall, monster!", Color.Orange.R, Color.Orange.G, Color.Orange.B);
                    Main.PlaySound(4, (int)npc.position.X, (int)npc.position.Y, 10);
                    deadFurosia = true;
                }
            }
            #endregion
            #region teleport alpha
            if (runTPAlphaChange)
            {
                tpAlphaChangeTimer++;
                if (tpAlphaChangeTimer < 20)
                {
                    npc.alpha += 15;
                }
                if (tpAlphaChangeTimer == 20)
                {
                    npc.position.X = telePosX;
                    npc.position.Y = telePosY;
                }
                if (tpAlphaChangeTimer > 20)
                {
                    npc.alpha -= 15;                    
                    if (npc.alpha <= 0)
                    {
                        runTPAlphaChange = false;
                    }
                }
            }
            else
            {
                npc.alpha = 0;
            }
            if (npc.alpha < 0)
            {
                npc.alpha = 0;
            }
            if (npc.alpha > 255)
            {
                npc.alpha = 255;
            }
            #endregion
            if (npc.life < npc.lifeMax / 2 && !createdHealer && MyWorld.awakenedMode)
            {
                NPC healer = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("HealingHearth"))];
                healer.ai[1] = npc.whoAmI;
                createdHealer = true;
            }
            //droppingMonsters = true;
            if (!droppingMonsters)
            {
                if (npc.life > npc.lifeMax * 0.25f)
                {
                    float movementSpeed = 2.5f;
                    if (Main.expertMode) movementSpeed = 3f;
                    if (MyWorld.awakenedMode) movementSpeed = 3.5f;
                    MoveDirect(P, movementSpeed);
                    if (npc.ai[1] < 700f)
                    {
                        if (Main.netMode != 1 && shootTimer1 == 0f)
                        {
                            Spike(P, 10f, projectileBaseDamage);
                            shootTimer1 = 130f;
                        }
                        //tp dust
                        int maxdusts = 20;
                        if (tpCooldown1 <= 20f && tpDustCooldown <= 0)
                        {
                            for (int i = 0; i < maxdusts; i++)
                            {
                                float dustDistance = 100;
                                float dustSpeed = 10;
                                Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                                Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                                Dust vortex = Dust.NewDustPerfect(npc.Center + offset, 6, velocity, 0, default(Color), 1.5f);
                                vortex.noGravity = true;

                                tpDustCooldown = 5;
                            }
                        }
                        //teleport
                        if (tpCooldown1 <= 0f)
                        {
                            int distance = 200 + Main.rand.Next(0, 200);
                            int choice = Main.rand.Next(4);
                            if (choice == 0)
                            {
                                Teleport(Main.player[npc.target].position.X + distance, Main.player[npc.target].position.Y - distance);
                            }
                            if (choice == 1)
                            {
                                Teleport(Main.player[npc.target].position.X - distance, Main.player[npc.target].position.Y - distance);
                            }
                            if (choice == 2)
                            {
                                Teleport(Main.player[npc.target].position.X + distance, Main.player[npc.target].position.Y + distance);
                            }
                            if (choice == 3)
                            {
                                Teleport(Main.player[npc.target].position.X - distance, Main.player[npc.target].position.Y + distance);
                            }
                            tpCooldown1 = 300f;
                        }
                    }
                    if (npc.ai[1] == 700f)
                    {
                        fireAITimer = 0f;
                    }
                    // greek fire and fly upwards
                    if (npc.ai[1] >= 700f && npc.ai[1] <= 1060)
                    {
                        fireAITimer++;
                        npc.velocity.X = 0;
                        npc.velocity.Y = -6;
                        if (fireAITimer == 1f)
                        {
                            Teleport(Main.player[npc.target].position.X + 300, Main.player[npc.target].position.Y + 200);
                        }
                        if (fireAITimer == 180f)
                        {
                            Teleport(Main.player[npc.target].position.X - 300, Main.player[npc.target].position.Y + 200);
                        }
                        float projSpeedX = fireAITimer < 180f ? -5f : 5f;
                        if (fireAITimer >= 20 && fireTimer <= 0f && Main.netMode != 1)
                        {
                            int type = mod.ProjectileType("InfernaceFire");
                            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 13);
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, projSpeedX, -1, type, projectileBaseDamage, 0f, 0);
                            fireTimer = 10f + Main.rand.Next(0, 15);
                        }
                    }
                    if (npc.ai[1] > 1060f && npc.ai[1] <= 1660)
                    {
                        if (npc.ai[1] == 1070f)
                        {
                            Teleport(Main.player[npc.target].position.X, Main.player[npc.target].position.Y - 250);
                        }
                        // waves
                        if (Main.netMode != 1)
                        {
                            npc.velocity *= 0;
                            if (shootTimer2 == 0f)
                            {
                                Waves(P, 10f, projectileBaseDamage - 5, 4);
                                shootTimer2 = 50 + Main.rand.Next(0, 30);
                            }
                        }
                    }
                    if (npc.ai[1] == 1660)
                    {
                        npc.ai[2] = 0;
                    }
                    if (npc.ai[1] > 1660)
                    {
                        float speed = 8f;
                        float speedX = 0f;
                        float speedY = 0f;

                        npc.ai[2]++;
                        if (npc.ai[2] == 1)
                        {
                            Teleport(Main.player[npc.target].position.X + 500, Main.player[npc.target].position.Y + 500);
                        }
                        if (npc.ai[2] >= 1 && npc.ai[2] < 140)
                        {
                            npc.velocity.X = -8f;
                            npc.velocity.Y = -8f;

                            speedX = speed;
                            speedY = -speed;
                        }
                        if (npc.ai[2] == 120)
                        {
                            Teleport(Main.player[npc.target].position.X - 500, Main.player[npc.target].position.Y + 500);
                        }
                        if (npc.ai[2] >= 140 && npc.ai[2] < 240)
                        {
                            npc.velocity.X = 8f;
                            npc.velocity.Y = -8f;

                            speedX = -speed;
                            speedY = -speed;
                        }
                    }
                }
            }
            #region Last Breaths
            if (droppingMonsters)
            {
                invinceTimer++;
                npc.velocity = Vector2.Zero;
                if (colourIncrease)
                {
                    colourPulsate++;
                    if (colourPulsate >= 60)
                    {
                        colourIncrease = false;
                    }
                }
                else
                {
                    colourPulsate--;
                    if (colourPulsate <= 0)
                    {
                        colourIncrease = true;
                    }
                }
                int r = 255;
                int g = (int)MathHelper.Lerp(60, 255, (float)colourPulsate / 60f);
                int b = (int)MathHelper.Lerp(20, 255, (float)colourPulsate / 60f);
                npc.color = new Color(r, g, b);
                if (monsterDropAI[1] == 0)
                {
                    for (int k = 0; k < Main.npc.Length; k++)
                    {
                        NPC other = Main.npc[k];
                        if (other.type == NPCID.Hellbat || other.type == NPCID.LavaSlime)
                        {
                            other.active = false;
                        }
                    }
                }
                monsterDropAI[0]--;
                monsterDropAI[1]++;
                if (monsterDropAI[1] < 450)
                {
                    if (monsterDropAI[0] <= 0)
                    {
                        int type = Main.rand.Next(2) == 0 ? NPCID.Hellbat : NPCID.LavaSlime;
                        NPC monster = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, type)];
                        monster.velocity = new Vector2(Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f));

                        monsterDropAI[0] = 45;
                    }
                }
                shadowAlpha += 5;
                shadowScale += 0.02f;
                if (shadowAlpha >= 255)
                {
                    shadowAlpha = 100;
                    shadowScale = 1f;
                }
                if ((NPC.AnyNPCs(NPCID.Hellbat) || NPC.AnyNPCs(NPCID.LavaSlime) || monsterDropAI[1] <= 450) && invinceTimer < 1200)
                {
                    npc.immortal = true;
                    npc.dontTakeDamage = true;
                }
                else if ((!NPC.AnyNPCs(NPCID.Hellbat) && !NPC.AnyNPCs(NPCID.LavaSlime) && monsterDropAI[1] >= 450) || invinceTimer >=  1200)
                {
                    droppingMonsters = false;
                }
            }
            else
            {
                npc.color = default(Color);
            }
            /*if (npc.life <= npc.lifeMax * 0.25f)
            {
                //movement
                float speed = 0.075f;
                float playerX = P.Center.X - npc.Center.X;
                float playerY = P.Center.Y - 150f - npc.Center.Y;
                if (moveAi == 0)
                {
                    playerX = P.Center.X - 400f - npc.Center.X;
                    if (Math.Abs(P.Center.X - 400f - npc.Center.X) <= 20)
                    {
                        moveAi = 1;
                    }
                }
                if (moveAi == 1)
                {
                    playerX = P.Center.X + 400f - npc.Center.X;
                    if (Math.Abs(P.Center.X + 400f - npc.Center.X) <= 20)
                    {
                        moveAi = 0;
                    }
                }
                Move(P, speed, playerX, playerY);
                //left and right
                float strikeSpeed = 10f;
                if (Main.rand.Next(8) == 0)
                {
                    int damage = 30;
                    float posX = P.Center.X + 1000;
                    float posY = P.Center.Y + Main.rand.Next(5000) - 3000;
                    Projectile.NewProjectile(posX, posY, -strikeSpeed, 0f, mod.ProjectileType("InfernaceStrike"), damage, 0f);
                    float posX2 = P.Center.X - 1000;
                    float posY2 = P.Center.Y + Main.rand.Next(5000) - 3000;
                    Projectile.NewProjectile(posX2, posY2, strikeSpeed, 0f, mod.ProjectileType("InfernaceStrike"), damage, 0f);
                }
                //up and down
                if (Main.rand.Next(8) == 0)
                {
                    int damage = 30;
                    float posX = P.Center.X + Main.rand.Next(5000) - 3000;
                    float posY = P.Center.Y + 1000;
                    Projectile.NewProjectile(posX, posY, 0f, -strikeSpeed, mod.ProjectileType("InfernaceStrike"), damage, 0f);
                    float posX2 = P.Center.X + Main.rand.Next(5000) - 3000;
                    float posY2 = P.Center.Y + 1000;
                    Projectile.NewProjectile(posX2, posY2, 0f, strikeSpeed, mod.ProjectileType("InfernaceStrike"), damage, 0f);
                }
            }*/
            #endregion

            Lighting.AddLight(npc.Center, ((255 - npc.alpha) * 0.4f) / 255f, ((255 - npc.alpha) * 0.1f) / 255f, ((255 - npc.alpha) * 0f) / 255f);
            int dust = Dust.NewDust(npc.position, npc.width, npc.height, 6);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;

        }
        private void MoveDirect(Player P, float moveSpeed)
        {
            Vector2 toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
            toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
            toTarget.Normalize();
            npc.velocity = toTarget * moveSpeed;
        }

        private void Move(Player P, float speed, float playerX, float playerY)
        {
            int maxDist = 1500;
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
                    npc.velocity.Y = npc.velocity.Y - speed;
                    if (npc.velocity.Y > 0f && playerY < 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y - speed;
                        return;
                    }
                }
            }
        }

        private void Spike(Player P, float speed, int damage)
        {
            int type = mod.ProjectileType("InfernaceSpike");
            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
            float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), type, damage, 0f, 0);
        }

        private void Waves(Player P, float speed, int damage, int numberProj)
        {
            for (int k = 0; k < numberProj; k++)
            {
                Vector2 perturbedSpeed = new Vector2(speed, speed).RotatedByRandom(MathHelper.ToRadians(15));
                Vector2 vector8 = new Vector2(npc.Center.X - 46, npc.Center.Y - 69);
                float rotation = (float)Math.Atan2(vector8.Y - P.Center.Y, vector8.X - P.Center.X);
                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * perturbedSpeed.X) * -1), (float)((Math.Sin(rotation) * perturbedSpeed.Y) * -1), mod.ProjectileType("InfernaceWave"), damage, 0f, Main.myPlayer);
            }
        }

        private void Teleport(float posX, float posY)
        {
            runTPAlphaChange = true;
            tpAlphaChangeTimer = 0;
            telePosX = posX;
            telePosY = posY;
        }
    }
}
