using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.ToySlime
{
    [AutoloadEquip(EquipType.Body)]
    public class ToyBreastplate : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.buyPrice(0, 0, 10, 0);
            item.rare = 1;

            item.defense = 6;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toy Breastplate");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "BrokenToys", 14);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
