using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class MjolnirP : ModProjectile
    {
        public int arcTimer = 20;
        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 40;

            projectile.friendly = true;
            projectile.thrown = true;

            projectile.penetrate = -1;
            projectile.timeLeft = 500;
            projectile.extraUpdates = 2;
            projectile.aiStyle = 3;
            aiType = 301;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mjolnir");
        }
        public override void AI()
        {
            if (projectile.localAI[0] == 0)
            {
                projectile.localAI[0] = 1;
                arcTimer = 10 + Main.rand.Next(0, 20);
            }

            arcTimer--;
            if (arcTimer <= 0)
            {
                int num3 = 0;
                int[] array4 = new int[5];
                Vector2[] array5 = new Vector2[5];
                int num844 = 0;
                float num845 = 1000f;
                for (int i = 0; i < 200; i = num3 + 1)
                {
                    NPC nPC = Main.npc[i];
                    if (nPC.active && !nPC.friendly && !nPC.dontTakeDamage && nPC.damage > 0)
                    {
                        Vector2 center9 = nPC.Center;
                        float num847 = Vector2.Distance(center9, projectile.Center);
                        if (num847 < num845 && Collision.CanHit(projectile.Center, 1, 1, center9, 1, 1))
                        {
                            array4[num844] = i;
                            array5[num844] = center9;
                            num3 = num844 + 1;
                            num844 = num3;
                            if (num3 >= array5.Length)
                            {
                                break;
                            }
                        }
                    }
                    num3 = i;
                }
                for (int num848 = 0; num848 < num844; num848 = num3 + 1)
                {
                    Vector2 vector94 = array5[num848] - projectile.Center;
                    float ai = (float)Main.rand.Next(100);
                    Vector2 vector95 = Vector2.Normalize(vector94.RotatedByRandom(0.78539818525314331)) * 7f;
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector95.X, vector95.Y, mod.ProjectileType("LightningArc"), projectile.damage, 0f, Main.myPlayer, vector94.ToRotation(), ai);
                    num3 = num848;
                    Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 122);
                }
                arcTimer = 30 + Main.rand.Next(0,60);
            }
        }     
    }
}