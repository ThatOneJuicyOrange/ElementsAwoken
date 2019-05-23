using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class LifesLamentAxe : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 40;
            projectile.aiStyle = 18;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.tileCollide = true;
            projectile.penetrate = 2;
            aiType = 45;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lifes Lament");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            {
                target.immune[projectile.owner] = 3;
            }
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.3f) / 255f, ((255 - projectile.alpha) * 0.4f) / 255f, ((255 - projectile.alpha) * 1f) / 255f);
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 60);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1.2f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }
    }
}