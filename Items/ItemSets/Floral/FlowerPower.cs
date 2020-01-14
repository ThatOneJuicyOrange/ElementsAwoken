using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Floral
{
    public class FlowerPower : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30; 
            
            item.damage = 8;
            item.mana = 3;
            item.knockBack = 1;

            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.useTime = 8;
            item.useAnimation = 24;
            item.useStyle = 5;

            item.value = Item.buyPrice(0, 2, 0, 0);
            item.rare = 3;

            item.UseSound = SoundID.Item20;
            item.shoot = mod.ProjectileType("FlowerPowerProj");
            item.shootSpeed = 16f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flower Power");
            Tooltip.SetDefault("Unleash a wave of low damaging petals");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = Main.rand.Next(2,5);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Petal", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
