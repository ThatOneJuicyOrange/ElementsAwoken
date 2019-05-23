using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.Explosions
{
    public class LightsAfflictionExplosion : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 98;
            projectile.height = 98;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 40;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lights Affliction");
            Main.projFrames[projectile.type] = 7;

        }
        public override void AI()
        {
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PinkFlame);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1.5f;
            Main.dust[dust].velocity *= 1f;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 3)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 6)
                    projectile.Kill();
            }
            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 8;
        }
    }
}