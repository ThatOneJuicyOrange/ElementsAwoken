using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Consumable.StatIncreases
{
    public class LunarStar : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.maxStack = 999;

            item.consumable = true;
        
            item.useStyle = 4;
            item.useTime = 30;
            item.useAnimation = 30;
            item.UseSound = SoundID.Item4;

            item.rare = 10;
            item.value = Item.sellPrice(0, 2, 0, 0);
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lunar Star");
            Tooltip.SetDefault("Condense your mana with raw lunar power\nFighters of The Calamity need not apply\nIncreases mana by 100");
        }
        public override bool CanUseItem(Player player)
        {
            bool calamityEnabled = ModLoader.GetMod("CalamityMod") != null;
            return !calamityEnabled && player.statManaMax == 200 && player.GetModPlayer<MyPlayer>().lunarStarsUsed < 1;
        }

        public override bool UseItem(Player player)
        {
            player.statManaMax2 += 100;
            player.statMana += 100;
            if (Main.myPlayer == player.whoAmI)
            {
                player.ManaEffect(100);
            }
            player.GetModPlayer<MyPlayer>().lunarStarsUsed += 1;
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Stardust", 25);
            recipe.AddIngredient(ItemID.ManaCrystal, 5);
            recipe.AddIngredient(ItemID.LunarBar, 40);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            if (ModLoader.GetMod("CalamityMod") == null) recipe.AddRecipe();
        }
    }
}
