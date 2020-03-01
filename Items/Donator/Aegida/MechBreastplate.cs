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
    [AutoloadEquip(EquipType.Body)]
    public class MechBreastplate : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 11;

            item.defense = 32;

            item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mech Breastplate");
            Tooltip.SetDefault("16% increased ranged damage\n16% increased ranged critical strike chance\n33% increased chance to not consume ammo\nAn experimental breastplate made by the scientists for its elite rangers\nAegida's donator item");
        }
        public override void UpdateEquip(Player player)
        {
            player.rangedDamage *= 1.16f;
            player.rangedCrit += 16;
            player.GetModPlayer<MyPlayer>().saveAmmo += 33;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.VortexBreastplate);
            recipe.AddIngredient(ItemType<Pyroplasm>(), 20);
            recipe.AddIngredient(ItemType<NeutronFragment>(), 3);
            recipe.AddIngredient(ItemType<VolcanicStone>(), 6);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
