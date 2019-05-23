using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ElementsAwoken.Items.Elements.Fire
{
    public class InfernoLance : ModItem
    {
        public override void SetDefaults()
        {       
            item.damage = 24;
            item.melee = true;
            item.noMelee = true;
            item.useTurn = true;
            item.noUseGraphic = true;
            item.useAnimation = 20;
            item.useTime = 20;
            item.useStyle = 5;
            item.knockBack = 4.75f;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.height = 60;
            item.width = 60;
            item.maxStack = 1;
            item.value = Item.buyPrice(0, 7, 0, 0);
            item.rare = 4;
            item.shoot = mod.ProjectileType("InfernoLanceP");
            item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Inferno Lance");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX/2, speedY/2, mod.ProjectileType("FirelashFlames"), damage/2, knockBack, player.whoAmI, 0.0f, 0.0f);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FireEssence", 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
