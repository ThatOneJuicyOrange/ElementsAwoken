using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.VoidLeviathan
{
    public class ExtinctionBlast : ModProjectile
    {
        public override void SetDefaults()
        {
            //projectile.CloneDefaults(ProjectileID.CultistBossFireBall);
            //aiType = ProjectileID.CultistBossFireBall;
            //ProjectileID.Sets.Homing[projectile.type] = true;
            Main.projFrames[projectile.type] = 4;
            projectile.width = 32;
            projectile.height = 32;
            //projectile.aiStyle = 1;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 1;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.tileCollide = false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Extinction Blast");
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("ExtinctionCurse"), 80);

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
            for (int k = 0; k < 2; k++)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PinkFlame);
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].scale *= 3f;
                Main.dust[dust].noGravity = true;
            }
            Lighting.AddLight(projectile.Center, 1.1f, 0.0f, 0.9f);
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int num3 = -1;
                float num4 = 2000f;
                for (int k = 0; k < 255; k++)
                {
                    if (Main.player[k].active && !Main.player[k].dead)
                    {
                        Vector2 center = Main.player[k].Center;
                        float num5 = Vector2.Distance(center, projectile.Center);
                        if ((num5 < num4 || num3 == -1) && Collision.CanHit(projectile.Center, 1, 1, center, 1, 1))
                        {
                            num4 = num5;
                            num3 = k;
                        }
                    }
                }
                if (num4 < 20f)
                {
                    projectile.Kill();
                    return;
                }
                if (num3 != -1)
                {
                    projectile.ai[1] = 21f;
                    projectile.ai[0] = (float)num3;
                    projectile.netUpdate = true;
                }
            }
            else if (projectile.ai[1] > 20f && projectile.ai[1] < 200f)
            {
                projectile.ai[1] += 1f;
                int num6 = (int)projectile.ai[0];
                if (!Main.player[num6].active || Main.player[num6].dead)
                {
                    projectile.ai[1] = 1f;
                    projectile.ai[0] = 0f;
                    projectile.netUpdate = true;
                }
                else
                {
                    float num7 = projectile.velocity.ToRotation();
                    Vector2 vector2 = Main.player[num6].Center - projectile.Center;
                    if (vector2.Length() < 20f)
                    {
                        projectile.Kill();
                        return;
                    }
                    float targetAngle = vector2.ToRotation();
                    if (vector2 == Vector2.Zero)
                    {
                        targetAngle = num7;
                    }
                    float num8 = num7.AngleLerp(targetAngle, 0.008f);
                    projectile.velocity = new Vector2(projectile.velocity.Length(), 0f).RotatedBy((double)num8, default(Vector2));
                }
            }
            if (projectile.ai[1] >= 1f && projectile.ai[1] < 20f)
            {
                projectile.ai[1] += 1f;
                if (projectile.ai[1] == 20f)
                {
                    projectile.ai[1] = 1f;
                }
            }
            projectile.alpha -= 40;
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            projectile.spriteDirection = projectile.direction;
            projectile.frameCounter++;
            if (projectile.frameCounter >= 3)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame >= 4)
                {
                    projectile.frame = 0;
                }
            }
        }
    }
}