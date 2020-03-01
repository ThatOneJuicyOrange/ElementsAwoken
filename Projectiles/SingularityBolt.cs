using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class SingularityBolt : ModProjectile
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
            projectile.tileCollide = false;
            projectile.timeLeft = 200;
            projectile.melee = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Singularity");
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.3f) / 255f, ((255 - projectile.alpha) * 0.4f) / 255f, ((255 - projectile.alpha) * 1f) / 255f);
            if (Main.rand.Next(1) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 242);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1.2f;
                int dust2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 197);
                Main.dust[dust2].noGravity = true;
                Main.dust[dust2].scale = 1.2f;
                int dust3 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6);
                Main.dust[dust3].noGravity = true;
                Main.dust[dust3].scale = 1.2f;
                int dust4 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 229);
                Main.dust[dust4].noGravity = true;
                Main.dust[dust4].scale = 1.2f;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[0] == 0)
            {
                int swirlCount = 5;
                int orbital = projectile.whoAmI;
                projectile.ai[1] = projectile.whoAmI;
                for (int l = 0; l < swirlCount; l++)
                {
                    //cos = y, sin = x
                    int distance = 59;
                    orbital = Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("SingularityBeam"), projectile.damage, projectile.knockBack, projectile.owner, l * distance, target.whoAmI);

                }
                projectile.ai[0] = 1;
            }
        }
    }
}