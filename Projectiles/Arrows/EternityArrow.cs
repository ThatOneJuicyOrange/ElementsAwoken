using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Arrows
{
    public class EternityArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
            aiType = ProjectileID.WoodenArrowFriendly;
            projectile.scale = 1f;
            projectile.penetrate = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eternity Arrow");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.CursedInferno, 80);
            target.AddBuff(BuffID.OnFire, 80);
            target.AddBuff(BuffID.Frostburn, 80);
            target.AddBuff(BuffID.Venom, 80);
            target.AddBuff(BuffID.Poisoned, 80);
            target.AddBuff(BuffID.ShadowFlame, 80);

        }
        public override void AI()
        { 
            Lighting.AddLight(projectile.Center, 0.4f, 0.2f, 0.4f);
            for (int num121 = 0; num121 < 5; num121++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 58)];
                dust.velocity *= 0.6f;
                dust.scale *= 0.6f;
                dust.noGravity = true;
            }
        }
    }
}