using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using Terraria.Graphics.Effects;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class SuperGrenade : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;

            projectile.friendly = true;
            projectile.hostile = false;
            projectile.thrown = true;

            projectile.penetrate = -1;
            projectile.timeLeft = 300;
            //aiType = ProjectileID.WoodenArrowFriendly;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Super Grenade");
        }
        public override void AI()
        {
            if (projectile.ai[0] > 0)
            {
                Shockwave();
            }
            Main.NewText(Filters.Scene["Shockwave"].IsActive());
            
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.ai[0]++;
            ProjectileGlobal.Explosion(projectile, new int[] { 6 }, projectile.damage, "thrown");
        }
        public override void Kill(int timeLeft)
        {
            Filters.Scene["Shockwave"].Deactivate();
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.penetrate--;
            if (projectile.penetrate <= 0)
            {
                ProjectileGlobal.Explosion(projectile, new int[] { 6 }, projectile.damage, "thrown");
                projectile.ai[0]++;
            }
            else
            {
                projectile.ai[0] += 0.1f;
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }
            }
            return false;
        }
        private int rippleCount = 3;
        private int rippleSize = 200;
        private int rippleSpeed = 15;
        private float distortStrength = 100f;
        private void Shockwave()
        {
            projectile.alpha = 255;
            projectile.friendly = false;
            projectile.velocity *= 0f;
            projectile.ai[1]++;
            if (projectile.ai[1] > 180)
            {
                projectile.Kill();
            }
            else
            {
                if (!Filters.Scene["Shockwave"].IsActive())
                {
                    Filters.Scene.Activate("Shockwave", projectile.Center).GetShader().UseColor(rippleCount, rippleSize, rippleSpeed).UseTargetPosition(projectile.Center);
                }
                float progress = projectile.ai[1] / 60f;
                float distortStrength = 200;
                Filters.Scene["Shockwave"].GetShader().UseProgress(progress).UseOpacity(distortStrength * (1 - progress / 3f));
            }
        }
    }
}