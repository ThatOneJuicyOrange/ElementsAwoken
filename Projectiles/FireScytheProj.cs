using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class FireScytheProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.DeathSickle);
            aiType = ProjectileID.DeathSickle;
            projectile.scale = 1f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scythe of Eternal Flame");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 180, false);
        }
        public override void AI()
        {
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire);
            Main.dust[dust].velocity /= 1f;

        }
    }
}