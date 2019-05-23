using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Arrows
{
    public class DiscordantArrowChaos : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
            aiType = ProjectileID.WoodenArrowFriendly;
            projectile.alpha = 150;
            projectile.penetrate = -1;
            projectile.extraUpdates = 1;
            projectile.timeLeft = 200;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discordant Arrow");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("ChaosBurn"), 200);
            target.immune[projectile.owner] = 3;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.6f, 0.1f, 0.3f);
        }
    }
}