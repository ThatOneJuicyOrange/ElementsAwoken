using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj.Obsidious.Human
{
    public class ObsidiousIceCrystalSpin : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.hostile = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.timeLeft = 220;
            drawOffsetX = -10;
            drawOriginOffsetY = -10;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Obsidious Ice Crystal");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] < 30f)
            {
                Player player = Main.player[projectile.owner];
                Vector2 direction = player.Center - projectile.Center;
                projectile.rotation = direction.ToRotation() + 1.57f;

                NPC parent = Main.npc[(int)projectile.ai[1]];
                Vector2 offset = new Vector2(45, 0);

                    int distance = 50;
                    double rad = projectile.ai[0] * (Math.PI / 180); // angle to radians
                    projectile.position.X = parent.Center.X - (int)(Math.Cos(rad) * distance) - projectile.width / 2;
                    projectile.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * distance) - projectile.height / 2;
                
                if (!parent.active)
                {
                    projectile.Kill();
                }
            }
            else if (projectile.localAI[0] == 30f)
            {
                float speed = 15f;
                double angle = Math.Atan2(Main.player[Main.myPlayer].position.Y - projectile.position.Y, Main.player[Main.myPlayer].position.X - projectile.position.X);
                projectile.velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * speed;
                Main.PlaySound(SoundID.DD2_SonicBoomBladeSlash, projectile.position);
            }
            else if (projectile.localAI[0] > 30f)
            {
                for (int i = 0; i < 2; i++)
                {
                    int dust1 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 135, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[dust1].noGravity = true;
                    Main.dust[dust1].velocity *= 0f;
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 27);
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 135, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}