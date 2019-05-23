using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class TempleBall : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.alpha = 0;
            projectile.timeLeft = 100;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Temple Ball");
        }
        public override void AI()
        {
            projectile.rotation += projectile.velocity.X * 0.5f;
            projectile.velocity.X *= 0.99f;
            projectile.velocity.Y *= 0.99f;

            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1f);
            Main.dust[dust].velocity *= 0.3f;
            Main.dust[dust].fadeIn = 0.9f;
            Main.dust[dust].noGravity = true;
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 6, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.OnFire, 200);
        }       
    }
}