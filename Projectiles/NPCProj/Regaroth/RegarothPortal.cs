using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Regaroth
{
    public class RegarothPortal : ModProjectile
    {
        public int laserTimer = 180;

        public override void SetDefaults()
        {
            projectile.width = 46;
            projectile.height = 46;

            projectile.hostile = true;
            projectile.tileCollide = false;

            projectile.alpha = 60;
            projectile.penetrate = -1;
            projectile.timeLeft = 600;
            projectile.light = 1f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Regaroth Portal");
            Main.projFrames[projectile.type] = 4;
        }
        public override void AI()
        {
            projectile.velocity.X *= 0.95f;
            projectile.velocity.X *= 0.95f;
            if (Main.rand.Next(4) == 0 && !ModContent.GetInstance<Config>().lowDust)
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, Main.rand.Next(2) == 0 ? 135 : 164, 0f, 0f, 100, default, 1)];
                dust.velocity *= 0.2f;
                dust.noGravity = true;
            }
            laserTimer--;
            if (laserTimer <= 0)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 33);

                int type = mod.ProjectileType("RegarothBolt");
                float numberProjectiles = 8;
                float rotation = MathHelper.ToRadians(360);
                float speed = 3f;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(2, 2).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * speed;
                    int num1 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, type, projectile.damage, projectile.knockBack, projectile.owner);
                }
                laserTimer = 180;
            }

        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 4)
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