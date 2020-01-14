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

namespace ElementsAwoken.NPCs.Bosses.TheGuardian
{
    [AutoloadBossHead]
    public class TheGuardian : ModNPC
    {
        public bool isTransforming = false;
        public bool startDrop = true;
        public int projectileBaseDamage = 60; 
        public override void SetDefaults()
        {
            npc.width = 92;
            npc.height = 110;

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
            npc.DeathSound = SoundID.NPCDeath1;


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
            if (!isTransforming)
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
            if (isTransforming)
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
        
        public override void AI()
        {
            Lighting.AddLight(npc.Center, 1f, 1f, 1f);
            Player P = Main.player[npc.target];
            MyPlayer modPlayer = P.GetModPlayer<MyPlayer>();
            #region despawning
            if (Main.dayTime)
            {
                npc.localAI[0]++;
            }
            else if (!P.active || P.dead)
            {
                npc.TargetClosest(true);
                if (!P.active || P.dead)
                {
                    npc.localAI[0]++;
                }
            }
            else if (Vector2.Distance(P.Center, npc.Center) > 5000)
            {
                npc.localAI[0]++;
            }
            if (npc.localAI[0] >= 300)
            {
                npc.active = false;
            }
            if (npc.life < 1000)
            {
                isTransforming = true;
                npc.immortal = true;
            }
            #endregion
            if (startDrop)
            {
                npc.localAI[1]++;
                if (npc.localAI[1] == 5)
                {
                    npc.Center = P.Center - new Vector2(0, 400);
                }
                if (npc.localAI[1] >= 5)
                {
                    npc.alpha -= 5;
                }
                if (npc.alpha > 0)
                {
                    npc.velocity.X = 0;
                    npc.velocity.Y = 0;
                }
                if (npc.localAI[2] == 0)
                {
                    if (npc.alpha <= 0)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 69);
                        npc.localAI[2] = 1;
                        npc.velocity.Y = 5f;
                        npc.ai[0] = 120; // so it doesnt start shooting right away
                    }
                }
                else
                {
                    if (npc.velocity.Y == 0f)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 69);

                        if (Main.netMode == 0) modPlayer.screenshakeAmount = 8;                       
                        else ElementsAwoken.NPCApplyScreenShakeToAll(npc.whoAmI, 8, 2000);

                        for (int k = 0; k < 200; k++)
                        {
                            int dust = Dust.NewDust(new Vector2(npc.position.X, npc.Center.Y + 45), npc.width, 8, 0, 0f, 0f, 100, default(Color), 2f);
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].velocity *= 1.5f;
                        }
                        startDrop = false;
                    }
                }
            }            
            else
            {
                #region circle shield and player movement
                int maxDist = 1000;
                for (int i = 0; i < 80; i++)
                {
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);// unit circle yay
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
                        float speed = Vector2.Distance(player.Center, npc.Center) > maxDist + 500 ? 1f : 0.5f;
                        player.velocity += toTarget * 0.5f;

                        player.dashDelay = 2; // to stop dashing away
                        player.grappling[0] = -1;
                        // to stop grappling
                        player.grapCount = 0;
                        for (int p = 0; p < Main.projectile.Length; p++)
                        {
                            if (Main.projectile[p].active && Main.projectile[p].owner == player.whoAmI && Main.projectile[p].aiStyle == 7)
                            {
                                Main.projectile[p].Kill();
                            }
                        }
                    }
                }
                #endregion
                // ai
                npc.ai[1]++;
                if (npc.ai[1] > 1000f)
                {
                    npc.ai[1] = 0f;
                }
                npc.ai[0]--;
                // shoot 
                if (npc.ai[0] <= 0 && npc.ai[1] <= 500)
                {
                    float Speed = 17f;
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                    float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("GuardianShot"), projectileBaseDamage, 0f, 0);
                    npc.ai[0] = 50;
                }
                // targeting and beam
                if (npc.ai[1] >= 500)
                {
                    if (npc.ai[0] == 80)
                    {
                        Projectile.NewProjectile(P.Center.X, P.Center.Y, 0f, 0f, mod.ProjectileType("GuardianTargeter"), 0, 0f, 0, 0, P.whoAmI);
                    }
                    if (npc.ai[0] <= 0)
                    {
                        for (int k = 0; k < Main.maxProjectiles; k++)
                        {
                            Projectile other = Main.projectile[k];
                            if (other.type == mod.ProjectileType("GuardianTargeter") && other.active)
                            {
                                float Speed = 15f;
                                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                                float rotation = (float)Math.Atan2(npc.Center.Y - other.Center.Y, npc.Center.X - other.Center.X);
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("GuardianBeam"), projectileBaseDamage + 40, 0f, 0);

                                other.Kill();
                            }
                        }
                        npc.ai[0] = 100;
                    }
                }
            }
        }
        public override bool PreNPCLoot()
        {
            return false;
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
