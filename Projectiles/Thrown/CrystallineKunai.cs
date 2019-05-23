using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class CrystallineKunai : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 90;
            projectile.thrown = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystalline Kunai");
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.type == mod.NPCType("Storyteller"))
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            projectile.velocity.Y += 0.09f;
            if (Main.rand.Next(3) == 0)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 60, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f); // red : 12? 
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 61, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f); // green
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 59, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f); // blue
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 62, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f); // pink
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 27);
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 60, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f); // red : 12? 
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 61, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f); // green
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 59, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f); // blue
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 62, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f); // pink
            }
        }
    }
}