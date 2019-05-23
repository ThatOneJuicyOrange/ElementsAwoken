using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.Sentries
{
    public class RifleSentryBase : ModProjectile
    {
        public int consumeEnergyTimer = 0;
        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 28;
           // projectile.aiStyle = 53;

            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.sentry = true;

            projectile.timeLeft = Projectile.SentryLifeTime;

            projectile.penetrate = -1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rifle Sentry");
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[Main.myPlayer];
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>(mod);
            consumeEnergyTimer--;
            if (consumeEnergyTimer <= 0)
            {
                modPlayer.energy--;
                consumeEnergyTimer = 60;
            }
            if (modPlayer.energy <= 0)
            {
                projectile.Kill();
            }
            if (projectile.localAI[0] == 0)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("RifleSentryHead"), projectile.damage, projectile.knockBack, Main.myPlayer, projectile.whoAmI, 0f);
                projectile.localAI[0]++;
            }

            projectile.velocity.X = 0f;
            projectile.velocity.Y = projectile.velocity.Y + 0.2f;
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.penetrate == 0)
            {
                projectile.Kill();
            }
            return false;
        }
    }
}