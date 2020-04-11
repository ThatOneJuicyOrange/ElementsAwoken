using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.Items.Accessories;
using ElementsAwoken.Items.Elements.Desert;
using ElementsAwoken.Items.Elements.Sky;
using ElementsAwoken.Items.Elements.Frost;
using ElementsAwoken.Items.Elements.Void;
using ElementsAwoken.Items.Elements.Water;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Elements.Fire
{
    public class FireTreads : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 32;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 4;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Treads");
            Tooltip.SetDefault("Cool speed!\nGrants immunity to fire blocks\nTemporary immunity to lava");
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
                        player.armor[i].type == ItemType<SkylineWhirlwind>() ||
                        player.armor[i].type == ItemType<FrostWalkers>() ||
                        player.armor[i].type == ItemType<AqueousWaders>() ||
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
            player.accRunSpeed = 8.8f;
            player.rocketBoots = 1;

            player.fireWalk = true;

            player.lavaMax += 420;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FireEssence", 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddIngredient(ItemID.LavaCharm);
            recipe.AddIngredient(null, "DesertTrailers", 1);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
