using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class OceansSeashell : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;

            projectile.friendly = true;
            projectile.magic = true;

            projectile.penetrate = 1;
            projectile.timeLeft = 60;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Ball");
        }
        public override void AI()
        {
            projectile.rotation += 0.05f;
            projectile.velocity.Y += 0.2f;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.NPCHit2, (int)projectile.position.X, (int)projectile.position.Y);
        }
    }
}