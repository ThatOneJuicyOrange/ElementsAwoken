using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class CloudBlast : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 38;

            projectile.friendly = true;
            projectile.melee = true;
            projectile.tileCollide = false;

            projectile.penetrate = 2;
            projectile.timeLeft = 100;
            projectile.light = 0.75f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sky Fury");
        }
        public override void AI()
        {
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 16);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 0.8f;
            Main.dust[dust].velocity *= 0.1f;
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            projectile.localAI[0] += 1f;
            projectile.alpha = (int)projectile.localAI[0] * 2;

            int hitboxSize = 16;
            if (Collision.SolidCollision(projectile.Center - new Vector2(hitboxSize / 2, hitboxSize / 2), hitboxSize, hitboxSize))
            {
                projectile.Kill();
            }
        }

    }
}