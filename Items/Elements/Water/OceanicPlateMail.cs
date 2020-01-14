using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Water
{
    [AutoloadEquip(EquipType.Body)]
    public class OceanicPlateMail : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(0, 75, 0, 0);
            item.rare = 8;
            item.defense = 19;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Oceanic Plate Mail");
            Tooltip.SetDefault("6% increased damage\nFlight time increased by 25");
        }
        public override void UpdateEquip(Player player)
        {
            player.thrownDamage *= 1.06f;
            player.meleeDamage *= 1.06f;
            player.magicDamage *= 1.06f;
            player.rangedDamage *= 1.06f;
            player.minionDamage *= 1.06f;
            player.wingTimeMax += 25;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WaterEssence", 10);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 22);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
