using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class StarLunarFragment : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ignoreWater = true;
            projectile.magic = true;

            projectile.penetrate = 1;
            projectile.light = 1.5f;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wyvern Breath");
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.ai[1] <= 10)
            {
                return false;
            }
                return base.CanHitNPC(target);
        }
        public override void AI()
        {
            projectile.ai[1]++;

            int dustID = 6;
            if (projectile.ai[0] == 1) dustID = 242;
            else if (projectile.ai[0] == 2) dustID = 197;
            else if (projectile.ai[0] == 3) dustID = 229;

            for (int i = 0; i < 3; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, dustID, projectile.velocity.X, projectile.velocity.Y, 50, default(Color), 1.2f)];
                dust.position = (dust.position + projectile.Center) / 2f;
                dust.noGravity = true;
                dust.velocity *= 0.5f;
            }

            projectile.velocity.Y += 0.04f;
        }
    }
}