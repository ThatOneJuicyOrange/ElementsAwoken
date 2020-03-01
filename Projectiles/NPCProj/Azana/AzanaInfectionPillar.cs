using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Azana
{
    public class AzanaInfectionPillar : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;

            projectile.tileCollide = false;
            projectile.hostile = true;
            projectile.alpha = 255;
            projectile.penetrate = -1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infection Pillar");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (projectile.localAI[0] != 0) projectile.position -= projectile.velocity * 1f;
            projectile.localAI[0]++;
            if (projectile.ai[0] == 0f)
            {
                projectile.alpha -= (int)projectile.localAI[1];
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
                        float mult = 1;
                        if (projectile.ai[1] >= 80f + Main.rand.Next(0,6))
                        {
                            num47 = mod.ProjectileType("AzanaInfectionPillarTip");
                            mult = 1.4f;
                        }
                        int num48 = Projectile.NewProjectile(projectile.Center.X + projectile.velocity.X * mult, projectile.Center.Y + projectile.velocity.Y * mult, projectile.velocity.X, projectile.velocity.Y, num47, projectile.damage, projectile.knockBack, projectile.owner, 0f, projectile.ai[1] + 1f);
                        NetMessage.SendData(27, -1, -1, null, num48, 0f, 0f, 0f, 0, 0, 0);
                        Main.projectile[num48].localAI[1] = projectile.localAI[1];
                        return;
                    }
                }
            }
            else
            {
                if (projectile.alpha < 170 && projectile.alpha + 5 >= 170)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 127, projectile.velocity.X * 0.025f, projectile.velocity.Y * 0.025f)];
                        dust.noGravity = true;
                        dust.velocity *= 0.5f;
                    }
                }
                projectile.ai[0]++;
                if (projectile.ai[0] > 40)
                {
                    projectile.alpha += 15;
                    if (projectile.alpha >= 255)
                    {
                        projectile.Kill();
                        return;
                    }
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 127, projectile.oldVelocity.X * 0.025f, projectile.oldVelocity.Y * 0.025f);
            }
        }
    }
}