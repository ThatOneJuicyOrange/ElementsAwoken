using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class BabySharkP : ModProjectile
    {
        public int[] canHit = new int[Main.maxNPCs];
        public override void SetDefaults()
        {
            projectile.width = 54;
            projectile.height = 36;
            projectile.friendly = true;
            projectile.aiStyle = 39;
            aiType = 190;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.ranged = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Baby Shark");
        }
        public override void AI()
        {
            for (int i = 0; i < canHit.Length; i++)
            {
                 canHit[i]--;
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (canHit[target.whoAmI] > 0) return false;
            return base.CanHitNPC(target);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            canHit[target.whoAmI] = 30;
        }
    }
}