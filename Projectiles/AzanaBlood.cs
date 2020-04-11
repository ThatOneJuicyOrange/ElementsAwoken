using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class AzanaBlood : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;

            projectile.penetrate = 1;
            projectile.timeLeft = 320;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Azana Blood");
        }
        public override void AI()
        {
            projectile.velocity.Y += 0.2f;
            for (int i = 0; i < 4; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, Main.rand.NextBool() ? 127: 5, 0f, 0f, 0, default(Color), 2.75f)];
                if (dust.type == 5) dust.scale *= 0.5f;
                dust.velocity *= 0.05f;
                dust.noGravity = true;
            }
        }
    }
}