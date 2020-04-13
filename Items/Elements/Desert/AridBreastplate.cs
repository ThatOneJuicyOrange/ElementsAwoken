using ElementsAwoken.Items.Essence;
using ElementsAwoken.Tiles.Crafting;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Elements.Desert
{
    [AutoloadEquip(EquipType.Body)]
    public class AridBreastplate : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 3;
            item.defense = 7;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arid Breastplate");
            Tooltip.SetDefault("3% increased critical strike chance\nIncreases invincibility time after hit");
        }
        public override void UpdateEquip(Player player)
        {
            player.magicCrit += 3;
            player.meleeCrit += 3;
            player.rangedCrit += 3;
            player.thrownCrit += 3;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<DesertEssence>(), 8);
            recipe.AddRecipeGroup("ElementsAwoken:SandGroup", 25);
            recipe.AddRecipeGroup("ElementsAwoken:SandstoneGroup", 10);
            recipe.AddTile(TileType<ElementalForge>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
