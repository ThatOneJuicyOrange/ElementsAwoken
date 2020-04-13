using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.ItemSets.ToySlime
{
    [AutoloadEquip(EquipType.Body)]
    public class ToyBreastplate : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.sellPrice(0, 0, 75, 0);
            item.rare = 3;

            item.defense = 7;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toy Breastplate");
            Tooltip.SetDefault("Reduces damage taken by 4%");
        }
        public override void UpdateEquip(Player player)
        {
            player.endurance += 0.04f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<BrokenToys>(), 14);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
