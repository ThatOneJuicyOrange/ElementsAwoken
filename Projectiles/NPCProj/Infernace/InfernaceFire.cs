using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Infernace
{
    public class InfernaceFire : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.GreekFire1);
            aiType = ProjectileID.GreekFire1;
            projectile.scale = 1f;
            projectile.hostile = true;
            projectile.timeLeft = 180;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire");
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 80, false);
        }
    }
}