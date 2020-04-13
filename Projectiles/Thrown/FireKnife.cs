using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class FireKnife : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 75;
            projectile.thrown = true;
            projectile.aiStyle = ProjectileID.VampireKnife;
            aiType = ProjectileID.VampireKnife;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Knife");
        }
        public override void AI()
        {
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 240f)
            {
                projectile.alpha += 3;
                projectile.damage = (int)((double)projectile.damage * 0.95);
                projectile.knockBack = (float)((int)((double)projectile.knockBack * 0.95));
            }
            if (projectile.ai[0] < 240f)
            {
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            }
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire);
            Main.dust[dust].velocity *= 0.1f;
            Main.dust[dust].scale *= 1.5f;
            Main.dust[dust].noGravity = true;
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, 6, damageType: "thrown");
            int numberProjectiles = 3;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 value15 = new Vector2((float)Main.rand.Next(-12, 12), (float)Main.rand.Next(-12, 12));
                value15.X *= 0.25f;
                value15.Y *= 0.25f;
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, value15.X, value15.Y, mod.ProjectileType("FireKnifeBolt"), projectile.damage / 2, 2f, projectile.owner, 0f, 0f);
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 80);   //this make so when the projectile/flame hit a npc, gives it the buff  onfire , 80 = 3 seconds
        }
    }
}