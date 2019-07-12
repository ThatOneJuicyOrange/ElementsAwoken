using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Arrows
{
    public class DesolationShard : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;

            projectile.timeLeft = 60;

            projectile.friendly = true;
            projectile.ranged = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desolation Shard");
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.localAI[0] <= 15)
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override void AI()
        {
            projectile.localAI[0]++;
            projectile.rotation += 0.1f;
            projectile.velocity *= 0.97f;
            if (Math.Abs(projectile.velocity.X) < 0.1f && Math.Abs(projectile.velocity.Y) < 0.1f)
            {
                projectile.Kill();
            }
            if (Main.rand.Next(5) == 0)
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType("AncientGreen"), 0f, 0f, 100, default(Color), 1f)];
                dust.noGravity = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, mod.DustType("AncientGreen"), 0f, 0f, 100, default(Color));
                Main.dust[dust].noGravity = true;
            }
        }
    }
}