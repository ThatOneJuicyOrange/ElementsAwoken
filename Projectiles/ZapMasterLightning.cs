using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ZapMasterLightning : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public Vector2 velocity = new Vector2();
        public int[] hitCounter = new int[201];

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.friendly = true;
            projectile.tileCollide = true;

            projectile.penetrate = 5;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 100;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Strange Ukulele");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            hitCounter[target.whoAmI]++;

            Vector2 loc = new Vector2();

            float closestEntity = 200f;
            bool home = false;
            int whoAmI = -1;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.CanBeChasedBy(projectile, false) && Collision.CanHit(projectile.Center, 1, 1, nPC.Center, 1, 1))
                {
                    float dist = Math.Abs(projectile.Center.X - nPC.Center.X) + Math.Abs(projectile.Center.Y - nPC.Center.Y);
                    if (dist < closestEntity && !CheckForNearbyLessHitNPCs(nPC) && nPC.whoAmI != whoAmI)
                    {
                        closestEntity = dist;
                        loc = nPC.Center;
                        home = true;
                        whoAmI = nPC.whoAmI;
                    }
                }
            }
            if (home)
            {
                float speed = 7f;
                double angle = Math.Atan2(loc.Y - projectile.position.Y, loc.X - projectile.position.X);
                velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * speed;
            }
            else projectile.Kill();
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
            projectile.localAI[0]++;
            if (projectile.localAI[0] > 7)
            {
                int dustLength = ModContent.GetInstance<Config>().lowDust ? 1 : 3;
                for (int i = 0; i < dustLength; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 226)];
                    dust.velocity = Vector2.Zero;
                    dust.position -= projectile.velocity / dustLength * (float)i;
                    dust.noGravity = true;
                }
            }
            if (velocity == Vector2.Zero) velocity = projectile.velocity;
             projectile.velocity = velocity.RotatedByRandom(.75);
           
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