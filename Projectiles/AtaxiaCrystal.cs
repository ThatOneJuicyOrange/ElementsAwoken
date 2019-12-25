using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class AtaxiaCrystal : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 24;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.melee = true;
            projectile.tileCollide = false;

            projectile.penetrate = -1;
            projectile.timeLeft = 420;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ataxia");
            Main.projFrames[projectile.type] = 5;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frame = (int)projectile.ai[1];
            return true;
        }
        public override void AI()
        {
            projectile.rotation -= 0.2f;

            Player player = Main.player[projectile.owner];

            projectile.ai[0] += 5f;
            float distance = projectile.localAI[0];
            double rad = projectile.ai[0] * (Math.PI / 180); // angle to radians
            projectile.Center = player.Center- new Vector2((int)(Math.Cos(rad) * distance) - projectile.width / 2, (int)(Math.Sin(rad) * distance) - projectile.height / 2);

            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType("AncientRed"));
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].scale *= 1.2f;
                Main.dust[dust].noGravity = true;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, mod.DustType("AncientRed"))];
                dust.noGravity = true;
                dust.velocity *= 1.5f;
            }
        }
    }
}