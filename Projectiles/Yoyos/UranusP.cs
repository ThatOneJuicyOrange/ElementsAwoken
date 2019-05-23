using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace ElementsAwoken.Projectiles.Yoyos
{
    public class UranusP : ModProjectile
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

            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 265f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 14f;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 8f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Uranus");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 180, false);
        }
    }
}