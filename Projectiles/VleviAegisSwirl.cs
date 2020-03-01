using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles
{
    public class VleviAegisSwirl : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 100000;
            projectile.alpha = 255;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override void AI()
        {
            Player parent = Main.player[(int)projectile.ai[1]];
            MyPlayer modPlayer = parent.GetModPlayer<MyPlayer>();

            int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.PinkFlame, 0f, 0f, 100, default(Color), 1.5f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 0f;
            Main.dust[dust].customData = parent;

            if (modPlayer.vleviAegisBoost <= 0)
            {
                projectile.Kill();
            }
            if (!parent.active || parent.dead)
            {
                projectile.Kill();
            }
            projectile.ai[0] += projectile.localAI[0] == 0 ? -5f : 5f;
            int distance = projectile.localAI[0] == 0 ? 40 : 30;
            double rad = projectile.ai[0] * (Math.PI / 180); // angle to radians
            projectile.position.X = parent.Center.X - (int)(Math.Cos(rad) * distance) - projectile.width / 2;
            projectile.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * distance) - projectile.height / 2;


        }
    }
}
    