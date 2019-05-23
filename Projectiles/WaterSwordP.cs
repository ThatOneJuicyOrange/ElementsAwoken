using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class WaterSwordP : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Arkhalis);
            aiType = ProjectileID.Arkhalis;
            projectile.width = 68;
            projectile.height = 62;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.ownerHitCheck = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Forever Tide");
            Main.projFrames[projectile.type] = 28;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);

            float num = 0f;
            if (projectile.spriteDirection == -1)
            {
                num = 3.14159274f;
            }
            int num29 = projectile.frame + 1;
            projectile.frame = num29;
            if (num29 >= Main.projFrames[projectile.type])
            {
                projectile.frame = 0;
            }
            projectile.soundDelay--;
            if (projectile.soundDelay <= 0)
            {
                Main.PlaySound(SoundID.Item1, projectile.Center);
                projectile.soundDelay = 12;
            }
            if (Main.myPlayer == projectile.owner)
            {
                if (player.channel && !player.noItems && !player.CCed)
                {
                    float scaleFactor6 = 1f;
                    if (player.inventory[player.selectedItem].shoot == projectile.type)
                    {
                        scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
                    }
                    Vector2 vector20 = Main.MouseWorld - vector;
                    vector20.Normalize();
                    if (vector20.HasNaNs())
                    {
                        vector20 = Vector2.UnitX * (float)player.direction;
                    }
                    vector20 *= scaleFactor6;
                    if (vector20.X != projectile.velocity.X || vector20.Y != projectile.velocity.Y)
                    {
                        projectile.netUpdate = true;
                    }
                    projectile.velocity = vector20;
                }
                else
                {
                    projectile.Kill();
                }
            }
            Vector2 vector21 = projectile.Center + projectile.velocity * 3f;
            Lighting.AddLight(vector21, 0.8f, 0.8f, 0.8f);
            if (Main.rand.Next(3) == 0)
            {
                int num31 = Dust.NewDust(vector21 - projectile.Size / 2f, projectile.width, projectile.height, 59, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 2f);
                Main.dust[num31].noGravity = true;
                Main.dust[num31].position -= projectile.velocity;
            }
            projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f;
            projectile.rotation = projectile.velocity.ToRotation() + num;
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));

            /*float num = 0f;
            if (++projectile.frame >= Main.projFrames[projectile.type])
            {
                projectile.frame = 0;
            }
            projectile.soundDelay--;
            if (projectile.soundDelay <= 0)
            {
                Main.PlaySound(SoundID.Item15, projectile.Center);
                projectile.soundDelay = 24;
            }
            Vector2 vector14 = projectile.Center + projectile.velocity * 3f;
            Lighting.AddLight(vector14, 1f, 0.2f, 0.2f);
            if (Main.rand.Next(3) == 0) ;*/
        }


        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 2;
        }
    }
}