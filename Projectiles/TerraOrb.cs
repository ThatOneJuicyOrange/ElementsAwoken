using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles
{
    public class TerraOrb : ModProjectile
    {
        public int shootTimer = 20;
        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 38;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.melee = true;
            projectile.tileCollide = true;

            projectile.penetrate = -1;
            projectile.timeLeft = 400;
            projectile.light = 1f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Belter");
            Main.projFrames[projectile.type] = 5;
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
            projectile.velocity *= 0.997f;
            projectile.localAI[0]++;
            if (projectile.localAI[0] >= 90)
            {
                projectile.alpha += 5;
            }
            if (projectile.alpha >= 255)
            {
                projectile.Kill();
            }

            shootTimer--;
            if (projectile.owner == Main.myPlayer)
            {
                float max = 400f;
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC nPC = Main.npc[i];
                    if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(projectile.Center, nPC.Center) <= max)
                    {
                        float Speed = 9f;
                        float rotation = (float)Math.Atan2(projectile.Center.Y - nPC.Center.Y, projectile.Center.X - nPC.Center.X);
                        if (shootTimer <= 0)
                        {
                            Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                            Projectile.NewProjectile(projectile.Center.X - 4f, projectile.Center.Y, speed.X, speed.Y, mod.ProjectileType("TerraBolt"), projectile.damage, projectile.knockBack, projectile.owner);
                            shootTimer = 60;
                        }
                    }
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 107, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default(Color), 1.8f)];
                dust.noGravity = true;
                dust.velocity *= 0.5f;
            }
        }
    }
}