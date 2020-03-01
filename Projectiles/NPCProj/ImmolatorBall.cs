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

            projectile.penetrate = -1;

            projectile.hostile = true;

            projectile.timeLeft = 90;

            projectile.scale *= 0.95f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Immolator Blast");
            Main.projFrames[projectile.type] = 4;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 1f, 0.2f, 0.4f);

            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (Main.rand.NextBool(3))
            {
                int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 127, projectile.velocity.X * 0.15f, projectile.velocity.Y * 0.15f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale *= 1.5f;
            }
        }
        public override void Kill(int timeLeft)
        {
            float numberProjectiles = Main.expertMode ? MyWorld.awakenedMode ? 4 : 3 : 2;
            float rotation = MathHelper.ToRadians(360);
            float projSpeed = 4f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(projSpeed, projSpeed).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 2f;
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("ImmolatorBolt"), projectile.damage, projectile.knockBack, Main.myPlayer);
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