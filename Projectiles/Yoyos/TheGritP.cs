using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Yoyos
{
    public class TheGritP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;

            projectile.aiStyle = 99;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;

            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 150f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 11f;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 5f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Grit");
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            if (projectile.ai[0] > 30)
            {
                projectile.timeLeft = 50;
            }


        }

    }
}