using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class PutridTrail : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 8;

            projectile.friendly = true;
            projectile.tileCollide = false;

            projectile.penetrate = -1;
            projectile.timeLeft = 90;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Putrid Trail");
        }
        public override void AI()
        {
            projectile.velocity.Y += 0.05f;
            for (int i = 0; i < 2; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.width, 46, 0f, 0f, 150, default(Color), 0.75f)];
                dust.velocity *= 0.05f;
                dust.noGravity = true;
            }
            projectile.ai[1]++;
            if (projectile.ai[1] == 6) projectile.tileCollide = true;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
    }
}