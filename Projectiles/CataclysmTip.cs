using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class CataclysmTip : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.TerraBeam);
            Main.projFrames[projectile.type] = 1;
            projectile.scale = 1.2f;
            projectile.penetrate = 1;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.timeLeft = 300;
            ProjectileID.Sets.Homing[projectile.type] = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cataclysm");
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, 127, damageType: "melee");
            //MORE BLADES
            {
                Vector2 vector8 = new Vector2(projectile.position.X + (projectile.width / 2), projectile.position.Y + (projectile.height / 2));
                int damage = 40;
                int type = mod.ProjectileType("CataclysmicLaser");
                Projectile.NewProjectile(vector8.X, vector8.Y, 0, 12, type, damage, 0f, 0);
                Projectile.NewProjectile(vector8.X, vector8.Y, 0, -12, type, damage, 0f, 0);
                Projectile.NewProjectile(vector8.X, vector8.Y, 12, 0, type, damage, 0f, 0);
                Projectile.NewProjectile(vector8.X, vector8.Y, -12, 0, type, damage, 0f, 0);
            }
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.3f) / 255f, ((255 - projectile.alpha) * 0.4f) / 255f, ((255 - projectile.alpha) * 1f) / 255f);
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 127);
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