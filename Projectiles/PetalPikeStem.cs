using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class PetalPikeStem : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;

            projectile.aiStyle = 4;
            projectile.alpha = 255;
            projectile.penetrate = -1;

            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.melee = true;
            projectile.friendly = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Petal Pike");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (projectile.ai[0] == 0f)
            {
                projectile.alpha -= 50;
                if (projectile.alpha <= 0)
                {
                    projectile.alpha = 0;
                    projectile.ai[0] = 1f;
                    if (projectile.ai[1] == 0f)
                    {
                        projectile.ai[1] += 1f;
                        projectile.position += projectile.velocity * 1f;
                    }
                    if (Main.myPlayer == projectile.owner)
                    {
                        int num47 = projectile.type;
                        if (projectile.ai[1] >= 4f + Main.rand.Next(0,6))
                        {
                            num47 = mod.ProjectileType("PetalPikeTip");
                        }
                        int num48 = Projectile.NewProjectile(projectile.position.X + projectile.velocity.X + (float)(projectile.width / 2), projectile.position.Y + projectile.velocity.Y + (float)(projectile.height / 2), projectile.velocity.X, projectile.velocity.Y, num47, projectile.damage, projectile.knockBack, projectile.owner, 0f, projectile.ai[1] + 1f);
                        NetMessage.SendData(27, -1, -1, null, num48, 0f, 0f, 0f, 0, 0, 0);
                        return;
                    }
                }
            }
            else
            {
                projectile.alpha += 5;
                if (projectile.alpha >= 255)
                {
                    projectile.Kill();
                    return;
                }
            }
        }
    }
}