using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.TheGuardian
{
    public class GuardianOrb : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft = 100;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Guardian");
        }
        public override void AI()
        {
            for (int i = 0; i < 5; i++)
            {
                Vector2 position = projectile.Center + Main.rand.NextVector2Circular(projectile.width * 0.5f, projectile.height * 0.5f);
                Dust dust = Dust.NewDustPerfect(position, 6, Vector2.Zero);
                dust.noGravity = true;
            }
        }
        public override void Kill(int timeLeft)
        {
            Player P = Main.player[Main.myPlayer];
            float Speed = 20f;
            Main.PlaySound(SoundID.DD2_FlameburstTowerShot, projectile.position);
            float rotation = (float)Math.Atan2(projectile.Center.Y - P.Center.Y, projectile.Center.X - P.Center.X);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("GuardianShot"), projectile.damage, 0f, 0);
        }
    }
}