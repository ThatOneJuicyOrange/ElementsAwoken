using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj.TheCelestial
{
    public class CelestialIllusionsCenter : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 10000;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Celestials");
        }
        public override void AI()
        {
            Player player = Main.player[(int)projectile.ai[1]];
            int distance = 200;
            if (projectile.ai[0] == 0)
            {
                projectile.position.X = player.Center.X + distance;
                projectile.position.Y = player.Center.Y + distance;
            }
            if (projectile.ai[0] == 1)
            {
                projectile.position.X = player.Center.X - distance;
                projectile.position.Y = player.Center.Y + distance;
            }
            if (projectile.ai[0] == 2)
            {
                projectile.position.X = player.Center.X + distance;
                projectile.position.Y = player.Center.Y - distance;
            }
            if (projectile.ai[0] == 3)
            {
                projectile.position.X = player.Center.X - distance;
                projectile.position.Y = player.Center.Y - distance;
            }
            if (player.dead || !NPC.AnyNPCs(mod.NPCType("TheCelestial")))
            {
                projectile.Kill();
            }
            if (!player.active)
            {
                projectile.Kill();
            }

            if (projectile.localAI[0] == 0)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("CelestialIllusions"), projectile.damage, projectile.knockBack, 0, projectile.ai[0], projectile.whoAmI);
                projectile.localAI[0] = 1;
            }
        }
    }
}
    