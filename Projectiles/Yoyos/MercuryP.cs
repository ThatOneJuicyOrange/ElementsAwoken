using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace ElementsAwoken.Projectiles.Yoyos
{
    public class MercuryP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;

            projectile.aiStyle = 99;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;

            projectile.light = 0.5f;

            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 245f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 13f;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 6f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mercury");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 180, false);
        }
    }
}