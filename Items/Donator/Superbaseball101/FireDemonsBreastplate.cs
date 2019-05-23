using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.Superbaseball101
{
    [AutoloadEquip(EquipType.Body)]
    public class FireDemonsBreastplate : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.buyPrice(0, 3, 0, 0);
            item.rare = 3;

            item.defense = 6;

            item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Demon's Breastplate");
            Tooltip.SetDefault("10% increased minion damage\nSuperbaseball101's donator armor");
        }
        public override void UpdateEquip(Player player)
        {
            player.minionDamage *= 1.1f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FireEssence", 4);
            recipe.AddIngredient(ItemID.HellstoneBar, 16);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
