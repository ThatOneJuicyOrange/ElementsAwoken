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
    public class AzanaEye : ModNPC
    {
        int text = 0;

        public override void SetDefaults()
        {
            npc.lifeMax = 300000;
            npc.damage = 150;
            npc.defense = 60;
            npc.knockBackResist = 0f;

            npc.width = 126;
            npc.height = 116;

            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;

            npc.scale = 1f;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;

            music = MusicID.Boss2;
            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/AzanaThemeP1");

            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[mod.BuffType("IceBound")] = true;
            npc.buffImmune[mod.BuffType("EndlessTears")] = true;
            bossBag = mod.ItemType("AzanaBag");

            NPCID.Sets.TrailCacheLength[npc.type] = 3;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Azana");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 200;
            npc.lifeMax = 450000;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 600000;
                npc.damage = 300;
                npc.defense = 75;
            }
        }
        public override bool PreDraw(SpriteBatch spritebatch, Color drawColor)
        {
            Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, Main.npcTexture[npc.type].Height * 0.5f);
            SpriteEffects spriteEffects = npc.direction != 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            for (int k = 0; k < npc.oldPos.Length; k++)
            {
                Vector2 drawPos = npc.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
                Color color = npc.GetAlpha(drawColor) * ((float)(npc.oldPos.Length - k) / (float)npc.oldPos.Length);
                spritebatch.Draw(Main.npcTexture[npc.type], drawPos, null, color, npc.rotation, drawOrigin, npc.scale, spriteEffects, 0f);
            }
            var texture = Main.npcTexture[npc.type];
            var frame = texture.Frame();
            var origin = frame.Size() * 0.5f;
            spritebatch.Draw(Main.npcTexture[npc.type], npc.Center - Main.screenPosition, frame, drawColor, npc.rotation, origin, npc.scale, spriteEffects, 0f);
            spritebatch.Draw(mod.GetTexture("NPCs/Bosses/Azana/AzanaEye_Glow"), npc.Center - Main.screenPosition, frame, Color.White, npc.rotation, origin, npc.scale, spriteEffects, 0f);
            return false;
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ChaoticGaze"));
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Vector2 spawnAt = npc.Center + new Vector2(0f, (float)npc.height / 2f);
                NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("AzanaSpawner"));
            }
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            bool expertMode = Main.expertMode;
            npc.TargetClosest(true);

            Lighting.AddLight(npc.Center, ((255 - npc.alpha) * 0.9f) / 255f, ((255 - npc.alpha) * 0.1f) / 255f, ((255 - npc.alpha) * 0f) / 255f);
            npc.spriteDirection = npc.direction;


            if (npc.life <= npc.lifeMax * 0.5f && text == 0)
            {
                Main.NewText("You havent seen a thing yet...", new Color(93, 25, 43, 200));
                text++;
            }
            if (npc.life <= npc.lifeMax * 0.25f && text == 1)
            {
                Main.NewText("Oh you think this is easy? We will see.", new Color(93, 25, 43, 200));
                text++;
            }
            float num1 = npc.position.X + (float)(npc.width / 2) - Main.player[npc.target].position.X - (float)(Main.player[npc.target].width / 2);
            float num2 = npc.position.Y + (float)npc.height - 59f - Main.player[npc.target].position.Y - (float)(Main.player[npc.target].height / 2);
            float num3 = (float)Math.Atan2((double)num2, (double)num1) + 1.57f;
            if (num3 < 0f)
            {
                num3 += 6.283f;
            }
            else if ((double)num3 > 6.283)
            {
                num3 -= 6.283f;
            }
            float num4 = 0.15f; // speed?
            if (npc.rotation < num3)
            {
                if ((double)(num3 - npc.rotation) > 3.1415)
                {
                    npc.rotation -= num4;
                }
                else
                {
                    npc.rotation += num4;
                }
            }
            else if (npc.rotation > num3)
            {
                if ((double)(npc.rotation - num3) > 3.1415)
                {
                    npc.rotation += num4;
                }
                else
                {
                    npc.rotation -= num4;
                }
            }
            if (npc.rotation > num3 - num4 && npc.rotation < num3 + num4)
            {
                npc.rotation = num3;
            }
            if (npc.rotation < 0f)
            {
                npc.rotation += 6.283f;
            }
            else if ((double)npc.rotation > 6.283)
            {
                npc.rotation -= 6.283f;
            }
            if (npc.rotation > num3 - num4 && npc.rotation < num3 + num4)
            {
                npc.rotation = num3;
            }

            if (!Main.player[npc.target].active || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (!Main.player[npc.target].active || Main.player[npc.target].dead)
                {
                    npc.localAI[0]++;
                    npc.velocity.Y = npc.velocity.Y + 0.11f;
                    if (npc.localAI[0] >= 300)
                    {
                        npc.active = false;
                    }
                }
                else
                    npc.localAI[0] = 0;
            }
            if (Main.dayTime || !P.active || P.dead)
            {
                npc.velocity.Y = npc.velocity.Y - 0.04f;
                if (npc.timeLeft > 200)
                {
                    npc.timeLeft = 200;
                    return;
                }
            }
            else
            {
                if (npc.ai[1] == 0f) // sideways movement
                {
                    npc.TargetClosest(true);
                    float num424 = 12f;
                    float speed = 0.4f;
                    int side = 1;
                    if (npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)Main.player[npc.target].width)
                    {
                        side = -1;
                    }
                    if (npc.ai[0] == 0f)
                    {

                    }
                    Vector2 vector44 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    float targetX = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) + (float)(side * 400) - vector44.X;
                    float targetY = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector44.Y;
                    float targetPos = (float)Math.Sqrt((double)(targetX * targetX + targetY * targetY));
                    targetPos = num424 / targetPos;
                    targetX *= targetPos;
                    targetY *= targetPos;
                    if (npc.velocity.X < targetX)
                    {
                        npc.velocity.X = npc.velocity.X + speed;
                        if (npc.velocity.X < 0f && targetX > 0f)
                        {
                            npc.velocity.X = npc.velocity.X + speed;
                        }
                    }
                    else if (npc.velocity.X > targetX)
                    {
                        npc.velocity.X = npc.velocity.X - speed;
                        if (npc.velocity.X > 0f && targetX < 0f)
                        {
                            npc.velocity.X = npc.velocity.X - speed;
                        }
                    }
                    if (npc.velocity.Y < targetY)
                    {
                        npc.velocity.Y = npc.velocity.Y + speed;
                        if (npc.velocity.Y < 0f && targetY > 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y + speed;
                        }
                    }
                    else if (npc.velocity.Y > targetY)
                    {
                        npc.velocity.Y = npc.velocity.Y - speed;
                        if (npc.velocity.Y > 0f && targetY < 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y - speed;
                        }
                    }

                    npc.ai[2] += 1f;
                    if (npc.ai[2] >= 600f) // 10 seconds of shooting
                    {
                        npc.ai[1] = 1f;
                        npc.ai[2] = 0f;
                        npc.ai[3] = 0f;
                        npc.target = 255;
                        npc.netUpdate = true;
                    }

                    npc.ai[3] += 1f;
                    if (npc.life < npc.lifeMax * 0.8)
                    {
                        npc.ai[3] += 0.3f;
                    }
                    if (npc.life < npc.lifeMax * 0.6)
                    {
                        npc.ai[3] += 0.3f;
                    }
                    if (npc.life < npc.lifeMax * 0.4)
                    {
                        npc.ai[3] += 0.3f;
                    }
                    if (npc.life < npc.lifeMax * 0.2)
                    {
                        npc.ai[3] += 0.3f;
                    }
                    if (npc.life < npc.lifeMax * 0.1)
                    {
                        npc.ai[3] += 0.3f;
                    }
                    if (Main.expertMode)
                    {
                        npc.ai[3] += 0.5f;
                    }

                    if (npc.ai[3] >= 60f && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int numberProjectiles = 3;
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                        Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                        int type = mod.ProjectileType("AzanaMiniBlast");
                        int damage = expertMode ? 75 : 100;
                        float Speed = 14f;
                        float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1)).RotatedByRandom(MathHelper.ToRadians(5));
                            Projectile.NewProjectile(vector8.X, vector8.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, 0f, Main.myPlayer, 0f, 0f);
                        }
                        npc.ai[3] = 0f;
                    }
                }
                else if (npc.ai[1] == 1f) // dash
                {
                    if (npc.ai[0] == 0f)
                    {
                        npc.rotation = num3;
                        float num24 = 21f;
                        if (Main.expertMode)
                        {
                            num24 += 4f;
                        }
                        Vector2 vector4 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                        float num25 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector4.X;
                        float num26 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector4.Y;
                        float num27 = (float)Math.Sqrt((double)(num25 * num25 + num26 * num26));
                        num27 = num24 / num27;
                        npc.velocity.X = num25 * num27;
                        npc.velocity.Y = num26 * num27;
                        npc.ai[0] = 1f;
                        npc.netUpdate = true;
                    }
                    else if (npc.ai[0] == 1f)
                    {
                        npc.ai[2] += 1f;
                        if (npc.life < npc.lifeMax * 0.8)
                        {
                            npc.ai[2] += 0.5f;
                        }
                        if (npc.life < npc.lifeMax * 0.6)
                        {
                            npc.ai[2] += 0.5f;
                        }
                        if (npc.life < npc.lifeMax * 0.4)
                        {
                            npc.ai[2] += 0.5f;
                        }
                        if (npc.life < npc.lifeMax * 0.2)
                        {
                            npc.ai[2] += 0.5f;
                        }
                        if (npc.life < npc.lifeMax * 0.1)
                        {
                            npc.ai[2] += 0.5f;
                        }
                        if (Main.expertMode)
                        {
                            npc.ai[2] += 1f;
                        }
                        npc.ai[2] += 1f;
                        if (npc.ai[2] >= 40f)
                        {
                            npc.velocity *= 0.99f;
                            if (Main.expertMode)
                            {
                                npc.velocity *= 0.95f;
                            }
                            if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                            {
                                npc.velocity.X = 0f;
                            }
                            if ((double)npc.velocity.Y > -0.1 && (double)npc.velocity.Y < 0.1)
                            {
                                npc.velocity.Y = 0f;
                            }
                        }
                        else
                        {
                            npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) - 1.57f;
                        }
                        int dashTime = 80;
                        if (Main.expertMode)
                        {
                            dashTime = 60;
                        }
                        if (npc.ai[2] >= (float)dashTime)
                        {
                            npc.ai[3] += 1f;
                            npc.ai[2] = 0f;
                            npc.target = 255;
                            npc.rotation = num3;
                            if (npc.ai[3] >= 10f)
                            {
                                npc.ai[0] = 0f;
                                npc.ai[1] = 0f;
                                npc.ai[2] = 0f;
                                npc.ai[3] = 0f;
                            }
                            else
                            {
                                npc.ai[0] = 0f;
                            }
                        }
                    }
                }
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}
