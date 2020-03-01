using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class GreekFire : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.GreekFire1);
            aiType = ProjectileID.GreekFire1;
            projectile.scale = 1f;
            projectile.friendly = true;
            projectile.hostile = false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Greek Fire");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(10) == 0)
            {
                target.AddBuff(BuffID.OnFire, 180, false);
            }
        }
    }
}