using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Arrows
{
    public class PincerArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
            aiType = ProjectileID.WoodenArrowFriendly;
            projectile.scale = 1f;
            projectile.penetrate = 2;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scorpion Arrow");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 200);
            target.AddBuff(BuffID.Venom, 200);
        }
        public override void AI()
        {
            if (projectile.ai[1] == 0 && projectile.penetrate == 1)
            {
                projectile.damage = (int)(projectile.damage * 0.4f);
                projectile.ai[1]++;
            }
        }
    }
}