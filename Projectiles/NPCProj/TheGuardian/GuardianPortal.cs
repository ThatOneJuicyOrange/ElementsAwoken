using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.TheGuardian
{
    public class GuardianPortal : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Temples Vortex");
        }
        public override void AI()
        {
            for (int i = 0; i < 10; i++)
            {
                Vector2 position = projectile.Center + Main.rand.NextVector2Circular(projectile.width * 0.5f, projectile.height * 0.5f);
                Dust dust = Dust.NewDustPerfect(position, 6, Vector2.Zero);
                dust.noGravity = true;
            }
            if (projectile.localAI[0] == 0)
            {
                int swirlCount = 5;
                for (int l = 0; l < swirlCount; l++)
                {
                    int distance = 360 / swirlCount;
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("GuardianPortalSwirl"), projectile.damage, projectile.knockBack, 0, l * distance, projectile.whoAmI);
                }
                projectile.localAI[0]++;
            }
        }
    }
}