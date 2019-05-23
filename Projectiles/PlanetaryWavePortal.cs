using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class PlanetaryWavePortal : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.alpha = 0;
            projectile.timeLeft = 120;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Planetary Wave");
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            projectile.Center = player.Center - new Vector2(0, 70);

            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 220, 0f, 0f, 100, default(Color), 1f);
                Main.dust[dust].velocity *= 0.3f;
                Main.dust[dust].fadeIn = 0.9f;
                Main.dust[dust].noGravity = true;
            }
            projectile.localAI[1]++;
            int numProj = 5;
            if (projectile.localAI[0] < numProj && projectile.localAI[1] % 3 == 0)
            {
                float speedX = projectile.ai[0];
                float speedY = projectile.ai[1];
                float rotation = MathHelper.ToRadians(1.75f);
                float amount = (projectile.localAI[0] - numProj / 2);
                float amount2 = player.direction == -1 ? amount - 3 : -amount + 3; // to make it from down to up. 3 is to roughly centralise the middle projectile
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, amount2));
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("PlanetarySpike"), projectile.damage, projectile.knockBack, player.whoAmI);
                projectile.localAI[0]++;
            }
            if (projectile.localAI[0] >= numProj)
            {
                projectile.alpha += 30;
                if (projectile.alpha >= 255)
                {
                    projectile.Kill();
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 220, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 360);
        }       
    }
}