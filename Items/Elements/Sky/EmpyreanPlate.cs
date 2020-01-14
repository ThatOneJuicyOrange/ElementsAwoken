using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Sky
{
    [AutoloadEquip(EquipType.Body)]
    public class EmpyreanPlate : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 6;
            item.defense = 17;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Empyrean Breastplate");
            Tooltip.SetDefault("10% increased critical strike chance\nFlight time increased by 30");
        }
        public override void UpdateEquip(Player player)
        {
            player.magicCrit += 10;
            player.meleeCrit += 10;
            player.rangedCrit += 10;
            player.thrownCrit += 10;
            player.wingTimeMax += 30;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SkyEssence", 8);
            recipe.AddIngredient(ItemID.Cloud, 25);
            recipe.AddIngredient(ItemID.HallowedBar, 24);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
