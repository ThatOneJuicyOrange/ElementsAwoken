using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.ItemSets.ToySlime
{
    [AutoloadEquip(EquipType.Legs)]
    public class ToyLeggings : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.sellPrice(0, 0, 10, 0);
            item.rare = 3;

            item.defense = 7;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toy Leggings");
            Tooltip.SetDefault("5% increased movement speed\n2% increased damage\nReduces damage taken by 2%");
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed *= 1.05f;
            player.allDamage *= 1.02f;
            player.endurance += 0.02f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<BrokenToys>(), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
