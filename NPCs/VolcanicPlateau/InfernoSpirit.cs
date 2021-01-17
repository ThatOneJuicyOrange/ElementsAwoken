using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.VolcanicPlateau
{
    public class InfernoSpirit : ModNPC
    {
        private float dashState
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float direction
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float pulseAI
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float aiState
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        private float pulseMax = 45;
        public override void SetDefaults()
        {
            npc.width = 46;
            npc.height = 30;
            
            npc.aiStyle = -1;

            npc.damage = 120;
            npc.defense = 12;
            npc.lifeMax = 1200;
            npc.knockBackResist = 0.05f;

            npc.value = Item.buyPrice(0, 2, 0, 0);
            npc.alpha = 255;

            npc.HitSound = SoundID.NPCHit52;
            npc.DeathSound = SoundID.NPCDeath55;

            banner = npc.type;
            bannerItem = ModContent.ItemType<Items.Banners.InfernoSpiritBanner>();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infernal Echo");
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void FindFrame(int frameHeight)
        {
            if (npc.velocity.X < 0f)
            {
                npc.direction = -1;
            }
            else
            {
                npc.direction = 1;
            }
            npc.spriteDirection = npc.direction;
            npc.rotation = (float)Math.Atan2((double)(npc.velocity.Y * (float)npc.direction), (double)(npc.velocity.X * (float)npc.direction));
            npc.frameCounter += 0.15f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int frame = (int)npc.frameCounter;
            npc.frame.Y = frame * frameHeight;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (pulseAI > 0)
            {
                pulseAI--;
                Texture2D tex = ModContent.GetTexture("ElementsAwoken/NPCs/VolcanicPlateau/InfernoSpiritHot");
                Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, (Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type]) * 0.5f);
                SpriteEffects spriteEffects = npc.spriteDirection != -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                Vector2 drawPos = npc.position - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
                float alpha = pulseAI / pulseMax;
                float scale = MathHelper.Lerp(2.4f,0,pulseAI / pulseMax);
                Color color = Color.White * alpha;
                spriteBatch.Draw(tex, drawPos, npc.frame, color, npc.rotation, drawOrigin, scale, spriteEffects, 0f);
            }
            return true;
        }

        public override void AI()
        {
            Lighting.AddLight(npc.Center, 1.0f, 0.2f, 0.7f);
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];

            int dust = Dust.NewDust(npc.position, npc.width, npc.height, 6);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;

            if (Main.rand.NextBool(60) && pulseAI <= 0)
            {
                pulseAI = pulseMax;
            }

            if (npc.alpha > 0)
            {
                npc.alpha -= 30;
                if (npc.alpha < 0) npc.alpha = 0;
            }
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 0f;

            EAUtils.PushOtherEntities(npc);
            dashState++;
            int proj = 4;
            int total = 8;
            if (dashState % (total / proj) == 0 && dashState > 180)
            {
                float rotation = (float)Math.Atan2(npc.Center.Y - player.Center.Y, npc.Center.X - player.Center.X);
                float speed = 10f;
                Projectile pro2j = Main.projectile[Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), ModContent.ProjectileType<Projectiles.NPCProj.InfernalScream>(), npc.damage / 2, 0f, Main.myPlayer, 0f, 0f)];
                pro2j.ai[0] = Main.rand.Next(0, 10);
            }
            if (dashState > 180 + total) dashState = 0;
            /*if (aiState == 0)
            {
                float num145 = 1f;
                float num146 = 0.011f;
                npc.TargetClosest(true);
                Vector2 vector17 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                float num147 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector17.X;
                float num148 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector17.Y;
                float num149 = (float)Math.Sqrt((double)(num147 * num147 + num148 * num148));
                float num150 = num149;
                npc.ai[1] += 1f;
                if (npc.ai[1] > 600f)
                {
                    num146 *= 8f;
                    num145 = 4f;
                    if (npc.ai[1] > 650f)
                    {
                        npc.ai[1] = 0f;
                    }
                }
                else if (num150 < 250f)
                {
                    npc.ai[0] += 0.9f;
                    if (npc.ai[0] > 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y + 0.019f;
                    }
                    else
                    {
                        npc.velocity.Y = npc.velocity.Y - 0.019f;
                    }
                    if (npc.ai[0] < -100f || npc.ai[0] > 100f)
                    {
                        npc.velocity.X = npc.velocity.X + 0.019f;
                    }
                    else
                    {
                        npc.velocity.X = npc.velocity.X - 0.019f;
                    }
                    if (npc.ai[0] > 200f)
                    {
                        npc.ai[0] = -200f;
                    }
                }
                if (num150 > 350f)
                {
                    num145 = 5f;
                    num146 = 0.3f;
                }
                else if (num150 > 300f)
                {
                    num145 = 3f;
                    num146 = 0.2f;
                }
                else if (num150 > 250f)
                {
                    num145 = 1.5f;
                    num146 = 0.1f;
                }
                num149 = num145 / num149;
                num147 *= num149;
                num148 *= num149;
                if (Main.player[npc.target].dead)
                {
                    num147 = (float)npc.direction * num145 / 2f;
                    num148 = -num145 / 2f;
                }
                if (npc.velocity.X < num147)
                {
                    npc.velocity.X = npc.velocity.X + num146;
                }
                else if (npc.velocity.X > num147)
                {
                    npc.velocity.X = npc.velocity.X - num146;
                }
                if (npc.velocity.Y < num148)
                {
                    npc.velocity.Y = npc.velocity.Y + num146;
                }
                else if (npc.velocity.Y > num148)
                {
                    npc.velocity.Y = npc.velocity.Y - num146;
                }
                if (num147 > 0f)
                {
                    npc.spriteDirection = -1;
                    npc.rotation = (float)Math.Atan2((double)num148, (double)num147);
                }
                if (num147 < 0f)
                {
                    npc.spriteDirection = 1;
                    npc.rotation = (float)Math.Atan2((double)num148, (double)num147) + 3.14f;
                }
            }
            else
            {
                if (dashState == 0f)
                {
                    npc.TargetClosest(true);
                    dashState = 1f;
                    direction = (float)npc.direction;
                }
                else if (dashState == 1f)
                {
                    npc.TargetClosest(true);
                    float num1335 = 0.3f;
                    float num1336 = 7f;
                    float num1337 = 4f;
                    float num1338 = 660f;
                    float num1339 = 4f;
                    if (npc.type == 521)
                    {
                        num1335 = 0.7f;
                        num1336 = 14f;
                        num1338 = 500f;
                        num1337 = 6f;
                        num1339 = 3f;
                    }
                    npc.velocity.X = npc.velocity.X + direction * num1335;
                    if (npc.velocity.X > num1336)
                    {
                        npc.velocity.X = num1336;
                    }
                    if (npc.velocity.X < -num1336)
                    {
                        npc.velocity.X = -num1336;
                    }
                    float num1340 = Main.player[npc.target].Center.Y - npc.Center.Y;
                    if (Math.Abs(num1340) > num1337)
                    {
                        num1339 = 15f;
                    }
                    if (num1340 > num1337)
                    {
                        num1340 = num1337;
                    }
                    else if (num1340 < -num1337)
                    {
                        num1340 = -num1337;
                    }
                    npc.velocity.Y = (npc.velocity.Y * (num1339 - 1f) + num1340) / num1339;
                    if ((direction > 0f && Main.player[npc.target].Center.X - npc.Center.X < -num1338) || (direction < 0f && Main.player[npc.target].Center.X - npc.Center.X > num1338))
                    {
                        dashState = 2f;
                        direction = 0f;
                        if (npc.Center.Y + 20f > Main.player[npc.target].Center.Y)
                        {
                            direction = -1f;
                        }
                        else
                        {
                            direction = 1f;
                        }
                    }
                }
                else if (dashState == 2f)
                {
                    float num1341 = 0.4f;
                    float scaleFactor13 = 0.95f;
                    float num1342 = 5f;
                    if (npc.type == 521)
                    {
                        num1341 = 0.3f;
                        num1342 = 7f;
                        scaleFactor13 = 0.9f;
                    }
                    npc.velocity.Y = npc.velocity.Y + direction * num1341;
                    if (npc.velocity.Length() > num1342)
                    {
                        npc.velocity *= scaleFactor13;
                    }
                    if (npc.velocity.X > -1f && npc.velocity.X < 1f)
                    {
                        npc.TargetClosest(true);
                        dashState = 3f;
                        direction = (float)npc.direction;
                    }
                }
                else if (dashState == 3f)
                {
                    float num1343 = 0.4f;
                    float num1344 = 0.2f;
                    float num1345 = 5f;
                    float scaleFactor14 = 0.95f;
                    if (npc.type == 521)
                    {
                        num1343 = 0.6f;
                        num1344 = 0.3f;
                        num1345 = 7f;
                        scaleFactor14 = 0.9f;
                    }
                    npc.velocity.X = npc.velocity.X + direction * num1343;
                    if (npc.Center.Y > Main.player[npc.target].Center.Y)
                    {
                        npc.velocity.Y = npc.velocity.Y - num1344;
                    }
                    else
                    {
                        npc.velocity.Y = npc.velocity.Y + num1344;
                    }
                    if (npc.velocity.Length() > num1345)
                    {
                        npc.velocity *= scaleFactor14;
                    }
                    if (npc.velocity.Y > -1f && npc.velocity.Y < 1f)
                    {
                        npc.TargetClosest(true);
                        dashState = 0f;
                        direction = (float)npc.direction;
                    }
                }
            }*/
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.player.ZoneDungeon) &&
            !spawnInfo.player.ZoneTowerStardust &&
            !spawnInfo.player.ZoneTowerSolar &&
            !spawnInfo.player.ZoneTowerVortex &&
            !spawnInfo.player.ZoneTowerNebula &&
            !spawnInfo.playerInTown &&
            !Main.snowMoon && !Main.pumpkinMoon && NPC.downedMoonlord ? 0.045f : 0f;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.8f);
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.OnFire, 400, true);
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 6, hitDirection, -1f, 0, default(Color), 1f);
            }
            if (npc.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 6, hitDirection, -1f, 0, default(Color), 1f);
                }
            }
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Items.Materials.Pyroplasm>(), Main.rand.Next(1, 4));
        }
    }
}