using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class AncientShard : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ignoreWater = true;
            projectile.melee = true;

            projectile.penetrate = 1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 200;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Shard");
            Main.projFrames[projectile.type] = 4;
        }
            public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frame = (int)projectile.ai[0];
            return true;
        }
        public override void AI()
        {
            if (projectile.localAI[0] == 0)
            {
                projectile.ai[0] = Main.rand.Next(0, 3);
                projectile.localAI[0] = 1;
            }

            projectile.velocity.Y += 0.1f;
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 27);
        }
    }
}