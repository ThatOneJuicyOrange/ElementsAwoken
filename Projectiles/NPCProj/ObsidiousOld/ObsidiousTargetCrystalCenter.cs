using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj.ObsidiousOld
{
    public class ObsidiousTargetCrystalCenter : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 200;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Obsidious");
        }
        public override void AI()
        {
            Player player = Main.player[(int)projectile.ai[1]];

            projectile.localAI[1]++;
            if (projectile.localAI[1] < 120)
            {
                projectile.position.X = player.Center.X;
                projectile.position.Y = player.Center.Y;
            }

            if (!player.active)
            {
                projectile.Kill();
            }

            if (projectile.localAI[0] == 0)
            {
                int swirlCount = 5;
                for (int l = 0; l < swirlCount; l++)
                {
                    int distance = 360 / swirlCount;
                    int orbital = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("ObsidiousTargetCrystal"), projectile.damage, projectile.knockBack, 0, l * distance, projectile.whoAmI);
                    Projectile Orbital = Main.projectile[orbital];

                }
                projectile.localAI[0] = 1;
            }
        }
    }
}
    