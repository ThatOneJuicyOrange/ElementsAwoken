using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.Buildmonger
{
    [AutoloadEquip(EquipType.Body)]
    public class ForgedBreastplate : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 3;

            item.defense = 7;

            item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Forged Breastplate");
            Tooltip.SetDefault("10% increased melee and movement speed\nThe Buildmonger's donator item");
        }
        public override void UpdateEquip(Player player)
        {
            player.meleeSpeed *= 1.1f;
            player.moveSpeed *= 1.1f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ForgedIronBar", 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
