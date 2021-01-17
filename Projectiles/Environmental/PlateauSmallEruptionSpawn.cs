using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Environmental
{
    public class PlateauSmallEruptionSpawn : ModProjectile
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
                    float scale = MyWorld.plateauWeather == 2 ? 0.7f : 1f;
                    for (int i = 0; i < 3; i++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f)];
                        dust.velocity *= Main.rand.NextFloat(0.3f,1f) * scale;
                    }
                }
                else if (projectile.ai[0] == 90)
                {
                    Main.PlaySound(SoundID.DD2_BetsyFlameBreath, projectile.position);
                }
                else if (projectile.ai[0] >= 90)
                {
                    if (projectile.ai[0] % 6 == 0)
                    {
                        Projectile.NewProjectile(projectile.Center, new Vector2(Main.rand.NextFloat(-1, 1), -4) * 2, ModContent.ProjectileType<PlateauSmallEruption>(), projectile.damage, projectile.knockBack, projectile.owner);
                    }
                }
                if (projectile.ai[0] > 120) projectile.Kill();
            }
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
    }
}
