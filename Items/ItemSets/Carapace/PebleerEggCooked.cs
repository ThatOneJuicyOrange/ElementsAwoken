using System;
using System.IO;
using ElementsAwoken.Buffs.PotionBuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.ItemSets.Carapace
{
    public class PebleerEggCooked : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 28;

            item.UseSound = SoundID.Item2;
            item.useStyle = 2;
            item.useAnimation = 17;
            item.useTime = 17;

            item.useTurn = true;
            item.consumable = true;

            item.maxStack = 999;

            item.value = Item.sellPrice(0, 0, 1, 0);
            item.rare = 0;

            item.buffType = BuffID.WellFed;
            item.buffTime = 36000;
            return;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cooked Pebleer Egg");
            Tooltip.SetDefault("Minor improvements to all stats\nHeals the player 20 life over 10 seconds");
        }
        public override bool UseItem(Player player)
        {
            player.AddBuff(BuffType<HeartyMeal>(), 600);
            return base.UseItem(player);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<PebleerEgg>(), 1);
            recipe.AddTile(TileID.Campfire);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
