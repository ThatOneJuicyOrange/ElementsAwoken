using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles
{
    public class AbyssalPortal : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 34;
            projectile.height = 34;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;

            projectile.penetrate = -1;

            projectile.timeLeft = 800;

            projectile.light = 1f;

            projectile.alpha = 255;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyssal Portal");
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
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PinkFlame, 0f, 0f, 100, default(Color));
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 3;
            }
        }
        public override void AI()
        {
            projectile.velocity *= 0.99f;
            projectile.localAI[0]++;
            if (projectile.ai[1] != 0 || projectile.ai[0] > 300)
            {
                projectile.alpha += 255 / 30;
                if (projectile.alpha >= 255) projectile.Kill();
            }
            else if (projectile.alpha > 0)
            {
                projectile.alpha -= 255 / 30;
            }
            if (Main.rand.NextBool(6))
            {
                int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustID.PinkFlame, 0f, 0f, 100, default(Color));
                Main.dust[dust].noGravity = true;
            }
            projectile.ai[0]++;
            if (projectile.ai[1] == 0 && Main.rand.NextBool(20) && projectile.owner == Main.myPlayer && projectile.alpha <= 0)
            {
                float max = 600f;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC nPC = Main.npc[i];
                    if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(projectile.Center, nPC.Center) <= max)
                    {
                        float Speed = 9f;
                        float rotation = (float)Math.Atan2(projectile.Center.Y - nPC.Center.Y, projectile.Center.X - nPC.Center.X);
                        Vector2 projSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                        Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 20);
                        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projSpeed.X, projSpeed.Y, ModContent.ProjectileType<AbyssalBall>(), projectile.damage, projectile.knockBack, projectile.owner);
                        projectile.ai[1] = 1;
                        break;
                    }
                }
            }
        }
    }
}