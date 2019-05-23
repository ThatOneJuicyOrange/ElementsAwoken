using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class FireRunes : ModProjectile
    {
        public int type = 0;
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 20;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.alpha = 255;
            projectile.timeLeft = 200;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Runes");
            Main.projFrames[projectile.type] = 20;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frame = type;
            return true;
        }
        public override void AI()
        {
            if (projectile.localAI[0] == 0)
            {
                type = Main.rand.Next(20);
                projectile.localAI[0]++;
            }
            projectile.velocity.X *= 0.99f;
            projectile.velocity.Y *= 0.99f;

            projectile.alpha -= 7;
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1f);
                Main.dust[dust].velocity *= 0.3f;
                Main.dust[dust].fadeIn = 0.9f;
                Main.dust[dust].noGravity = true;
            }
            projectile.localAI[1]++;
            if (projectile.localAI[1] >= 100)
            {
                float shootSpeed = 18f;
                Vector2 targetPos = Main.MouseWorld;
                projectile.tileCollide = true;

                if (Main.myPlayer == projectile.owner)
                {
                    Vector2 shootVel = targetPos - projectile.Center;
                    if (shootVel == Vector2.Zero)
                    {
                        shootVel = new Vector2(0f, 1f);
                    }
                    shootVel.Normalize();
                    shootVel *= shootSpeed;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 103);
                    }

                    int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootVel.X, shootVel.Y, mod.ProjectileType("RuneBlast"), projectile.damage, projectile.knockBack, Main.myPlayer, 0f, 0f);
                    Main.projectile[proj].timeLeft = 300;
                    Main.projectile[proj].netUpdate = true;
                    projectile.netUpdate = true;
                }
                projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 6, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 360);
        }
    }
}