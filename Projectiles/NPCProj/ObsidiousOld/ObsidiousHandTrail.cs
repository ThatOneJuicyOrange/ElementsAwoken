using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.ObsidiousOld
{
    public class ObsidiousHandTrail : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.hostile = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.timeLeft = 60;
            projectile.magic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Obsidious");
        }
        public override void AI()
        {
            projectile.localAI[0] += 1f;

            if (projectile.localAI[0] > 3f)
            {
                if (projectile.ai[0] == 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1f);
                        Main.dust[dust].velocity *= 0f;
                        Main.dust[dust].noGravity = true;
                    }
                    if (Main.rand.Next(100) == 0)
                    {
                        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, mod.ProjectileType("GreekFireHostile"), projectile.damage, 1, Main.myPlayer);
                    }
                }
                if (projectile.ai[0] == 1)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 75, 0f, 0f, 100, default(Color), 1f);
                        Main.dust[dust].velocity *= 0f;
                        Main.dust[dust].noGravity = true;
                    }
                }
                if (projectile.ai[0] == 2)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 135, 0f, 0f, 100, default(Color), 1f);
                        Main.dust[dust].velocity *= 0f;
                        Main.dust[dust].noGravity = true;
                    }
                }
                if (projectile.ai[0] == 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int dust1 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 62, 0f, 0f, 100, default(Color), 1.5f);
                        Main.dust[dust1].noGravity = true;
                        Main.dust[dust1].velocity *= 0f;
                    }
                    for (int i = 0; i < 1; i++)
                    {
                        int dust2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.PinkFlame, 0f, 0f, 100, default(Color), 1.5f);
                        Main.dust[dust2].noGravity = true;
                        Main.dust[dust2].velocity *= 0f;
                    }
                }
            }
        }
    }
}