using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Held.Drills
{
    public class CombatDrillHeld : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;

            projectile.aiStyle = 20;
            projectile.penetrate = -1;

            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.hide = true;
            projectile.ownerHitCheck = true;
            projectile.melee = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Combat Drill");

        }

    }
}
