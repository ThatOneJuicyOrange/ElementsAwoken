using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Volcanox
{
    public class VolcanicBoom : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 100;
            projectile.height = 100;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.alpha = 255;
            projectile.penetrate = 1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Volcanic Boom");
        }
        public override void AI()
        {
            if (projectile.localAI[0] == 0f)
            {
                Main.PlaySound(SoundID.Item74, projectile.position);
                projectile.localAI[0] += 1f;
            }
            projectile.ai[0] += 1f;
            float num467 = 25f;
            if (projectile.ai[0] > 180f)
            {
                num467 -= (projectile.ai[0] - 180f) / 2f;
            }
            if (num467 <= 0f)
            {
                num467 = 0f;
                projectile.Kill();
            }
            int num468 = 0;
            while ((float)num468 < num467)
            {
                if (Main.rand.Next(2) == 0)
                {
                    float num469 = (float)Main.rand.Next(-10, 11);
                    float num470 = (float)Main.rand.Next(-10, 11);
                    float num471 = (float)Main.rand.Next(3, 9);
                    float num472 = (float)Math.Sqrt((double)(num469 * num469 + num470 * num470));
                    num472 = num471 / num472;
                    num469 *= num472;
                    num470 *= num472;
                    int num473 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 174, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[num473].noGravity = true;
                    Main.dust[num473].position.X = projectile.Center.X;
                    Main.dust[num473].position.Y = projectile.Center.Y;
                    Dust var_2_157AD_cp_0_cp_0 = Main.dust[num473];
                    var_2_157AD_cp_0_cp_0.position.X = var_2_157AD_cp_0_cp_0.position.X + (float)Main.rand.Next(-10, 11);
                    Dust var_2_157D8_cp_0_cp_0 = Main.dust[num473];
                    var_2_157D8_cp_0_cp_0.position.Y = var_2_157D8_cp_0_cp_0.position.Y + (float)Main.rand.Next(-10, 11);
                    Main.dust[num473].velocity.X = num469;
                    Main.dust[num473].velocity.Y = num470;
                    int num3 = num468;
                    num468 = num3 + 1;
                }
            }
            return;
        }
    }
}