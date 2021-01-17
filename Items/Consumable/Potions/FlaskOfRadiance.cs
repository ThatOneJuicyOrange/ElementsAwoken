using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Consumable.Potions
{
    public class FlaskOfRadiance : ModItem
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

            item.buffType = ModContent.BuffType<Buffs.StarstruckImbue>();
            item.buffTime = 72000;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flask of Radiance");
            Tooltip.SetDefault("Melee attacks inflict Starstruck");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddIngredient(ModContent.ItemType<ItemSets.Radia.Radia>(), 1);
            recipe.AddTile(TileID.ImbuingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
