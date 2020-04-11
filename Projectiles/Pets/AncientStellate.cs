using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.Pets
{
    public class AncientStellate : ModProjectile
    {
        private float aiState
        {
            get => projectile.ai[1];
            set => projectile.ai[1] = value;
        }
        public override void SetDefaults()
        {
            projectile.width = 34;
            projectile.height = 34;

            projectile.aiStyle = -1;

            projectile.netImportant = true;
            projectile.friendly = true;
            Main.projPet[projectile.type] = true;
            projectile.tileCollide = false;

            projectile.minionSlots = 1f;
            projectile.timeLeft = 18000;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Stellate");
            Main.projFrames[projectile.type] = 8;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            if (aiState == 0)
            {
                projectile.frame = 0;
            }
            else
            {
                projectile.frameCounter++;
                if (projectile.frameCounter >= 4)
                {
                    projectile.frame++;
                    projectile.frameCounter = 0;
                    if (projectile.frame > 7)
                        projectile.frame = 0;
                }
            }
            return true;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.dead) modPlayer.stellate = false;
            if (modPlayer.stellate)  projectile.timeLeft = 2;

            if (!Collision.CanHitLine(projectile.Center, 1, 1, player.Center, 1, 1))
            {
                projectile.ai[0] = 1f;
            }
            float speed = 6f;
            if (projectile.ai[0] == 1f)
            {
                speed = 15f;
            }

            if (aiState == 0)
            {
                if (projectile.velocity.Y < 0) projectile.velocity.Y *= 0.99f;           
                    projectile.velocity.Y += 0.06f;
                if (projectile.Center.Y > player.Center.Y + 50) aiState = 1;
            }
            else
            {
                if (projectile.velocity.Y > -4) projectile.velocity.Y -= 0.5f;
                if (projectile.Center.Y < player.Center.Y - 75) aiState = 0;
            }
            float toVelX = player.Center.X -( 50 * player.direction) - projectile.Center.X;
            if (Math.Abs(toVelX) < 100f && projectile.ai[0] == 1f && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
            {
                projectile.ai[0] = 0f;
                projectile.netUpdate = true;
            }
            if (Vector2.Distance(player.Center,projectile.Center) > 2000f) projectile.Center = player.Center;
            if (Math.Abs(toVelX) > 48f)
            {
                toVelX = Math.Sign(toVelX) * speed;
                float temp = 40 / 2f;
                projectile.velocity.X = (projectile.velocity.X * temp + toVelX) / (temp + 1);
            }
            else
            {
                projectile.velocity.X *= (float)Math.Pow(0.9, 40.0 / 40);
            }

            projectile.rotation = projectile.velocity.X * 0.05f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.penetrate == 0)
            {
                projectile.Kill();
            }
            return false;
        }
    }
}