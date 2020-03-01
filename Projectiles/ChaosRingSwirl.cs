using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ChaosRingSwirl : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.magic = true;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.timeLeft = 10000;
            projectile.extraUpdates = 2;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Ring");
        }
        public override void AI()
        {
            Vector2 offset = new Vector2(40, 0);
            Projectile parent = Main.projectile[(int)projectile.ai[1]];
            projectile.ai[0] += 0.1f;
            projectile.position = parent.position + offset.RotatedBy(projectile.ai[0] + projectile.ai[1] * (Math.PI * 2 / 8));

            if (parent.active == false)
            {
                projectile.Kill();
            }
            for (int l = 0; l < 5; l++)
            {
                float num95 = projectile.velocity.X / 3f * (float)l;
                float num96 = projectile.velocity.Y / 3f * (float)l;
                int num97 = 4;
                int num98 = Dust.NewDust(new Vector2(projectile.position.X + (float)num97, projectile.position.Y + (float)num97), projectile.width - num97 * 2, projectile.height - num97 * 2, 127, 0f, 0f, 100, default(Color), 1.2f);
                Main.dust[num98].noGravity = true;
                Dust dust = Main.dust[num98];
                dust.velocity *= 0.1f;
                dust = Main.dust[num98];
                dust.velocity += projectile.velocity * 0.1f;
                Dust var_2_4829_cp_0_cp_0 = Main.dust[num98];
                var_2_4829_cp_0_cp_0.position.X = var_2_4829_cp_0_cp_0.position.X - num95;
                Dust var_2_4843_cp_0_cp_0 = Main.dust[num98];
                var_2_4843_cp_0_cp_0.position.Y = var_2_4843_cp_0_cp_0.position.Y - num96;
            }
            if (Main.rand.Next(5) == 0)
            {
                int num99 = 4;
                int num100 = Dust.NewDust(new Vector2(projectile.position.X + (float)num99, projectile.position.Y + (float)num99), projectile.width - num99 * 2, projectile.height - num99 * 2, 127, 0f, 0f, 100, default(Color), 0.6f);
                Dust dust = Main.dust[num100];
                dust.velocity *= 0.25f;
                dust = Main.dust[num100];
                dust.velocity += projectile.velocity * 0.5f;
            }
        }
        
    }
}