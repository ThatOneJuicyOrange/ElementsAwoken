using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.TheCelestial
{
    public class CelestialVortexPortal : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 46;
            projectile.height = 32;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.alpha = 60;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.scale = 1.3f;
            projectile.timeLeft = 400;
            Main.projFrames[projectile.type] = 4;
            projectile.magic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Celestial");
        }
        public override void AI()
        {
            projectile.velocity.X *= 0.9f;
            projectile.velocity.X *= 0.9f;
            Lighting.AddLight(projectile.Center, 0.6f, 0.6f, 0.6f);
            Player P = Main.player[Main.myPlayer];

            projectile.ai[0]++;
            if (projectile.ai[0] == 180)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 122);

                Vector2 vector94 = P.Center - projectile.Center;
                float ai = (float)Main.rand.Next(100);
                float speed = 5f;
                Vector2 vector95 = Vector2.Normalize(vector94.RotatedByRandom(0.78539818525314331)) * speed;
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector95.X, vector95.Y, mod.ProjectileType("CelestialLightning"), projectile.damage, 0f, 0, vector94.ToRotation(), ai);
            }
            if (projectile.ai[0] >= 240)
            {
                projectile.alpha += 10;
            }
            if (projectile.alpha >= 180)
            {
                projectile.damage = 0; // to stop the player running into semi invisible portals
            }
            if (projectile.alpha >= 255)
            {
                projectile.Kill();
            }
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 4)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                    projectile.frame = 0;
            }
            return true;
        }
    }
}