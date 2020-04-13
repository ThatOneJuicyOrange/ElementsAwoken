using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class TheSilencerP : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.timeLeft = 220;
            projectile.magic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Silencer");
        }
        public override void AI()
        {
            projectile.localAI[0] += 1f;
			if (projectile.localAI[0] > 2f)
			{
                int dustLength = 10;
                for (int i = 0; i < dustLength; i++)
                {
                    int dustType = 135;
                    switch (Main.rand.Next(2))
                    {
                        case 0:
                            dustType = 135;
                            break;
                        case 1:
                            dustType = 242;
                            break;
                        default: break;
                    }
                    Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType)];
                    dust.velocity = Vector2.Zero;
                    dust.position -= projectile.velocity / dustLength * (float)i;
                    dust.noGravity = true;
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, new int[] { 135, 242 }, damageType: "melee");
        }
    }
}