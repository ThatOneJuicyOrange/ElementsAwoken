using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class AzanaNanoBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.alpha = 0;
            projectile.timeLeft = 600;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Azana");
            Main.projFrames[projectile.type] = 4;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 6)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                    projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = 1f;
            }
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 127, 0f, 0f, 100, default(Color), 1f);
            Main.dust[dust].velocity *= 0.3f;
            Main.dust[dust].fadeIn = 0.9f;
            Main.dust[dust].noGravity = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("ChaosBurn"), 180);
        }
        public override void Kill(int timeLeft)
        {
            float spread = 45f * 0.0174f;
            double startAngle = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - spread / 2;
            double deltaAngle = spread / 8f;
            double offsetAngle;
            int type = mod.ProjectileType("AnarchyWave");
            for (int i = 0; i < 4; i++)
            {
                offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(Math.Sin(offsetAngle) * 8f), (float)(Math.Cos(offsetAngle) * 8f), type, projectile.damage, 8f, 0);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(-Math.Sin(offsetAngle) * 8f), (float)(-Math.Cos(offsetAngle) * 8f), type, projectile.damage, 8f, 0);
            }
            ProjectileUtils.Explosion(projectile, 127, damageType: "magic");
        }
    }
}