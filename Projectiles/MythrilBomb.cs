using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class MythrilBomb : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;

            projectile.penetrate = 1;

            projectile.magic = true;
            projectile.friendly = true;

            projectile.timeLeft = 120;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mythril Bomb");
        }
        public override void AI()
        {
            projectile.rotation += projectile.velocity.X * 0.2f;
            projectile.velocity.Y += 0.2f;

        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.ai[0]++;
            if (projectile.ai[0] > 5)
            {
                projectile.Kill();
            }
            else
            {
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }
                projectile.velocity *= 0.5f;
            }
            return false;
        }
        public override void Kill(int timeLeft)
        {
            int numProj = 5;
            for (int i = 0; i < numProj; i++)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.NextFloat(-5,5), Main.rand.NextFloat(-5, 5), mod.ProjectileType("MythrilSpike"), (int)(projectile.damage * 0.5f), 0f, projectile.owner, 0f, 0f);
            }
        }
    }
}