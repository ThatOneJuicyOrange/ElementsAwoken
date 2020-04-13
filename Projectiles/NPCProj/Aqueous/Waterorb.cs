using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Aqueous
{
    public class Waterorb : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.melee = true;
            projectile.penetrate = 1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Waterorb");
        }
        public override void AI()
        {
            projectile.velocity.X *= 0.99f;
            projectile.velocity.Y *= 0.99f;
            Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 15, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            projectile.Kill();
            ProjectileUtils.HostileExplosion(projectile, 111);
        }
    }
}