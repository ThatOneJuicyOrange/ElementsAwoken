using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Consumable.Potions
{
    public class DrakoniteSkinPotion : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 28;

            item.UseSound = SoundID.Item3;
            item.useStyle = 2;
            item.useAnimation = 30;
            item.useTime = 30;

            item.useTurn = true;
            item.consumable = true;

            item.maxStack = 30;

            item.value = Item.sellPrice(0, 0, 30, 0);
            item.rare = 6;

            item.buffType = BuffType<Buffs.PotionBuffs.DrakoniteSkinBuff>();
            item.buffTime = 28800;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Drakonite Skin Potion");
            Tooltip.SetDefault("Provides immunity to lava and Incineration\nSafely protects against the Volcanic Plateau lava\n50% increased fire resistance during plateau eruptions");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ObsidianSkinPotion, 1);
            recipe.AddIngredient(ItemType<ItemSets.Drakonite.Refined.RefinedDrakonite>(), 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
