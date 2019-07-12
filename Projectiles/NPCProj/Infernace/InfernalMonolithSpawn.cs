using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Infernace
{
    public class InfernalMonolithSpawn : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;

            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft = 180;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infernal Monolith");
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override void AI()
        {
            float numDusts = 64f;
            Vector2 shape = new Vector2(50f, 10f);

            for (int k = 0; k < numDusts; k++)
            {
                Vector2 vector11 = Vector2.UnitX * 0f;
                vector11 += -Vector2.UnitY.RotatedBy((double)((float)k * (6.28318548f / numDusts)), default(Vector2)) * shape;
                vector11 = vector11.RotatedBy((double)projectile.velocity.ToRotation(), default(Vector2));
                Dust dust = Main.dust[Dust.NewDust(projectile.Center, 0, 0, 6, 0f, 0f, 0, default(Color), 1f)];
                dust.scale = 1f;
                dust.noGravity = true;
                dust.position = projectile.Center + vector11;
                dust.velocity = new Vector2(0, Main.rand.NextFloat(-4f, 0f));
            }
        }

        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y + 40, 0f, 0f, mod.ProjectileType("InfernalMonolith"), projectile.damage, 0f, projectile.owner, 0f, 0f);
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 69);
        }
    }
}