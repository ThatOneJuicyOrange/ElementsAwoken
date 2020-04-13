using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.ScourgeFighter
{
    public class ScourgeRocket : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.alpha = 0;
            projectile.timeLeft = 150;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scourge Rocket");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            for (int num255 = 0; num255 < 2; num255++)
            {
                float num256 = 0f;
                float num257 = 0f;
                if (num255 == 1)
                {
                    num256 = projectile.velocity.X * 0.5f;
                    num257 = projectile.velocity.Y * 0.5f;
                }
                Vector2 position71 = new Vector2(projectile.position.X + 3f + num256, projectile.position.Y + 3f + num257) - projectile.velocity * 0.5f;
                int width67 = projectile.width - 8;
                int height67 = projectile.height - 8;
                int num258 = Dust.NewDust(position71, width67, height67, DustID.PinkFlame, 0f, 0f, 100, default(Color), 1f);
                Dust dust = Main.dust[num258];
                dust.scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
                dust = Main.dust[num258];
                dust.velocity *= 0.2f;
                Main.dust[num258].noGravity = true;
                Vector2 position72 = new Vector2(projectile.position.X + 3f + num256, projectile.position.Y + 3f + num257) - projectile.velocity * 0.5f;
                int width68 = projectile.width - 8;
                int height68 = projectile.height - 8;
                num258 = Dust.NewDust(position72, width68, height68, 31, 0f, 0f, 100, default(Color), 0.5f);
                Main.dust[num258].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
                dust = Main.dust[num258];
                dust.velocity *= 0.05f;
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            projectile.penetrate--;
            if (projectile.penetrate <= 0)
            {
                projectile.Kill();
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.penetrate--;
            if (projectile.penetrate <= 0)
            {
                projectile.Kill();
            }
            return false;
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.HostileExplosion(projectile, DustID.PinkFlame);
        }
    }
}
 