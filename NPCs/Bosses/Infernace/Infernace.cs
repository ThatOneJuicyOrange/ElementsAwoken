using ElementsAwoken.Projectiles.GlobalProjectiles;
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

namespace ElementsAwoken.NPCs.Bosses.Infernace
{
    [AutoloadBossHead]
    public class Infernace : ModNPC
    {
        private const int tpDuration = 30;
        private int projectileBaseDamage = 37;

        private int furosiaState = 0;
        private int dropNum = 0;
        private float monsterDropAI = 0;
        private float monolithTimer = 0f;

        private float telePosX = 0;
        private float telePosY = 0;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(furosiaState);
            writer.Write(dropNum);
            writer.Write(monsterDropAI);
            writer.Write(monolithTimer);

            writer.Write(telePosX);
            writer.Write(telePosY);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            furosiaState = reader.ReadInt32();
            dropNum = reader.ReadInt32();
            monsterDropAI = reader.ReadSingle();
            monolithTimer = reader.ReadSingle();

            telePosX = reader.ReadSingle();
            telePosY = reader.ReadSingle();
        }
        private float despawnTimer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float aiTimer
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float tpAlphaChangeTimer
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

            npc.value = Item.buyPrice(0, 7, 50, 0);
            npc.npcSlots = 1f;

