using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class BlightRing : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 42;
            projectile.height = 42;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.magic = true;

            projectile.alpha = 60;
            projectile.penetrate = 10;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blight Ring");
        }
        /*public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.velocity = -projectile.velocity;
            return projectile.penetrate == 0; 
        }*/
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.penetrate--;
            if (projectile.penetrate <= 0) projectile.Kill();
            else
            {
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }
                Main.PlaySound(SoundID.Item10, projectile.position);
            }
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Corroding"), 300);

            projectile.velocity = -projectile.velocity;
        }
        public override void AI()
        {
            projectile.rotation += 0.5f;
            for (int l = 0; l < 3; l++)
            {
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 74, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 130, default(Color), 1f);
                Main.dust[dust].velocity *= 0.6f;
                Main.dust[dust].scale *= Main.rand.NextFloat(0.5f, 0.9f);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}