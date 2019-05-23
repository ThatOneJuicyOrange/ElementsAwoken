using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.InfinityGauntlet
{
    public class AridStone : ModItem
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
            DisplayName.SetDefault("Arid Stone");
            Tooltip.SetDefault("While in your inventory:\nImmunity to strong winds\n10% increased movement speed\nYou cannot hold more than one infinity stone");
        }
        public override void UpdateInventory(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);

            if (!player.HasItem(mod.ItemType("EmptyGauntlet")))
            {
                if (player.HasItem(mod.ItemType("MoonStone")) || player.HasItem(mod.ItemType("PyroStone")) || player.HasItem(mod.ItemType("FrigidStone")) || player.HasItem(mod.ItemType("AquaticStone")) || player.HasItem(mod.ItemType("DeathStone")))
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

            player.buffImmune[BuffID.WindPushed] = true;
            player.moveSpeed *= 1.1f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CInfinityCrys", 1);
            recipe.AddIngredient(null, "DesertEssence", 16);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
