using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class DeterioratorKnife : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;

            projectile.friendly = true;
            projectile.thrown = true;

            projectile.penetrate = 1;
            projectile.timeLeft = 120;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Knife");
        }
        public override void AI()
        {
            if (projectile.localAI[0] == 0)
            {
                projectile.timeLeft = Main.rand.Next(60, 90);
                projectile.localAI[0]++;
            }
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 40f)
            {
                projectile.damage = (int)((double)projectile.damage * 0.95);
                projectile.knockBack = (float)((int)((double)projectile.knockBack * 0.95));
            }
            if (projectile.ai[0] < 240f)
            {
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            }
            if (Main.rand.Next(6) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 74);
                Main.dust[dust].velocity *= 0.07f;
                Main.dust[dust].scale = Main.rand.NextFloat(0.5f, 1.2f);
                Main.dust[dust].noGravity = true;
            }
        }
        public override void Kill(int timeLeft)
        {
            if (Main.rand.Next(4) == 0)
            {
                Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/AcidHiss"));
            }
            for (int k = 0; k < 3; k++)
            {
                int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 74, 0f, 0f, 100, default(Color));
                Main.dust[dust].noGravity = true;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Corroding"), 300, false);
        }
    }
}