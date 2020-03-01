using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class AsteroxShieldBase : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }

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
            DisplayName.SetDefault("Asterox");
        }
        public override void AI()
        {
            projectile.position.X = Main.player[projectile.owner].Center.X - (float)(projectile.width / 2);
            projectile.position.Y = Main.player[projectile.owner].Center.Y - (float)(projectile.height / 2);
            if (projectile.ai[0] == 0)
            {
                int swirlCount = 5;
                int orbital = projectile.whoAmI;
                projectile.ai[1] = projectile.whoAmI;
                for (int l = 0; l < swirlCount; l++)
                {
                    //cos = y, sin = x
                    int distance = 59;
                    orbital = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("AsteroxShieldSwirl"), projectile.damage, projectile.knockBack, projectile.owner, l * distance, projectile.whoAmI);

                }
                projectile.ai[0] = 1;
            }
        }
    }
}