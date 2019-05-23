using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
	public class SuperStinkPotion : ModProjectile
	{
		public override void SetDefaults()
		{
            projectile.CloneDefaults(ProjectileID.ToxicFlask);
			projectile.width = 16;
			projectile.height = 16;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
		}

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Super Stink Potion");
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.penetrate--;
			if (projectile.penetrate <= 0)
			{
				projectile.Kill();
			}
			else
			{
				projectile.ai[0] += 0.1f;
				if (projectile.velocity.X != oldVelocity.X)
				{
					projectile.velocity.X = -oldVelocity.X;
				}
				if (projectile.velocity.Y != oldVelocity.Y)
				{
					projectile.velocity.Y = -oldVelocity.Y;
				}
				projectile.velocity *= 0.5f;
				Main.PlaySound(SoundID.Item27, projectile.position);
            }

            return false;
		}

		public override void Kill(int timeLeft)
		{
            int numberProjectiles = 3;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 value15 = new Vector2((float)Main.rand.Next(-12, 12), (float)Main.rand.Next(-12, 12));
                value15.X *= 0.1f;
                value15.Y *= 0.1f;
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, value15.X, value15.Y, 228, projectile.damage / 2, 2f, projectile.owner, 0f, 0f);
            }
            Main.PlaySound(SoundID.Item27, projectile.position);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			projectile.ai[0] += 0.2f;
			projectile.velocity *= 0.6f;
		}
	}
}