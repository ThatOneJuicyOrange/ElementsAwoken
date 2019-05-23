using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Melee
{
    public class CosmoStriker : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 86;
            item.height = 86;
            item.damage = 150;
            item.melee = true;
            item.useAnimation = 17;
            item.useStyle = 1;
            item.useTime = 17;
            item.useTurn = true;
            item.knockBack = 6;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.maxStack = 1;
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.rare = 10;
            item.shoot = mod.ProjectileType("UniverseBall");
            item.shootSpeed = 20f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmo Striker");
            Tooltip.SetDefault("Forged in the heart of Terraria");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NeutronFragment", 8);
            recipe.AddIngredient(null, "CInfinityCrys", 1);
            recipe.AddIngredient(null, "Pyroplasm", 25);
            recipe.AddIngredient(ItemID.StarWrath);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 4 + Main.rand.Next(2);
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
                float SpeedX = num16 + (float)Main.rand.Next(-40, 41) * 0.02f;  //this defines the projectile X position speed and randomnes
                float SpeedY = num17 + (float)Main.rand.Next(-40, 41) * 0.02f;  //this defines the projectile Y position speed and randomnes
                Projectile.NewProjectile(vector2_1.X, vector2_1.Y, SpeedX, SpeedY, ProjectileID.StarWrath, damage, knockBack, Main.myPlayer, 0.0f, (float)Main.rand.Next(5));
            }
            int numProj = Main.rand.Next(1, 2);
            for (int index = 0; index < numProj; ++index)
            {
                float SpeedX = speedX + (float)Main.rand.Next(-25, 26) * 0.05f;
                float SpeedY = speedY + (float)Main.rand.Next(-25, 26) * 0.05f;
                switch (Main.rand.Next(3))
                {
                    case 0: type = mod.ProjectileType("UniverseBall"); break;
                    default: break;
                }
                Projectile.NewProjectile(position.X, position.Y, SpeedX, SpeedY, type, damage/2, knockBack, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
        }
    }
}
