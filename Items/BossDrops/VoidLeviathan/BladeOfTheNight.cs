using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.VoidLeviathan
{
    public class BladeOfTheNight : ModItem
    {
        public int swingNum = 1;
        public int hitNum = 1;

        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 60;

            item.damage = 200;
            item.knockBack = 8;

            item.melee = true;
            item.autoReuse = true;
            item.useTurn = true;

            item.useTime = 8;
            item.useAnimation = 8;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.value = Item.sellPrice(0, 25, 0, 0);

            item.shoot = mod.ProjectileType("ExtinctionBall");
            item.shootSpeed = 20f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blade of the Night");
            Tooltip.SetDefault("Projectiles become more unstable the longer they are alive\nEvery 30th swing unleashes the spirit of The Void Leviathan to tear apart your enemies\nEvery 10th hit with the actual blade will do 10x damage");
        }
        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            if (hitNum == 10)
            {
                damage *= 10;
            }
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.immune[item.owner] = 3;
            hitNum++;
            if (hitNum > 10)
            {
                damage *= 10;
                hitNum = 1;
            }
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            swingNum++;
            if (swingNum > 30)
            {
                Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 113);

                int current = Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX * 0.75f, speedY * 0.75f, mod.ProjectileType("VoidLeviathanProjHead"), damage, 0f, Main.myPlayer);

                int previous = current;
                for (int k = 0; k < 12; k++)
                {
                    previous = current;
                    current = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("VoidLeviathanProjBody"), damage, 0f, Main.myPlayer, previous);
                }
                previous = current;
                current = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("VoidLeviathanProjTail"), damage, 0f, Main.myPlayer, previous);
                Main.projectile[previous].localAI[1] = (float)current;
                Main.projectile[previous].netUpdate = true;

                // make dust in an expanding circle
                int numDusts = 36;
                for (int i = 0; i < numDusts; i++)
                {
                    Vector2 dustPos = (Vector2.Normalize(new Vector2(5, 5)) * new Vector2((float)player.width / 2f, (float)player.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + player.Center;
                    Vector2 velocity = dustPos - player.Center;
                    int dust = Dust.NewDust(dustPos + velocity, 0, 0, DustID.PinkFlame, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].noLight = true;
                    Main.dust[dust].velocity = Vector2.Normalize(velocity) * 3f;
                    Main.dust[dust].fadeIn = 1.3f;
                }

                swingNum = 1;
            }
            int numProj = 3;
            for (int i = 0; i < numProj; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(8));
                Projectile proj = Main.projectile[Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("VoidSinewave"), damage, knockBack, player.whoAmI)];
                proj.localAI[0] = Main.rand.NextFloat();
            }
            if (swingNum % 2 == 0)
            {
                int numProj2 = Main.rand.Next(1, 4);
                for (int i = 0; i < numProj2; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
                    Projectile proj = Main.projectile[Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X * Main.rand.NextFloat(0.75f, 1f), perturbedSpeed.Y * Main.rand.NextFloat(0.75f, 1f), mod.ProjectileType("VoidOrb"), damage, knockBack, player.whoAmI)];
                }
            }
            /* int numberProjectiles = 3;
             for (int index = 0; index < numberProjectiles; ++index)
             {
                 Vector2 vector2_1 = new Vector2((float)((double)player.position.X + (double)player.width * 0.5 + (double)(Main.rand.Next(201) * -player.direction) + ((double)Main.mouseX + (double)Main.screenPosition.X - (double)player.position.X)), (float)((double)player.position.Y + (double)player.height * 0.5 - 600.0));   //this defines the projectile width, direction and position
                 vector2_1.X = (float)(((double)vector2_1.X + (double)player.Center.X) / 2.0) + (float)Main.rand.Next(-200, 201);
                 vector2_1.Y -= (float)(100 * index);
                 float num12 = (float)Main.mouseX + Main.screenPosition.X - vector2_1.X;
                 float num13 = (float)Main.mouseY + Main.screenPosition.Y - vector2_1.Y;
                 if ((double)num13 < 0.0) num13 *= -1f;
                 if ((double)num13 < 20.0) num13 = 20f;
                 float num14 = (float)Math.Sqrt((double)num12 * (double)num12 + (double)num13 * (double)num13);
                 float num15 = item.shootSpeed / num14;
                 float num16 = num12 * num15;
                 float num17 = num13 * num15;
                 int num18 = damage / 2;
                 float SpeedX = num16 + (float)Main.rand.Next(-40, 41) * 0.02f;
                 float SpeedY = num17 + (float)Main.rand.Next(-40, 41) * 0.02f;
                 Projectile.NewProjectile(vector2_1.X, vector2_1.Y, SpeedX, SpeedY, mod.ProjectileType("NightSword"), num18, knockBack, Main.myPlayer, 0.0f, (float)Main.rand.Next(5));
             }
             int num6 = Main.rand.Next(2, 4);
             for (int index = 0; index < num6; ++index)
             {
                 float SpeedX = speedX + (float)Main.rand.Next(-25, 26) * 0.05f;
                 float SpeedY = speedY + (float)Main.rand.Next(-25, 26) * 0.05f;
                 switch (Main.rand.Next(3))
                 {
                     case 0: type = mod.ProjectileType("ExtinctionBall"); break;
                     default: break;
                 }
                 int num19 = damage / 2;
                 Projectile.NewProjectile(position.X, position.Y, SpeedX, SpeedY, type, num19, knockBack, player.whoAmI, 0.0f, 0.0f);
             }*/
            return false;
        }
    }
}
