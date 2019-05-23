using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Yoyos
{
    public class VoidInfernoP : ModProjectile
    {
        public float timer = 0;
        public float cooldown = 0;
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;

            projectile.aiStyle = 99;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;

            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 450f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 18f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Inferno");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("ExtinctionCurse"), 200);
        }
        public override void AI()
        {
            if (Main.rand.Next(1) == 0)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustID.PinkFlame, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            }
            if (cooldown <= 0)
            {
                cooldown = 45;
            }
            cooldown--;
            timer--;
            float max = 600f;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(projectile.Center, nPC.Center) <= max)
                {
                    float Speed = 9f;
                    float rotation = (float)Math.Atan2(projectile.Center.Y - nPC.Center.Y, projectile.Center.X - nPC.Center.X);
                    Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                    if (timer <= 0 && cooldown <= 18)
                    {
                        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("VoidInfernoBlast"), projectile.damage, 0f, Main.myPlayer, 0f, 0f);
                        Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 20);
                        timer = 6;
                    }
                }
            }
        }
        }
}