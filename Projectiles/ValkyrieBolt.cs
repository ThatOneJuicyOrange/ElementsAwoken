using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ValkyrieBolt : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.melee = true;
            projectile.alpha = 100;
            projectile.penetrate = -1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 200;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Valkyrie");
        }
        public override void AI()
        {
            projectile.velocity.Y += 0.2f;

            for (int l = 0; l < 5; l++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 60)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / 6f * (float)l;
                dust.noGravity = true;
                dust.scale = 1f;
                dust.color = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, mod.ProjectileType("RainbowExplosion"), projectile.damage, 0, projectile.owner);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, mod.ProjectileType("RainbowExplosion"), projectile.damage, 0, projectile.owner);
            return true;
        }
    }
}