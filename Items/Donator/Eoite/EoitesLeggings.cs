using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.Eoite
{
    [AutoloadEquip(EquipType.Legs)]
    public class EoitesLeggings : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 10;

            item.defense = 20;

            item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eoite's Leggings");
            Tooltip.SetDefault("The leggings of a weeb streamer\n5% increased magic damage\nEoite's donator item");
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed *= 1.1f;
            player.magicDamage *= 1.05f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NeutronFragment", 6);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(ItemID.Silk, 18);
            recipe.AddIngredient(ItemID.Amethyst, 8);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
