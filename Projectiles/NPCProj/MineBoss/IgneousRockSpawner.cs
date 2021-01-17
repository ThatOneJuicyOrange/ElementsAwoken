using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj.MineBoss
{
    public class IgneousRockSpawner : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;

            projectile.hostile = true;

            projectile.penetrate = -1;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Igneous Rock");
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override void AI()
        {
            if (projectile.ai[0] == 0)
            {
                projectile.velocity.Y = -6;
                if (projectile.position.Y < (EAWorldGen.mineBossArenaLoc.Y + 4) * 16) projectile.ai[0] = 1;
            }
            else
            {
                projectile.ai[1]++;
                projectile.velocity.Y = 0;
                int dustWidth = 20;
                if (Main.rand.NextBool(3))
                {
                    Dust dust = Main.dust[Dust.NewDust(new Vector2(projectile.Center.X - dustWidth / 2, projectile.position.Y), dustWidth, 2, Main.rand.NextBool() ? 6 : 31, 0f, 0f, 100, default(Color), 1.5f)];
                    dust.velocity.X *= Main.rand.NextFloat(0.3f, 1f);
                    dust.velocity.Y = 1 * Main.rand.NextFloat(0.3f, 1f);
                }
                if (projectile.ai[1] >= 60)
                {
                    Projectile.NewProjectile(projectile.Center, new Vector2(0, 0), ModContent.ProjectileType<IgneousRockProj>(), projectile.damage, projectile.knockBack, projectile.owner);
                    projectile.Kill();
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.ai[0] = 1;
            return false;
        }
    }
}