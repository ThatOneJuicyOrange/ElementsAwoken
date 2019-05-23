using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class StingerP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.HornetStinger);
            aiType = ProjectileID.HornetStinger;
            projectile.scale = 1f;
            projectile.friendly = true;
            projectile.hostile = false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stinger");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 80, false);
        }
    }
}