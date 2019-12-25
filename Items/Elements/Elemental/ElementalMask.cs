using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Elemental
{
    [AutoloadEquip(EquipType.Head)]
    public class ElementalMask : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.value = Item.sellPrice(0, 20, 0, 0);

            item.defense = 26;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elemental Mask");
            Tooltip.SetDefault("35% increased critical strike chance\n+5 max minions");
        }

        public override void UpdateEquip(Player player)
        {
            player.magicCrit += 35;
            player.meleeCrit += 35;
            player.rangedCrit += 35;
            player.thrownCrit += 35;
            player.maxMinions += 5;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("ElementalBreastplate") && legs.type == mod.ItemType("ElementalLeggings");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "If you are killed, you are revived back to half health\n45 second cooldown";
            player.GetModPlayer<MyPlayer>().elementalArmor = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ElementalEssence", 5);
            recipe.AddIngredient(null, "VoiditeBar", 6);
            recipe.AddIngredient(ItemID.LunarBar, 16);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
