using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class SilvestreStar : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Starfury);
            aiType = ProjectileID.Starfury;
            projectile.scale = 1f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Silvestre Star");
        }
        public override bool PreKill(int timeLeft)
        {
            projectile.type = ProjectileID.Starfury;
            return true;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(50, 250, Main.DiscoB);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 2;
        }
    }
}