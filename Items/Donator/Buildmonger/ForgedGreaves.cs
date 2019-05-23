using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.Buildmonger
{
    [AutoloadEquip(EquipType.Legs)]
    public class ForgedGreaves : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.sellPrice(0, 0, 80, 0);
            item.rare = 3;

            item.defense = 6;

            item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Forged Greaves");
            Tooltip.SetDefault("6 increased armour penetration\nThe Buildmonger's donator item");
        }
        public override void UpdateEquip(Player player)
        {
            player.armorPenetration += 6;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ForgedIronBar", 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
