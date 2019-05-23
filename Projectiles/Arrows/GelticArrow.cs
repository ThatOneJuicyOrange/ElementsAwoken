using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Arrows
{
    public class GelticArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
            aiType = ProjectileID.WoodenArrowFriendly;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Geltic Arrow");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(5) == 0)
            {
                target.AddBuff(BuffID.Poisoned, 200);
            }
            if (Main.rand.Next(5) == 0)
            {
                target.AddBuff(BuffID.Slow, 200);
            }
        }
    }
}