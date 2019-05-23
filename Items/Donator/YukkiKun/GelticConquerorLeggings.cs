using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.YukkiKun
{
    [AutoloadEquip(EquipType.Legs)]
    public class GelticConquerorLeggings : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 1;

            item.defense = 2;

            item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Geltic Conqueror Leggings");
            Tooltip.SetDefault("Yukki-Kun's donator armor");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Gel, 50);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
