using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.YukkiKun
{
    public class AncientIchorFlagon : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 38;

            item.damage = 54;

            item.thrown = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = false;

            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 1;
            item.knockBack = 3f;
            item.UseSound = SoundID.Item1;

            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 7;

            item.shoot = mod.ProjectileType("AncientIchorFlagonP");
            item.shootSpeed = 6.5f;

            item.GetGlobalItem<EATooltip>().donator = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Ichor Flagon");
            Tooltip.SetDefault("Yukki-Kun's donator item");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SunFragment", 8);
            recipe.AddIngredient(ItemID.Ichor, 15);
            recipe.AddIngredient(ItemID.Bottle, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
