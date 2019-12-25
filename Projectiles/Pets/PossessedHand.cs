using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.Pets
{
    public class PossessedHand : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 34;
            projectile.height = 34;

            projectile.netImportant = true;
            projectile.friendly = true;
            Main.projPet[projectile.type] = true;
            projectile.tileCollide = false;

            projectile.minionSlots = 1f;
            projectile.timeLeft = 18000;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;

            //projectile.aiStyle = 54;
            //aiType = 317;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Possessed Hand");
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.dead)
            {
                modPlayer.possessedHand = false;
            }
            if (modPlayer.possessedHand)
            {
                projectile.timeLeft = 2;
            }


            float num535 = projectile.position.X;
            float num536 = projectile.position.Y;

            float num546 = 8f;
            if (projectile.ai[0] == 1f)
            {
                num546 = 12f;
            }
            Vector2 vector42 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
            float num547 = player.Center.X - vector42.X;
            float num548 = player.Center.Y - vector42.Y - 60f;
            float num549 = (float)Math.Sqrt((double)(num547 * num547 + num548 * num548));
            if (num549 < 100f && projectile.ai[0] == 1f && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
            {
                projectile.ai[0] = 0f;
            }
            if (num549 > 2000f)
            {
                projectile.position.X = player.Center.X - (float)(projectile.width / 2);
                projectile.position.Y = player.Center.Y - (float)(projectile.width / 2);
            }
            if (num549 > 70f)
            {
                num549 = num546 / num549;
                num547 *= num549;
                num548 *= num549;
                projectile.velocity.X = (projectile.velocity.X * 20f + num547) / 21f;
                projectile.velocity.Y = (projectile.velocity.Y * 20f + num548) / 21f;
            }
            else
            {
                if (projectile.velocity.X == 0f && projectile.velocity.Y == 0f)
                {
                    projectile.velocity.X = -0.15f;
                    projectile.velocity.Y = -0.05f;
                }
                projectile.velocity *= 1.01f;
            }
            projectile.rotation = projectile.velocity.X * 0.2f;

            if ((double)Math.Abs(projectile.velocity.X) > 0.2)
            {
                projectile.spriteDirection = -projectile.direction;
                return;
            }

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