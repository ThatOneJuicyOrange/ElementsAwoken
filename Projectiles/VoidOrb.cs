using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles
{
    public class VoidOrb : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.melee = true;
            projectile.tileCollide = false;

            projectile.penetrate = -1;

            projectile.timeLeft = 300;

            projectile.light = 1f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Orb");
            Main.projFrames[projectile.type] = 4;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 6)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                    projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            projectile.velocity *= 0.97f;

            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, DustID.PinkFlame, damageType: "melee");
        }
    }
}