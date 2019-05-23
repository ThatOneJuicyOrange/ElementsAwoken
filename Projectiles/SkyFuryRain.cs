using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class SkyFuryRain : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.RainFriendly);
            aiType = ProjectileID.RainFriendly;
            projectile.scale = 1f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rain");
        }
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            projectile.velocity.X *= 0f;
        }
    }
}