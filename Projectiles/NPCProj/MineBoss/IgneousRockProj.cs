using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj.MineBoss
{
    public class IgneousRockProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 24;

            projectile.hostile = true;

            projectile.penetrate = -1;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Igneous Rock");

        }
        public override void AI()
        {
            projectile.velocity.Y += 0.16f;
            projectile.rotation += 0.05f;
            projectile.ai[0]++;
            projectile.tileCollide = projectile.ai[0] > 10;
        }
    }
}