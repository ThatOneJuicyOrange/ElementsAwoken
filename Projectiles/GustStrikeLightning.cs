using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class GustStrikeLightning : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public Vector2 velocity = new Vector2();
        public int[] hitCounter = new int[201];
        public int targetted = -1;
        public int tileCollideTimer = 0;
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.friendly = true;
            projectile.tileCollide = false;

            projectile.penetrate = 3;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 200;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Strange Ukulele");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            hitCounter[target.whoAmI]++;
        }
        public override void AI()
        {
            if (projectile.velocity.X != projectile.velocity.X)
            {
                projectile.position.X = projectile.position.X + projectile.velocity.X;
                projectile.velocity.X = -projectile.velocity.X;
            }
            if (projectile.velocity.Y != projectile.velocity.Y)
            {
                projectile.position.Y = projectile.position.Y + projectile.velocity.Y;
                projectile.velocity.Y = -projectile.velocity.Y;
            }
            int dustLength = ModContent.GetInstance<Config>().lowDust ? 1 : 3;
            for (int i = 0; i < dustLength; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 226)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / dustLength * (float)i;
                dust.noGravity = true;
            }
            if (velocity == Vector2.Zero) velocity = projectile.velocity;

            float targetX = projectile.Center.X;
            float targetY = projectile.Center.Y;
            float closestEntity = 200f;
            bool home = false;
            targetted = -1;
            for (int i = 0; i < 200; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.CanBeChasedBy(projectile, false) && Collision.CanHit(projectile.Center, 1, 1, nPC.Center, 1, 1))
                {
                    float dist = Math.Abs(projectile.Center.X - nPC.Center.X) + Math.Abs(projectile.Center.Y - nPC.Center.Y);
                    if (dist < closestEntity && !CheckForNearbyLessHitNPCs(nPC))
                    {
                        closestEntity = dist;
                        targetX = nPC.Center.X;
                        targetY = nPC.Center.Y;
                        targetted = nPC.whoAmI;
                        home = true;
                    }
                }
            }
            if (home)
            {
                float speed = 7f;
                float goToX = targetX - projectile.Center.X;
                float goToY = targetY - projectile.Center.Y;
                float dist = (float)Math.Sqrt((double)(goToX * goToX + goToY * goToY));
                dist = speed / dist;
                goToX *= dist;
                goToY *= dist;
                projectile.velocity = new Vector2((projectile.velocity.X * 20f + goToX) / 21f, (projectile.velocity.Y * 20f + goToY) / 21f).RotatedByRandom(.5);
                //velocity = projectile.velocity;
                return;
            }
            else
            {
                projectile.velocity = velocity.RotatedByRandom(2.5);
            }

            if (Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
            {
                tileCollideTimer++;
                if (tileCollideTimer > 10) projectile.Kill();
            }
            else tileCollideTimer = 0;
        }
        private bool CheckForNearbyLessHitNPCs(NPC currentNPC) // the worst name
        {
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.whoAmI != currentNPC.whoAmI && nPC.CanBeChasedBy(projectile, false) && Collision.CanHit(projectile.Center, 1, 1, nPC.Center, 1, 1))
                {
                    if (Vector2.Distance(nPC.Center, projectile.Center) < 200 && hitCounter[i] < hitCounter[currentNPC.whoAmI])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}