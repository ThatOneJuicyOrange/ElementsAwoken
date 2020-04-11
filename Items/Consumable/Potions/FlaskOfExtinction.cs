using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Consumable.Potions
{
    class FlaskOfExtinction : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 24;
            item.maxStack = 30;
            item.rare = 8;
            item.value = Item.buyPrice(0, 5, 0, 0);
            item.useStyle = 2;
            item.useAnimation = 17;
            item.useTime = 17;
            item.UseSound = SoundID.Item3;
            item.consumable = true;
            item.buffType = mod.BuffType("ExtinctionCurseImbue");
            item.buffTime = 72000;
            //item.potion = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flask of Extinction");
            Tooltip.SetDefault("Melee attacks inflict Extinction Curse");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddIngredient(null, "VoiditeBar", 1);
            recipe.AddTile(TileID.ImbuingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
