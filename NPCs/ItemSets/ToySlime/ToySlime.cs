using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using ElementsAwoken.Items.ItemSets.ToySlime;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Projectiles.NPCProj.ToySlime;

namespace ElementsAwoken.NPCs.ItemSets.ToySlime
{
    public class ToySlime : ModNPC
    {
        // all the npc.ais are used in slime ai
        private int projectileBaseDamage = 15;
        private float jumpTimer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float state
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float brickTimer
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float slimeSpawnLife
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 22;

            npc.aiStyle = -1;

            npc.knockBackResist = 0.1f;
            npc.damage = 25;
            npc.defense = 8;
            npc.lifeMax = 1200;

            animationType = NPCID.RainbowSlime;
            npc.value = Item.buyPrice(0, 3, 0, 0);

            npc.alpha = 60;

            npc.lavaImmune = false;
            npc.noGravity = false;
            npc.noTileCollide = false;

            npc.alpha = 75;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toy Slime");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 30;
            npc.lifeMax = 1600;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 2000;
                npc.damage = 35;
                npc.defense = 10;
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float spawnchance = Main.expertMode ? 0.006f : 0.005f;  
            MyPlayer modPlayer = spawnInfo.player.GetModPlayer<MyPlayer>();
            if (modPlayer.toySlimeChanceTimer > 0 && !NPC.AnyNPCs(npc.type))
            {
                spawnchance = 0.3f;
            }
          //SpawnCondition.OverworldDaySlime.Chance * 0.9f;

