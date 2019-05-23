using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.ScourgeFighter
{
    public class ScourgeBullet : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.BulletDeadeye);
            aiType = ProjectileID.Bullet;
            projectile.tileCollide = false;
            projectile.scale = 1f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scourge Bullet");
        }
    }
}