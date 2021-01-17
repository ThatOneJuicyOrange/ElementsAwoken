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

namespace ElementsAwoken.NPCs.Bosses.Obsidious.Old
{
    public class ObsidiousHand : ModNPC
    {
        private float handSwipeTimer = 0;
        private float direction
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float swipeAI
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float aiTimer
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(handSwipeTimer);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            handSwipeTimer = reader.ReadSingle();
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 10000;
            npc.damage = 90;
            npc.defense = 25;
            npc.knockBackResist = 0f;

            npc.aiStyle = -1;

            npc.width = 52;
            npc.height = 76;

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.immortal = true;
            npc.netAlways = true;
            npc.noTileCollide = true;
            npc.dontTakeDamage = true;
            npc.npcSlots = 1f;

            NPCID.Sets.TrailCacheLength[npc.type] = 3;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Obsidious Hand");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 140;
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            NPC parent = Main.npc[(int)npc.ai[1]];
            if (parent.ai[1] == 3)
            {
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("ExplosionHostile"), npc.damage, 1f, 0, 0f, 0f);
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 14);
                for (int num369 = 0; num369 < 20; num369++)
                {
                    int num370 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, DustID.PinkFlame, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[num370].velocity *= 1.4f;
                }
                for (int num371 = 0; num371 < 10; num371++)
                {
                    int num372 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 62, 0f, 0f, 100, default(Color), 2.5f);
                    Main.dust[num372].noGravity = true;
                    Main.dust[num372].velocity *= 5f;
                    num372 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 62, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[num372].velocity *= 3f;
                }
                int num373 = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num373].velocity *= 0.4f;
                Gore gore85 = Main.gore[num373];
                gore85.velocity.X = gore85.velocity.X + 1f;
                Gore gore86 = Main.gore[num373];
                gore86.velocity.Y = gore86.velocity.Y + 1f;
                num373 = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num373].velocity *= 0.4f;
                Gore gore87 = Main.gore[num373];
                gore87.velocity.X = gore87.velocity.X - 1f;
                Gore gore88 = Main.gore[num373];
                gore88.velocity.Y = gore88.velocity.Y + 1f;
                num373 = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num373].velocity *= 0.4f;
                Gore gore89 = Main.gore[num373];
                gore89.velocity.X = gore89.velocity.X + 1f;
                Gore gore90 = Main.gore[num373];
                gore90.velocity.Y = gore90.velocity.Y - 1f;
                num373 = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num373].velocity *= 0.4f;
                Gore gore91 = Main.gore[num373];
                gore91.velocity.X = gore91.velocity.X - 1f;
                Gore gore92 = Main.gore[num373];
                gore92.velocity.Y = gore92.velocity.Y - 1f;
            }
        }

        public override void AI()
        {
            Lighting.AddLight(npc.Center, 0.5f, 0.5f, 0.5f);
            NPC parent = Main.npc[(int)npc.ai[1]];
            Player player = Main.player[npc.target];
            npc.active = parent.active;
            if (npc.localAI[0] == 0)
            {
                // bad way to do this probably :lul:
                Projectile.NewProjectile(npc.position.X, npc.position.Y, 0, 0, mod.ProjectileType("ObsidiousHandOverlay"), 0, 0, Main.myPlayer, 0, npc.whoAmI);
                npc.alpha = 255; // so u cant see the weird ass offset :shruggy:
                npc.localAI[0]++;
                npc.netUpdate = true;
            }
            if (parent.ai[1] == 2)
            {
                npc.localAI[1] = 0;
                npc.netUpdate = true;
            }
            if (parent.ai[1] == 1 && npc.localAI[1] == 0)
            {
                int orbitalCount = 3;
                for (int l = 0; l < orbitalCount; l++)
                {
                    int distance = 360 / orbitalCount;
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("ObsidiousRockOrbital"), npc.damage, 0f, Main.myPlayer, l * distance, npc.whoAmI);
                }
                npc.localAI[1]++;
                npc.netUpdate = true;
            }
            if (parent.ai[3] != 1)
            {
                aiTimer++;
                if (aiTimer >= 300)
                {
                    aiTimer = 0;
                    swipeAI++;
                }
                if (swipeAI > 1)
                {
                    swipeAI = 0;
                }
                if (swipeAI == 0)
                {
                    float targetX = parent.Center.X + 110 * direction - (npc.width * 0.5f) * direction;
                    float targetY = parent.Center.Y + 50 - (npc.height * 0.5f);
                    int maxDist = 1000;
                    if (Vector2.Distance(new Vector2(targetX, targetY), npc.Center) >= maxDist)
                    {
                        float moveSpeed = 8f;
                        Vector2 toTarget = new Vector2(targetX, targetY);
                        toTarget.Normalize();
                        npc.velocity = toTarget * moveSpeed;
                    }
                    else
                    {
                        if (npc.Center.Y > targetY)
                        {
                            if (npc.velocity.Y > 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y * 0.96f;
                            }
                            npc.velocity.Y = npc.velocity.Y - 0.07f;
                            if (npc.velocity.Y > 3f)
                            {
                                npc.velocity.Y = 3f;
                            }
                        }
                        else if (npc.Center.Y < targetY)
                        {
                            if (npc.velocity.Y < 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y * 0.96f;
                            }
                            npc.velocity.Y = npc.velocity.Y + 0.07f;
                            if (npc.velocity.Y < -3f)
                            {
                                npc.velocity.Y = -3f;
                            }
                        }
                        if (npc.Center.X > targetX)
                        {
                            if (npc.velocity.X > 0f)
                            {
                                npc.velocity.X = npc.velocity.X * 0.96f;
                            }
                            npc.velocity.X = npc.velocity.X - 0.35f;
                            if (npc.velocity.X > 12f)
                            {
                                npc.velocity.X = 12f;
                            }
                        }
                        else if (npc.Center.X < targetX)
                        {
                            if (npc.velocity.X < 0f)
                            {
                                npc.velocity.X = npc.velocity.X * 0.96f;
                            }
                            npc.velocity.X = npc.velocity.X + 0.35f;
                            if (npc.velocity.X < -12f)
                            {
                                npc.velocity.X = -12f;
                            }
                        }
                    }
                    npc.rotation = 0;
                    npc.spriteDirection = (int)direction;
                }
                if (swipeAI == 1)
                {
                    Projectile.NewProjectile(npc.Center.X + 10 * direction, npc.Center.Y - 20, 0, 0, mod.ProjectileType("ObsidiousHandTrail"), (int)(npc.damage / 2), 1, Main.myPlayer, parent.ai[1]);

                    handSwipeTimer++;

                    float speed = 15f;
                    float num25 = player.Center.X - npc.Center.X;
                    float num26 = player.Center.Y - npc.Center.Y;
                    float num27 = (float)Math.Sqrt(num25 * num25 + num26 * num26); // pythagorus distance between points
                    num27 = speed / num27;
                    npc.velocity.X = num25 * num27;
                    npc.velocity.Y = num26 * num27;
                    if (handSwipeTimer >= 30)
                    {
                        swipeAI++;
                        handSwipeTimer = 0;
                        if (parent.ai[1] == 2)
                        {
                            int numberProjectiles = parent.life <= parent.lifeMax * 0.5f ? Main.rand.Next(4, 8) : Main.rand.Next(2, 4);
                            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 1);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(npc.velocity.X, npc.velocity.Y).RotatedByRandom(MathHelper.ToRadians(10));
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("ObsidiousIceCrystal"), npc.damage / 2, 0f, Main.myPlayer, 0f, 0f);
                            }
                        }
                    }
                    npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) - 1.57f;
                    Vector2 playerDir = player.Center - npc.Center;
                    if (playerDir.X > 0f)
                    {
                        npc.spriteDirection = 1;
                    }
                    if (playerDir.X < 0f)
                    {
                        npc.spriteDirection = -1;
                    }
                }
            }
            else
            {            
                float targetX = parent.Center.X + 80 * direction - (npc.width * 0.5f) * direction;
                float targetY = parent.Center.Y + 60 - (npc.height * 0.5f);
                int maxDist = 1000;
                if (Vector2.Distance(new Vector2(targetX, targetY), npc.Center) >= maxDist)
                {
                    float moveSpeed = 8f;
                    Vector2 toTarget = new Vector2(targetX, targetY);
                    toTarget.Normalize();
                    npc.velocity = toTarget * moveSpeed;
                }
                else
                {
                    if (npc.Center.Y > targetY)
                    {
                        if (npc.velocity.Y > 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y * 0.96f;
                        }
                        npc.velocity.Y = npc.velocity.Y - 0.15f;
                        if (npc.velocity.Y > 3f)
                        {
                            npc.velocity.Y = 3f;
                        }
                    }
                    else if (npc.Center.Y < targetY)
                    {
                        if (npc.velocity.Y < 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y * 0.96f;
                        }
                        npc.velocity.Y = npc.velocity.Y + 0.15f;
                        if (npc.velocity.Y < -3f)
                        {
                            npc.velocity.Y = -3f;
                        }
                    }
                    if (npc.Center.X > targetX)
                    {
                        if (npc.velocity.X > 0f)
                        {
                            npc.velocity.X = npc.velocity.X * 0.96f;
                        }
                        npc.velocity.X = npc.velocity.X - 0.4f;
                        if (npc.velocity.X > 12f)
                        {
                            npc.velocity.X = 12f;
                        }
                    }
                    else if (npc.Center.X < targetX)
                    {
                        if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X = npc.velocity.X * 0.96f;
                        }
                        npc.velocity.X = npc.velocity.X + 0.4f;
                        if (npc.velocity.X < -12f)
                        {
                            npc.velocity.X = -12f;
                        }
                    }
                }
                npc.rotation = 0;
                npc.spriteDirection = (int)direction;
            }
        }

        public override bool CheckActive()
        {
            return false;
        }
    }
}
