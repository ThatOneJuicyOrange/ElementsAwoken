using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class FistsOfFuryFire : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;

            projectile.friendly = true;
            projectile.ignoreWater = false;
            projectile.melee = true;

            projectile.alpha = 255;
            projectile.penetrate = 1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 30;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fists of Fury");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 180, false);
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.9f, 0.2f, 0.4f);

            Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 6)];
            dust.velocity = Vector2.Zero;
            dust.position -= projectile.velocity / 6f;
            dust.noGravity = true;
            dust.scale = 1f;
        }
    }
}
