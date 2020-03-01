using ElementsAwoken.Items.BossDrops.Volcanox;
using ElementsAwoken.Items.Materials;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Donator.Lantard
{
    [AutoloadEquip(EquipType.Legs)]
    public class AwokenWoodGreaves : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 11;

            item.defense = 12;
            item.defense = 12;
            item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Awoken Wooden Greaves");
            Tooltip.SetDefault("6% increased damage\n10% increased movement speed\nSomething so simple has become so powerful... As have you\nLantards's donator item");
        }
        public override void UpdateEquip(Player player)
        {
            player.allDamage *= 1.06f;
            player.moveSpeed *= 1.1f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WoodGreaves);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(ItemType<NeutronFragment>(), 12);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
