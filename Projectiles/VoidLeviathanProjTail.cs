using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class VoidLeviathanProjTail : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.netImportant = true;
            projectile.tileCollide = false;
            projectile.melee = true;

            projectile.penetrate = -1;
            projectile.timeLeft = 200;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Leviathan");
        }
        public override void AI()
        {
            Player player10 = Main.player[projectile.owner];
            int num1049 = 10;
            Vector2 parCenter = Vector2.Zero;
            float parRot = 0f;
            float scaleFactor16 = 0f;
            float scaleFactor17 = 1f;
            if (projectile.ai[1] == 1f)
            {
                projectile.ai[1] = 0f;
                projectile.netUpdate = true;
            }
            Projectile parent = Main.projectile[(int)projectile.ai[0]];
            if ((int)projectile.ai[0] >= 0 && parent.active)
            {
                parCenter = parent.Center;
                parRot = parent.rotation;
                scaleFactor17 = MathHelper.Clamp(parent.scale, 0f, 50f);
                scaleFactor16 = 16f;
                parent.localAI[0] = projectile.localAI[0] + 1f;
            }
            else
            {
                return;
            }
            if (projectile.alpha > 0)
            {
                int num3;
                for (int num1068 = 0; num1068 < 2; num1068 = num3 + 1)
                {
                    int num1069 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 135, 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num1069].noGravity = true;
                    Main.dust[num1069].noLight = true;
                    num3 = num1068;
                }
            }
            projectile.alpha -= 42;
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            projectile.velocity = Vector2.Zero;
            Vector2 vector151 = parCenter - projectile.Center;
            if (parRot != projectile.rotation)
            {
                float num1070 = MathHelper.WrapAngle(parRot - projectile.rotation);
                vector151 = vector151.RotatedBy((double)(num1070 * 0.1f), default(Vector2));
            }
            projectile.rotation = vector151.ToRotation() + 1.57079637f;
            projectile.position = projectile.Center;
            projectile.scale = scaleFactor17;
            projectile.width = (projectile.height = (int)((float)num1049 * projectile.scale));
            projectile.Center = projectile.position;
            if (vector151 != Vector2.Zero)
            {
                projectile.Center = parCenter - Vector2.Normalize(vector151) * scaleFactor16 * scaleFactor17;
            }
            projectile.spriteDirection = ((vector151.X > 0f) ? 1 : -1);

            if (!parent.active || (parent.type != mod.ProjectileType("VoidLeviathanProjBody") && parent.type != mod.ProjectileType("VoidLeviathanProjHead") && parent.type != mod.ProjectileType("VoidLeviathanProjTail")))
            {
                projectile.Kill();
            }
            return;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 31; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PinkFlame, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default(Color), 1.8f)];
                dust.noGravity = true;
                dust.velocity *= 0.5f;
            }
        }
    }
}
