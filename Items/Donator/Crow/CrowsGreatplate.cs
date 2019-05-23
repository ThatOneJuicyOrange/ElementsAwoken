using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.Crow
{
    // the cape doesnt draw if its in the armour slot
    [AutoloadEquip(EquipType.Body, EquipType.Front, EquipType.Back)]
    public class CrowsGreatplate : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 11;

            item.defense = 20;

            item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crow's Greatplate");
            Tooltip.SetDefault("The ultimate sorcerer armor\nDamage taken reduced by 5%\n15% increased magic critical strike chance\nMana increased by 30\nCrow's donator item");
        }
        public override void UpdateEquip(Player player)
        {
            player.endurance *= 1.05f;
            player.magicCrit += 15;
            player.statManaMax2 += 30;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Pyroplasm", 75);
            recipe.AddIngredient(null, "VolcanicStone", 12);
            recipe.AddIngredient(ItemID.NebulaBreastplate, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
