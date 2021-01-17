using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class CorroderSpit : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.minion = true;

            projectile.alpha = 255;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corroder Spit");
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.3f, 0.9f, 0.6f);

            for (int i = 0; i < 5; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 74)];
                dust.velocity *= 0.6f;
                dust.position -= projectile.velocity / 8f * (float)i;
                dust.noGravity = true;
                dust.scale = 0.8f;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Corroding"), 300, false);
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/AcidHiss"));
        }
    }
}