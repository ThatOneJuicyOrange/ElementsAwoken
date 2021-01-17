using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.Items.Accessories;
using ElementsAwoken.Items.Elements.Desert;
using ElementsAwoken.Items.Elements.Fire;
using ElementsAwoken.Items.Elements.Frost;
using ElementsAwoken.Items.Elements.Void;
using ElementsAwoken.Items.Elements.Sky;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Elements.Water
{
    public class AqueousWaders : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 32;
            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 8;
            item.accessory = true;

            item.GetGlobalItem<EATooltip>().flyingBoots = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquatic Waders");
            Tooltip.SetDefault("Reach speeds of up to 60mph\nGreater mobility on ice\nWater and lava walking\nTemporary immunity to lava\n25% increased wingtime\n25% increased wing speed");
        }
        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i &&
                        (player.armor[i].type == ItemID.HermesBoots ||
                        player.armor[i].type == ItemID.SpectreBoots ||
                        player.armor[i].type == ItemID.LightningBoots ||
                        player.armor[i].type == ItemID.FrostsparkBoots ||
                        player.armor[i].type == ItemType<DesertTrailers>() ||
                        player.armor[i].type == ItemType<FireTreads>() ||
                        player.armor[i].type == ItemType<SkylineWhirlwind>() ||
                        player.armor[i].type == ItemType<FrostWalkers>() ||                      
                        player.armor[i].type == ItemType<VoidBoots>() ||
                        player.armor[i].type == ItemType<NyanBoots>()))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.flyingBoots = true;
            player.accRunSpeed = 11.75f;

            player.iceSkate = true;
            player.waterWalk = true;
            player.noFallDmg = true;
            player.fireWalk = true;

            modPlayer.wingTimeMult *= 1.25f;
            modPlayer.wingAccXMult *= 1.25f;
            modPlayer.wingSpdXMult *= 1.25f;
            modPlayer.wingAccYMult *= 1.25f;
            modPlayer.wingSpdYMult *= 1.25f;

            player.lavaMax += 420;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WaterEssence", 8);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
            recipe.AddIngredient(null, "FrostWalkers", 1);
            recipe.AddIngredient(ItemID.FishronWings);
            recipe.AddIngredient(ItemID.WaterWalkingBoots);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
