using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class StellarPortal : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.alpha = 255;
            projectile.timeLeft = 20;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stellar Portal");
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
            projectile.localAI[1]++;
            if (projectile.localAI[1] % 6 == 0)
            {            
                int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.NextFloat(-1.5f, 1.5f), 12f, mod.ProjectileType("StellarMeteor"), projectile.damage, projectile.knockBack, Main.myPlayer, 0f, 0.5f + (float)Main.rand.NextDouble() * 0.6f);
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 180, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }     
    }
}