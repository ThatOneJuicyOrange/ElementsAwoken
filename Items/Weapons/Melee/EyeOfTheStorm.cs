using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Melee
{
    public class EyeOfTheStorm : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;

            item.knockBack = 5;
            item.damage = 71;

            item.useTurn = true;
            item.autoReuse = true;
            item.melee = true;

            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.value = Item.buyPrice(0, 20, 0, 0);

            item.rare = 10;
            item.UseSound = SoundID.Item1;

            item.shoot = mod.ProjectileType("LightningBolt");
            item.shootSpeed = 20f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eye of the Storm");
            Tooltip.SetDefault("Shoots a lightning bolt along with lightning balls\nProjectiles rain down from the sky");
        }


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int i = Main.myPlayer;
            float num72 = item.shootSpeed;
            float num74 = knockBack;
            num74 = player.GetWeaponKnockback(item, num74);
            player.itemTime = item.useTime;
            int numberProjectiles = Main.rand.Next(2, 3);
            for (int k = 0; k < numberProjectiles; k++)
            {
                Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
                vector2.X = player.position.X + Main.rand.Next(-100, 100);
                vector2.Y = player.position.Y - Main.rand.Next(10, 150);
                Projectile.NewProjectile(vector2.X, vector2.Y, speedX, speedY, mod.ProjectileType("EyeOfTheStormBall"), damage, num74, i, 0f, 0f);
            }
            int numberProjectiles2 = Main.rand.Next(3, 5);
            for (int index = 0; index < numberProjectiles2; ++index)
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
                float SpeedX = num16 + (float)Main.rand.Next(-40, 41) * 0.02f;
                float SpeedY = num17 + (float)Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(vector2_1.X, vector2_1.Y, SpeedX, SpeedY, mod.ProjectileType("EyeOfTheStormP"), damage, knockBack, Main.myPlayer, 0.0f, (float)Main.rand.Next(5));
            }
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("LightningBolt"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }

        public override void AddRecipes()
        {   
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FragmentSolar, 6);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(null, "GustStrike", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
