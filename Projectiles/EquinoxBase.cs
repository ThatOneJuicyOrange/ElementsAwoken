using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class EquinoxBase : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.alpha = 60;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.timeLeft = 120;
            projectile.magic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fireweaver");
        }
        public override void AI()
        {
            if (projectile.ai[0] == 0)
            {
                int swirlCount = 2;
                int orbital = projectile.whoAmI;
                for (int l = 0; l < swirlCount; l++)
                {
                    int distance = 16;
                    int proj = mod.ProjectileType("EquinoxSwirl");
                    orbital = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, proj, projectile.damage, projectile.knockBack, projectile.owner, l * distance, projectile.whoAmI);
                        
                }
                projectile.ai[0] = 1;
            }
            if (Main.rand.Next(3) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 242);
                Main.dust[dust].scale *= 0.8f;
                Main.dust[dust].noGravity = true;
                int dust1 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6);
                Main.dust[dust1].scale *= 0.8f;
                Main.dust[dust1].noGravity = true;
                int dust2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 229);
                Main.dust[dust2].scale *= 0.5f;
                Main.dust[dust2].noGravity = true;
                int dust3 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 197);
                Main.dust[dust3].scale *= 0.5f;
                Main.dust[dust3].noGravity = true;
            }
        }
    }
}