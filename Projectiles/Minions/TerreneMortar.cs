using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.Minions
{
    public class TerreneMortar : ModProjectile
    {
        public int shootTimer = 30;
        public override void SetDefaults()
        {
            projectile.width = 58;
            projectile.height = 42;
           // projectile.aiStyle = 53;

            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.sentry = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;

            projectile.timeLeft = Projectile.SentryLifeTime;

            projectile.penetrate = -1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terrene Mortar");
            Main.projFrames[projectile.type] = 2;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 12)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 1)
                    projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            projectile.velocity.X = 0f;
            projectile.velocity.Y = projectile.velocity.Y + 0.2f;
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }

            shootTimer--;
            if (shootTimer <= 0)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.NextFloat(-3, 3), -8, mod.ProjectileType("TerreneRock"), projectile.damage, projectile.knockBack, Main.myPlayer, 0f, 0f);
                shootTimer = Main.rand.Next(5, 40);
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