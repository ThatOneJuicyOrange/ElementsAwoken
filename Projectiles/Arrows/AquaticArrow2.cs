using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Arrows
{
    public class AquaticArrow2 : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.arrow = true;
            projectile.width = 10;
            projectile.height = 10;
            //projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquatic Arrow");
        }
        public override void AI()
        {
            Lighting.AddLight((int)projectile.Center.X, (int)projectile.Center.Y, 0f, 0.1f, 0.5f);
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            Dust dust = Main.dust[Dust.NewDust(projectile.Center, 4, 4, 111)];
            dust.velocity *= 0.6f;
            dust.scale *= 0.6f;
            dust.noGravity = true;
        }
    }
}