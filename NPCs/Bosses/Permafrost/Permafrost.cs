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

namespace ElementsAwoken.NPCs.Bosses.Permafrost
{
    [AutoloadBossHead]
    public class Permafrost : ModNPC
    {
        int shootTimer = 0;

        int portalTimer = 600;
        int minionTimer = 300;

        bool canSpawnOrbitals = true;
        int phase = 1;

        bool enraged = false;
        int enrageTimer = 0;
        bool lowLife = false;
        bool animate = true;

        float spinAI = 0f;

        int projectileBaseDamage = 60;

        public override void SetDefaults()
        {
            npc.width = 152;
            npc.height = 158;

            npc.lifeMax = 50000;
            npc.damage = 60;
            npc.defense = 30;
            npc.knockBackResist = 0f;

            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;

            npc.HitSound = SoundID.NPCHit52;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.value = Item.buyPrice(0, 20, 0, 0);
            music = MusicID.Boss3;
            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/PermafrostTheme");
            bossBag = mod.ItemType("PermafrostBag");

            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[mod.BuffType("IceBound")] = true;
            npc.buffImmune[mod.BuffType("EndlessTears")] = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Permafrost");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 80;
            npc.lifeMax = 60000;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 75000;
                npc.damage = 100;
                npc.defense = 40;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            if (npc.ai[1] < 1200 || animate)
            {
                npc.spriteDirection = npc.direction;
                ++npc.frameCounter;
                if (npc.frameCounter >= 32.0)
                    npc.frameCounter = 0.0;
                npc.frame.Y = 158 * (int)(npc.frameCounter / 8.0);
            }

            if (npc.ai[1] > 1200 && npc.ai[1] < 1350)
            {           
                npc.spriteDirection = npc.direction;
                ++npc.frameCounter;
                if (npc.frameCounter >= 64.0)
                    npc.frameCounter = 0.0;
                npc.frame.Y = 158 * (int)(npc.frameCounter / 16.0);
            }
            if (!animate)
            {
                npc.frame.Y = 2 * frameHeight;
            }
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.Frostburn, 180, false);
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PermafrostTrophy"));
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PermafrostMask"));
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                int choice = Main.rand.Next(4);
                if (choice == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("IceReaver"));
                }
                if (choice == 1)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Snowdrift"));
                }
                if (choice == 2)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("IceWrath"));
                }
                if (choice == 3)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Flurry"));
                }
            }
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FrostEssence"), Main.rand.Next(5, 25));
            if (!MyWorld.downedPermafrost)
            {
                ElementsAwoken.encounter = 2;
                ElementsAwoken.encounterTimer = 3600;
                ElementsAwoken.DebugModeText("encounter 2 start");
            }
            MyWorld.downedPermafrost = true;
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            // despawn
            if (!Main.player[npc.target].active || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (!Main.player[npc.target].active || Main.player[npc.target].dead)
                {
                    npc.ai[0]++;
                    npc.velocity.Y = npc.velocity.Y + 0.11f;
                    if (npc.ai[0] >= 300)
                    {
                        npc.active = false;
                    }
                }
                else
                    npc.ai[3] = 0;
            }

            lowLife = npc.life <= npc.lifeMax * 0.3f;
            #region enrage
            if (!P.ZoneSnow)
            {
                enrageTimer++;
                //enraged = true;
            }
            if (P.ZoneSnow)
            {
                enraged = false;
                enrageTimer = 0;
            }
            if (enrageTimer > 180)
            {
                enraged = true;
            }
            #endregion
            npc.ai[1] += 1f;
            if (!lowLife)
            {
                if (npc.ai[1] > 1700f)
                {
                    npc.ai[1] = 0f;
                }
            }
            else
            {
                if (npc.ai[1] > 2100f)
                {
                    npc.ai[1] = 0f;
                }
            }
            portalTimer--;
            shootTimer--;
            minionTimer--;
            int dust = Dust.NewDust(npc.position, npc.width, npc.height, 135);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;
            // minions
            int numMinions= NPC.CountNPCS(mod.NPCType("PermafrostMinion"));
            if (minionTimer <= 0 && numMinions <= 5)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("PermafrostMinion"));
                minionTimer = enraged ? Main.rand.Next(150, 250) :  Main.rand.Next(200, 300);
            }
            // portals
            if (portalTimer <= 0)
            {
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 33);
                Projectile.NewProjectile(npc.Center.X + 200, npc.Center.Y, 8, 0, mod.ProjectileType("PermafrostPortal"), 0, 0f, Main.myPlayer);
                Projectile.NewProjectile(npc.Center.X - 200, npc.Center.Y, -8, 0, mod.ProjectileType("PermafrostPortal"), 0, 0f, Main.myPlayer);

                portalTimer = enraged ? 200 : 600;
            }
            #region orbitals
            if (NPC.AnyNPCs(mod.NPCType("PermaOrbital")))
            {
                npc.immortal = true;
                npc.dontTakeDamage = true;
            }
            else
            {
                npc.immortal = false;
                npc.dontTakeDamage = false;
            }
            if (npc.life <= npc.lifeMax * 0.75 && phase == 1)
            {
                phase++;
                canSpawnOrbitals = true;
            }
            if (npc.life <= npc.lifeMax * 0.50 && phase == 2)
            {
                phase++;
                canSpawnOrbitals = true;
            }
            if (npc.life <= npc.lifeMax * 0.25 && phase == 3)
            {
                phase++;
                canSpawnOrbitals = true;
            }
            if (npc.life <= npc.lifeMax * 0.10 && phase == 4 && Main.expertMode)
            {
                phase++;
                canSpawnOrbitals = true;
            }
            //spawn orbitals
            if (canSpawnOrbitals == true)
            {
                if (phase == 1)
                {
                    int orbitalcount = 3;
                    for (int l = 0; l < orbitalcount; l++)
                    {
                        int distance = 360 / orbitalcount;
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("PermaOrbital"), npc.whoAmI, l * distance, npc.whoAmI);
                    }
                }
                if (phase == 2)
                {
                    int orbitalcount = 4;
                    for (int l = 0; l < orbitalcount; l++)
                    {
                        int distance = 360 / orbitalcount;
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("PermaOrbital"), npc.whoAmI, l * distance, npc.whoAmI);
                    }
                }
                if (phase == 3)
                {
                    int orbitalcount = 5;
                    for (int l = 0; l < orbitalcount; l++)
                    {
                        int distance = 360 / orbitalcount;
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("PermaOrbital"), npc.whoAmI, l * distance, npc.whoAmI);
                    }
                }
                if (phase == 4)
                {
                    int orbitalcount = 7;
                    for (int l = 0; l < orbitalcount; l++)
                    {
                        int distance = 360 / orbitalcount;
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("PermaOrbital"), npc.whoAmI, l * distance, npc.whoAmI);
                    }
                }
                if (phase == 5)
                {
                    int orbitalcount = 9;
                    for (int l = 0; l < orbitalcount; l++)
                    {
                        int distance = 360 / orbitalcount;
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("PermaOrbital"), npc.whoAmI, l * distance, npc.whoAmI);
                    }
                }
                canSpawnOrbitals = false;
            }
            #endregion
            // bolts
            if (npc.ai[1] <= 1200)
            {
                animate = true;
                if (shootTimer <= 0)
                {
                    int angle = 12;
                    int numProj = 4;
                    if (npc.life <= npc.lifeMax * 0.5f)
                    {
                        angle = 20;
                        numProj = 5;
                    }
                    Bolts(P, 4.5f, projectileBaseDamage, numProj, angle);
                    shootTimer = enraged ? 20 : 55;
                    shootTimer += Main.rand.Next(0, 20);
                }
                Move(P, 7.5f);
            }
            // laser preparation
            if (npc.ai[1] == 1200)
            {
                spinAI = 0f;
                Main.PlaySound(29, (int)npc.position.X, (int)npc.position.Y, 105);
                npc.velocity.X = 0f;
                npc.velocity.Y = 0f;
            }
            // laser spins
            if (npc.ai[1] > 1350 && npc.ai[1] < 1700)
            {
                animate = false;
                npc.velocity.X = 0f;
                npc.velocity.Y = 0f;
                // shoot in circle
                Vector2 offset = new Vector2(400, 0);
                spinAI += enraged ? 0.06f : 0.015f;

                float projSpeed = 10f;
                int damage = npc.ai[1] <= 1410 ? 0 : projectileBaseDamage - 20;

                if (shootTimer <= 0)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                    int numProj = 2;
                    for (int i = 0; i < numProj; i++)
                    {
                        float projOffset = 360 / numProj;
                        Vector2 shootTarget = npc.Center + offset.RotatedBy(spinAI + (projOffset * i) * (Math.PI * 2 / 8));
                        float rotation = (float)Math.Atan2(npc.Center.Y - shootTarget.Y, npc.Center.X - shootTarget.X);
                        int proj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * projSpeed) * -1), (float)((Math.Sin(rotation) * projSpeed) * -1), mod.ProjectileType("PermafrostLaser"), damage, 0f, Main.myPlayer);
                        Main.projectile[proj].timeLeft = (int)(npc.ai[1] - 1350);
                    }
                    shootTimer = enraged ? 1 : 3;
                }
            }
            // preparation
            if (npc.ai[1] == 1700)
            {
                npc.ai[2] = 0f;
            }
            // fly to corner and shoot
            if (npc.ai[1] >= 1700)
            {
                if (lowLife == true && npc.localAI[0] == 0)
                {
                    string text = "Ice melts, but I am forever!";
                    Main.NewText(text, Color.Cyan.R, Color.Cyan.G, Color.Cyan.B);
                    npc.localAI[0]++;
                }
                animate = true;
                npc.ai[2]++;
                if (npc.ai[2] > 310)
                {
                    npc.ai[2] = 0f;
                }
                Vector2 targetPos = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y) + new Vector2(-500f, -400f);
                if (npc.ai[2] >= 0 && npc.ai[2] <= 100)
                {
                    targetPos = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y) + new Vector2(-500f, -400f);
                }
                if (npc.ai[2] >= 150 && npc.ai[2] <= 250)
                {
                    targetPos = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y) + new Vector2(500f, -400f);
                }
                MoveSpecific(P, 8f, targetPos);
                if ((npc.ai[2] > 100 && npc.ai[2] < 150) || (npc.ai[2] > 250 && npc.ai[2] < 300))
                {
                    if (shootTimer <= 0)
                    {
                        float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                        float speed = 4f;
                        Vector2 preSpeed = new Vector2((float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1));
                        Vector2 perturbedSpeed = new Vector2(preSpeed.X, preSpeed.Y).RotatedByRandom(MathHelper.ToRadians(30));
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y + 25, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("PermafrostBolt"), projectileBaseDamage - 10, 0f, Main.myPlayer, 0f, 0f);
                        shootTimer = enraged ? 1 : 4;
                    }
                    npc.velocity.X = 0f;
                    npc.velocity.Y = 0f;
                }
            }
        }
        private void Move(Player P, float moveSpeed)
        {
            Vector2 toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
            toTarget.Normalize();
            if (Vector2.Distance(P.Center, npc.Center) >= 30)
            {
                npc.velocity = toTarget * moveSpeed;
            }
        }
        private void MoveSpecific(Player P, float moveSpeed, Vector2 toTarget)
        {
            toTarget.Normalize();
            if (Vector2.Distance(P.Center, npc.Center) >= 30)
            {
                npc.velocity = toTarget * moveSpeed;
            }
        }
        private void Bolts(Player P, float speed, int damage, int numberProj, int angle)
        {
            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 12);
            float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
            for (int i = 0; i < numberProj; i++)
            {
                Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1)).RotatedByRandom(MathHelper.ToRadians(angle));
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("PermafrostBolt"), damage, 0f, Main.myPlayer);
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}
