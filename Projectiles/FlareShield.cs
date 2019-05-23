using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class FlareShield : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft = 10000;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flare Shield");
        }
        public override void AI()
        {
            Player P = Main.player[projectile.owner];

            projectile.position.X = P.position.X;
            projectile.position.Y = P.position.Y;

            int maxDist = 100;
            for (int i = 0; i < 120; i++)
            {
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);
                Dust dust = Main.dust[Dust.NewDust(projectile.Center + offset, 0, 0, 6, 0, 0, 100)];
                dust.noGravity = true;
            }
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.damage > 0 && !npc.boss && Vector2.Distance(npc.Center, projectile.Center) < maxDist)
                {
                    Vector2 toTarget = new Vector2(projectile.Center.X - npc.Center.X, projectile.Center.Y - npc.Center.Y);
                    toTarget.Normalize();
                    npc.velocity -= toTarget * 0.3f;
                }
            }
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj.active && proj.hostile && Vector2.Distance(proj.Center, projectile.Center) < maxDist)
                {
                    Vector2 toTarget = new Vector2(projectile.Center.X - proj.Center.X, projectile.Center.Y - proj.Center.Y);
                    toTarget.Normalize();
                    proj.velocity -= toTarget;
                }
            }
            if (P.FindBuffIndex(mod.BuffType("FlareShield")) == -1 || P.dead)
            {
                projectile.active = false;
            }
        }
    }
}