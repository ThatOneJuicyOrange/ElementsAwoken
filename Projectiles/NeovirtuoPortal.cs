using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class NeovirtuoPortal : ModProjectile
    {
        public int laserTimer = 5;

        public override void SetDefaults()
        {
            projectile.width = 36;
            projectile.height = 36;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.alpha = 60;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.scale = 1.3f;
            projectile.timeLeft = 600;
            Main.projFrames[projectile.type] = 4;
            projectile.magic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Neovirtuo Portal");
        }
        public override void AI()
        {
            projectile.velocity.X *= 0.9f;
            projectile.velocity.X *= 0.9f;
            Lighting.AddLight(projectile.Center, 0.4f, 0.2f, 0.4f);
            laserTimer--;
            float max = 400f;
            for (int i = 0; i < 200; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.friendly && !nPC.dontTakeDamage && Vector2.Distance(projectile.Center, nPC.Center) <= max && nPC.damage > 0)
                {
                    Vector2 vector8 = new Vector2(projectile.position.X + (projectile.width / 2), projectile.position.Y + (projectile.height / 2));
                    int type = mod.ProjectileType("NeovirtuoBolt");
                    float Speed = 6f;
                    float rotation = (float)Math.Atan2(vector8.Y - (nPC.position.Y + (nPC.height * 0.5f)), vector8.X - (nPC.position.X + (nPC.width * 0.5f)));
                    if (laserTimer <= 0)
                    {
                        Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1)).RotatedByRandom(MathHelper.ToRadians(20));
                        Projectile.NewProjectile(vector8.X, vector8.Y, perturbedSpeed.X, perturbedSpeed.Y, type, 60, 0f, Main.myPlayer, 0f, 0f);
                        laserTimer = 5;
                    }
                }
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