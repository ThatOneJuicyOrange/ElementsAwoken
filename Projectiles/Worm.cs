using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class Worm : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.MaxUpdates = 2;
            projectile.timeLeft = 500;
        }
        public override void AI()
        {
            projectile.velocity.Y += 0.05f;
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Worm");
        }
    }
}