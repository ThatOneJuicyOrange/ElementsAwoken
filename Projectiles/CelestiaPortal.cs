using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class CelestiaPortal : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.ranged = true;

            projectile.alpha = 60;
            projectile.penetrate = -1;
            projectile.timeLeft = 400;
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestia");
        }
        public override void AI()
        {
            projectile.velocity.X *= 0.98f;
            projectile.velocity.Y *= 0.98f;

            Lighting.AddLight(projectile.Center, 0.6f, 0.6f, 0.6f);

            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(projectile.Center, nPC.Center) <= 250)
                {
                    if (projectile.ai[0] <= 0)
                    {
                        Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 122);
                        Vector2 vector94 = nPC.Center - projectile.Center;
                        float ai = (float)Main.rand.Next(100);
                        float speed = 5f;
                        Vector2 vector95 = Vector2.Normalize(vector94.RotatedByRandom(0.78539818525314331)) * speed;
                        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector95.X, vector95.Y, mod.ProjectileType("CelestiaArc"), projectile.damage, 0f, 0, vector94.ToRotation(), ai);
                        projectile.ai[0]++;
                    }
                }
            }
            projectile.ai[1]++;
            if (projectile.ai[1] >= 120)
            {
                projectile.alpha += 10;
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