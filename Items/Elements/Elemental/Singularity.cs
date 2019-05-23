using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ElementsAwoken.Items.Elements.Elemental
{
    public class Singularity : ModItem
    {
        public override void SetDefaults()
        {
            item.height = 60;
            item.width = 60;

            item.damage = 142;
            item.knockBack = 4.75f;

            item.melee = true;
            item.noMelee = true;
            item.useTurn = true;
            item.noUseGraphic = true;
            item.autoReuse = false;

            item.useAnimation = 16;
            item.useTime = 16;
            item.useStyle = 5;
            item.UseSound = SoundID.Item1;

            item.maxStack = 1;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.value = Item.sellPrice(0, 20, 0, 0);

            item.shoot = mod.ProjectileType("SingularityP");
            item.shootSpeed = 11f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Singularity");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("SingularityBolt"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ElementalEssence", 5);
            recipe.AddIngredient(null, "VoidAshes", 8);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
