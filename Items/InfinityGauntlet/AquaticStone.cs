using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.InfinityGauntlet
{
    public class AquaticStone : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;

            item.maxStack = 1;

            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 10;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquatic Stone");
            Tooltip.SetDefault("While in your inventory:\nGrants uninhibited water movement\n50 increased max mana\nYou cannot hold more than one infinity stone");
        }
        public override void UpdateInventory(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            if (!player.HasItem(mod.ItemType("EmptyGauntlet")))
            {
                if (player.HasItem(mod.ItemType("MoonStone")) || player.HasItem(mod.ItemType("PyroStone")) || player.HasItem(mod.ItemType("AridStone")) || player.HasItem(mod.ItemType("FrigidStone")) || player.HasItem(mod.ItemType("DeathStone")))
                {
                    if (modPlayer.overInfinityCharged == 0)
                    {
                        Main.NewText("Your feeble body cannot contain the power of more than one infinity stone!");
                    }
                    modPlayer.overInfinityCharged++;
                }
                else
                {
                    modPlayer.overInfinityCharged = 0;
                }
            }
            else
            {
                modPlayer.overInfinityCharged = 0;
            }

            player.ignoreWater = true;
            player.statManaMax2 += 50;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CInfinityCrys", 1);
            recipe.AddIngredient(null, "WaterEssence", 16);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
