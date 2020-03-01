using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class VoidBlood : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.friendly = true;
            projectile.tileCollide = true;

            projectile.penetrate = -1;
            projectile.timeLeft = 180;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Voidblood");
        }
        public override void AI()
        {
            for (int i = 0; i < 2; i++)
            {
                int dustType = Main.rand.Next(3) < 2 ? 5 : 127;
                Dust dust = Main.dust[Dust.NewDust(projectile.position, 1, 1, dustType)];
                dust.scale = Main.rand.NextFloat(0.3f,1.3f);
                dust.noGravity = true;
                if (projectile.ai[0] == 0) dust.velocity *= 0.05f;
                else dust.velocity *= 1.5f;
            }
            if (projectile.ai[0] != 1)
            {
                projectile.velocity.Y += 0.05f;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.velocity = Vector2.Zero;
            projectile.position.Y -= 6; // to elevate slightly above the ground
            projectile.ai[0] = 1;
            return false;
        }
    }
}