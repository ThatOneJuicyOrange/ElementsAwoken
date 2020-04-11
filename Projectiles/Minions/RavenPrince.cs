using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.Minions
{
    public class RavenPrince : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;

            projectile.melee = true;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.timeLeft = 18000;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;

            projectile.penetrate = -1;
            projectile.timeLeft *= 5;

            aiType = 317;
            projectile.aiStyle = 54;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Raven");
            Main.projFrames[projectile.type] = 8;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            if (player.HeldItem.type != mod.ItemType("BladeOfThePrince"))
            {
                for (int k = 0; k < 20; k++)
                {
                    Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, mod.DustType("PrinceDust"), projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
                }
                projectile.Kill();
            }
            ProjectileUtils.PushOtherEntities(projectile);

        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.penetrate == 0)
            {
                projectile.Kill();
            }
            return false;
        }
    }
}