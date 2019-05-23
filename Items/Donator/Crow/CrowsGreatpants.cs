using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.Crow
{
    [AutoloadEquip(EquipType.Legs)]
    public class CrowsGreatpants : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 11;

            item.defense = 18;

            item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crow's Greatpants");
            Tooltip.SetDefault("The ultimate sorcerer armor\n15% increased magic damage\n15% increased movement speed\nCrow's donator item");
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed *= 1.15f;
            player.magicDamage *= 1.15f;
            player.statManaMax2 += 30;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Pyroplasm", 50);
            recipe.AddIngredient(null, "VolcanicStone", 8);
            recipe.AddIngredient(ItemID.NebulaLeggings, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
