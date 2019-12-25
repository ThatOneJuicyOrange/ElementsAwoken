using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.InfinityGauntlet
{
    public class MoonStone : ModItem
    {
        public int pushTimer = 0;
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
            DisplayName.SetDefault("Moon Stone");
            Tooltip.SetDefault("While in your inventory:\nIncresed flight speed and time\nEvery 5 seconds, minor enemies are pushed away\nYou cannot hold more than one infinity stone");
        }
        public override void UpdateInventory(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            if (!player.HasItem(mod.ItemType("EmptyGauntlet")))
            {
                if (player.HasItem(mod.ItemType("AridStone")) || player.HasItem(mod.ItemType("PyroStone")) || player.HasItem(mod.ItemType("FrigidStone")) || player.HasItem(mod.ItemType("AquaticStone")) || player.HasItem(mod.ItemType("DeathStone")))
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

            player.wingTimeMax = (int)(player.wingTimeMax * 1.2f);

            pushTimer--;
            if (pushTimer <= 0)
            {
                for (int l = 0; l < Main.npc.Length; l++)
                {
                    NPC nPC = Main.npc[l];
                    if (!nPC.friendly && nPC.active && nPC.damage > 0 && !nPC.boss && Vector2.Distance(nPC.Center, player.Center) < 300)
                    {
                        Vector2 toTarget = new Vector2(player.Center.X - nPC.Center.X, player.Center.Y - nPC.Center.Y);
                        toTarget.Normalize();
                        nPC.velocity -= toTarget * 8f;
                        if (!nPC.noGravity)
                        {
                            nPC.velocity.Y -= 7.5f;
                        }
                    }
                }
                pushTimer = 300;
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CInfinityCrys", 1);
            recipe.AddIngredient(null, "SkyEssence", 16);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
