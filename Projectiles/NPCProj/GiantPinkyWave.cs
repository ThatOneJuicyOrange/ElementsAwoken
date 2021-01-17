using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj
{
    public class GiantPinkyWave : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 34;
            projectile.height = 16;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 600;
            projectile.alpha = 60;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slime Wave");
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override void AI()
        {
            projectile.velocity.Y = 3f;
            projectile.spriteDirection = Math.Sign(projectile.velocity.X);
            if (Math.Abs(projectile.velocity.X) < 0.1f)
            {
                projectile.Kill();
            }
            if (projectile.ai[0] == 0)
            {
                projectile.scale = 0.01f;
                projectile.ai[0] = 1;
            }
            if (projectile.scale < 1) projectile.scale += 0.05f;

            Dust dust1 = Main.dust[Dust.NewDust(projectile.BottomLeft - new Vector2(0,2), projectile.width, 2, 4, projectile.velocity.X, projectile.velocity.Y, 100, new Color(219, 0, 219, 100), 1.2f)];
            dust1.noGravity = true;
            dust1.velocity *= 0.5f;
            dust1.velocity.Y *= 0.1f;
        }
    }
}