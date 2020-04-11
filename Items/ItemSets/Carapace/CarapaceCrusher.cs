using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.ItemSets.Carapace
{
    public class CarapaceCrusher : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 28;

            item.damage = 8;
            item.pick = 45;
            item.knockBack = 6f;

            item.useTime = 39;
            item.useAnimation = 39;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;

            item.melee = true;
            item.autoReuse = true;
            item.useTurn = true;

            item.value = Item.sellPrice(0, 0, 1, 50);
            item.rare = 0;
            item.GetGlobalItem<ItemsGlobal>().miningRadius = 1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Carapace Crusher");
            Tooltip.SetDefault("Mines a 3x3 area");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<CarapaceItem>(), 8);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
