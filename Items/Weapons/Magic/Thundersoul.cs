using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Magic
{
    public class Thundersoul : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;

            item.damage = 42;
            item.knockBack = 5;
            item.mana = 18;

            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 5;
            item.rare = 6;

            Item.staff[item.type] = true;     
            item.noMelee = true;
            item.autoReuse = true;
            item.useTurn = true;
            item.magic = true;

            item.value = Item.buyPrice(0, 50, 0, 0);
            item.UseSound = SoundID.Item122;

            item.shoot = mod.ProjectileType("ThundersoulBolt");
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thundersoul");
            Tooltip.SetDefault("Shoots a bolt that electrocutes nearby enemies");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MysticLeaf", 1);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddIngredient(ItemID.MartianConduitPlating, 50);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
