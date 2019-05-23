using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Ranged
{
    public class MegaRocket : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26;

            item.damage = 58;
            item.knockBack = 3.5f;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 5;
            item.UseSound = SoundID.Item61;

            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 6;

            item.shootSpeed = 10f;
            item.shoot = ProjectileID.RocketI;
            item.useAmmo = AmmoID.Rocket;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mega Rocket");
            Tooltip.SetDefault("Fires a rocket\nHas a chance to shoot a Mega Rocket");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (Main.rand.Next(4) == 0)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("MegaRocket"), damage * 2, knockBack, player.whoAmI, 0.0f, 0.0f);
            }
            else
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.RocketLauncher, 1);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 8);
            recipe.AddIngredient(null, "MysticLeaf", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
