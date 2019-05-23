using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace ElementsAwoken.Projectiles.Yoyos
{
    public class MarsP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;

            projectile.aiStyle = 99;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;

            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 205f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 8f;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 5f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mars");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 180, false);
        }
    }
}