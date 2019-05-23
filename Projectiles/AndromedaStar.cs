using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class AndromedaStar : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Starfury);
            aiType = ProjectileID.Starfury;
            projectile.scale = 1f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Star");
        }
        public override bool PreKill(int timeLeft)
        {
            projectile.type = ProjectileID.Starfury;
            return true;
        }
    }
}