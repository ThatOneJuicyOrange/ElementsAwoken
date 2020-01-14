using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class DubstepGunHeld : ModProjectile
    {
        public float shootTimer1 = 0;
        public float shootTimer2 = 0;
        public float shootTimer3 = 0;
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;

            //projectile.aiStyle = 75;

            projectile.penetrate = -1;

            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.hide = true;
            projectile.ranged = true;
            projectile.ignoreWater = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Destroyer");
        }
        public override void AI()
        {

            Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/Dubstep"), 1, 0);
        
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);

            projectile.ai[0] += 1f;
            projectile.ai[1] += 1f;

            if (projectile.ai[1] >= 10)
            {
                projectile.ai[1] = 0f;

                float scaleFactor3 = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
                Vector2 vector11 = vector;
                Vector2 value4 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - vector11;
                if (player.gravDir == -1f)
                {
                    value4.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - vector11.Y;
                }
                Vector2 vector12 = Vector2.Normalize(value4);
                if (float.IsNaN(vector12.X) || float.IsNaN(vector12.Y))
                {
                    vector12 = -Vector2.UnitY;
                }
                vector12 *= scaleFactor3;
                if (vector12.X != projectile.velocity.X || vector12.Y != projectile.velocity.Y)
                {
                    projectile.netUpdate = true;
                }
                projectile.velocity = vector12;

            }
            shootTimer1--;
            shootTimer2--;
            shootTimer3--;
            if (Main.myPlayer == projectile.owner)
            {
                Vector2 vector16 = Vector2.Normalize(projectile.velocity) * 15f;
                if (float.IsNaN(vector16.X) || float.IsNaN(vector16.Y))
                {
                    vector16 = -Vector2.UnitY;
                }
                if (shootTimer1 <= 0)
                {
                    Vector2 speed = vector16.RotatedByRandom(MathHelper.ToRadians(20));
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, speed.X, speed.Y, mod.ProjectileType("DubstepWave"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);                  
                    shootTimer1 = 6;
                }
                if (shootTimer2 <= 0)
                {
                    Vector2 speed = vector16.RotatedByRandom(MathHelper.ToRadians(50));
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, speed.X, speed.Y, mod.ProjectileType("DubstepBeam"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                    shootTimer2 = 14;
                    PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
                    modPlayer.energy -= 3;
                }
                if (shootTimer3 <= 0)
                {
                    Vector2 speed = vector16;
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, speed.X, speed.Y, mod.ProjectileType("DubstepPulse"), (int)(projectile.damage * 1.75), 20f, projectile.owner, 0f, 0f);
                    shootTimer3 = 60;
                }
                if (!player.channel)
                {
                    projectile.Kill();
                }
            }
            Vector2 thing = projectile.velocity;
            thing.Normalize(); // makes the value = 1
            thing *= 20f;
            Vector2 yAdd = new Vector2(0, 0);
            if (player.direction == 1)
            {
                yAdd.Y = 10;
            }
            projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f + thing.RotatedBy((double)(MathHelper.Pi / 10), default(Vector2)) - yAdd;
            projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));

            float scaleFactor = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
            Vector2 vector3 = vector;
            Vector2 value2 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - vector3;
            if (player.gravDir == -1f)
            {
                value2.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - vector3.Y;
            }
            Vector2 vector4 = Vector2.Normalize(value2);
            if (float.IsNaN(vector4.X) || float.IsNaN(vector4.Y))
            {
                vector4 = -Vector2.UnitY;
            }
            vector4 *= scaleFactor;
            if (vector4.X != projectile.velocity.X || vector4.Y != projectile.velocity.Y)
            {
                projectile.netUpdate = true;
            }
            projectile.velocity = vector4;
        }
    }
}