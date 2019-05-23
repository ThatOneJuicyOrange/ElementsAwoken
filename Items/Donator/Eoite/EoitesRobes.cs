using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.Eoite
{
    [AutoloadEquip(EquipType.Body)]
    public class EoitesRobes : ModItem
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
            DisplayName.SetDefault("Eoite's Robes");
            Tooltip.SetDefault("The robes of a weeb streamer\nDamage taken reduced by 18%\n5% increased magic damage");
        }
        public override void UpdateEquip(Player player)
        {
            player.endurance *= 1.18f;
            player.magicDamage *= 1.05f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NeutronFragment", 10);
            recipe.AddIngredient(ItemID.LunarBar, 18);
            recipe.AddIngredient(ItemID.Silk, 24);
            recipe.AddIngredient(ItemID.Amethyst, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
