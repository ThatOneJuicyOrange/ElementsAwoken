using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class GnomeSurfer : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 44;
            projectile.height = 44;

            projectile.penetrate = -1;

            projectile.friendly = true;
            projectile.melee = true;

            projectile.alpha = 0;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gnome Surfer");
            Main.projFrames[projectile.type] = 4;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 18)
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
            if (projectile.localAI[0] == 0)
            {
                projectile.spriteDirection = Main.rand.Next(2) == 0 ? -1 : 1;
                projectile.localAI[0]++;
            }

            projectile.ai[0]++;
            float max = 500f;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(projectile.Center, nPC.Center) <= max)
                {
                    float Speed = 9f;
                    float rotation = (float)Math.Atan2(projectile.Center.Y - nPC.Center.Y, projectile.Center.X - nPC.Center.X);
                    if (projectile.ai[0] == 15)
                    {
                        Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                        Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 8);
                        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, speed.X, speed.Y, mod.ProjectileType("GnomeBolt"), projectile.damage, projectile.knockBack, projectile.owner);
                    }
                }
            }
            if (projectile.ai[0] >= 30)
            {
                projectile.alpha += 30;
                if (projectile.alpha >= 255)
                {
                    projectile.Kill();
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            int numDusts = 20;
            for (int i = 0; i < numDusts; i++)
            {
                Vector2 position = (Vector2.Normalize(new Vector2(5,5)) * new Vector2((float)projectile.width / 2f, (float)projectile.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + projectile.Center;
                Vector2 velocity = position - projectile.Center;
                int dust = Dust.NewDust(position + velocity, 0, 0, 134, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].noLight = true;
                Main.dust[dust].velocity = Vector2.Normalize(velocity) * 1.5f;
            }
        }
    }
}