using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj
{
    public class SulfurBall : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;

            projectile.hostile = true;

            projectile.penetrate = 1;
            projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sulfur Ball");
        }
        public override void AI()
        {
            projectile.rotation += projectile.velocity.X * 0.02f;
            projectile.velocity.Y += projectile.ai[0];
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            projectile.Kill();
            target.AddBuff(ModContent.BuffType<Buffs.Debuffs.AcidBurn>(), 200);
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/AcidHiss"));
            for (int k = 0; k < 4; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 74, projectile.oldVelocity.X * 0.25f, projectile.oldVelocity.Y * 0.25f);
                Dust dust = Main.dust[Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 31, projectile.oldVelocity.X * 0.25f, projectile.oldVelocity.Y * 0.25f)];
                dust.color = Main.rand.NextBool() ? new Color(151, 121, 49) : new Color(149, 173, 87);
            }
        }
    }
}