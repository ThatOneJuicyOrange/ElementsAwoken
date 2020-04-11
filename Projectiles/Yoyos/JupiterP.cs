using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace ElementsAwoken.Projectiles.Yoyos
{
    public class JupiterP : ModProjectile
    {
        public float timer = 30;
        public bool hasGas = false;
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;

            projectile.aiStyle = 99;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;

            projectile.light = 0.5f;

            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 410f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 18f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jupiter");
        }
        public override void AI()
        {
            int maxDist = 200;
            float gravStength = 0.075f;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                bool immune = false;
                foreach (int k in ElementsAwoken.instakillImmune)
                {
                    if (npc.type == k)
                    {
                        immune = true;
                    }
                }
                if (!immune && npc.CanBeChasedBy(this) && !npc.boss && npc.lifeMax < 10000 && Vector2.Distance(npc.Center, projectile.Center) < maxDist)
                {
                    Vector2 toTarget = new Vector2(projectile.Center.X - npc.Center.X, projectile.Center.Y - npc.Center.Y);
                    toTarget.Normalize();
                    npc.velocity.X += toTarget.X * gravStength;
                    npc.velocity.Y += toTarget.Y * gravStength * 5;
                }
            }
            for (int i = 0; i < Main.maxItems; i++)
            {
                Item item = Main.item[i];
                if (item.active && Vector2.Distance(item.Center, projectile.Center) < maxDist)
                {
                    Vector2 toTarget = new Vector2(projectile.Center.X - item.Center.X, projectile.Center.Y - item.Center.Y);
                    toTarget.Normalize();
                    item.velocity += toTarget * gravStength;
                }
            }

            /*timer--;
            float max = 400f;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(projectile.Center, nPC.Center) <= max)
                {
                    float Speed = 10f;
                    float rotation = (float)Math.Atan2(projectile.Center.Y - nPC.Center.Y, projectile.Center.X - nPC.Center.X);
                    Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                    if (timer <= 0)
                    {
                        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y - 2f, mod.ProjectileType("JupiterGas"), projectile.damage, 0f, Main.myPlayer, 0f, 0f);
                        timer = Main.rand.Next(3, 15);
                    }
                }
            }*/
            if (!hasGas && projectile.localAI[0] >= 20) // 0 is a timer
            {
                int swirlCount = 8;
                for (int l = 0; l < swirlCount; l++)
                {
                    int distance = 360 / swirlCount;
                    int orbital = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("JupiterGasOrbit"), projectile.damage, projectile.knockBack, 0, l * distance, projectile.whoAmI);
                    Projectile Orbital = Main.projectile[orbital];
                }
                hasGas = true;
            }
        }
    }
}