using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Prompts
{
    public class InfernaceGuardian : ModNPC
    {
        private float aiTimer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float tpLocX
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float tpLocY
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float visualsAI
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public override void SetDefaults()
        {
            npc.damage = 10;

            npc.aiStyle = -1;

            npc.width = 26;
            npc.height = 50;

            npc.defense = 25;
            npc.lifeMax = 75;
            npc.knockBackResist = 0f;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath6;

            npc.buffImmune[BuffID.OnFire] = true;
            npc.lavaImmune = true;
            npc.GetGlobalNPC<VolcanicPlateau.PlateauNPCs>().tomeText = "Infernace’s Guardians are members of the Flamewilled cult that have been blessed by the guardian of the creatures of the Underworld, Infernace. They utilise powerful Fire magic to attack those that defy their god and can teleport to places which Infernace is focusing on.";
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infernace's Guardian");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 30;
            npc.lifeMax = 150;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SpriteEffects spriteEffects = npc.direction != 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Texture2D eyes = mod.GetTexture("NPCs/Prompts/InfernaceGuardianEyes");
            Texture2D cast = mod.GetTexture("NPCs/Prompts/InfernaceGuadianCast");
            Vector2 castOrigin = new Vector2(cast.Width * 0.5f, cast.Height * 0.5f);
            Vector2 eyesOrigin = new Vector2(eyes.Width * 0.5f, eyes.Height * 0.5f);
            Vector2 castAddition = npc.direction != 1 ? new Vector2(-2, 6) : new Vector2(2, 6);
            Vector2 eyesAddition = npc.direction != 1 ? new Vector2(6, 16) : new Vector2(12, 16);
            Vector2 castPos = npc.position - Main.screenPosition + castOrigin + new Vector2(0f, npc.gfxOffY) + castAddition;
            Vector2 eyesPos = npc.position - Main.screenPosition + eyesOrigin + new Vector2(0f, npc.gfxOffY) + eyesAddition;
            if (visualsAI < 150 && visualsAI > 90) spriteBatch.Draw(eyes, eyesPos, null, Color.White * (1 - ((visualsAI - 90) / 60)), npc.rotation, eyesOrigin, npc.scale, spriteEffects, 0f);
            if (visualsAI < 120 && aiTimer > 60) spriteBatch.Draw(cast, castPos, null, Color.White * (1 - ((visualsAI - 60) / 60)), visualsAI / 15f, castOrigin, npc.scale, spriteEffects, 0f);
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                string text = "";
                switch (Main.rand.Next(4))
                {
                    case 0:
                        text = "He awaits";
                        break;
                    case 1:
                        text = "Don't resist";
                        break;
                    case 2:
                        text = "You cant wait longer";
                        break;
                    case 3:
                        text = "His reign is forever";
                        break;
                    default:
                        break;
                }
                CombatText.NewText(npc.getRect(), Color.Orange, text);
                for (int i = 0; i < 31; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, 6, hitDirection * Main.rand.NextFloat(2f,30f), -Main.rand.NextFloat(-20f, 0f), 100, default(Color), 1.8f)];
                    dust.noGravity = true;
                    dust.velocity *= 0.5f;
                }
            }
        }
        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            if (aiTimer < 120) aiTimer = 120;
        }
        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            if (aiTimer < 120) aiTimer = 120;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            int maxNPCs = Main.expertMode ? 6 : 3;
            if (MyWorld.awakenedMode) maxNPCs = 9;

            float spawnChance = MathHelper.Lerp(0f, 0.15f, ((float)spawnInfo.player.Center.Y - ((float)Main.maxTilesY * 16) * 0.2f) / ((float)Main.maxTilesY * 16));
            return spawnInfo.spawnTileY > Main.maxTilesY * 0.8f &&
            !spawnInfo.player.ZoneTowerStardust &&
            !spawnInfo.player.ZoneTowerSolar &&
            !spawnInfo.player.ZoneTowerVortex &&
            !spawnInfo.player.ZoneTowerNebula &&
            !spawnInfo.playerInTown &&
            MyWorld.firePrompt > ElementsAwoken.bossPromptDelay && NPC.CountNPCS(npc.type) < maxNPCs && !Main.snowMoon && !Main.pumpkinMoon ? spawnChance : 0f;
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            Player P = Main.player[npc.target];
            if (P.position.Y < Main.maxTilesY * 16 * 0.25f) npc.active = false;

            Vector2 direction = P.Center - npc.Center;
            npc.spriteDirection = Math.Sign(direction.X);
            npc.velocity.X = 0f;

            aiTimer--;
            visualsAI--;
            if (Main.netMode != NetmodeID.MultiplayerClient && aiTimer == 60f)
            {
                float Speed = 2f;
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("InfernaceGuardianProj"), 12, 0f, Main.myPlayer);
            }
            if (aiTimer <= 0f)
            {
                aiTimer = 240f;
                visualsAI = 240f;
                Teleport(P, 0);
            }
        }
        private void Teleport(Player P, int attemptNum)
        {
            int playerTileX = (int)P.position.X / 16;
            int playerTileY = (int)P.position.Y / 16;
            int npcTileX = (int)npc.position.X / 16;
            int npcTileY = (int)npc.position.Y / 16;
            int maxTileDist = 12;
            bool foundNewLoc = false;
            int targetX = Main.rand.Next(playerTileX - maxTileDist, playerTileX + maxTileDist);
            for (int targetY = Main.rand.Next(playerTileY - maxTileDist, playerTileY + maxTileDist); targetY < playerTileY + maxTileDist; ++targetY)
            {
                if ((targetY < playerTileY - 4 ||
                    targetY > playerTileY + 4 ||
                    (targetX < playerTileX - 4 || targetX > playerTileX + 4)) &&
                    (targetY < npcTileY - 1 || targetY > npcTileY + 1 || (targetX < npcTileX - 1 || targetX > npcTileX + 1)) && Main.tile[targetX, targetY].nactive())
                {
                    bool flag2 = true;
                    if (Main.tile[targetX, targetY - 1].lava()) flag2 = false;

                    if (flag2 && Main.tileSolid[(int)Main.tile[targetX, targetY].type] && !Collision.SolidTiles(targetX - 1, targetX + 1, targetY - 4, targetY - 1))
                    {
                        tpLocX = (float)targetX;
                        tpLocY = (float)targetY;
                        foundNewLoc = true;
                        break;
                    }
                    if (ModContent.GetInstance<Config>().debugMode)
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            Dust dust = Main.dust[Dust.NewDust(new Vector2(targetX * 16, targetY * 16), 16, 16, 62)];
                            dust.noGravity = true;
                            dust.scale = 1f;
                            dust.velocity *= 0.1f;
                        }
                    }
                }
            }
            Main.PlaySound(SoundID.Item8, npc.position);
            if (tpLocX != 0 && tpLocY != 0 && foundNewLoc)
            {
                npc.position.X = (float)((double)tpLocX * 16.0 - (double)(npc.width / 2) + 8.0);
                npc.position.Y = tpLocY * 16f - (float)npc.height;
                npc.netUpdate = true;

                for (int i = 0; i < 20; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, 6)];
                    dust.noGravity = true;
                    dust.scale = 1f;
                    dust.velocity *= 0.1f;
                }
            }
            else if (attemptNum < 10) Teleport(P, attemptNum + 1);
            else ElementsAwoken.DebugModeText("Failed TP");
        }
    }
}