            music = MusicID.Plantera;

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
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("InfernaceTrophy"));
                }
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("InfernaceMask"));
                }
            }
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FireEssence"), Main.rand.Next(5, 22));
            if (!MyWorld.downedInfernace)
            {
                ElementsAwoken.encounter = 1;
                ElementsAwoken.encounterTimer = 3600;
                ElementsAwoken.DebugModeText("encounter 1 start");
            }

            // box
            if (Main.netMode != NetmodeID.MultiplayerClient)
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
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;
        }

        public override bool CheckActive()
        {
            return false;
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (npc.alpha > 100) return false;
            return base.CanHitPlayer(target, ref cooldownSlot);
        }

        public override void AI()
        {
            Player P = Main.player[npc.target];
            #region despawning
            if (!P.active || P.dead || !P.ZoneUnderworldHeight)
            {
                npc.TargetClosest(true);
                if (!P.active || P.dead || !P.ZoneUnderworldHeight)
                {
                    despawnTimer++;
                }
                else
                {
                    despawnTimer = 0;
                }
            }
            if (despawnTimer >= 300) npc.active = false;
            #endregion
            #region teleport alpha
            if (tpAlphaChangeTimer > 0)
            {
                tpAlphaChangeTimer--;
                if (tpAlphaChangeTimer > (int)(tpDuration / 2))
                {
                    npc.alpha += 26;
                }
                if (tpAlphaChangeTimer == (int)(tpDuration / 2) && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    npc.position.X = telePosX - npc.width / 2;
                    npc.position.Y = telePosY - npc.height / 2;
                    npc.netUpdate = true;
                }
                if (tpAlphaChangeTimer < (int)(tpDuration / 2))
                {
                    npc.alpha -= 26;
                    if (npc.alpha <= 0)
                    {
                        tpAlphaChangeTimer = 0;
                    }
                }
            }
            #endregion

            if ((npc.life > npc.lifeMax * 0.75f && aiTimer > 1060f) ||
                (npc.life <= npc.lifeMax * 0.75f && npc.life > npc.lifeMax * 0.45f && aiTimer > 1660f) ||
                (npc.life <= npc.lifeMax * 0.45f && aiTimer > 1900f)) aiTimer = 0f;
        
            if (npc.life <= npc.lifeMax * 0.25f && MyWorld.awakenedMode)
            {
                monolithTimer--;
                if (monolithTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 monolithPos = P.Bottom;

                    Point monolithPoint = monolithPos.ToTileCoordinates();
                    for (int j = monolithPoint.Y; j < Main.maxTilesY; j++)
                    {
                        Tile newTile = Framing.GetTileSafely(monolithPoint.X, j);
                        if (newTile.active() && Main.tileSolid[newTile.type] /*&& !TileID.Sets.Platforms[newTile.type]*/)
                        {
                            monolithPoint = new Point(monolithPoint.X, j);
                            monolithPos = new Vector2(monolithPoint.X * 16, monolithPoint.Y * 16);
                            break;
                        }
                    }
                    Projectile.NewProjectile(monolithPos.X, monolithPos.Y, 0f, 0f, mod.ProjectileType("InfernalMonolithSpawn"), projectileBaseDamage + 20, 0f, Main.myPlayer);
                    monolithTimer = (int)(MathHelper.Lerp(120, 500, (float)npc.life / (float)(npc.lifeMax * 0.25f)));
                }
            }
            if (npc.life <= npc.lifeMax * 0.65f && MyWorld.awakenedMode && dropNum == 0)
            {
                EnterDroppingAI();
                dropNum++;
            }
            if (npc.life <= npc.lifeMax * 0.15f)
            {
                bool canDrop = dropNum == 0;
                if (MyWorld.awakenedMode) canDrop = dropNum == 1;
                if (canDrop)
                {
                    Main.NewText("I will kill you in my dying breaths!", Color.Orange.R, Color.Orange.G, Color.Orange.B);
                    EnterDroppingAI();
                    dropNum++;
                }
            }
            #region furosia 
            if (P.active && Main.expertMode)
            {
                bool validLife = npc.life <= npc.lifeMax * 0.5f;
                if (MyWorld.awakenedMode) validLife = npc.life <= npc.lifeMax * 0.75f;
                if (validLife && furosiaState == 0)
                {
                    Furosia furosiaNPC = (Furosia)Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Furosia"))].modNPC;
                    //furosiaNPC.dashAI = 30;
                    Main.NewText("Furosia, help me rid of this pest!", Color.Orange.R, Color.Orange.G, Color.Orange.B);
                    furosiaState++;
                }
                if (furosiaState == 1 && !NPC.AnyNPCs(mod.NPCType("Furosia")))
                {
                    Main.NewText("My daughter... I will be your downfall, monster!", Color.Orange.R, Color.Orange.G, Color.Orange.B);
                    Main.PlaySound(4, (int)npc.position.X, (int)npc.position.Y, 10);
                    furosiaState++;
                }
            }
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
            #endregion

            if (npc.life < npc.lifeMax / 2 && npc.localAI[0] == 0 && MyWorld.awakenedMode)
            {
                NPC healer = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("HealingHearth"))];
                healer.ai[1] = npc.whoAmI;
                npc.localAI[0]++;
            }
            aiTimer++;
            shootTimer--;
            if (monsterDropAI <= 0)
            {
                npc.color = default(Color);

                float movementSpeed = Main.expertMode ? 3 : 2.5f;
                if (MyWorld.awakenedMode) movementSpeed = 3.5f;
                if (aiTimer < 700f)
                {
                    MoveDirect(P, movementSpeed);

                    int tpTimer = (int)(aiTimer - Math.Floor(aiTimer / 300f) * 300) + 1;
                    if (Main.netMode != NetmodeID.MultiplayerClient && shootTimer <= 0f)
                    {
                        Spike(P, 10f, projectileBaseDamage);
                        shootTimer = Main.expertMode ? 90 : 130f;
                        if (MyWorld.awakenedMode) shootTimer = 70;
                    }
                    //tp dust
                    int maxdusts = 20;
                    if (tpTimer >= 280f && tpTimer % 5 == 0 && !ModContent.GetInstance<Config>().lowDust)
                    {
                        for (int i = 0; i < maxdusts; i++)
                        {
                            float dustDistance = 100;
                            float dustSpeed = 10;
                            Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                            Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                            Dust vortex = Dust.NewDustPerfect(npc.Center + offset, 6, velocity, 0, default(Color), 1.5f);
                            vortex.noGravity = true;
                        }
                    }
                    //teleport
                    if (tpTimer >= 300)
                    {
                        int distance = 200 + Main.rand.Next(0, 200);
                        int choice = Main.rand.Next(4);
                        if (choice == 0) Teleport(P.position.X + distance, P.position.Y - distance);
                        else if (choice == 1) Teleport(P.position.X - distance, P.position.Y - distance);
                        else if (choice == 2) Teleport(P.position.X + distance, P.position.Y + distance);
                        else if (choice == 3) Teleport(P.position.X - distance, P.position.Y + distance);
                    }
                }

                // greek fire and fly upwards
                if (aiTimer >= 700f && aiTimer <= 1060)
                {
                    if (aiTimer == 700) shootTimer = 0;

                    if (aiTimer >= 700 + tpDuration / 2) npc.velocity = new Vector2(0, -6);

                    if (aiTimer == 700) Teleport(P.position.X + 300, P.position.Y + 300);
                    if (aiTimer == 880) Teleport(P.position.X - 300, P.position.Y + 300);
                    if (aiTimer == 1060) Teleport(P.position.X, P.position.Y - 300);
                    float projSpeedX = aiTimer < 880f ? -5f : 5f;
                    if (shootTimer <= 0f && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 13);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, projSpeedX, -1, mod.ProjectileType("InfernaceFire"), projectileBaseDamage, 0f, Main.myPlayer);
                        shootTimer = Main.rand.Next(15, 35);
                    }
                }
                if (aiTimer > 1060f && aiTimer <= 1660)
                {
                    if (aiTimer == 1061) shootTimer = 30;
                    // waves
                    npc.velocity = Vector2.Zero;
                    if (shootTimer <= 0f && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int damage = Main.expertMode ? (int)(projectileBaseDamage * 1.3f) : (int)(projectileBaseDamage * 0.8f);
                        if (MyWorld.awakenedMode) damage = (int)(projectileBaseDamage * 1.6f);

                        float speed = Main.expertMode ? 8f : 6f;
                        if (MyWorld.awakenedMode) speed = 10f;
                        Waves(P, speed, damage, 4);
                        shootTimer = Main.rand.Next(50, 80);
                        npc.netUpdate = true;
                    }
                }
                if (aiTimer > 1660)
                {
                    int tpTimer = (int)(aiTimer - 1660);
                    float speed = Main.expertMode ? 5f : 4f;
                    if (MyWorld.awakenedMode) speed = 7f;

                    if (tpTimer == 1)
                    {
                        Teleport(P.position.X + 500, P.position.Y + 500);
                    }
                    if (tpTimer >= 1 + tpDuration / 2 && tpTimer < 120)
                    {
                        npc.velocity.X = -speed;
                        npc.velocity.Y = -speed;
                    }
                    if (tpTimer == 120)
                    {
                        Teleport(P.position.X - 500, P.position.Y + 500);
                    }
                    if (tpTimer >= 120 + tpDuration / 2 && tpTimer < 240)
                    {
                        npc.velocity.X = speed;
                        npc.velocity.Y = -speed;
                    }
                }
            }
            // lava slimes and bats
            else
            {
                npc.velocity = Vector2.Zero;
                float scaleValue = (float)(Math.Sin(monsterDropAI / 17) + 1) / 2f;
                int r = 255;
                int g = (int)MathHelper.Lerp(60, 255, scaleValue);
                int b = (int)MathHelper.Lerp(20, 255, scaleValue);
                //Main.NewText(r + " " + g + " " + b);
                npc.color = new Color(r, g, b);
                monsterDropAI--;
                if (monsterDropAI > 750)
                {
                    int modNum = Main.expertMode ? 45 : 60;
                    if (MyWorld.awakenedMode) modNum = 20;
                    if (monsterDropAI % modNum == 0)
                    {
                        NPC monster = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, GetDropIDs()[Main.rand.Next(GetDropIDs().Count)])];
                        monster.velocity = new Vector2(Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f));
                        monster.SpawnedFromStatue = true; // so it doesnt drop stuff
                    }
                }
                if ((AnyDropNPCs() || monsterDropAI > 750) && monsterDropAI > 0)
                {
                    npc.immortal = true;
                    npc.dontTakeDamage = true;
                }
                else if ((!AnyDropNPCs() && monsterDropAI <= 750) || monsterDropAI <= 0)
                {
                    ExitDroppingAI();
                }

            }

            Lighting.AddLight(npc.Center, ((255 - npc.alpha) * 0.4f) / 255f, ((255 - npc.alpha) * 0.1f) / 255f, ((255 - npc.alpha) * 0f) / 255f);
            int dust = Dust.NewDust(npc.position, npc.width, npc.height, 6);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;
        }
        private List<int> GetDropIDs()
        {
            List<int> idList = new List<int>()
            {
                NPCID.LavaSlime,
                NPCID.Hellbat
            };
            return idList;
        }
        private bool AnyDropNPCs()
        {
            for (var i = 0; i < GetDropIDs().Count; i++)
            {
                if (NPC.AnyNPCs(GetDropIDs()[i])) return true;
            }
            return false;
        }
        private void ExitDroppingAI()
        {
            npc.immortal = false;
            npc.dontTakeDamage = false;
            monsterDropAI = 0;
        }
        private void EnterDroppingAI()
        {
            monsterDropAI = 1200;

            for (int k = 0; k < Main.npc.Length; k++)
            {
                NPC other = Main.npc[k];
                if (GetDropIDs().Contains(other.type))
                {
                    other.active = false;
                }
            }
        }
        private void MoveDirect(Player P, float moveSpeed)
        {
            Vector2 toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
            toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
            toTarget.Normalize();
            npc.velocity = toTarget * moveSpeed;
        }

        private void Spike(Player P, float speed, int damage)
        {
            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
            float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
            Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), mod.ProjectileType("InfernaceSpike"), damage, 0f, Main.myPlayer)];
            proj.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
        }

        private void Waves(Player P, float speed, int damage, int numberProj)
        {
            for (int k = 0; k < numberProj; k++)
            {
                Vector2 perturbedSpeed = new Vector2(speed, speed).RotatedByRandom(MathHelper.ToRadians(15));
                float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * perturbedSpeed.X) * -1), (float)((Math.Sin(rotation) * perturbedSpeed.Y) * -1), mod.ProjectileType("InfernaceWave"), damage, 0f, Main.myPlayer)];
                proj.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
            }
        }

        private void Teleport(float posX, float posY)
        {
            tpAlphaChangeTimer = tpDuration;
            telePosX = posX;
            telePosY = posY;
        }
    }
}
