using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class Snowflake : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.MagicMissile);
            projectile.width = 20;
            projectile.height = 20;
            projectile.scale = 1.2f;
            projectile.aiStyle = 9;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.alpha = 70;
            projectile.penetrate = 5;
            projectile.timeLeft = 300;
            projectile.magic = true;
            //aiType = 491;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snowflake");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 200);
        }
    }
}