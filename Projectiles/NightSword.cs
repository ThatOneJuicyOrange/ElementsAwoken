using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class NightSword : ModProjectile
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
            DisplayName.SetDefault("Blade Of The Night");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(2) == 0)
            {
                target.AddBuff(mod.BuffType("ExtinctionCurse"), 100);
            }
            {
                target.immune[projectile.owner] = 3;
            }
            {
                if (Main.rand.Next(3) == 0)
                {
                    float random = Main.rand.Next(30, 90);
                    float spread = random * 0.0174f;
                    double startAngle = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - spread / 2;
                    double deltaAngle = spread / 8f;
                    double offsetAngle;
                    int i;
                    for (i = 0; i < 4; i++)
                    {
                        offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f), mod.ProjectileType("ExtinctionRay"), (int)((double)projectile.damage * 1.75f), projectile.knockBack, projectile.owner, 0f, 0f);
                    }
                    projectile.penetrate--;
                    if (projectile.penetrate <= 0)
                    {
                        projectile.Kill();
                    }
                }
            }
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.3f) / 255f, ((255 - projectile.alpha) * 0.4f) / 255f, ((255 - projectile.alpha) * 1f) / 255f);
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PinkFlame);
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