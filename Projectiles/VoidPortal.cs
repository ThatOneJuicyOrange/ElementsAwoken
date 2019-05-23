using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles
{
    public class VoidPortal : ModProjectile
    {
        public float shootTimer = 0f;

        public override void SetDefaults()
        {
            projectile.width = 46;
            projectile.height = 46;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;

            projectile.penetrate = -1;

            projectile.timeLeft = 800;

            projectile.light = 1f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Portal");
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
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustID.PinkFlame, 0f, 0f, 100, default(Color));
                Main.dust[dust].noGravity = true;
            }
        }
        public override void AI()
        {
            projectile.localAI[0]++;
            if(projectile.localAI[0] >= 600)
            {
                projectile.alpha += 5;
                if (projectile.alpha >= 255)
                {
                    projectile.Kill();
                }
            }

            int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustID.PinkFlame, 0f, 0f, 100, default(Color));
            Main.dust[dust].noGravity = true;
            float speed = 0.6f;
            if (Vector2.Distance(projectile.Center, Main.MouseWorld) >= 500)
            {
                speed = 1f;
            }

            Vector2 toTarget = new Vector2(Main.MouseWorld.X - projectile.Center.X, Main.MouseWorld.Y - projectile.Center.Y);
            toTarget.Normalize();
            projectile.velocity += toTarget * speed;

            shootTimer--;
            float max = 600f;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(projectile.Center, nPC.Center) <= max)
                {
                    float Speed = 9f;
                    float rotation = (float)Math.Atan2(projectile.Center.Y - nPC.Center.Y, projectile.Center.X - nPC.Center.X);
                    if (shootTimer <= 0)
                    {
                        Vector2 projSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                        Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 103);
                        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projSpeed.X, projSpeed.Y, mod.ProjectileType("VoidPortalSinewave"), projectile.damage, projectile.knockBack, projectile.owner);
                        shootTimer = 5;
                    }
                }
            }
        }
    }
}