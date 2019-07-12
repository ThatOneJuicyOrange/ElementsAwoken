using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class DesolationDisc : ModProjectile
    {
        NPC prevTarget = null;
        NPC currTarget = null;
        int[] hitCounter = new int[201];
        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 26;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.ranged = true;

            projectile.penetrate = 10;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desolation Disc");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            Lighting.AddLight(projectile.Center, 0.4f, 0.2f, 0.4f);
            for (int l = 0; l < 5; l++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("AncientGreen"))];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / 6f * (float)l;
                dust.noGravity = true;
                dust.scale = 1f;
            }
            NPC closestNPC = null;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                bool isPrevious = false;
                if (prevTarget != null)
                {
                    if (nPC == prevTarget)
                    {
                        isPrevious = true;
                    }
                }
                if (nPC.active && !isPrevious && nPC.CanBeChasedBy(projectile, false) && Collision.CanHit(projectile.Center, 1, 1, nPC.Center, 1, 1) && Vector2.Distance(nPC.Center, projectile.Center) < 600)
                {
                    bool isCloser = true;
                    if (closestNPC != null)
                    {
                        if (Vector2.Distance(nPC.Center, projectile.Center) > Vector2.Distance(closestNPC.Center, projectile.Center))
                        {
                            isCloser = false;
                        }
                    }
                    if (isCloser || closestNPC == null)
                    {
                        if (hitCounter[i] > 0)
                        {
                            if (!CheckForNearbyLessHitNPCs(nPC))
                            {
                                closestNPC = nPC;
                            }
                        }
                        else
                        {
                            closestNPC = nPC;
                        }
                    }
                }
            }
            //if (closestNPC != null) ElementsAwoken.DebugModeText(closestNPC);
            currTarget = closestNPC;

            if (currTarget != null)
            {
                double angle = Math.Atan2(currTarget.position.Y - projectile.position.Y, currTarget.position.X - projectile.position.X);
                projectile.velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 20f;
            }
        }
        public int CountNearbyNPCs()
        {
            int num = 0;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (Vector2.Distance(nPC.Center, projectile.Center) < 600 && nPC.active && nPC.CanBeChasedBy(projectile, false) && Collision.CanHit(projectile.Center, 1, 1, nPC.Center, 1, 1))
                {
                    num++;
                }
            }
            return num;
        }
        private bool CheckForNearbyLessHitNPCs(NPC currentNPC) // the worst name
        {
            int highestHits = 0;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.whoAmI != currentNPC.whoAmI && nPC.active)
                {
                    if (hitCounter[i] > highestHits)
                    {
                        highestHits = hitCounter[i];
                    }
                }
            }
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.whoAmI != currentNPC.whoAmI && nPC.active)
                {
                    if (Vector2.Distance(nPC.Center, projectile.Center) < 600 && hitCounter[i] < highestHits)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (currTarget != null)
            {
                if (target == currTarget)
                {
                    prevTarget = currTarget;
                }
            }
            hitCounter[target.whoAmI]++;

            if (CountNearbyNPCs() <= 1)
            {
                projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {
            // waves
            float numDusts = 48f;
            Vector2 shape = new Vector2(0.5f, 6f);
            float size = 1.5f;
            int numWaves = 3;
            for (int l = 0; l < numWaves; l++)
            {
                for (int k = 0; k < numDusts; k++)
                {
                    Vector2 vector11 = Vector2.UnitX * 0f;
                    vector11 += -Vector2.UnitY.RotatedBy((double)((float)k * ((Math.PI * 2) / numDusts)), default(Vector2)) * shape;
                    vector11 = vector11.RotatedBy((double)projectile.velocity.ToRotation() + MathHelper.ToRadians(l * (360 / numWaves)), default(Vector2));
                    Dust dust = Main.dust[Dust.NewDust(projectile.Center, 0, 0, mod.DustType("AncientGreen"), 0f, 0f, 0, default(Color), 1f)];
                    dust.scale = 1f;
                    dust.noGravity = true;
                    dust.position = projectile.Center + vector11;
                    dust.velocity = projectile.velocity * 0f + vector11.SafeNormalize(Vector2.UnitY) * size * ((l + 1) * 0.4f);
                }
            }

            // circles
            for (int i = 0; i < 10; i++)
            {
                Vector2 position = projectile.Center + Main.rand.NextVector2Circular(projectile.width * 0.5f, projectile.height * 0.5f);
                Dust dust = Dust.NewDustPerfect(position, mod.DustType("AncientGreen"), Vector2.Zero);
                dust.noGravity = true;
                dust.fadeIn = 2f;
            }
        }
    }
}
