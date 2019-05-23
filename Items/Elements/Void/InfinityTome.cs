using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Void
{
    public class InfinityTome : ModItem
    {

        public override void SetDefaults()
        {

            item.damage = 130;
            item.magic = true;
            item.mana = 8;
            item.width = 28;
            item.crit = 10;
            item.height = 30;
            item.useTime = 5;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 3.5f;
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item103;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("VoidTentacle");
            item.shootSpeed = 17f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tome of Infinity");
            Tooltip.SetDefault("The void hungers for life. It pulls in anything it touches");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VoidEssence", 10);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(ItemID.SpellTome);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 velocity = new Vector2(speedX, speedY).SafeNormalize(-Vector2.UnitY);
            Vector2 randomVel = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101)).SafeNormalize(-Vector2.UnitY);
            velocity = (velocity * 4f + randomVel).SafeNormalize(-Vector2.UnitY) * item.shootSpeed;
            float randAi0 = Main.rand.Next(10, 80) * 0.001f;
            if (Main.rand.Next(2) == 0)
            {
                randAi0 *= -1f;
            }
            float randAi1 = Main.rand.Next(10, 80) * 0.001f;
            if (Main.rand.Next(2) == 0)
            {
                randAi1 *= -1f;
            }
            Projectile.NewProjectile(position, velocity, type, damage, knockBack, player.whoAmI, randAi0, randAi1);
            return false;
        }
    }
}
