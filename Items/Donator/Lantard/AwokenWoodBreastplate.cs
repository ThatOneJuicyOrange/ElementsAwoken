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
    [AutoloadEquip(EquipType.Body)]
    public class AwokenWoodBreastplate : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 11;

            item.defense = 22;

            item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Awoken Wooden Breastplate");
            Tooltip.SetDefault("12% increased damage\nIncreases your max number of minions by 2\nSomething so simple has become so powerful... As have you\nLantard's donator item");
        }
        public override void UpdateEquip(Player player)
        {
            player.allDamage *= 1.12f;
            player.maxMinions += 2;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WoodBreastplate);
            recipe.AddIngredient(ItemID.LunarBar, 16);
            recipe.AddIngredient(ItemType<NeutronFragment>(), 16);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
