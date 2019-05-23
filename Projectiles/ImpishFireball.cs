using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ImpishFireball : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.alpha = 255;
            projectile.timeLeft = 45;

            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = false;
            projectile.magic = true;

            projectile.penetrate = -1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Bolt");
        }
        public override void AI()
        {
            int dustLength = 6;
            for (int i = 0; i < dustLength; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 6)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / dustLength * (float)i;
                dust.noGravity = true;
            }

            // thanks kitty
            float rotateIntensity = 3f;
            float waveTime = 10f;
            projectile.ai[0]++;
            if (projectile.ai[1] == 0) // this part is to fix the offset (it is still slightlyyyy offset)
            {
                if (projectile.ai[0] > waveTime * 0.5f)
                {
                    projectile.ai[0] = 0;
                    projectile.ai[1] = 1;
                }
                else
                {
                    Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(-rotateIntensity));
                    projectile.velocity = perturbedSpeed;
                }
            }
            else
            {
                if (projectile.ai[0] <= waveTime)
                {
                    Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(rotateIntensity));
                    projectile.velocity = perturbedSpeed;
                }
                else
                {
                    Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(-rotateIntensity));
                    projectile.velocity = perturbedSpeed;
                }
                if (projectile.ai[0] >= waveTime * 2)
                {
                    projectile.ai[0] = 0;
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 180);
            target.AddBuff(mod.BuffType("ImpishCurse"), 240);
        }
    }
}