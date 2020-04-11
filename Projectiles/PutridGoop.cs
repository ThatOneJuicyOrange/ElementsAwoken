using System;
using System.Collections.Generic;
using ElementsAwoken.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Projectiles
{
    public class PutridGoop : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;

            projectile.friendly = true;
            projectile.ranged = true;

            projectile.penetrate = -1;
            projectile.timeLeft = 90;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Putrid Trail");
        }
        public override void AI()
        {
            projectile.velocity.Y += 0.05f;
            for (int i = 0; i < 2; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.width, 46, 0f, 0f, 150, default(Color), 0.75f)];
                dust.velocity *= 0.05f;
                dust.noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffType<FastPoison>(), 60);
        }
    }
}