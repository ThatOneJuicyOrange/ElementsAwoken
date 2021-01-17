using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Projectiles.NPCProj;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Audio;

namespace ElementsAwoken.NPCs.VolcanicPlateau.Sulfur
{
    public class Erius : ModNPC
    {
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
        private float damageTaken
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float startAI
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public override void SetDefaults()
        {
            npc.width = 124;
            npc.height = 52;

            npc.aiStyle = -1;

            npc.defense = 20;
            npc.lifeMax = 5000;
            npc.damage = 30;
            npc.knockBackResist = 0f;

            npc.HitSound = SoundID.NPCHit29;
            npc.DeathSound = SoundID.NPCDeath22;

            npc.value = Item.buyPrice(0, 0, 2, 0);
            npc.knockBackResist = 0f;
            npc.gfxOffY = -4f;

            npc.scale *= 1.5f;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.boss = true;

            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffType<Buffs.Debuffs.AcidBurn>()] = true;

            //npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
            npc.GetGlobalNPC<PlateauNPCs>().tomeText = "Erius, the Lost Weaverqueen. She is the queen of The Silkborn living in the silken caverns and has killed many unwary adventurers. After the fall of her queendom, she lost a great deal of her power but is still a force to be reckoned with. Beware her acidic attacks.”";
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Erius");
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void NPCLoot()
        {
            //if (Main.rand.NextBool())Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<Items.Placeable.Tiles.Plateau.SulfuricSedimentItem>(), Main.rand.Next(1, 3));
            MyWorld.downedErius = true;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (aiState == 0) damageTaken += (float)damage;
        }
        public override bool CheckDead()
        {
            if (aiTimer >= 0)
            {
                npc.ai[0] = 0;
                aiTimer = -300;
                npc.damage = 0;
                npc.life = npc.lifeMax;
                npc.dontTakeDamage = true;
                npc.netUpdate = true;
                return false;
            }
            return true;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Rectangle frame = new Rectangle(0, npc.frame.Y, texture.Width, texture.Height / Main.npcFrameCount[npc.type]);
            Vector2 origin = new Vector2(texture.Width * 0.5f, (texture.Height / Main.npcFrameCount[npc.type]) * 0.5f);
            SpriteEffects effects = npc.spriteDirection == -1 ? (npc.directionY == -1 ? SpriteEffects.FlipVertically : SpriteEffects.None) : (npc.directionY == -1 ? SpriteEffects.FlipVertically | SpriteEffects.FlipHorizontally : SpriteEffects.FlipHorizontally);
            //SpriteEffects effects = npc.directionY == -1 ? SpriteEffects.FlipVertically : SpriteEffects.None;
            spriteBatch.Draw(texture, npc.position - Main.screenPosition + new Vector2(0, npc.gfxOffY) + new Vector2(0, 4), frame, drawColor, npc.rotation, Vector2.Zero, npc.scale, effects, 0.0f);
            return false;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += (int)Math.Abs(npc.velocity.X);
            if (Math.Abs(npc.velocity.Y) < 0.02f)
            {
                if (npc.frameCounter > 15)
                {
                    npc.frame.Y = npc.frame.Y + frameHeight;
                    npc.frameCounter = 0.0;
                }
                if (npc.frame.Y > frameHeight * 3)
                {
                    npc.frame.Y = 0;
                }
            }
            else
            {
                npc.frame.Y = frameHeight * 4;
            }
        }
        public override void AI()
        {
            if (aiTimer < 0)
            {
                npc.noGravity = false;
                if (Math.Abs(npc.velocity.Y) < 0.05f && aiTimer > -290) npc.ai[0] = 1;
                if (npc.ai[0] == 0)
                {
                   // npc.velocity.Y = 5;
                    npc.ai[2] = npc.Center.X;
                    npc.ai[3] = npc.Center.Y;
                }
                else
                {
                    npc.velocity = Vector2.Zero;
                    float strength = (1 - (aiTimer / -300)) * 5;
                    npc.Center = new Vector2(npc.ai[2] + Main.rand.NextFloat(-strength, strength), npc.ai[3] + Main.rand.NextFloat(-strength, strength));
                }
                npc.velocity.X = 0;
                aiTimer++;


                if (aiTimer == -1)
                {
                    npc.life = 0;
                    npc.HitEffect(0, 0);
                    npc.checkDead();
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int num = Main.expertMode ? MyWorld.awakenedMode ? 6 : 4 : 3;
                        for (int i = 0; i < num; i++)
                        {
                            NPC spider = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<EriusBaby>())];
                            spider.velocity.Y = Main.rand.NextFloat(-12, 0f);
                            spider.direction = Main.rand.NextBool() ? 1 : -1;
                            spider.ai[1] = Main.rand.NextFloat(1f,3f);
                        }
                    }
                }
            }
            else
            {
                npc.spriteDirection = npc.direction;
                npc.TargetClosest(true);
                Player player = Main.player[npc.target];
                Point playerTile = player.Center.ToTileCoordinates();
                Rectangle temple = new Rectangle(EAWorldGen.spiderTempleLoc.X + 4, EAWorldGen.spiderTempleLoc.Y + 4, 53, 49);
                Rectangle trigger = new Rectangle(EAWorldGen.spiderTempleLoc.X + 4, EAWorldGen.spiderTempleLoc.Y + 14, 44, 40);
                /*for (int i = 0; i < 40; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(trigger.TopLeft() * 16, trigger.Width * 16, trigger.Height * 16, 6)];
                    dust.noGravity = true;
                    dust.scale *= 1.6f;
                    dust.velocity *= 0;
                }*/
                if (!trigger.Contains(player.Center.ToTileCoordinates()) && startAI == 0)
                {
                    npc.noGravity = true;
                    npc.directionY = -1;
                    npc.dontTakeDamage = true;
                    npc.immortal = true;
                    npc.GivenName = "???";
                    music = -1;
                    npc.boss = false;
                }
                else if (!temple.Contains(player.Center.ToTileCoordinates()))
                {
                    npc.TargetClosest(true);
                    npc.active = false;                  
                }
                else
                {
                    startAI = 1;
                    if (npc.soundDelay <= 0)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, (int)npc.position.X, (int)npc.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/NPC/EriusAmbient" + Main.rand.Next(3)));
                        npc.soundDelay = Main.rand.Next(300, 1200);
                    }
                    npc.boss = true;
                    music = 0;
                    if (npc.soundDelay == 0)
                    {
                        npc.soundDelay = -1;
                        Main.PlaySound(3, (int)npc.position.X, (int)npc.position.Y, 36, 1, 0f);
                    }
                    npc.GivenName = "";
                    if (aiState != 0)
                    {
                        damageTaken = 0;
                        npc.dontTakeDamage = true;
                        npc.immortal = true;
                    }
                    else
                    {
                        npc.dontTakeDamage = false;
                        npc.immortal = false;
                    }
                    if (aiState == 0)
                    {
                        npc.noGravity = false;
                        npc.directionY = 1;
                        if (npc.velocity.Y == 0 && aiTimer % 60 == 0)
                        {
                            Main.PlaySound(29, (int)npc.position.X, (int)npc.position.Y, 26, 1, 0.6f);
                            int strength = 12;
                            if (playerTile.Y < EAWorldGen.spiderTempleLoc.Y + 40)
                            {
                                strength = 18;
                            }
                            int sign = Math.Sign(player.Center.X - npc.Center.X);
                            npc.velocity.X += sign * 6;
                            npc.direction = sign;
                            npc.velocity.Y -= strength;
                        }
                        if (npc.velocity.Y == 0)
                        {
                            npc.velocity.X *= 0.95f;
                        }
                        npc.velocity.Y += 0.015f; // stronger grav
                        aiTimer++;
                        if (aiTimer > 600 || damageTaken > npc.lifeMax / 5)
                        {
                            aiState = 1;
                            aiTimer = 0;
                            npc.velocity.Y = -18;

                            Main.PlaySound(SoundLoader.customSoundType, (int)npc.position.X, (int)npc.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/NPC/EriusCry"));
                        }

                    }
                    else if (aiState == 1)
                    {
                        aiTimer++;
                        npc.noGravity = true;
                        if (Math.Abs(npc.velocity.Y) < 0.05f && aiTimer > 10)
                        {
                            aiState = Main.rand.NextBool() ? 3 : 2;
                            aiTimer = 0;
                            npc.directionY = -1;
                            npc.netUpdate = true;
                        }
                        npc.velocity.X = 0;
                    }
                    else if (aiState == 2)
                    {
                        npc.directionY = -1;
                        npc.velocity.Y = -2;
                        aiTimer++;
                        if (aiTimer < 300)
                        {
                            if (Math.Abs(npc.velocity.X) < 0.05f && aiTimer > 5) aiTimer = 300;
                            npc.velocity.X = -15;
                        }
                        else if (aiTimer < 3000)
                        {
                            if (Math.Abs(npc.velocity.X) < 0.05f && aiTimer > 305)
                            {
                                aiTimer = 3000;
                            }
                            npc.velocity.X += 0.2f;
                            npc.velocity.X = MathHelper.Clamp(npc.velocity.X, -9, 9);
                            npc.direction = Math.Sign(npc.velocity.X);
                            aiTimer++;
                            if (aiTimer % 30 == 0)
                            {
                                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 118, 1, 0.3f);
                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                {
                                    Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 0, ProjectileType<Projectiles.NPCProj.Erius.SulfurWebTop>(), 0, 0f, Main.myPlayer)];
                                    proj.Center = npc.Top - new Vector2(0, 0);
                                }
                            }

                        }
                        else
                        {
                            npc.velocity = Vector2.Zero;
                            aiTimer++;
                            if (aiTimer >= 3600)
                            {
                                aiTimer = 0;
                                aiState = 0;
                            }
                        }
                    }
                    else if (aiState == 3)
                    {
                        npc.directionY = -1;
                        int sign = Math.Sign(player.Center.X - npc.Center.X);
                        npc.velocity.X += sign * 0.05f;
                        if (sign > 0 && npc.velocity.X < 0) npc.velocity.X *= 0.95f;
                        if (sign < 0 && npc.velocity.X > 0) npc.velocity.X *= 0.95f;
                        npc.velocity.X = MathHelper.Clamp(npc.velocity.X, -9, 9);
                        npc.direction = Math.Sign(npc.velocity.X);
                        npc.velocity.Y = -2;
                        aiTimer++;
                        if (aiTimer % 60 == 0)
                        {
                            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 73, 1, 0.3f);
                            if (Main.netMode != NetmodeID.MultiplayerClient) Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 3, ProjectileType<Projectiles.NPCProj.Erius.EriusAcidBall>(), npc.damage / 2, 0f, Main.myPlayer);
                        }
                        if (aiTimer >= 450)
                        {
                            aiTimer = 0;
                            aiState = 0;
                        }
                    }
                }
            }
        }
    }
}
