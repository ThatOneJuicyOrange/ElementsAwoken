using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Ancients.Gores
{
    public class XernonShard : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 30;

            projectile.timeLeft = 100000;
            projectile.scale *= 1.3f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
            Main.projFrames[projectile.type] = 2;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frame = (int)projectile.ai[0];
            return true;
        }
        public override void AI()
        {
            projectile.rotation += 0.1f;

            Projectile parent = Main.projectile[0];
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].type == mod.ProjectileType("ShardBase"))
                {
                    parent = Main.projectile[i];
                }
            }
            float movespeed = 5f;
            if (Vector2.Distance(parent.Center, projectile.Center) >= 200)
            {
                movespeed = 12f;
            }
            Vector2 toTarget = new Vector2(parent.Center.X - projectile.Center.X, parent.Center.Y - projectile.Center.Y);
            toTarget = new Vector2(parent.Center.X - projectile.Center.X, parent.Center.Y - projectile.Center.Y);
            toTarget.Normalize();
            if (Vector2.Distance(parent.Center, projectile.Center) >= 40)
            {
                projectile.velocity = toTarget * movespeed;
            }

            for (int k = 0; k < Main.projectile.Length; k++)
            {
                Projectile other = Main.projectile[k];
                if (k != projectile.whoAmI && (other.type == projectile.type || other.type == mod.ProjectileType("KirveinShard") || other.type == mod.ProjectileType("KrecheusShard") || other.type == mod.ProjectileType("IzarisShard")) 
                    && other.active && Math.Abs(projectile.position.X - other.position.X) + Math.Abs(projectile.position.Y - other.position.Y) < projectile.width)
                {
                    const float pushAway = 0.05f;
                    if (projectile.position.X < other.position.X)
                    {
                        projectile.velocity.X -= pushAway;
                    }
                    else
                    {
                        projectile.velocity.X += pushAway;
                    }
                    if (projectile.position.Y < other.position.Y)
                    {
                        projectile.velocity.Y -= pushAway;
                    }
                    else
                    {
                        projectile.velocity.Y += pushAway;
                    }
                }
            }
        }
    }
}