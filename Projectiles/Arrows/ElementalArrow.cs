using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Arrows
{
    public class ElementalArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 2;
            projectile.timeLeft = 600;
            projectile.alpha = 0;
            projectile.light = 1f;
            projectile.extraUpdates = 1;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            aiType = ProjectileID.Bullet;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elemental Arrow");
        }
        public override void AI()
        {
            if (Main.rand.Next(3) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 242);
                Main.dust[dust].scale *= 0.8f;
                Main.dust[dust].noGravity = true;
                int dust1 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6);
                Main.dust[dust1].scale *= 0.8f;
                Main.dust[dust1].noGravity = true;
                int dust2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 229);
                Main.dust[dust2].scale *= 0.8f;
                Main.dust[dust2].noGravity = true;
                int dust3 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 197);
                Main.dust[dust3].scale *= 0.8f;
                Main.dust[dust3].noGravity = true;
            }
        }
    }
}