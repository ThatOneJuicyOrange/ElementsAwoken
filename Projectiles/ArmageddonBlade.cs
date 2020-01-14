using System;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ArmageddonBlade : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;

            projectile.penetrate = 1;
            projectile.light = 0.5f;
            projectile.alpha = 20;

            projectile.friendly = true;
            projectile.melee = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Armageddon");
            Main.projFrames[projectile.type] = 3;
        }
        public override void AI()
        {        
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, GetDustID());
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;

            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frame = (int)projectile.ai[0];
            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[0] == 0) target.AddBuff(BuffID.CursedInferno, 200);
            else if (projectile.ai[0] == 1) target.AddBuff(BuffID.Frostburn, 200);
            else target.AddBuff(BuffID.OnFire, 200); ;
        }
        private int GetDustID()
        {
            if (projectile.ai[0] == 0) return 75;
            else if (projectile.ai[0] == 1) return 135;
            else return 6;
        }
        public override void Kill(int timeLeft)
        {
            ProjectileGlobal.Explosion(projectile, new int[] { GetDustID() }, projectile.damage);
        }
    }
}