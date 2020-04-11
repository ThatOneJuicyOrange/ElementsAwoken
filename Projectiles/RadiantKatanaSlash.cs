using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class RadiantKatanaSlash : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 64;
            projectile.height = 48;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiant Katana");
            Main.projFrames[projectile.type] = 5;
        }
        public override void AI()
        {
            float addRad = 0;
            if (projectile.direction == -1) addRad = 3.14f;
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + addRad;
            Vector2 dir = projectile.velocity;
            dir.Normalize();
            Player player = Main.player[projectile.owner];
            player.velocity = dir * 25f;
            player.immune = true;
            player.direction = Math.Sign(projectile.velocity.X);
            projectile.Center = player.Center + dir * 7;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 0;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Player player = Main.player[projectile.owner];

            projectile.frameCounter++;
            if (projectile.frameCounter >= 3)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 4)
                {
                    projectile.Kill();
                    float maxSpeed = Math.Max(player.accRunSpeed, player.maxRunSpeed);
                    if (Math.Abs(player.velocity.X) > maxSpeed) player.velocity.X = maxSpeed * player.direction;
                }
               
            }
            return true;
        }
    }
}
