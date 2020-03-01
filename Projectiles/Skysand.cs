using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class Skysand : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;

            projectile.friendly = true;
            projectile.magic = true;

            projectile.penetrate = -1;
            projectile.timeLeft = 300;
            projectile.alpha = 255;
            projectile.extraUpdates = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Skysand");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.ai[0]++;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.ai[0] != 0) return false;
            return base.CanHitNPC(target);
        }
        public override void AI()
        {
            int numDusts = projectile.ai[0] != 0 ? 1 : 2;
        	for (int i = 0; i < numDusts; i++)
			{
				Dust dust4 = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 138, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 130, default(Color), 3.75f)];
				dust4.velocity = Vector2.Zero;
				dust4.position -= projectile.velocity / numDusts * (float)i;
				dust4.noGravity = true;
				dust4.scale = 0.8f * MathHelper.Lerp(0.9f,0.3f,i/numDusts);
			}
            if (projectile.ai[0] != 0) projectile.ai[1]++;
            if (projectile.ai[1] > 10) projectile.Kill();
        }
    }
}