using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.InfinityGauntlet
{
    public class FrigidStone : ModItem
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
            DisplayName.SetDefault("Frigid Stone");
            Tooltip.SetDefault("While in your inventory:\nNearby enemies are frostburned\n5 increased critical strike chance\nYou cannot hold more than one infinity stone");
        }
        public override void UpdateInventory(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            if (!player.HasItem(mod.ItemType("EmptyGauntlet")))
            {
                if (player.HasItem(mod.ItemType("MoonStone")) || player.HasItem(mod.ItemType("PyroStone")) || player.HasItem(mod.ItemType("AridStone")) || player.HasItem(mod.ItemType("AquaticStone")) || player.HasItem(mod.ItemType("DeathStone")))
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

            player.magicCrit += 5;
            player.meleeCrit += 5;
            player.rangedCrit += 5;
            player.thrownCrit += 5;

            if (Main.rand.Next(60) == 0)
            {
                for (int l = 0; l < Main.npc.Length; l++)
                {
                    NPC nPC = Main.npc[l];
                    if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(player.Center, nPC.Center) <= 600)
                    {
                        nPC.AddBuff(BuffID.Frostburn, 180, false);
                        return;
                    }
                }
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CInfinityCrys", 1);
            recipe.AddIngredient(null, "FrostEssence", 16);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
