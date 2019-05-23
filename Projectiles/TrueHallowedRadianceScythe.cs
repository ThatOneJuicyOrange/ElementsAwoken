using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class TrueHallowedRadianceScythe : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 48;
            projectile.height = 48;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.melee = true;

            projectile.alpha = 60;
            projectile.penetrate = -1;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Hallowed Radiance");
        }
        public override void AI()
        {
            projectile.rotation += 1f;

            if (Main.rand.Next(3) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 73, projectile.velocity.X, projectile.velocity.Y, 255, default(Color), 1.8f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1f;
                Main.dust[dust].noLight = true;
            }
            int hitboxSize = 12;
            if (Collision.SolidCollision(projectile.Center - new Vector2(hitboxSize / 2, hitboxSize / 2), hitboxSize, hitboxSize))
            {
                projectile.Kill();
            }
        }
        /*public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            int hitboxSize = 12;
            hitbox = new Rectangle((int)projectile.Center.X - hitboxSize / 2, (int)projectile.Center.Y - hitboxSize / 2, hitboxSize, hitboxSize);
        }*/
    }
}