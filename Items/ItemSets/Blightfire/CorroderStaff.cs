using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.IO;
using Terraria.ObjectData;
using Terraria.Utilities;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Blightfire
{
    public class CorroderStaff : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;

            item.damage = 120;
            item.knockBack = 1.25f;
            item.mana = 20;

            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 1;

            item.noMelee = true;
            item.autoReuse = true;
            item.summon = true;

            item.value = Item.sellPrice(0, 7, 50, 0);
            item.rare = 11;

            item.UseSound = SoundID.Item113;
            item.shoot = mod.ProjectileType("CorroderMinion");
            item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corroder Staff");
            Tooltip.SetDefault("Summons a corroder to protect you\nEach corroders only takes 0.75 minion slots");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Blightfire", 10);
            recipe.AddIngredient(ItemID.LunarBar, 2);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
