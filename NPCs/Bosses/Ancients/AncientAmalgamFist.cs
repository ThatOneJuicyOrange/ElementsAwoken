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

namespace ElementsAwoken.NPCs.Bosses.Ancients
{
    public class AncientAmalgamFist : ModNPC
    {
        float handSwipeTimer = 0;
        bool hasOverlay = false;
        float despawnDelay = 2;

        public int spinAI = 0;
        public int spinTimer = 0;
        public int spinProjTimer = 0;
        public Vector2 spinOrigin = new Vector2();
        public int spinDetectDelay = 0;
        public bool spinDontProj = false;

        public float[] attackAI = new float[4];

        public override void SetDefaults()
        {
            npc.lifeMax = 10000;
            npc.damage = 90;
            npc.defense = 25;
            npc.knockBackResist = 0f;

            npc.width = 58;
            npc.height = 72;

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.immortal = true;
            npc.netAlways = true;
            npc.noTileCollide = true;
            npc.dontTakeDamage = true;
            npc.npcSlots = 1f;
            npc.scale *= 1.3f;

            NPCID.Sets.TrailCacheLength[npc.type] = 3;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Amalgamate Fist");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 140;
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (npc.ai[2] > 100)
            {
                return false;
            }
            return base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override void AI()
        {
            Lighting.AddLight(npc.Center, 0.5f, 0.5f, 0.5f);
            NPC parent = Main.npc[(int)npc.ai[1]];
            Player player = Main.player[npc.target];

            List<int> noSwingHands = new List<int>();
            noSwingHands.Add(2);
            noSwingHands.Add(5);
            noSwingHands.Add(6);
            noSwingHands.Add(8);
            bool dontSwing = false;
            foreach (int k in noSwingHands)
            {
                if (parent.ai[2] == k)
                {
                    dontSwing = true;
                }
            }
            npc.ai[2] = parent.alpha;
            if (!parent.active)
            {
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC newParent = Main.npc[i];
                    if (newParent.type == mod.NPCType("AncientAmalgamDeath") && newParent.ai[1] == npc.ai[1])
                    {
                        npc.ai[1] = i;
                    }
                }

                despawnDelay--;
                if (despawnDelay <= 0)
                {
                    npc.active = false;
                }
            }
        
            else
            {
                despawnDelay = 2;
            }
            if (!hasOverlay)
            {
                // bad way to do this probably :lul:
                Projectile.NewProjectile(npc.position.X, npc.position.Y, 0, 0, mod.ProjectileType("AAHandOverlay"), 0, 0, Main.myPlayer, 0, npc.whoAmI);
                npc.alpha = 255; // so u cant see the weird ass offset :shruggy:
                hasOverlay = true;
            }
            if (parent.type == mod.NPCType("AncientAmalgamDeath"))
            {
                attackAI[2] = 0; // stop swiping
                npc.damage = 0;
            }
            if (dontSwing)
            {
                attackAI[2] = 0; // stop swiping
            }
            if (parent.ai[2] != 2)
            {
                if (attackAI[2] == 0)
                {
                    npc.ai[3]++;
                    if (npc.ai[3] >= 300)
                    {
                        npc.ai[3] = 0;
                        attackAI[2]++;
                    }
                    int fistX1 = 90;
                    if (parent.direction == 1)
                    {
                        fistX1 = 105;
                    }
                    int fistX2 = 105;
                    if (parent.direction == 1)
                    {
                        fistX2 = 90;
                    }
                    int fistPos = npc.ai[0] == 1 ? fistX1 : -fistX2;
                    float targetX = parent.Center.X + fistPos;
                    float targetY = parent.Center.Y + 140 - (npc.height * 0.5f);

                    if (Vector2.Distance(new Vector2(targetX, targetY), npc.Center) >= 40)
                    {
                        npc.rotation = -((float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) - 1.57f);
                    }
                    else
                    {
                        npc.rotation = 0;
                    }

                    int maxDist = 500;
                    if (Vector2.Distance(new Vector2(targetX, targetY), npc.Center) >= maxDist)
                    {
                        float moveSpeed = 18f;
                        Vector2 toTarget = new Vector2(targetX, targetY) - npc.Center;
                        toTarget.Normalize();
                        npc.velocity = toTarget * moveSpeed;
                    }
                    else
                    {
                        float speed = 1f;
                        if (npc.Center.Y > targetY)
                        {
                            if (npc.velocity.Y > 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y * 0.96f;
                            }
                            npc.velocity.Y = npc.velocity.Y - speed;
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
                            npc.velocity.Y = npc.velocity.Y + speed;
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
                            npc.velocity.X = npc.velocity.X - speed;
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
                            npc.velocity.X = npc.velocity.X + speed;
                            if (npc.velocity.X < -12f)
                            {
                                npc.velocity.X = -12f;
                            }
                        }
                    }
                    npc.spriteDirection = (int)npc.ai[0];
                }
                if (attackAI[2] == 1) // move away
                {
                    handSwipeTimer++;

                    float speed = 8f;
                    float num25 = player.Center.X - npc.Center.X;
                    float num26 = player.Center.Y - npc.Center.Y;
                    float num27 = (float)Math.Sqrt(num25 * num25 + num26 * num26);
                    num27 = speed / num27;
                    npc.velocity.X = -(num25 * num27);
                    npc.velocity.Y = -(num26 * num27);
                    if (handSwipeTimer >= 45)
                    {
                        attackAI[2]++;
                        handSwipeTimer = 0;
                    }
                    npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) - 3.14f;
                    Vector2 direction = player.Center - npc.Center;
                    if (direction.X > 0f)
                    {
                        npc.spriteDirection = 1;
                    }
                    if (direction.X < 0f)
                    {
                        npc.spriteDirection = -1;
                    }
                }
                if (attackAI[2] == 2) // swipe
                {
                    handSwipeTimer++;

                    float speed = 15f;
                    float num25 = player.Center.X - npc.Center.X;
                    float num26 = player.Center.Y - npc.Center.Y;
                    float num27 = (float)Math.Sqrt(num25 * num25 + num26 * num26);
                    num27 = speed / num27;
                    npc.velocity.X = num25 * num27;
                    npc.velocity.Y = num26 * num27;
                    if (handSwipeTimer >= 45 || Vector2.Distance(player.Center, npc.Center) < 20 || Vector2.Distance(parent.Center, npc.Center) > 500)
                    {
                        attackAI[2] = 0;
                        handSwipeTimer = 0;
                    }
                    npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) - 1.57f;
                    Vector2 direction = player.Center - npc.Center;
                    if (direction.X > 0f)
                    {
                        npc.spriteDirection = 1;
                    }
                    if (direction.X < 0f)
                    {
                        npc.spriteDirection = -1;
                    }
                }
                npc.localAI[0] = 0;
            }
            else
            {
                if (npc.localAI[0] == 0)
                {
                    spinTimer = npc.ai[0] == 1 ? 180 : 0;
                    spinDetectDelay = 30;
                    spinDontProj = false;
                    npc.localAI[0]++;
                }
                int distance = 300;
                double rad = spinTimer * (Math.PI / 180); // angle to radians
                float spinX = parent.Center.X - (int)(Math.Cos(rad) * distance) - npc.width / 2;
                float spinY = parent.Center.Y - (int)(Math.Sin(rad) * distance) - npc.height / 2;
                Vector2 target = new Vector2(spinX, spinY);


                if (spinAI == 0)
                {
                    spinOrigin = target;
                    Vector2 toTarget = new Vector2(target.X - npc.Center.X, target.Y - npc.Center.Y);
                    toTarget.Normalize();
                    if (Vector2.Distance(target, npc.Center) > 20)
                    {
                        npc.velocity = toTarget * 16;
                    }
                    else
                    {
                        spinAI = 1;
                    }
                    npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) - 1.57f;
                }
                else
                {
                    spinTimer += 3; // speed

                    npc.velocity *= 0f;

                    npc.position.X = spinX;
                    npc.position.Y = spinY;

                    Vector2 direction = parent.Center - npc.Center;
                    npc.rotation = direction.ToRotation() + 1.57f;

                    spinProjTimer--;
                    if (spinProjTimer <= 0 && !spinDontProj)
                    {
                        Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("CrystalFlower"), npc.damage, 8f, 0)];
                        proj.rotation = npc.rotation;
                        spinProjTimer = 3;
                    }
                }
                spinDetectDelay--;
                if (spinDetectDelay <= 0)
                {
                    if (Vector2.Distance(spinOrigin, npc.Center) < 75)
                    {
                        spinDontProj = true;
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
