using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Aqueous
{
    public class Aquanado : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 150;
            projectile.height = 42;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.alpha = 255;
            projectile.timeLeft = 540;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquanado");
            Main.projFrames[projectile.type] = 6;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(102, 255, 216);
        }
        public override void AI()
        {
            int num606 = 10; // scaling
            int num607 = 15; // scaling
            float num608 = 1f; // scaling
            int num609 = 150;
            int num610 = 42;
            if (projectile.velocity.X != 0f)
            {
                projectile.direction = (projectile.spriteDirection = -Math.Sign(projectile.velocity.X));
            }
            int num3 = projectile.frameCounter;
            projectile.frameCounter = num3 + 1;
            if (projectile.frameCounter > 2)
            {
                num3 = projectile.frame;
                projectile.frame = num3 + 1;
                projectile.frameCounter = 0;
            }
            if (projectile.frame >= 6)
            {
                projectile.frame = 0;
            }
            if (projectile.localAI[0] == 0f && Main.myPlayer == projectile.owner)
            {
                projectile.localAI[0] = 1f;
                projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
                projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
                projectile.scale = ((float)(num606 + num607) - projectile.ai[1]) * num608 / (float)(num607 + num606);
                projectile.width = (int)((float)num609 * projectile.scale);
                projectile.height = (int)((float)num610 * projectile.scale);
                projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
                projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
                projectile.netUpdate = true;
            }
            if (projectile.ai[1] != -1f)
            {
                projectile.scale = ((float)(num606 + num607) - projectile.ai[1]) * num608 / (float)(num607 + num606);
                projectile.width = (int)((float)num609 * projectile.scale);
                projectile.height = (int)((float)num610 * projectile.scale);
            }
            if (!Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
            {
                projectile.alpha -= 30;
                if (projectile.alpha < 60)
                {
                    projectile.alpha = 60;
                }
            }
            else
            {
                projectile.alpha += 30;
                if (projectile.alpha > 150)
                {
                    projectile.alpha = 150;
                }
            }
            if (projectile.ai[0] > 0f)
            {
                float[] var_2_19A93_cp_0 = projectile.ai;
                int var_2_19A93_cp_1 = 0;
                float num73 = var_2_19A93_cp_0[var_2_19A93_cp_1];
                var_2_19A93_cp_0[var_2_19A93_cp_1] = num73 - 1f;
            }
            if (projectile.ai[0] == 1f && projectile.ai[1] > 0f && projectile.owner == Main.myPlayer)
            {
                projectile.netUpdate = true;
                Vector2 center = projectile.Center;
                center.Y -= (float)num610 * projectile.scale / 2f;
                float num611 = ((float)(num606 + num607) - projectile.ai[1] + 1f) * num608 / (float)(num607 + num606);
                center.Y -= (float)num610 * num611 / 2f;
                center.Y += 2f;
                Projectile.NewProjectile(center.X, center.Y, projectile.velocity.X, projectile.velocity.Y, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 10f, projectile.ai[1] - 1f);
                int num612 = 2;
                if ((int)projectile.ai[1] % num612 == 0 && projectile.ai[1] != 0f)
                {
                    int num614 = NPC.NewNPC((int)center.X, (int)center.Y, mod.NPCType("AquaticReaper"), 0, 0f, 0f, 0f, 0f, 255);
                    Main.npc[num614].velocity = projectile.velocity;
                    Main.npc[num614].netUpdate = true;
                    if (projectile.type == 386)
                    {
                        Main.npc[num614].ai[2] = (float)projectile.width;
                        Main.npc[num614].ai[3] = -1.5f;
                    }
                }
            }
            if (projectile.ai[0] <= 0f)
            {
                float num615 = 0.104719758f;
                float num616 = (float)projectile.width / 5f;
                if (projectile.type == 386) // change this
                {
                    num616 *= 2f;
                }
                float num617 = (float)(Math.Cos((double)(num615 * -(double)projectile.ai[0])) - 0.5) * num616;
                projectile.position.X = projectile.position.X - num617 * (float)(-(float)projectile.direction);
                float[] var_2_19D98_cp_0 = projectile.ai;
                int var_2_19D98_cp_1 = 0;
                float num73 = var_2_19D98_cp_0[var_2_19D98_cp_1];
                var_2_19D98_cp_0[var_2_19D98_cp_1] = num73 - 1f;
                num617 = (float)(Math.Cos((double)(num615 * -(double)projectile.ai[0])) - 0.5) * num616;
                projectile.position.X = projectile.position.X + num617 * (float)(-(float)projectile.direction);
                return;
            }
        }
    }
}