using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles
{
    public class LightningCloudSwirl : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 100000;
            projectile.alpha = 255;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning Cloud");
        }
        public override void AI()
        {
            Player parent = Main.player[(int)projectile.ai[1]];
            MyPlayer modPlayer = parent.GetModPlayer<MyPlayer>();

            int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 15, 0f, 0f, 100, default(Color), 1.5f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 0f;
        
            if (modPlayer.lightningCloudCharge < 300)
            {
                projectile.Kill();
            }

            projectile.ai[0] += 3f;
            int distance = 25;
            double rad = projectile.ai[0] * (Math.PI / 180); // angle to radians
            projectile.position.X = parent.Center.X - (int)(Math.Cos(rad) * distance) - projectile.width / 2;
            projectile.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * distance) - projectile.height / 2;

            if (!parent.active || modPlayer.lightningCloudHidden || !modPlayer.lightningCloud)
            {
                projectile.Kill();
            }

            if (Main.netMode != 0)
            {
                if (parent.ownedProjectileCounts[projectile.type] > 3)
                {
                    projectile.Kill(); // failsafe
                }
            }
        }
    }
}
    