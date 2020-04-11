using System;
using System.Collections.Generic;
using ElementsAwoken.Items.Materials;
using ElementsAwoken.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Developer
{
    public class TimeGun : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26;
            
            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = false;
            item.GetGlobalItem<EATooltip>().developer = true;

            item.damage = 121;
            item.knockBack = 3.5f;

            item.useAnimation = 2;
            item.useTime = 2;
            item.useStyle = 5;
            item.UseSound = SoundID.Item11;

            item.value = Item.buyPrice(1, 0, 0, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            
            item.shootSpeed = 16f;
            item.shoot = 10;
            item.useAmmo = 97;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Gun That Shot Time");
            Tooltip.SetDefault("Break laws, ascend reality\nShoots as fast as you can click\nGenih Wat's developer weapon");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (Main.rand.Next(3) == 0)
            {
                int numberProjectiles1 = Main.rand.Next(1, 3);
                for (int i = 0; i < numberProjectiles1; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<TimeRocket>(), damage, knockBack, player.whoAmI);
                }
            }
            int numberProjectiles2 = Main.rand.Next(3,5);
            for (int i = 0; i < numberProjectiles2; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X * 1.1f, perturbedSpeed.Y * 1.1f, ProjectileType<TimecleaverRound>(), damage, knockBack, player.whoAmI);
            }
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 0);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<Pyroplasm>(), 50);
            recipe.AddIngredient(ItemType<NeutronFragment>(), 8);
            recipe.AddIngredient(ItemType<VoiditeBar>(), 8);
            recipe.AddIngredient(ItemID.VortexBeater, 1);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
