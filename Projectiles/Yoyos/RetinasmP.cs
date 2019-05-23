using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace ElementsAwoken.Projectiles.Yoyos
{
    public class RetinasmP : ModProjectile
    {
        public float timer = 30;
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;

            projectile.extraUpdates = 0;

            projectile.aiStyle = 99;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;

            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 270f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 11f;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 5f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Retinasm");
        }
        public override void AI()
        {
            timer--;
            float max = 400f;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(projectile.Center, nPC.Center) <= max)
                {
                    int type = ProjectileID.MiniRetinaLaser;
                    LegacySoundStyle sound = SoundID.Item11;
                    switch (Main.rand.Next(2))
                    {
                        case 0:
                            type = ProjectileID.MiniRetinaLaser;
                            sound = SoundID.Item33;
                            break;
                        case 1:
                            type = ProjectileID.CursedFlameFriendly;
                            sound = SoundID.Item20;
                            break;
                        default: break;
                    }
                    float Speed = 6f;
                    float rotation = (float)Math.Atan2(projectile.Center.Y - nPC.Center.Y, projectile.Center.X - nPC.Center.X);
                    Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                    if (timer <= 0)
                    {
                        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, type, projectile.damage, 0f, Main.myPlayer, 0f, 0f);
                        Main.PlaySound(sound, projectile.position);
                        timer = Main.rand.Next(15, 40);
                    }
                }
            }            
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.CursedInferno, 120);
        }
    }
}