            return (spawnInfo.spawnTileY < Main.worldSurface) 
                && NPC.downedBoss3
                && !spawnInfo.playerInTown
                && !spawnInfo.invasion && !Main.snowMoon && !Main.pumpkinMoon
                && !spawnInfo.player.ZoneTowerStardust && !spawnInfo.player.ZoneTowerSolar && !spawnInfo.player.ZoneTowerVortex && !spawnInfo.player.ZoneTowerNebula
                && !spawnInfo.player.ZoneSnow
                && !spawnInfo.player.ZoneCorrupt
                && !spawnInfo.player.ZoneCrimson
                && !spawnInfo.player.ZoneSnow
                && !spawnInfo.player.ZoneDungeon
                && !NPC.AnyNPCs(NPCType<ToySlime>())
                ? spawnchance : 0f;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SpriteEffects spriteEffects = npc.direction != 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            var origin = npc.frame.Size() * 0.5f;
            Color color = drawColor;
            spriteBatch.Draw(mod.GetTexture("NPCs/ItemSets/ToySlime/ToySlimeArmor"), npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), npc.frame, color, npc.rotation, origin, npc.scale * 0.9f, spriteEffects, 0);
            return true;
        }

        public override void NPCLoot()
        {
            int numToys = 0;
            for (int i = 0; i < Main.player.Length; i++)
            {
                if (Main.player[i].active)
                {
                    if (MyWorld.awakenedMode) numToys += Main.rand.Next(10, 35);
                    else if (Main.expertMode) numToys = Main.rand.Next(10, 25);
                    else numToys += Main.rand.Next(10, 12);
                }
            }
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<BrokenToys>(), numToys);
            if (Main.rand.Next(10) == 0) Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<ToySlimeMask>());           
            if (MyWorld.awakenedMode) Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<ToySlimeClaw>());
            int item = Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Gel, Main.rand.Next(10, 35));
            Main.item[item].color = new Color(0, 220, 40, 100);
            MyWorld.downedToySlime = true;
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            MyPlayer modPlayer = target.GetModPlayer<MyPlayer>();
            if (MyWorld.awakenedMode && modPlayer.toySlimed < -600)
            {
                modPlayer.toySlimedID = npc.whoAmI;
                modPlayer.toySlimed = 180;
                Main.PlaySound(SoundID.Item95, npc.Center);
            }
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
            if (P.active && !P.dead) npc.localAI[0] = 0;
            if (npc.localAI[0] >= 300) npc.active = false;
            #endregion

            float num234 = 1f;
            bool flag8 = false;
            bool hideSlime = false;
            if (slimeSpawnLife == 0f && npc.life > 0)
            {
                slimeSpawnLife = (float)npc.lifeMax;
            }
            if (npc.localAI[3] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                jumpTimer = -100f;
                npc.localAI[3] = 1f;
                npc.TargetClosest(true);
                npc.netUpdate = true;
            }
            // despawning
            if (Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead)
                {
                    npc.timeLeft = 0;
                    if (Main.player[npc.target].Center.X < npc.Center.X)
                    {
                        npc.direction = 1;
                    }
                    else
                    {
                        npc.direction = -1;
                    }
                }
            }
            
            if (state == 5f)
            {
                flag8 = true;
                jumpTimer++;

                num234 = MathHelper.Clamp((60f - jumpTimer) / 60f, 0f, 1f);
                num234 = 0.5f + num234 * 0.5f;
                if (jumpTimer >= 60f)
                {
                    hideSlime = true;
                }
                if (jumpTimer >= 60f && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    npc.Bottom = new Vector2(npc.localAI[1], npc.localAI[2]);
                    state = 6f;
                    jumpTimer = 0f;
                    npc.netUpdate = true;
                }
                if (Main.netMode == 1 && jumpTimer >= 120f)
                {
                    state = 6f;
                    jumpTimer = 0f;
                }
                if (!hideSlime)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(npc.position + Vector2.UnitX * -20f, npc.width + 40, npc.height, 4, npc.velocity.X, npc.velocity.Y, 150, new Color(0, 220, 40, 100), 2f)];
                        dust.noGravity = true;
                        dust.velocity *= 0.5f;
                    }
                }
            }
            else if (state == 6f)
            {
                flag8 = true;
                jumpTimer++;

                num234 = MathHelper.Clamp(jumpTimer / 30f, 0f, 1f);
                num234 = 0.5f + num234 * 0.5f;
                if (jumpTimer >= 30f && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    state = 0f;
                    jumpTimer = 0f;
                    npc.netUpdate = true;
                    npc.TargetClosest(true);
                }
                if (Main.netMode == 1 && jumpTimer >= 60f)
                {
                    state = 0f;
                    jumpTimer = 0f;
                    npc.TargetClosest(true);
                }
                for (int k = 0; k < 10; k++)
                {
                    Dust dust = Main.dust[Dust.NewDust(npc.position + Vector2.UnitX * -20f, npc.width + 40, npc.height, 4, npc.velocity.X, npc.velocity.Y, 150, new Color(0, 220, 40, 100), 2f)];
                    dust.noGravity = true;
                    dust.velocity *= 2f;
                }
            }
            npc.dontTakeDamage = (npc.hide = hideSlime);
            if (npc.velocity.Y == 0f) // if its on the ground
            {
                npc.velocity.X = npc.velocity.X * 0.8f;
                if (npc.velocity.X > -0.1 && npc.velocity.X < 0.1)
                {
                    npc.velocity.X = 0f;
                }
                if (!flag8)
                {
                    // jumps faster when low health
                    jumpTimer += 2f;                   
                    if (npc.life < npc.lifeMax * 0.8)
                    {
                        jumpTimer += 1f;
                    }
                    if (npc.life < npc.lifeMax * 0.6)
                    {
                        jumpTimer += 1f;
                    }
                    if (npc.life < npc.lifeMax * 0.4)
                    {
                        jumpTimer += 2f;
                    }
                    if (npc.life < npc.lifeMax * 0.2)
                    {
                        jumpTimer += 3f;
                    }
                    if (npc.life < npc.lifeMax * 0.1)
                    {
                        jumpTimer += 4f;
                    }
                    if (jumpTimer >= 0f)
                    {
                        npc.netUpdate = true;
                        npc.TargetClosest(true);
                        if (state == 3f) // big jump
                        {
                            npc.velocity.Y = -13f;
                            npc.velocity.X = npc.velocity.X + 3.5f * npc.direction;
                            jumpTimer = -200f;
                            state = 0f;
                        }
                        else if (state == 2f) // small jump
                        {
                            npc.velocity.Y = -6f;
                            npc.velocity.X = npc.velocity.X + 4.5f * npc.direction;
                            jumpTimer = -120f;
                            state += 1f;
                        }
                        else // medium jump
                        {
                            npc.velocity.Y = -8f;
                            npc.velocity.X = npc.velocity.X + 4f * npc.direction;
                            jumpTimer = -120f;
                            state += 1f;
                        }
                    }
                }
            }
            else if (npc.target < 255 && ((npc.direction == 1 && npc.velocity.X < 3f) || (npc.direction == -1 && npc.velocity.X > -3f)))
            {
                if ((npc.direction == -1 && npc.velocity.X < 0.1) || (npc.direction == 1 && npc.velocity.X > -0.1))
                {
                    npc.velocity.X = npc.velocity.X + 0.2f * (float)npc.direction;
                }
                else
                {
                    npc.velocity.X = npc.velocity.X * 0.93f;
                }
            }
            Dust dust1 = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, 4, npc.velocity.X, npc.velocity.Y, 255, new Color(0, 220, 40, 100), npc.scale * 1.2f)];
            dust1.noGravity = true;
            dust1.velocity *= 0.5f;
            if (npc.life > 0)
            {
                float npcLife = (float)npc.life / (float)npc.lifeMax;
                npcLife = npcLife * 0.5f + 0.75f;
                npcLife *= num234;
                if (npcLife != npc.scale)
                {
                    npc.position.X = npc.position.X + (float)(npc.width / 2);
                    npc.position.Y = npc.position.Y + (float)npc.height;
                    npc.scale = npcLife;
                    npc.width = (int)(74f * npc.scale);
                    npc.height = (int)(50f * npc.scale);
                    npc.position.X = npc.position.X - (float)(npc.width / 2);
                    npc.position.Y = npc.position.Y - (float)npc.height;
                }
            }
            // spawn slimes
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 spawnAt = npc.Center + new Vector2(0f, (float)npc.height / 2f);
                int type = NPCType<MiniToySlime>();
                int spawnRate = (int)(npc.lifeMax * 0.05);
                if (npc.life + spawnRate < slimeSpawnLife)
                {
                    slimeSpawnLife = npc.life; // assign the current life
                    int numSlimes = Main.rand.Next(1, 2);
                    if (Main.expertMode) numSlimes += Main.rand.Next(1, 2);
                    if (MyWorld.awakenedMode) numSlimes += Main.rand.Next(1, 2);
                    for (int i = 0; i < numSlimes; i++)
                    {
                        NPC slime = Main.npc[NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, type)];
                        slime.ai[2] = 10000; // to stop it shooting bricks in awakened
                        MiniToySlime toyBoi = (MiniToySlime)slime.modNPC;
                        toyBoi.dropBlocks = false;
                        npc.netUpdate = true;
                    }
                }
            }
            if (Main.expertMode)
            {
                brickTimer--;
                if (npc.life < npc.lifeMax * 0.5)
                {
                    brickTimer--;
                }
                if (MyWorld.awakenedMode)
                {
                    if (npc.life < npc.lifeMax * 0.25)
                    {
                        brickTimer--;
                    }
                    if (npc.life < npc.lifeMax * 0.1)
                    {
                        brickTimer--;
                    }
                }
                if (brickTimer <= 0)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 10, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-6, -2), ProjectileType<LegoBrick>(), projectileBaseDamage, 0f, Main.myPlayer, 0, 0);
                    }
                    brickTimer = Main.rand.Next(200, 350);
                    if (MyWorld.awakenedMode)
                    {
                        brickTimer = Main.rand.Next(150, 300);
                    }
                }
                npc.netUpdate = true;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 4, hitDirection, -1f, 0, new Color(0, 220, 40, 100), 1f);
            }
            if (npc.life <= 0)
            {
                for (int k = 0; k < 50; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 4, (float)(2 * hitDirection), -2f, npc.alpha, new Color(0, 220, 40, 100), 1f);
                }
            }
        }
    }
}