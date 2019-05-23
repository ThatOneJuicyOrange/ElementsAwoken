using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Arrows
{
    public class VoidArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
            aiType = ProjectileID.WoodenArrowFriendly;
            projectile.scale = 1f;
            projectile.penetrate = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Arrow");
        }
        public override void AI()
        { 
            Lighting.AddLight(projectile.Center, 0.4f, 0.2f, 0.4f);
            for (int num121 = 0; num121 < 5; num121++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 127)];
                dust.velocity *= 0.6f;
                dust.scale *= 0.6f;
                dust.noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("HandsOfDespair"), 180);
            int numberProjectiles = 2;
            for (int num131 = 0; num131 < numberProjectiles; num131++)
            {
                int num1 = Main.rand.Next(-30, 30);
                int num2 = Main.rand.Next(300, 500);
                Projectile.NewProjectile(projectile.Center.X + num1, projectile.Center.Y - num2, 0, 20, mod.ProjectileType("CrimsonSkyLaser"), projectile.damage, 0, projectile.owner);
            }
        }
    }
}