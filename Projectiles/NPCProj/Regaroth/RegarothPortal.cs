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
            projectile.width = 32;
            projectile.height = 32;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.alpha = 60;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft = 600;
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
            if (Main.rand.Next(2) == 0)
            {
                int dustType = 135;
                switch (Main.rand.Next(2))
                {
                    case 0:
                        dustType = 135;
                        break;
                    case 1:
                        dustType = 164;
                        break;
                    default: break;
                }
                int dust1 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, dustType, 0f, 0f, 100, default(Color), 1);
                Main.dust[dust1].velocity *= 0.2f;
                Main.dust[dust1].noGravity = true;
            }
            laserTimer--;
            if (laserTimer <= 0)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 33);

                /*float Speed = 10f;
                int type = mod.ProjectileType("RegarothBeam");
                Vector2 vector8 = new Vector2(projectile.position.X + (projectile.width / 2), projectile.position.Y + (projectile.height / 2));
                float spread = 45f * 0.0174f;
                double startAngle = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - spread / 2;
                double deltaAngle = spread / 8f;
                double offsetAngle;
                for (int i = 0; i < 4; i++)
                {
                    offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                    Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Sin(offsetAngle) * Speed), (float)(Math.Cos(offsetAngle) * Speed), type, projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                    Projectile.NewProjectile(vector8.X, vector8.Y, (float)(-Math.Sin(offsetAngle) * Speed), (float)(-Math.Cos(offsetAngle) * Speed), type, projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                }*/

                int type = mod.ProjectileType("RegarothBeam");
                int projDamage = Main.expertMode ? 200 : 300;
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