using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class DeathwarpCenter : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.alpha = 60;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft = 600;
            projectile.magic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fireweaver");
        }
        public override void AI()
        {
            projectile.position.X = Main.player[projectile.owner].Center.X;
            projectile.position.Y = Main.player[projectile.owner].Center.Y;
            if (projectile.ai[0] == 0)
            {
                int swirlCount = 2;
                int orbital = projectile.whoAmI;
                for (int l = 0; l < swirlCount; l++)
                {
                    int distance = 180;

                    orbital = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("DeathwarpSpinner"), projectile.damage, projectile.knockBack, projectile.owner, l * distance, projectile.whoAmI);
                }
                projectile.ai[0] = 1;
            }
        }
    }
}