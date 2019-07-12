using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Bullets
{
    public class BlightedBulletP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;

            projectile.aiStyle = 1;

            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ranged = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;

            projectile.penetrate = 1;

            projectile.timeLeft = 600;

            projectile.extraUpdates = 1;

            aiType = ProjectileID.Bullet;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blighted Bullet");

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Corroding"), 300, false);
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.3f, 0.9f, 0.6f);

            if (Main.rand.Next(220) == 0)
            {
                Vector2 cloudVel = new Vector2(-projectile.velocity.X * 0.3f, -projectile.velocity.Y * 0.3f);
                cloudVel = cloudVel.RotatedByRandom(MathHelper.ToRadians(8));
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -projectile.velocity.X * 0.1f, -projectile.velocity.Y * 0.1f, mod.ProjectileType("BlightCloud"), (int)(projectile.damage * 0.5f), projectile.knockBack, projectile.owner, 0f, 0f);
            }

            int dustLength = 2;
            for (int l = 0; l < dustLength; l++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 74)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / dustLength * (float)l;
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }
    }
}
