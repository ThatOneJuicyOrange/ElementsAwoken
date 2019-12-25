using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.VoidLeviathan
{
    [AutoloadEquip(EquipType.Body)]
    public class VoidWalkersBreastplate : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.value = Item.sellPrice(0, 25, 0, 0);

            item.defense = 20;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Walker's Breastplate");
            Tooltip.SetDefault("Life regenerates rapidly after taking more than half the players life of damage\nDamage taken reduced by 8%");
        }
        public override void UpdateEquip(Player player)
        {
            player.endurance += 0.08f;
            player.GetModPlayer<MyPlayer>().voidWalkerChest = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VoiditeBar", 8);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
