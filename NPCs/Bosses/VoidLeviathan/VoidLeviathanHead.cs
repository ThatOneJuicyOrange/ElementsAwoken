using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.VoidLeviathan
{
    [AutoloadBossHead]
    public class VoidLeviathanHead : ModNPC
    {
        public int halfLife = 0;
        public int dayTime = 0;
        public int shootTimer = 0;

        public float speed = Main.expertMode ? 40f : 35f;
        public float acceleration = Main.expertMode ? 0.8f : 0.6f;

        public int aiTimer = 0;

        public bool speech = false;
        public bool lastPrismSpeech = false;
        public int roarTimer = 120;

        bool tooFar = false;

        int projectileBaseDamage = 100;
        public override void SetDefaults()
        {
            npc.width = 48;
            npc.height = 60;

            npc.lifeMax = 850000;
            npc.damage = 350;
            npc.defense = 10;
            npc.knockBackResist = 0f;

            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.behindTiles = true;

            npc.scale = 1.1f;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;

            npc.value = Item.buyPrice(1, 50, 0, 0);
            npc.npcSlots = 1f;
            music = MusicID.LunarBoss;
            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/VoidLeviathanTheme");
            npc.netAlways = true;

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

            npc.takenDamageMultiplier = 4f;
            bossBag = mod.ItemType("VoidLeviathanBag");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Leviathan");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 1400000;
            npc.damage = 450;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 1750000;
                npc.damage = 999;
                npc.defense = 15;
            }
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.type == ProjectileID.LastPrismLaser)
            {
                damage = 20;
                if (!lastPrismSpeech)
                {
                    Main.NewText("A prism? I feast upon light!", Color.Purple.R, Color.Purple.G, Color.Purple.B);
                    lastPrismSpeech = true;
                }
            }
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidLeviathanTrophy"));
            }
            if (Main.expertMode)
            {
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidCrystal"));
                }
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }

            else
            {
                int choice = Main.rand.Next(8);
                if (choice == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidInferno"));
                }
                if (choice == 1)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EndlessAbyssBlaster"));
                }
                if (choice == 2)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ExtinctionBow"));
                }
                if (choice == 3)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Reaperstorm"));
                }
                if (choice == 4)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BladeOfTheNight"));
                }
                if (choice == 5)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CosmicWrath"));
                }
                if (choice == 6)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PikeOfEternalDespair"));
                }
                if (choice == 7)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidLeviathanAegis"));
                }

                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidLeviathanMask"));
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidLeviathanHeart"), 1);
            }
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidEssence"), Main.rand.Next(5, 15));
            if (!MyWorld.downedVoidLeviathan)
            {
                MyWorld.encounter3 = true;
                MyWorld.encounterTimer = 3600;
            }
            MyWorld.downedVoidLeviathan = true;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = mod.ItemType("SuperHealingPotion");
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = mod.GetTexture("NPCs/Bosses/VoidLeviathan/Glow/VoidLeviathanHead_Glow");
            Rectangle frame = new Rectangle(0, texture.Height * npc.frame.Y, texture.Width, texture.Height);
            Vector2 origin = frame.Size() * 0.5f;
            SpriteEffects effects = npc.direction != 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), frame, new Color(255, 255, 255, 0), npc.rotation, origin, npc.scale, effects, 0.0f);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            Main.spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(), drawColor, npc.rotation, origin, npc.scale, SpriteEffects.None, 0);
            return false;
        }
        public override bool PreAI()
        {
            Player P = Main.player[npc.target];
            bool expertMode = Main.expertMode;
            if (Main.player[npc.target].dead)
            {
                npc.velocity.Y = npc.velocity.Y + 0.11f;
                npc.ai[3]++;
                if (npc.ai[3] >= 300)
                {
                    npc.active = false;
                }
            }
            else
            {
                if (Main.dayTime)
                {
                    if (dayTime == 0)
                    {
                        Main.NewText("Times up buddy, I'll tear you apart", Color.Purple.R, Color.Purple.G, Color.Purple.B);
                        speed += 5;
                        npc.damage += 50;
                        dayTime++;
                    }
                }
                if (halfLife == 0)
                {
                    if (npc.life <= npc.lifeMax * 0.4f)
                    {
                        npc.damage += 25;
                        speed += 5;
                        Main.NewText("Ill feast upon your soul just like I did with the countless others who challenged me", Color.Purple.R, Color.Purple.G, Color.Purple.B);
                        halfLife++;
                    }
                }
                if (!speech)
                {
                    if (Main.autoPause == true)
                    {
                        Main.NewText("Using autopause huh? What a wimp", Color.LightCyan.R, Color.LightCyan.G, Color.LightCyan.B);
                    }
                    speech = true;
                }
                if (npc.localAI[1] == 0)
                {
                    int soulCount = Main.expertMode ? 7 : 5;
                    for (int l = 0; l < soulCount; l++)
                    {
                        //cos = y, sin = x
                        int distance = 360 / soulCount;
                        int orbital = NPC.NewNPC((int)(npc.Center.X + (Math.Sin(l * distance) * 150)), (int)(npc.Center.Y + (Math.Cos(l * distance) * 150)), mod.NPCType("BarrenSoul"), npc.whoAmI, l * distance, npc.whoAmI);
                    }
                    npc.localAI[1]++;
                }
                roarTimer--;
                if (roarTimer <= 0)
                {
                    Main.PlaySound(4, (int)npc.position.X, (int)npc.position.Y, 10);
                    roarTimer = 300 + Main.rand.Next(1, 400);
                }
                aiTimer++;
                if (aiTimer > 1750)
                {
                    aiTimer = 0;
                }
                shootTimer++;
                if (shootTimer >= 500)
                {
                    int numProj = Main.expertMode ? 50 : 40;
                    for (int i = 0; i < numProj; ++i)
                    {
                        Projectile.NewProjectile(P.Center.X + Main.rand.Next(-4000, 4000), P.Center.Y + 1500, 0f, -8f, mod.ProjectileType("VoidStrike"), projectileBaseDamage, 0f);
                        Projectile.NewProjectile(P.Center.X + Main.rand.Next(-4000, 4000), P.Center.Y - 1500, 0f, 8f, mod.ProjectileType("VoidStrike"), projectileBaseDamage, 0f);
                    }
                    shootTimer = 0;
                }
                #region worm stuff
                // creating the worm
                if (Main.netMode != 1)
                {
                    if (npc.ai[0] == 0)
                    {
                        npc.realLife = npc.whoAmI;
                        int latestNPC = npc.whoAmI;
                        int randomWormLength = Main.rand.Next(100, 110);
                        for (int i = 0; i < randomWormLength; ++i)
                        {
                            int body = mod.NPCType("VoidLeviathanBody");
                            if (Main.rand.Next(3) == 0) body = mod.NPCType("VoidLeviathanBodyWeak");
 
                            latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, body, npc.whoAmI, 0, latestNPC);
                            Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                            Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
                        }
                        latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("VoidLeviathanTail"), npc.whoAmI, 0, latestNPC);
                        Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                        Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;

                        npc.ai[0] = 1;
                        npc.netUpdate = true;
                    }
                }

                int minTilePosX = (int)(npc.position.X / 16.0) - 1;
                int maxTilePosX = (int)((npc.position.X + npc.width) / 16.0) + 2;
                int minTilePosY = (int)(npc.position.Y / 16.0) - 1;
                int maxTilePosY = (int)((npc.position.Y + npc.height) / 16.0) + 2;
                if (minTilePosX < 0)
                    minTilePosX = 0;
                if (maxTilePosX > Main.maxTilesX)
                    maxTilePosX = Main.maxTilesX;
                if (minTilePosY < 0)
                    minTilePosY = 0;
                if (maxTilePosY > Main.maxTilesY)
                    maxTilePosY = Main.maxTilesY;

                bool collision = false;
                // This is the initial check for collision with tiles.
                for (int i = minTilePosX; i < maxTilePosX; ++i)
                {
                    for (int j = minTilePosY; j < maxTilePosY; ++j)
                    {
                        if (Main.tile[i, j] != null && TileID.Sets.Platforms[Main.tile[i, j].type] != true && (Main.tile[i, j].nactive() && (Main.tileSolid[(int)Main.tile[i, j].type] || Main.tileSolidTop[(int)Main.tile[i, j].type] && (int)Main.tile[i, j].frameY == 0) || (int)Main.tile[i, j].liquid > 64))
                        {
                            Vector2 vector2;
                            vector2.X = (float)(i * 16);
                            vector2.Y = (float)(j * 16);
                            if (npc.position.X + npc.width > vector2.X && npc.position.X < vector2.X + 16.0 && (npc.position.Y + npc.height > (double)vector2.Y && npc.position.Y < vector2.Y + 16.0))
                            {
                                collision = true;

                                //Main.PlaySound(2, npc.position, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/EarthRumble"));
                                ElementsAwoken.screenshakeAmount = 6;

                                if (Main.rand.Next(100) == 0 && Main.tile[i, j].nactive())
                                    WorldGen.KillTile(i, j, true, true, false);
                            }
                        }
                    }
                }
                if (Vector2.Distance(npc.Center, P.Center) <= 500)
                {
                    Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                    if (Main.rand.Next(7) == 0)
                    {
                        int damage = Main.expertMode ? 45 : 60; // reduce damage in expert mode because it doubles anyway
                        Projectile.NewProjectile(vector8.X + npc.velocity.X, vector8.Y + npc.velocity.Y, npc.velocity.X * 1.5f + Main.rand.NextFloat(-0.7f, 0.7f) * 3, npc.velocity.Y * 1.5f + Main.rand.NextFloat(-0.7f, 0.7f) * 3, mod.ProjectileType("VoidLeviathanBreath"), damage, 0f, 0);
                        if (Main.rand.Next(2) == 0)
                        {
                            Main.PlaySound(SoundID.DD2_BetsyFlameBreath, npc.position);
                        }
                    }
                }

                if (Vector2.Distance(npc.Center, P.Center) <= 750)
                {
                    tooFar = false;
                }
                else
                {
                    tooFar = true;
                }
                Vector2 npcCenter = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float targetXPos = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2);
                float targetYPos = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2);

                float targetRoundedPosX = (float)((int)(targetXPos / 16.0) * 16);
                float targetRoundedPosY = (float)((int)(targetYPos / 16.0) * 16);
                npcCenter.X = (float)((int)(npcCenter.X / 16.0) * 16);
                npcCenter.Y = (float)((int)(npcCenter.Y / 16.0) * 16);
                float dirX = targetRoundedPosX - npcCenter.X;
                float dirY = targetRoundedPosY - npcCenter.Y;
                Vector2 vector168 = npc.Center;
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                // If we do not have any type of collision, we want the NPC to fall down and de-accelerate along the X axis.
                if (!collision)
                {
                    npc.TargetClosest(true);
                    npc.velocity.Y = npc.velocity.Y + 0.11f;
                    if (npc.velocity.Y > speed)
                        npc.velocity.Y = speed;
                    if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < speed * 0.4)
                    {
                        if (npc.velocity.X < 0.0)
                            npc.velocity.X = npc.velocity.X - acceleration * 1.1f;
                        else
                            npc.velocity.X = npc.velocity.X + acceleration * 1.1f;
                    }

                    else if (npc.velocity.Y == speed)
                    {
                        if (npc.velocity.X < dirX)
                            npc.velocity.X = npc.velocity.X + acceleration;
                        else if (npc.velocity.X > dirX)
                            npc.velocity.X = npc.velocity.X - acceleration;
                    }
                    else if (npc.velocity.Y > 4.0)
                    {
                        if (npc.velocity.X < 0.0)
                            npc.velocity.X = npc.velocity.X + acceleration * 0.9f;
                        else
                            npc.velocity.X = npc.velocity.X - acceleration * 0.9f;
                    }
                }
                // Else we want to play some audio (soundDelay) and move towards our target.
                if (collision || tooFar)
                {
                    if (npc.soundDelay == 0)
                    {
                        float num1 = length / 40f;
                        if (num1 < 10.0)
                            num1 = 10f;
                        if (num1 > 20.0)
                            num1 = 20f;
                        npc.soundDelay = (int)num1;
                        Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 1);
                    }
                    float absDirX = Math.Abs(dirX);
                    float absDirY = Math.Abs(dirY);
                    float newSpeed = speed / length;
                    dirX = dirX * newSpeed;
                    dirY = dirY * newSpeed;
                    if (npc.velocity.X > 0.0 && dirX > 0.0 || npc.velocity.X < 0.0 && dirX < 0.0 || (npc.velocity.Y > 0.0 && dirY > 0.0 || npc.velocity.Y < 0.0 && dirY < 0.0))
                    {
                        if (npc.velocity.X < dirX)
                            npc.velocity.X = npc.velocity.X + acceleration;
                        else if (npc.velocity.X > dirX)
                            npc.velocity.X = npc.velocity.X - acceleration;
                        if (npc.velocity.Y < dirY)
                            npc.velocity.Y = npc.velocity.Y + acceleration;
                        else if (npc.velocity.Y > dirY)
                            npc.velocity.Y = npc.velocity.Y - acceleration;
                        if (Math.Abs(dirY) < speed * 0.2 && (npc.velocity.X > 0.0 && dirX < 0.0 || npc.velocity.X < 0.0 && dirX > 0.0))
                        {
                            if (npc.velocity.Y > 0.0)
                                npc.velocity.Y = npc.velocity.Y + acceleration * 2f;
                            else
                                npc.velocity.Y = npc.velocity.Y - acceleration * 2f;
                        }
                        if (Math.Abs(dirX) < speed * 0.2 && (npc.velocity.Y > 0.0 && dirY < 0.0 || npc.velocity.Y < 0.0 && dirY > 0.0))
                        {
                            if (npc.velocity.X > 0.0)
                                npc.velocity.X = npc.velocity.X + acceleration * 2f;
                            else
                                npc.velocity.X = npc.velocity.X - acceleration * 2f;
                        }
                    }
                    else if (absDirX > absDirY)
                    {
                        if (npc.velocity.X < dirX)
                            npc.velocity.X = npc.velocity.X + acceleration * 1.1f;
                        else if (npc.velocity.X > dirX)
                            npc.velocity.X = npc.velocity.X - acceleration * 1.1f;
                        if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < speed * 0.5)
                        {
                            if (npc.velocity.Y > 0.0)
                                npc.velocity.Y = npc.velocity.Y + acceleration;
                            else
                                npc.velocity.Y = npc.velocity.Y - acceleration;
                        }
                    }
                    else
                    {
                        if (npc.velocity.Y < dirY)
                            npc.velocity.Y = npc.velocity.Y + acceleration * 1.1f;
                        else if (npc.velocity.Y > dirY)
                            npc.velocity.Y = npc.velocity.Y - acceleration * 1.1f;
                        if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < speed * 0.5)
                        {
                            if (npc.velocity.X > 0.0)
                                npc.velocity.X = npc.velocity.X + acceleration;
                            else
                                npc.velocity.X = npc.velocity.X - acceleration;
                        }
                    }
                }
                // Set the correct rotation for this NPC.
                npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;

                // Some netupdate stuff (multiplayer compatibility).
                if (collision)
                {
                    if (npc.localAI[0] != 1)
                        npc.netUpdate = true;
                    npc.localAI[0] = 1f;
                }
                else
                {
                    if (npc.localAI[0] != 0.0)
                        npc.netUpdate = true;
                    npc.localAI[0] = 0.0f;
                }
                if ((npc.velocity.X > 0.0 && npc.oldVelocity.X < 0.0 || npc.velocity.X < 0.0 && npc.oldVelocity.X > 0.0 || (npc.velocity.Y > 0.0 && npc.oldVelocity.Y < 0.0 || npc.velocity.Y < 0.0 && npc.oldVelocity.Y > 0.0)) && !npc.justHit)
                    npc.netUpdate = true;
                #endregion
            }
            return false;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.2f;
            return null;
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(mod.BuffType("ExtinctionCurse"), 300, true);
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/VoidLeviathanHead"), 1.1f);
                npc.position.X = npc.position.X + (float)(npc.width / 2);
                npc.position.Y = npc.position.Y + (float)(npc.height / 2);
                npc.width = 50;
                npc.height = 50;
                npc.position.X = npc.position.X - (float)(npc.width / 2);
                npc.position.Y = npc.position.Y - (float)(npc.height / 2);
            }
        }
    }
}
