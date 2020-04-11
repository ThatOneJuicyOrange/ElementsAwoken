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

namespace ElementsAwoken.NPCs.Bosses.TheGuardian
{
    [AutoloadBossHead]
    public class TheGuardian : ModNPC
    {
        private int projectileBaseDamage = 60;
        private float despawnTimer = 0;
        private float shootTimer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float aiTimer
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float dropAI
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float isTransforming
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(despawnTimer);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            despawnTimer = reader.ReadSingle();
        }
        public override void SetDefaults()
        {
            npc.width = 92;
            npc.height = 152;

            npc.aiStyle = -1;

            npc.lifeMax = 40000;
            npc.damage = 120;
            npc.defense = 35;
            npc.knockBackResist = 0f;

            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.netAlways = true;

            npc.scale = 1.2f;
            npc.alpha = 255;
            npc.HitSound = SoundID.NPCHit2;

            npc.value = Item.buyPrice(0, 0, 0, 0);
            music = MusicID.GoblinInvasion;
            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/EyeDragonTheme");

            // all vanilla buffs
            for (int num2 = 0; num2 < 206; num2++)
            {
                npc.buffImmune[num2] = true;
            }
            npc.buffImmune[mod.BuffType("IceBound")] = true;
            npc.buffImmune[mod.BuffType("EndlessTears")] = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Guardian");
            Main.npcFrameCount[npc.type] = 13;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 50000;
            npc.damage = 140;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 75000;
                npc.damage = 160;
                npc.defense = 45;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1;
            if (isTransforming == 0)
            {
                if (npc.frameCounter > 5)
                {
                    npc.frame.Y = npc.frame.Y + frameHeight;
                    npc.frameCounter = 0.0;
                }
                if (npc.frame.Y > frameHeight * 4)  // so it doesnt go over
                {
                    npc.frame.Y = 0;
                }
            }
            else
            {
                if (npc.frameCounter > 7)
                {
                    npc.frame.Y = npc.frame.Y + frameHeight;
                    npc.frameCounter = 0.0;
                }
                if (npc.frame.Y > frameHeight * 12)  // so it doesnt go over
                {
                    npc.immortal = false;
                    npc.StrikeNPCNoInteraction(9999, 0f, 0, false, false, false); // incase more than 1000 damage is dealt, the animation just skips instead
                }

            }
        }
        public override int SpawnNPC(int tileX, int tileY)
        {
            npc.TargetClosest(true);
            Player P = Main.player[npc.target];
            Vector2 pos = (P.Center - new Vector2(0, 900)) / 16;
            Main.NewText("F");
            return base.SpawnNPC((int)pos.X, (int)pos.Y);
        }
        public override void AI()
        {
            if (npc.target < 0 || npc.target == 255) npc.TargetClosest(true);
            Lighting.AddLight(npc.Center, 1f, 1f, 1f);
            Player P = Main.player[npc.target];

            #region despawning
            if (Main.dayTime) despawnTimer++;
            else if (!P.active || P.dead || Vector2.Distance(P.Center, npc.Center) > 5000)
            {
                npc.TargetClosest(true);
                if (!P.active || P.dead || Vector2.Distance(P.Center, npc.Center) > 5000) despawnTimer++;
            }
            if (despawnTimer >= 300) npc.active = false;
            #endregion

            if (npc.life <= 1000)
            {
                isTransforming = 1;
                npc.immortal = true;
                npc.dontTakeDamage = true;
                npc.life = 1000;
            }
            if (dropAI == 0)
            {
                npc.alpha -= 5;
                if (npc.alpha > 0)
                {
                    npc.velocity.X = 0;
                    npc.velocity.Y = 0;
                }
                if (npc.alpha <= 0)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 69);
                    dropAI = 1;
                    npc.velocity.Y = 5f;
                    shootTimer = 120; // so it doesnt start shooting right away
                }
            }
            else if (dropAI == 1)
            {
                if (npc.velocity.Y == 0f)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 69);

                    if (Main.netMode != NetmodeID.Server)
                    {
                        Player shakeP = Main.LocalPlayer;
                        MyPlayer modPlayer = shakeP.GetModPlayer<MyPlayer>();
                        if (Vector2.Distance(shakeP.Center, npc.Center) <= 2000) modPlayer.screenshakeAmount = 8;
                    }

                    for (int k = 0; k < 200; k++)
                    {
                        int dust = Dust.NewDust(new Vector2(npc.position.X, npc.Center.Y + 45), npc.width, 8, 0, 0f, 0f, 100, default(Color), 2f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].velocity *= 1.5f;
                    }
                    dropAI = -1;
                }
            }
            else
            {
                if (isTransforming == 0)
                {
                    #region circle shield and player movement
                    int maxDist = 1000;
                    for (int i = 0; i < 80; i++)
                    {
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);
                        Dust dust = Main.dust[Dust.NewDust(npc.Center + offset, 0, 0, 6, 0, 0, 100)];
                        dust.noGravity = true;
                    }
                    for (int i = 0; i < Main.player.Length; i++)
                    {
                        Player player = Main.player[i];
                        if (player.active && !P.dead && Vector2.Distance(player.Center, npc.Center) > maxDist)
                        {
                            Vector2 toTarget = new Vector2(npc.Center.X - player.Center.X, npc.Center.Y - player.Center.Y);
                            toTarget.Normalize();
                            float speed = MathHelper.Lerp(0.5f, 2.5f, (Vector2.Distance(P.Center, npc.Center) - maxDist) / 400);
                            player.velocity += toTarget * speed;

                            player.dashDelay = 2; // to stop dashing away
                            player.grappling[0] = -1; // to stop grappling
                            player.grapCount = 0;
                            for (int p = 0; p < Main.maxProjectiles; p++)
                            {
                                if (Main.projectile[p].active && Main.projectile[p].owner == player.whoAmI && Main.projectile[p].aiStyle == 7)
                                {
                                    Main.projectile[p].Kill();
                                }
                            }
                        }
                    }
                    #endregion
                    aiTimer++;
                    if (aiTimer > 1000f) aiTimer = 0f;
                    shootTimer--;
                    if (shootTimer <= 0 && aiTimer <= 500)
                    {
                        float Speed = 17f;
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                        float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("GuardianShot"), projectileBaseDamage, 0f, Main.myPlayer);
                        shootTimer = 50;
                    }
                    // targeting and beam
                    if (aiTimer >= 500)
                    {
                        if (shootTimer == 80)
                        {
                            Projectile.NewProjectile(P.Center.X, P.Center.Y, 0f, 0f, mod.ProjectileType("GuardianTargeter"), 0, 0f, Main.myPlayer, 0, P.whoAmI);
                        }
                        if (shootTimer <= 0)
                        {
                            int target = FindTargeter();
                            if (target != -1)
                            {
                                Projectile targetNPC = Main.projectile[target];
                                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);

                                float Speed = 15f;
                                float rotation = (float)Math.Atan2(npc.Center.Y - targetNPC.Center.Y, npc.Center.X - targetNPC.Center.X);
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("GuardianBeam"), projectileBaseDamage + 40, 0f, Main.myPlayer);
                                targetNPC.Kill();
                            }
                            shootTimer = 100;
                        }
                    }
                    else
                    {
                        int target = FindTargeter();
                        if (target != -1)
                        {
                            Projectile targetNPC = Main.projectile[target];
                            targetNPC.Kill();
                        }
                    }
                }
            }
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = mod.GetTexture("NPCs/Bosses/TheGuardian/" + GetType().Name + "_Glow");
            Rectangle frame = new Rectangle(0, npc.frame.Y, texture.Width, texture.Height / Main.npcFrameCount[npc.type]);
            Vector2 origin = new Vector2(texture.Width * 0.5f, (texture.Height / Main.npcFrameCount[npc.type]) * 0.5f);
            SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY) + new Vector2(0,4), frame, new Color(255, 255, 255, 0), npc.rotation, origin, npc.scale, effects, 0.0f);
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
        private int FindTargeter()
        {
            for (int k = 0; k < Main.maxProjectiles; k++)
            {
                Projectile other = Main.projectile[k];
                if (other.type == mod.ProjectileType("GuardianTargeter") && other.active)
                {
                    return other.whoAmI;
                }
            }
            return -1;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Vector2 spawnAt = npc.Center + new Vector2(0f, (float)npc.height / 2f);
                NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("TheGuardianFly"));
            }
        }
        public override bool CheckDead()
        {
            Main.NewText("This is only the beginning!", Color.Orange.R, Color.Orange.G, Color.Orange.B);
            return true;
        }

        public override bool CheckActive()
        {
            return false;
        }
    }
}
