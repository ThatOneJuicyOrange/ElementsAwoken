using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Aqueous
{
    public class AquaticBolt : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.melee = true;
            projectile.alpha = 255;
            projectile.penetrate = 1;
            projectile.timeLeft = 200;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aqueous");
        }
        public override void AI()
        {
            for (int num121 = 0; num121 < 4; num121++)
            {
                int num464 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 111, 0f, 0f, 100, default(Color), 1.2f);
                Main.dust[num464].noGravity = true;
                Dust dust = Main.dust[num464];
                dust.velocity *= 0.5f;
                dust = Main.dust[num464];
                dust.velocity += projectile.velocity * 0.1f;
            }
            if (projectile.timeLeft <= 50)
            {
                if (Main.rand.Next(12) == 0)
                {
                    projectile.Kill();
                }
            }
            if (Vector2.Distance(Main.player[Main.myPlayer].Center, projectile.Center) <= 75)
            {
                if (Main.rand.Next(6) == 0)
                {
                    projectile.Kill();
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("AquaticBlast"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
            //Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
        }
    }
}