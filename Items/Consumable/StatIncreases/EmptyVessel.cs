using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Consumable.StatIncreases
{
    public class EmptyVessel : ModItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 10;
            item.consumable = true;
            item.width = 18;
            item.height = 18;
            item.useStyle = 4;
            item.useTime = 30;
            item.UseSound = SoundID.Item4;
            item.useAnimation = 30;
            item.rare = 11;
            item.value = Item.sellPrice(0, 2, 0, 0);
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Empty Vessel");
            Tooltip.SetDefault("Your body is at its limits trying to contain your life force, adding a bit of extra space in your soul might fix this\nFighters of The Calamity need not apply\nPermanently increases maximum life by 10");
        }

        public override bool CanUseItem(Player player)
        {
            bool calamityEnabled = ModLoader.GetMod("CalamityMod") != null;
            return !calamityEnabled && player.statLifeMax == 500 && player.GetModPlayer<MyPlayer>().voidHeartsUsed < 10;
        }

        public override bool UseItem(Player player)
        {
            player.statLifeMax2 += 10;
            player.statLife += 10;
            if (Main.myPlayer == player.whoAmI)
            {
                player.HealEffect(10, true);
            }
            player.GetModPlayer<MyPlayer>().voidHeartsUsed += 1;
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VoidAshes", 12);
            recipe.AddIngredient(null, "VoidStone", 25);
            recipe.AddIngredient(null, "VoidEssence", 15);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            if (ModLoader.GetMod("CalamityMod") == null) recipe.AddRecipe();
        }
    }
}
