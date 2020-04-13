using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class GalacticaBlade2 : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.TerraBeam);
            Main.projFrames[projectile.type] = 1;
            projectile.scale = 1.2f;
            projectile.penetrate = -1;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.timeLeft = 300;
            ProjectileID.Sets.Homing[projectile.type] = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Galactica Blade");
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.3f) / 255f, ((255 - projectile.alpha) * 0.4f) / 255f, ((255 - projectile.alpha) * 1f) / 255f);
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 135);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1.2f;
            int dust2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 242);
            Main.dust[dust2].noGravity = true;
            Main.dust[dust2].scale = 1.2f;
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, new int[] { 135,242}, damageType: "melee");
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }
    }
}