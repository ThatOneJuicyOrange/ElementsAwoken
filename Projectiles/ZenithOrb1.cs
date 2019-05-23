using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles
{
    public class ZenithOrb1 : ModProjectile
    {
        public int shootTimer = 5;
        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 38;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.magic = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 400;
            projectile.light = 2f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zenith");
            Main.projFrames[projectile.type] = 5;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 6)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                    projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            projectile.velocity *= 0.997f;
            shootTimer--;
            if (projectile.owner == Main.myPlayer)
            {
                float num396 = projectile.position.X;
                float num397 = projectile.position.Y;
                float num398 = 300f;
                float max = 400f;
                bool flag11 = false;
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].CanBeChasedBy(projectile, true) && Vector2.Distance(projectile.Center, Main.npc[i].Center) <= max)
                    {
                        float num400 = Main.npc[i].position.X + (float)(Main.npc[i].width / 2);
                        float num401 = Main.npc[i].position.Y + (float)(Main.npc[i].height / 2);
                        float num402 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num400) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num401);
                        if (num402 < num398 && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[i].position, Main.npc[i].width, Main.npc[i].height))
                        {
                            num398 = num402;
                            num396 = num400;
                            num397 = num401;
                            flag11 = true;
                        }
                    }
                }
                if (flag11)
                {
                    float num403 = 30f; //modify the speed the projectile are shot.  Lower number = slower projectile.
                    Vector2 vector29 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                    float num404 = num396 - vector29.X;
                    float num405 = num397 - vector29.Y;
                    float num406 = (float)Math.Sqrt((double)(num404 * num404 + num405 * num405));
                    num406 = num403 / num406;
                    num404 *= num406;
                    num405 *= num406;
                    if (shootTimer <= 0)
                    {
                        Projectile.NewProjectile(projectile.Center.X - 4f, projectile.Center.Y, num404, num405, mod.ProjectileType("ZenithBeam1"), projectile.damage / 2, projectile.knockBack, projectile.owner, 0f, 0f);
                        shootTimer = 8;
                    }
                    return;
                }
            }
        }
    }
}