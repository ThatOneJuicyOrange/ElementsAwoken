using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.YukkiKun
{
    public class TheSpeedyStar : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 48;

            item.damage = 22;
            item.mana = 50;

            item.reuseDelay = 16;
            item.useAnimation = 25;
            item.useTime = 5;
            item.useStyle = 5;

            item.noMelee = true;
            item.magic = true;
            Item.staff[item.type] = true;
            item.autoReuse = true;
            item.knockBack = 2.25f;

            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 5;

            item.UseSound = SoundID.Item29;
            item.shoot = mod.ProjectileType("Speedstar");
            item.shootSpeed = 16f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Speedy Star");
            Tooltip.SetDefault("Hitting all the stars restores all the mana consumed in that shot\nYukki-Kun's donator item");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FallenStar, 10);
            recipe.AddIngredient(ItemID.SoulofLight, 5);
            recipe.AddIngredient(null, "DemonicFleshClump", 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
