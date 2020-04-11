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

namespace ElementsAwoken.Items.Weapons.Summon
{
    public class Deathwish : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 28;

            item.damage = 105;
            item.knockBack = 3;
            item.mana = 10;

            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 1;

            item.summon = true;
            item.noMelee = true;

            item.value = Item.buyPrice(1, 0, 0, 0);
            item.rare = 10;

            item.UseSound = SoundID.Item44;

            item.shoot = mod.ProjectileType("Deathwatcher");
            item.shootSpeed = 7f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deathwish");
            Tooltip.SetDefault("Summons a Deathwatcher to annihilate your enemies");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CInfinityCrys", 1);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(null, "Pyroplasm", 25);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
