using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class BlightCloud : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.penetrate = 1;

            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.ranged = true;

            projectile.alpha = 255;
            projectile.timeLeft = 40;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blight Cloud");
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            for (int k = 0; k < 5; k++)
            {
                Vector2 position = projectile.Center + Main.rand.NextVector2Circular(projectile.width / 2, projectile.height / 2);
                Dust dust = Dust.NewDustPerfect(position, 75, Vector2.Zero, 150);
                dust.noGravity = true;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 75, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.CursedInferno, 60);
        }       
    }
}