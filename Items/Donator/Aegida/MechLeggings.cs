using ElementsAwoken.Items.BossDrops.Volcanox;
using ElementsAwoken.Items.Materials;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Donator.Aegida
{
    [AutoloadEquip(EquipType.Legs)]
    public class MechLeggings : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.sellPrice(0, 0, 80, 0);
            item.rare = 11;

            item.defense = 22;

            item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mech Leggings");
            Tooltip.SetDefault("11% increased ranged damage\n10% increased movement speed\nAn experimental piece of armour made by the scientists for its elite rangers\nAegida's donator item");
        }
        public override void UpdateEquip(Player player)
        {
            player.rangedDamage *= 1.11f;
            player.moveSpeed *= 1.1f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.VortexLeggings);
            recipe.AddIngredient(ItemType<Pyroplasm>(), 20);
            recipe.AddIngredient(ItemType<NeutronFragment>(), 3);
            recipe.AddIngredient(ItemType<VolcanicStone>(), 4);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
