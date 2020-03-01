using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class StellariumBolt2 : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;

            projectile.magic = true;

            projectile.penetrate = 1;

            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;

            projectile.alpha = 255;
            projectile.timeLeft = 90;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stellarium Scepter");
        }
        public override void AI()
        {
            for (int i = 0; i < 5; i++)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 180, 0f, 0f, 100, default(Color), 1f);
                Main.dust[dust].velocity *= 0.3f;
                Main.dust[dust].fadeIn = 0.9f;
                Main.dust[dust].noGravity = true;
            }
        }
    }
}