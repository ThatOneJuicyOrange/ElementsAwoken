using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj
{
    public class ImmolatorBall : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.magic = true;
            projectile.penetrate = -1;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.alpha = 0;
            projectile.timeLeft = 90;

            projectile.scale *= 0.95f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Bolt");
            Main.projFrames[projectile.type] = 4;
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 127, projectile.velocity.X * 0.15f, projectile.velocity.Y * 0.15f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale *= 1.5f;
            Lighting.AddLight(projectile.Center, 1f, 1f, 1f);
        }
        public override void Kill(int timeLeft)
        {
            // 4 projectiles
            float spread = 45f * 0.0174f;
            double startAngle = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - spread / 2;
            double deltaAngle = spread / 8f;
            double offsetAngle;
            int i;
            for (i = 0; i < 2; i++)
            {
                offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 64f * i;
                Projectile.NewProjectile(projectile.position.X, projectile.position.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f), mod.ProjectileType("ImmolatorBolt"), projectile.damage, projectile.knockBack, projectile.owner);
                Projectile.NewProjectile(projectile.position.X, projectile.position.Y, (float)(-Math.Sin(offsetAngle) * 5f), (float)(-Math.Cos(offsetAngle) * 5f), mod.ProjectileType("ImmolatorBolt"), projectile.damage, projectile.knockBack, projectile.owner);
            }
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 5)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                    projectile.frame = 0;
            }
            return true;
        }
    }
}