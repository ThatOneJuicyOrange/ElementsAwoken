using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
	public class StrangePlantBall3 : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			projectile.aiStyle = 0;
            projectile.alpha = 255;
			projectile.timeLeft = 150;
			projectile.friendly = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.magic = true;          
			projectile.penetrate = 2;
		}
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Strange Orb");
        }
        public override void AI()
        {
            for (int num121 = 0; num121 < 3; num121++)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 56);
                Main.dust[dust].velocity *= 0.1f;
                if (projectile.velocity == Vector2.Zero)
                {
                    Main.dust[dust].velocity.Y -= 1f;
                    Main.dust[dust].scale = 1.2f;
                }
                else
                {
                    Main.dust[dust].velocity += projectile.velocity * 0.2f;
                }
                Main.dust[dust].position.X = projectile.Center.X + 4f + (float)Main.rand.Next(-2, 3);
                Main.dust[dust].position.Y = projectile.Center.Y + (float)Main.rand.Next(-2, 3);
                Main.dust[dust].noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            int numberProjectiles = 2;
            for (int i = 0; i < numberProjectiles; i++)
            {
                if (Main.rand.Next(2) == 0)
                {
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4), ProjectileID.Bubble, projectile.damage / 2, projectile.knockBack, projectile.owner, 0f, 0f);
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            int numberProjectiles = 2;
            for (int i = 0; i < numberProjectiles; i++)
            {
                if (Main.rand.Next(2) == 0)
                {
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4), ProjectileID.Bubble, projectile.damage / 2, projectile.knockBack, projectile.owner, 0f, 0f);
                }
            }
            projectile.Kill();
            return false;
        }
    }
}