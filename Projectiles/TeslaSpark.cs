using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class TeslaSpark : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 3;
            projectile.timeLeft = 120;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tesla Spark");
        }
        public override void AI()
        {
            if (Main.rand.Next(2) == 0)
            {
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 226);
                Main.dust[dust].velocity *= 0.6f;
                Main.dust[dust].scale *= 0.6f;
                Main.dust[dust].noGravity = true;
            }
            projectile.velocity *= 0.99f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (target.knockBackResist != 0f)
            {
                target.velocity.Y -= 1f; // little jolt
                target.velocity.X *= 0.5f;
            }
        }
    }
}