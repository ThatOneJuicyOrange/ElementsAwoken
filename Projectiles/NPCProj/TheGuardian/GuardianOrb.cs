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
        public override string Texture { get { return "ElementsAwoken/Projectiles/NPCProj/TheGuardian/GuardianTargeter"; } }

        public override void SetDefaults()
        {
            projectile.width = 62;
            projectile.height = 62;

            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;

            projectile.penetrate = -1;
            projectile.timeLeft = 100;
            projectile.scale = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Guardian");
        }
        public override void AI()
        {
            projectile.rotation += 0.05f;
            if (projectile.ai[0] ==0 )
            {
                projectile.scale = 0.01f;
                projectile.ai[0]++;
            }
            float maxScale = 0.7f;
            if (projectile.scale < maxScale) projectile.scale += maxScale / 30;
            else projectile.scale = maxScale;

            if (Main.rand.NextBool(3))
            {
                int innerCircle = 18;
                Vector2 position = projectile.Center + Main.rand.NextVector2Circular(innerCircle * 0.5f, innerCircle * 0.5f);
                Dust dust = Dust.NewDustPerfect(position, 6, Vector2.Zero);
                dust.noGravity = true;
                dust.velocity = projectile.velocity * -0.2f;
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.DD2_FlameburstTowerShot, projectile.position);
            Player P = Main.player[Main.myPlayer];
            float Speed = 20f;
            float rotation = (float)Math.Atan2(projectile.Center.Y - P.Center.Y, projectile.Center.X - P.Center.X);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("GuardianShot"), projectile.damage, 0f, 0);
        }
    }
}