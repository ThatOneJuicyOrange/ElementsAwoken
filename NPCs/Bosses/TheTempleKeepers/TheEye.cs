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

namespace ElementsAwoken.NPCs.Bosses.TheTempleKeepers
{
    [AutoloadBossHead]
    public class TheEye : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 66;
            npc.height = 52;

            npc.lifeMax = 25000;
            npc.damage = 0;
            npc.defense = 40;
            npc.knockBackResist = 0f;

            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.netAlways = true;

            npc.scale = 1.1f;

            npc.HitSound = SoundID.NPCHit2;
            npc.DeathSound = SoundID.NPCDeath8;

            npc.value = Item.buyPrice(0, 5, 0, 0);
            music = MusicID.GoblinInvasion;
            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/EyeDragonTheme");

            // all vanilla buffs
            for (int num2 = 0; num2 < 206; num2++)
            {
                npc.buffImmune[num2] = true;
            }
            npc.buffImmune[mod.BuffType("IceBound")] = true;
            npc.buffImmune[mod.BuffType("EndlessTears")] = true;

            bossBag = mod.ItemType("TempleKeepersBag");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Eye");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.defense = 50;
            npc.lifeMax = 35000;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 50000;
                npc.defense = 60;
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/TheEye" + i), npc.scale);
                }
            }
        }
        public override void NPCLoot()
        {
            if (!NPC.AnyNPCs(mod.NPCType("AncientWyrmHead")))
            {
                if (Main.expertMode)
                {
                    npc.DropBossBags();
                }
                else
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TempleFragment"));
                    int choice = Main.rand.Next(4);
                    if (choice == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TemplesCrystal"));
                    }
                    if (choice == 1)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GazeOfInferno"));
                    }
                    if (choice == 2)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TheAllSeer"));
                    }
                    if (choice == 3)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("WyrmClaw"));
                    }
                }
            }
            MyWorld.downedEye = true;
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1;
            if (npc.life > npc.lifeMax * 0.75f)
            {
                npc.frame.Y = 1 * frameHeight;
            }
            else
            {
                npc.frame.Y = 0 * frameHeight;
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
            if (Main.dayTime)
            {
                npc.localAI[0]++;
            }
            if (npc.localAI[0] >= 300)
            {
                npc.active = false;
            }
            #endregion
            #region circle shield and player movement
            int maxDist = 1000;
            for (int i = 0; i < 120; i++)
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
                    for (int p = 0; p < Main.maxProjectiles; p++)
                    {
                        if (Main.projectile[p].active && Main.projectile[p].owner == player.whoAmI && Main.projectile[p].aiStyle == 7)
                        {
                            Main.projectile[p].Kill();
                        }
                    }
                }
            }
            int maxdusts = 5;
            for (int i = 0; i < maxdusts; i++)
            {
                float dustDistance = 100;
                float dustSpeed = 8;
                Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                Dust vortex = Dust.NewDustPerfect(new Vector2(npc.Center.X, npc.Center.Y) + offset, 6, velocity, 0, default(Color), 1.5f);
                vortex.noGravity = true;
            }
            #endregion
            if (npc.localAI[1] == 0)
            {
                npc.Center = P.Center - new Vector2(100, 50);
                npc.localAI[1]++;
            }
            npc.ai[0]--;
            if (!NPC.AnyNPCs(mod.NPCType("AncientWyrmHead")))
            {
                if (npc.ai[0] <= 0)
                {
                    float Speed = 17f;
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                    float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("GuardianShot"), 60, 0f, Main.myPlayer);
                    if (npc.life > npc.lifeMax * 0.75f)
                    {
                        npc.ai[0] = Main.rand.Next(30, 120);
                    }
                    else
                    {
                        npc.ai[0] = Main.rand.Next(5, 80);
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
