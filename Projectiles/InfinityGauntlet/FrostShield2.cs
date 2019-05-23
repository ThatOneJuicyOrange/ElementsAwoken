using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.InfinityGauntlet
{
    public class FrostShield2 : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 148;
            projectile.height = 148;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 60000;
            projectile.light = 2f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost Shield");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 180, false);
            if (target.active && !target.friendly && target.damage > 0 && !target.dontTakeDamage && !target.boss)
            {
                Vector2 knockBack = (target.Center - projectile.Center);
                target.velocity = (target.velocity + knockBack) / 5;
            }

        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.Center = player.Center;
            projectile.rotation -= 0.01f;
            if (player.FindBuffIndex(mod.BuffType("FrostShield")) == -1)
            {
                projectile.Kill();
            }
        }
    }
}