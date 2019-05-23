using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.Superbaseball101
{
    [AutoloadEquip(EquipType.Legs)]
    public class FireDemonsLeggings : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.buyPrice(0, 3, 0, 0);
            item.rare = 3;

            item.defense = 4;

            item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Demon's Leggings");
            Tooltip.SetDefault("Max minions increased by 1\nSuperbaseball101's donator armor");
        }
        public override void UpdateEquip(Player player)
        {
            player.maxMinions += 1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FireEssence", 3);
            recipe.AddIngredient(ItemID.HellstoneBar, 14);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
