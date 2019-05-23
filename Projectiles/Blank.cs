using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class Blank : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.alpha = 255;
            projectile.damage = 0;
            projectile.timeLeft = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blank");
        }
        public override void AI()
        {
            projectile.velocity = Vector2.Zero;
        }
    }
}