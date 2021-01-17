using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Environmental
{
    public class AcidGeyserSpawn : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 2;
            projectile.alpha = 255;
            projectile.tileCollide = false;
            projectile.timeLeft = 210;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acid Geyser");
        }
        public override void AI()
        {
            projectile.ai[0]++;
            if (projectile.ai[0] < 90)
            {
                int dustWidth = 8;
                if (Main.rand.NextBool())
                {
                    Dust dust = Main.dust[Dust.NewDust(new Vector2(projectile.Center.X - dustWidth / 2, projectile.position.Y), dustWidth, 2, 74, 0f, 0f, 100, default(Color), 1f)]; // 74, 75, 107, 163
                    dust.velocity.X *= Main.rand.NextFloat(0.3f, 1f);
                    dust.velocity.Y = -4 * Main.rand.NextFloat(0.3f, 1f);
                }
            }
            else if (projectile.ai[0] == 90)
            {
                Main.PlaySound(SoundID.Item45, (int)projectile.position.X, (int)projectile.position.Y);
            }
            else if (projectile.ai[0] > 90 && projectile.ai[0] % 20 == 0)
            {
                Projectile.NewProjectile(projectile.Center, new Vector2(Main.rand.NextFloat(-1,1), Main.rand.NextFloat(-6, -3)), ModContent.ProjectileType<AcidGeyserProjectile>(), projectile.damage, projectile.knockBack, projectile.owner);
            }
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
    }
}
