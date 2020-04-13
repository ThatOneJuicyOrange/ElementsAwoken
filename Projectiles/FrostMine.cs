using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class FrostMine : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;

            projectile.penetrate = 1;
            projectile.timeLeft = 220;

            projectile.tileCollide = false;
            projectile.friendly = true;
            projectile.magic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost Mine");
        }
        public override void AI()
        {
            for (int i = 0; i < 4; i++)
            {
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 135, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0f;
            }
        }

        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, 135, damageType: "magic");
        }
    }
}