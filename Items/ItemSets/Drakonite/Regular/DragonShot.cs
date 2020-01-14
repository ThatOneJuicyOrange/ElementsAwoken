using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Drakonite.Regular
{
    public class DragonShot : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 44;

            item.damage = 11;
            item.knockBack = 2f;

            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.UseSound = SoundID.Item5;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = false;

            item.value = Item.sellPrice(0, 0, 20, 0);
            item.rare = 1;

            item.shoot = 10;
            item.shootSpeed = 9f;
            item.useAmmo = 40;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Shot");
            Tooltip.SetDefault("Turns normal arrows into Drakonite arrows");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Drakonite", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.WoodenArrowFriendly)type = mod.ProjectileType("DrakoniteArrow");
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
    }
}
