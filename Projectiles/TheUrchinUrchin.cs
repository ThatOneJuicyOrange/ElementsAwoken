using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    // the worst name in existance:
    public class TheUrchinUrchin : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 26;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.MaxUpdates = 2;
            projectile.timeLeft = 500;
            projectile.scale *= 0.9f;
            projectile.tileCollide = false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Urchin");
        }
        public override void AI()
        {
            projectile.localAI[0]++;
            if (projectile.localAI[0] >= 60)
            {
                projectile.alpha += 5;
            }
            if (projectile.alpha >= 255)
            {
                projectile.Kill();
            }
            projectile.localAI[1]++;
            if (projectile.localAI[0] >= 10)
            {
                projectile.tileCollide = true;
            }
            projectile.velocity.Y += 0.2f;
            projectile.rotation += projectile.velocity.X * 0.1f;
            // stick to enemies code
            Rectangle myRect = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
            for (int i = 0; i < 200; i++)
            {
                bool flag = (!projectile.usesLocalNPCImmunity && !projectile.usesIDStaticNPCImmunity) || (projectile.usesLocalNPCImmunity && projectile.localNPCImmunity[i] == 0) || (projectile.usesIDStaticNPCImmunity && Projectile.IsNPCImmune(projectile.type, i));
                if (((Main.npc[i].active && !Main.npc[i].dontTakeDamage) & flag))
                {
                    bool flag2 = false;
                    if (Main.npc[i].trapImmune && projectile.trap)
                    {
                        flag2 = true;
                    }
                    else if (Main.npc[i].immortal && projectile.npcProj)
                    {
                        flag2 = true;
                    }

                    bool flag3;
                    {
                        flag3 = projectile.Colliding(myRect, Main.npc[i].getRect());
                    }
                    if (!flag2 && (Main.npc[i].noTileCollide || !projectile.ownerHitCheck || projectile.CanHit(Main.npc[i])))
                    {
                        if (flag3)
                        {
                            projectile.ai[0] = 1f;
                            projectile.ai[1] = (float)i;
                            projectile.velocity = (Main.npc[i].Center - projectile.Center) * 0.75f;
                            projectile.netUpdate = true;
                        }
                    }
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 3; k++)
            {
                int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 111, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
                Main.dust[dust].velocity *= 0.6f;
            }
        }
    }
}