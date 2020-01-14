using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ShimmersparkHeld : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;

            //projectile.aiStyle = 75;

            projectile.penetrate = -1;
            projectile.scale = 1.3f;

            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.hide = true;
            projectile.ranged = true;
            projectile.ignoreWater = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shimmerspark");
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);

            float chargeLevel = 120f;

            projectile.ai[0] += 1f;
            projectile.ai[1] += 1f;
            int num16 = 0;

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
            if (projectile.ai[0] == 20)
            {
                Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/ShimmersparkCharge"), 1f);
            }
            if (projectile.ai[0] > 10f)
            {
                Vector2 vector13 = Vector2.UnitX * 18f;
                vector13 = vector13.RotatedBy((double)(projectile.rotation - 1.57079637f), default(Vector2));
                Vector2 value5 = projectile.Center + vector13;
                for (int k = 0; k < num16 + 1; k++)
                {
                    float num19 = 0.9f;
                    if (k % 2 == 1)
                    {
                        num19 *= 1.2f;
                    }
                    Vector2 vector14 = value5 + ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * (12f - (float)(num16 * 2));
                    int num20 = Dust.NewDust(vector14 - Vector2.One * 8f, 16, 16, GetDustID(), projectile.velocity.X / 2f, projectile.velocity.Y / 2f, 0, default(Color), 1f);
                    Main.dust[num20].velocity = Vector2.Normalize(value5 - vector14) * 1.5f * (10f - (float)num16 * 2f) / 10f;
                    Main.dust[num20].noGravity = true;
                    Main.dust[num20].scale = num19;
                    Main.dust[num20].customData = player;
                }
            }
            if (Main.myPlayer == projectile.owner)
            {
                if (player.channel && !player.noItems && !player.CCed)
                {
                    if (projectile.ai[0] >= chargeLevel)
                    {
                        Vector2 vector16 = Vector2.Normalize(projectile.velocity) * 50f;
                        if (float.IsNaN(vector16.X) || float.IsNaN(vector16.Y))
                        {
                            vector16 = -Vector2.UnitY;
                        }
                        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector16.X, vector16.Y, mod.ProjectileType("ShimmersparkStrike"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                        Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/BigExplosion"), 1f);
                        modPlayer.screenshakeAmount = 4f;
                        projectile.ai[0] = 0;
                    }
                }
                else
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
        private int GetDustID()
        {
            switch (Main.rand.Next(4))
            {
                case 0:
                    return mod.DustType("AncientRed");
                case 1:
                    return mod.DustType("AncientGreen");
                case 2:
                    return mod.DustType("AncientBlue");
                case 3:
                    return mod.DustType("AncientPink");
                default:
                    return mod.DustType("AncientRed");
            }
        }
    }
}