using System;
using System.Collections.Generic;
using ElementsAwoken.Items.Pets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class ChaosTomatoP : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Tomato");
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
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
        public override void AI()
        {
            projectile.rotation += 0.02f;
            projectile.ai[0]++;
            if (projectile.ai[0] < 60)
            {
                if (projectile.velocity.Y < -2) projectile.velocity.Y -= 0.02f;
                projectile.velocity.X *= 0.97f;
            }
            else
            {
                projectile.velocity *= 0.97f;
            }
            if (projectile.ai[0] > 20)
            {
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player p = Main.player[i];
                    if (p.active && p.Hitbox.Intersects(projectile.Hitbox))
                    {
                        Vector2 toTarget = new Vector2(projectile.Center.X - p.Center.X, projectile.Center.Y - p.Center.Y);
                        toTarget.Normalize();
                        projectile.velocity += toTarget * 0.75f;
                    }
                }
            }
            int maxDist = 20;
            for (int i = 0; i < 6; i++)
            {
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);
                Dust dust = Main.dust[Dust.NewDust(projectile.Center + offset - Vector2.One * 4, 0, 0, 127, 0, 0, 100)];
                dust.noGravity = true;
            }

        }
        public override void Kill(int timeLeft)
        {
            int item = Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, ModContent.ItemType<AzanaChibi>());
            NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
        }
    }
}