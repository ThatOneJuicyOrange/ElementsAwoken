using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.InfinityGauntlet
{
    public class DeathStone : ModItem
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
            DisplayName.SetDefault("Death Stone");
            Tooltip.SetDefault("While in your inventory:\n50 increased maximum health\nOccasionally kills minor enemies\nYou cannot hold more than one infinity stone");
        }
        public override void UpdateInventory(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            if (!player.HasItem(mod.ItemType("EmptyGauntlet")))
            {
                if (player.HasItem(mod.ItemType("MoonStone")) || player.HasItem(mod.ItemType("PyroStone")) || player.HasItem(mod.ItemType("AridStone")) || player.HasItem(mod.ItemType("FrigidStone")) || player.HasItem(mod.ItemType("AquaticStone")))
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

            player.statLifeMax2 += 50;

            if (Main.rand.Next(1200) == 0)
            {
                for (int l = 0; l < Main.npc.Length; l++)
                {
                    NPC nPC = Main.npc[l];
                    bool immune = false;
                    foreach (int i in ElementsAwoken.instakillImmune)
                    {
                        if (nPC.type == i)
                        {
                            immune = true;
                        }
                    }
                    if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(player.Center, nPC.Center) <= 600 && nPC.lifeMax < 30000 && !immune)
                    {
                        nPC.StrikeNPCNoInteraction(nPC.lifeMax, 0f, -nPC.direction, true);
                        for (int d = 0; d < 100; d++)
                        {
                            int dust = Dust.NewDust(nPC.position, nPC.width, nPC.height, 219);
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].scale = 1f;
                            Main.dust[dust].velocity *= 2f;
                        }
                        return; // to only kill 1 
                    }
                }
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CInfinityCrys", 1);
            recipe.AddIngredient(null, "VoidEssence", 16);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
