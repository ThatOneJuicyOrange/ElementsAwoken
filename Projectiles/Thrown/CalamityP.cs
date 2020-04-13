using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class CalamityP : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 52;
            projectile.height = 52;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = 6;
            projectile.aiStyle = 3;
            projectile.timeLeft = 1600;
            aiType = 52;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Calamity");
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.05f) / 255f, ((255 - projectile.alpha) * 0.25f) / 255f, ((255 - projectile.alpha) * 0.01f) / 255f);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
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
            ProjectileUtils.Explosion(projectile, 127, damageType: "thrown");
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("HandsOfDespair"), 180);
            ProjectileUtils.Explosion(projectile, 127, damageType: "thrown");
        }
    }
}