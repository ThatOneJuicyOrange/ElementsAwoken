using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Arrows
{
    public class StarstruckArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 3;
            projectile.aiStyle = 1;
            projectile.timeLeft = 600;
            aiType = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starstruck");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y + -300, 0, 15, mod.ProjectileType("StarstruckBeam"), projectile.damage, 0, projectile.owner);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y + 300, 0, -15, mod.ProjectileType("StarstruckBeam"), projectile.damage, 0, projectile.owner);
            Projectile.NewProjectile(projectile.Center.X + 300, projectile.Center.Y, -15, 0, mod.ProjectileType("StarstruckBeam"), projectile.damage, 0, projectile.owner);
            Projectile.NewProjectile(projectile.Center.X + -300, projectile.Center.Y, 15, 0, mod.ProjectileType("StarstruckBeam"), projectile.damage, 0, projectile.owner);
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.4f, 0.2f, 0.4f);
            for (int num121 = 0; num121 < 5; num121++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 164)];
                dust.velocity *= 0.6f;
                dust.scale *= 0.6f;
                dust.noGravity = true;
                Dust dust2 = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 135)];
                dust2.velocity *= 0.6f;
                dust2.scale *= 0.6f;
                dust2.noGravity = true;
            }
        }
    }
}