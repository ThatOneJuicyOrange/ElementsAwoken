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
            projectile.width = 58;
            projectile.height = 58;

            projectile.light = 0.5f;
            projectile.penetrate = -1;

            projectile.scale = 0.9f;

            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.friendly = true;

            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scythe of Eternal Flame");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.ai[0]++;
            target.AddBuff(BuffID.OnFire, 180, false);
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.alpha != 0) return false;
            return base.CanHitNPC(target);
        }
        public override void AI()
        {
            projectile.rotation += 0.5f;
            projectile.velocity *= 0.97f;
            Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire)];
            dust.noGravity = true;
            projectile.ai[1]++;
            if (projectile.ai[0] > 5 || projectile.ai[1] > 120) projectile.alpha += 255 / 30;
            if (projectile.alpha >= 255) projectile.Kill();

            int hitboxSize = projectile.width / 2;
            if (Collision.SolidCollision(projectile.Center - new Vector2(hitboxSize / 2, hitboxSize / 2), hitboxSize, hitboxSize))
            {
                projectile.ai[0] = 10;
            }
        }
    }
}