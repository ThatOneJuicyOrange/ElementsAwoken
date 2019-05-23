using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ElementalBurst : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 40;
            projectile.ranged = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elemental Burst");
        }
        public override void AI()
        {
            projectile.velocity *= 0.9f;
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] > 5f)
            {
                Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.1f) / 255f, ((255 - projectile.alpha) * 0.1f) / 255f, ((255 - projectile.alpha) * 0f) / 255f);
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 63, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 3.75f);
                Main.dust[dust].velocity *= 0.6f;
                Main.dust[dust].scale *= 0.6f;
                Main.dust[dust].noGravity = true;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Slow, 200);
            target.AddBuff(BuffID.OnFire, 200);
            target.AddBuff(BuffID.VortexDebuff, 200);
            target.AddBuff(BuffID.Frostburn, 200);
            target.AddBuff(BuffID.Wet, 200);
            target.AddBuff(mod.BuffType("ExtinctionCurse"), 200);
        }
    }
}