using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    class MiniEaterOfSouls : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 54;
            projectile.height = 36;
            projectile.friendly = true;
            projectile.aiStyle = 39;
            Main.projFrames[projectile.type] = 4;
            aiType = 190;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.ranged = true;
            //projectile.timeLeft = 500;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Miniature Eater Of Souls");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 12;
        }
        public override void AI()
        {
            if (Vector2.Distance(Main.player[projectile.owner].Center, projectile.Center) >= 800)
            {
                projectile.Kill();
            }
        }
    }
}