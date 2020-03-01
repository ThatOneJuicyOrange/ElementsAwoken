using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class RailgunBeam : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            //projectile.aiStyle = 48;

            projectile.friendly = true;
            projectile.ranged = true;

            projectile.penetrate = -1;
            projectile.extraUpdates = 20;
            projectile.timeLeft = 300;
            projectile.tileCollide = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Railgun");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (projectile.velocity.X != projectile.velocity.X)
            {
                projectile.position.X = projectile.position.X + projectile.velocity.X;
                projectile.velocity.X = -projectile.velocity.X;
            }
            if (projectile.velocity.Y != projectile.velocity.Y)
            {
                projectile.position.Y = projectile.position.Y + projectile.velocity.Y;
                projectile.velocity.Y = -projectile.velocity.Y;
            }
            float oldAI0 = projectile.ai[0];
            projectile.ai[0] += (float)(Math.PI / 10);

            int dustLength = 4;
            for (int i = 0; i < dustLength; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 220)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / dustLength * (float)i;
                dust.noGravity = true;
                dust.color = Color.Aqua;
            }

            for (int j = -1; j < 2; j += 2)
            {
                int numDust = 2;
                for (int i = 0; i < numDust; i++)
                {
                    float ai = i == 0 ? oldAI0 : projectile.ai[0];
                    Vector2 pos = i == 0 ? projectile.position - (projectile.position - projectile.oldPosition) : projectile.position;
                    pos += new Vector2(projectile.width,projectile.height) / 2;
                    float Y = ((float)Math.Sin(ai) * 20) * j;
                    Vector2 dustPos = new Vector2(Y, 0);
                    dustPos = dustPos.RotatedBy((double)projectile.rotation, default(Vector2));

                    Dust dust = Main.dust[Dust.NewDust(pos + dustPos - Vector2.One * 5f, 2, 2, 220)];
                    dust.velocity = Vector2.Zero;
                    dust.noGravity = true;
                    dust.color = Color.Aqua;
                }
            }
        }
    }
}