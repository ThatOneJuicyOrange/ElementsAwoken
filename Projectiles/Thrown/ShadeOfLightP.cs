using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class ShadeOfLightP : ModProjectile
    {
        public float shootTimer = 0f;

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;

            projectile.friendly = true;
            projectile.thrown = true;

            projectile.penetrate = 6;

            projectile.aiStyle = 3;
            projectile.timeLeft = 1600;
            aiType = 52;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shade Of Light");
        }
        public override void AI()
        {
            shootTimer--;
            if (projectile.owner == Main.myPlayer)
            {
                float max = 400f;
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC nPC = Main.npc[i];
                    if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(projectile.Center, nPC.Center) <= max)
                    {
                        float Speed = 4f;
                        float rotation = (float)Math.Atan2(projectile.Center.Y - nPC.Center.Y, projectile.Center.X - nPC.Center.X);
                        if (shootTimer <= 0)
                        {
                            Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 8);
                            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, speed.X, speed.Y, mod.ProjectileType("ShadeOfLightBall"), projectile.damage / 2, projectile.knockBack, projectile.owner);
                            shootTimer = Main.rand.Next(30,90);
                        }
                    }
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Glowing"), 180);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.ai[0] += 0.1f;
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }     
    }
}