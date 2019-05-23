using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Ancients.Krecheus
{
    public class KrecheusBlade : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;

            projectile.hostile = true;
            projectile.friendly = false;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;

            projectile.penetrate = -1;
            projectile.timeLeft = 1000000;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Krecheus");
        }
        public override void AI()
        {
            NPC parent = Main.npc[(int)projectile.ai[1]];

            projectile.localAI[0]++;

            if (projectile.localAI[0] == 5) // to stop it creating dust when they appear on the npc
            {
                for (int k = 0; k < 80; k++)
                {
                    int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("AncientRed"), projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f, 100, default(Color), 2f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = 1f + Main.rand.Next(10) * 0.1f;
                }               
            }

            Vector2 direction = parent.Center - projectile.Center;
            projectile.rotation = direction.ToRotation() + 0.785f;
            projectile.rotation += MathHelper.ToRadians(45);

            projectile.ai[0] += 2f; // speed
            int distance = 100;
            double rad = projectile.ai[0] * (Math.PI / 180); // angle to radians
            projectile.position.X = parent.Center.X - (int)(Math.Cos(rad) * distance) - projectile.width / 2;
            projectile.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * distance) - projectile.height / 2;

            if (parent.active == false)
            {
                projectile.Kill();
            }
        }
    }
}