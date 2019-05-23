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

namespace ElementsAwoken.NPCs.Bosses.ScourgeFighter
{
    [AutoloadBossHead]
    public class ScourgeFighter : ModNPC
    {
        public float burstTimer = 10f;
        public float shootCooldown = 10f;

        public float missileTimer = 30f;
        public float napalmTimer = 25f;

        public int minionTimer = 500;

        public float homingMissileTimer = 50f;

        public float homingMove = 0f;
        public float napalmMove = 0f;

        public int projectileBaseDamage = 40;

        int rocketDirection = 1;

        public override void SetDefaults()
        {
            npc.width = 96;
            npc.height = 92;

            npc.lifeMax = 35000;
            npc.damage = 60;
            npc.defense = 35;
            npc.knockBackResist = 0f;

            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;

            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;

            npc.value = Item.buyPrice(0, 15, 0, 0);

            music = MusicID.Boss3;
            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ScourgeFighterTheme");

            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[mod.BuffType("IceBound")] = true;
            npc.buffImmune[mod.BuffType("EndlessTears")] = true;

            bossBag = mod.ItemType("ScourgeFighterBag");

            NPCID.Sets.TrailCacheLength[npc.type] = 5;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scourge Fighter");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 75;
            npc.lifeMax = 50000;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 75000;
                npc.damage = 90;
                npc.defense = 45;
            }
        }
        // trail
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.5f);
            for (int k = 0; k < npc.oldPos.Length; k++)
            {
                Vector2 drawPos = npc.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
                Color color = npc.GetAlpha(lightColor) * ((float)(npc.oldPos.Length - k) / (float)npc.oldPos.Length);
                Texture2D texture = Main.npcTexture[npc.type];
                sb.Draw(texture, drawPos, null, color, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        // glow
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = mod.GetTexture("NPCs/Bosses/ScourgeFighter/Glow/ScourgeFighter_Glow");
            Rectangle frame = new Rectangle(0, texture.Height * npc.frame.Y, texture.Width, texture.Height);
            Vector2 origin = frame.Size() * 0.5f;
            SpriteEffects effects = npc.direction != 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), frame, new Color(255, 255, 255, 0), npc.rotation, origin, npc.scale, effects, 0.0f);
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ScourgeFighterTrophy"));
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ScourgeFighterMask"));
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
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ScourgeSword"));
                }
                if (choice == 1)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SignalBooster"));
                }
                if (choice == 2)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ScourgeFighterMachineGun"));
                }
                if (choice == 3)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ScourgeFighterRocketLauncher"));
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.RocketI, Main.rand.Next(50, 150));                 
                }
            }
            MyWorld.downedScourgeFighter = true;
            Main.NewText("Main.NewText(FATAL ERROR DETECTED. SYSTEM WILL NOW SHUT DOWN IMMEDIATELY.);", Color.Red.R, Color.Red.G, Color.Red.B);
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override void AI()
        {
            bool dayTime = Main.dayTime;
            Player P = Main.player[npc.target];
            npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;
            #region despawning
            if (!P.active || P.dead)
            {
                npc.TargetClosest(true);
                if (!P.active || P.dead)
                {
                    npc.ai[0]++;
                    npc.velocity.Y = npc.velocity.Y + 0.11f;
                    if (npc.ai[0] >= 300)
                    {
                        npc.active = false;
                    }
                }
                else
                    npc.ai[0] = 0;
            }
            if (Main.dayTime)
            {
                npc.ai[0]++;
                npc.velocity.Y = npc.velocity.Y + 0.11f;
                if (npc.ai[0] >= 300)
                {
                    npc.active = false;
                }
            }
#endregion
            //scourge fighter should teleport around the player and dash diagonally at the player, shoots homing rockets and shoots a targeting laser that inflicts debuff Targeted making your defense lowered by half for 10 seconds
            npc.ai[2] += 1f; //ai timer
            npc.ai[3] += 1f; //rockets are seperate from the ai
            minionTimer--;
            shootCooldown--;
            napalmTimer--;
            homingMissileTimer--;
            burstTimer--;
            if (shootCooldown <= 0)
            {
                shootCooldown = 80f;
            }

            if (npc.ai[2] > 1520f) // AI TIMER
            {
                npc.ai[2] = 0f;
            }

            if (npc.ai[3] > 1100f) // ROCKETS
            {
                npc.ai[3] = 0f;
            }
            //minions
            if (minionTimer <= 0)
            {
                Vector2 spawnAt = npc.Center + new Vector2(0f, (float)npc.height / 2f);
                NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("MiniFighter"));
                minionTimer = 750;
            }
            // fly at player and shoot bullets
            if (npc.ai[2] <= 720f)
            {
                Move(P, 6.5f);
                if (Main.netMode != 1 && burstTimer <= 0f && shootCooldown <= 30)
                {
                    BulletBurst(P, 10f, projectileBaseDamage);
                    burstTimer = 6f;
                }
            }
            // napalm preperation
            if (npc.ai[2] == 720f)
            {
                npc.ai[1] = 0f;
            }
            // napalm
            if (npc.ai[2] >= 720f && npc.ai[2] <= 1320f)
            {
                npc.ai[1]++;

                    if (npc.ai[1] > 400f) // NAPALM
                {
                    npc.ai[1] = 0f;
                }
                if (npc.ai[1] == 0f || npc.ai[1] == 200f)
                {
                    napalmMove = 0;
                }
                Vector2 targetPos = new Vector2(P.Center.X - 200 + napalmMove, P.Center.Y - 300); // position
                Vector2 toTarget = new Vector2(targetPos.X - npc.Center.X + napalmMove, targetPos.Y - npc.Center.Y); // velocity
                if (npc.ai[1] <= 200f)
                {
                    toTarget = new Vector2(P.Center.X - npc.Center.X - 200 + napalmMove, P.Center.Y - npc.Center.Y - 300);
                }
                if (npc.ai[1] >= 200f)
                {
                    toTarget = new Vector2(P.Center.X - npc.Center.X + 200 - napalmMove, P.Center.Y - npc.Center.Y - 300);
                }
                float increase = 9f;
                if (Vector2.Distance(npc.Center, targetPos) <= 100)
                {
                    increase = 20f;
                }
                napalmMove += increase;

                float moveSpeed = 8f;
                toTarget.Normalize();
                npc.velocity.X = toTarget.X * moveSpeed;
                npc.velocity.Y = toTarget.Y * moveSpeed * 1.25f;
                if (napalmTimer <= 0f)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 13);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 0, mod.ProjectileType("Napalm"), projectileBaseDamage, 0f, 0);
                    napalmTimer = 15f;
                }
            }
            // homing missile preparation
            if (npc.ai[2] == 1320f)
            {
                Main.NewText("Homing Missiles Firing", Color.Red.R, Color.Red.G, Color.Red.B);
                npc.ai[1] = 0f;
            }
            // homing missiles
            if (npc.ai[2] >= 1320f && npc.ai[2] <= 1520f)
            {
                if (Main.netMode != 1 && homingMissileTimer <= 0)
                {
                    float Speed = 6f;
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 72);
                    float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("ScourgeHomingRocket"), projectileBaseDamage - 20, 0f, 0);
                    homingMissileTimer = 50f;
                }
                npc.ai[1]++;
                homingMove += 6.5f;
                if (npc.ai[1] == 0f || npc.ai[1] == 100f)
                {
                    homingMove = 0;
                }
                Vector2 toTarget = new Vector2(P.Center.X - npc.Center.X - 200, P.Center.Y - npc.Center.Y + 300 - homingMove);
                if (npc.ai[1] <= 100f)
                {
                    toTarget = new Vector2(P.Center.X - npc.Center.X - 200, P.Center.Y - npc.Center.Y + 300 - homingMove);
                }
                if (npc.ai[1] >= 100f)
                {
                    toTarget = new Vector2(P.Center.X - npc.Center.X + 200, P.Center.Y - npc.Center.Y - 300 + homingMove);
                }
                float moveSpeed = 7f;
                toTarget.Normalize();
                npc.velocity = toTarget * moveSpeed;
            }
            //rocket preparation & direction determining
            if (npc.ai[3] == 1030)
            {
                Main.NewText("Missile Strike Incoming", Color.Red.R, Color.Red.G, Color.Red.B);
                switch (Main.rand.Next(2))
                {
                    case 0:
                        rocketDirection = 1;
                        break;
                    case 1:
                        rocketDirection = -1;
                        break;
                    default: break;
                }
            }
            // rockets
            if (npc.ai[3] == 1040 || npc.ai[3] == 1070 || npc.ai[3] == 1100)
            {
                for (int i = 0; i < Main.player.Length; i++)
                {
                    if (Main.player[i].active)
                    {
                        int numMissiles = 15;
                        Vector2 baseSpawn = new Vector2(P.Center.X + (500 * rocketDirection), P.Center.Y - 1000);
                        for (int l = 0; l < numMissiles; l++)
                        {
                            Vector2 spawn = baseSpawn;
                            spawn.X = spawn.X + (l * 200 * rocketDirection) - (numMissiles * 15);
                            Projectile.NewProjectile(spawn.X, spawn.Y, -6 * rocketDirection, 10, mod.ProjectileType("ScourgeRocket"), projectileBaseDamage + 20, 10f, Main.myPlayer, 0f, 0f);
                        }
                    }
                }
            }
            
            // dusts
            for (int i = 0; i < 2; i++)
            {
                int width = 16;
                int dust = Dust.NewDust(new Vector2(npc.Center.X - width / 2, npc.Center.Y - width / 2), width, width, DustID.PinkFlame, 0f, 0f, 100, default(Color), 1);
                Main.dust[dust].scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
                Main.dust[dust].velocity *= 0.2f;
                Main.dust[dust].noGravity = true;

                dust = Dust.NewDust(new Vector2(npc.Center.X - width / 2, npc.Center.Y - width / 2), width, width, 31, 0f, 0f, 100, default(Color), 0.5f);
                Main.dust[dust].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
                Main.dust[dust].velocity *= 0.05f;
            }
        }
        private void Move(Player P, float moveSpeed)
        {
            Vector2 toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
            toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
            toTarget.Normalize();
            if (Vector2.Distance(P.Center, npc.Center) >= 30)
            {
                npc.velocity = toTarget * moveSpeed;
            }
        }

        private void BulletBurst(Player P, float speed, int damage)
        {
            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 11);

            float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X + 35 - P.Center.X);
            Projectile.NewProjectile(npc.Center.X + 35, npc.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), mod.ProjectileType("ScourgeBullet"), damage, 0f, 0);

            float rotation2 = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - 35 - P.Center.X);
            Projectile.NewProjectile(npc.Center.X - 35, npc.Center.Y, (float)((Math.Cos(rotation2) * speed) * -1), (float)((Math.Sin(rotation2) * speed) * -1), mod.ProjectileType("ScourgeBullet"), damage, 0f, 0);
        }

        public override bool CheckActive()
        {
            return false;
        }
    }
}
