using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Explosions
{
    public class BigExplosion : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 200;
            projectile.height = 200;
            projectile.friendly = true;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 5;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Explosion");
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            damage = (int)(target.statLifeMax2 * 0.2f * Main.rand.NextFloat(0.8f,1.2f)) + target.statDefense;
            damage /= 4; // damage from other players (or urself) is multiplied by 4
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.townNPC) return false;
            return base.CanHitNPC(target);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 2;
        }
    }
}