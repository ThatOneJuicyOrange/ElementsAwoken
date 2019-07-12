using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Elemental
{
    public class Equinox : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 58;
            item.height = 22;

            item.damage = 130;
            item.knockBack = 2.25f;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.useTime = 8;
            item.useAnimation = 8;
            item.useStyle = 5;
            item.UseSound = SoundID.Item5;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.value = Item.sellPrice(0, 20, 0, 0);

            item.shoot = 10;
            item.shootSpeed = 18f;
            item.useAmmo = 40;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Equinox");
            Tooltip.SetDefault("Turns normal arrows into 3 elemental arrows");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == 1) // The normal arrow
            {
                type = mod.ProjectileType("ElementalArrow");
            }
            int numberProjectiles = 4;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(8));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            if (Main.rand.Next(4) == 0)
            {
                int numberProjectiles2 = 2;
                for (int i = 0; i < numberProjectiles2; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(8));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("EquinoxBase"), damage * 3, knockBack, player.whoAmI);
                }
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ElementalEssence", 5);
            recipe.AddIngredient(null, "VoiditeBar", 8);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
