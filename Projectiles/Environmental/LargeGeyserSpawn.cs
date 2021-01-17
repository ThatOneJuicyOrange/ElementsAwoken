using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Environmental
{
    public class LargeGeyserSpawn : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.alpha = 255;
            projectile.tileCollide = false;
            projectile.timeLeft = 900;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eruption");
        }
        public override void AI()
        {
            projectile.ai[0]++;
            if (projectile.ai[1] == 0)
            {
                if (projectile.ai[0] > 5)
                {
                    if (projectile.lavaWet)
                    {
                        projectile.velocity.Y = -2;
                    }
                    else
                    {
                        projectile.velocity.Y = 0;
                        if (ProjectileUtils.OnScreen(projectile))
                        {
                            projectile.ai[1] = 1;
                            projectile.ai[0] = 0;
                        }
                        else projectile.Kill();
                    }
                }
            }
            else
            {
                if (projectile.ai[0] < 90)
                {
                    int dustWidth = 60;
                    for (int i = 0; i < 6; i++)
                    { 
                        Dust dust = Main.dust[Dust.NewDust(new Vector2(projectile.Center.X - dustWidth / 2, projectile.position.Y), dustWidth, 2, Main.rand.NextBool() ? 6 : 31, 0f, 0f, 100, default(Color), 1.5f)];
                        dust.velocity.X *= Main.rand.NextFloat(0.3f, 1f);
                        dust.velocity.Y = -8 * Main.rand.NextFloat(0.3f, 1f);
                    }
                }
                else if (projectile.ai[0] == 90)
                {
                    bool clearAbove = true;
                    for (int j = 0; j < 10; j++)
                    {
                        Point tilePos = projectile.Center.ToTileCoordinates();
                        if (Main.tile[tilePos.X, tilePos.Y - j].active() && Main.tileSolid[Main.tile[tilePos.X, tilePos.Y - j].type])
                        {
                            clearAbove = false;
                            break;
                        }
                    }
                    if (clearAbove) Projectile.NewProjectile(projectile.Center, new Vector2(0, -2), ModContent.ProjectileType<LargeGeyser>(), projectile.damage, projectile.knockBack, projectile.owner);
                    projectile.Kill();
                }
            }
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
    }
}
