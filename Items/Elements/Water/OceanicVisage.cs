using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Water
{
    [AutoloadEquip(EquipType.Head)]
    public class OceanicVisage : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(0, 75, 0, 0);
            item.rare = 8;
            item.defense = 24;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Oceanic Visage");
            Tooltip.SetDefault("Can move quickly through liquids\n+1 max minions\n10% increased critical strike chance");
        }

        public override void UpdateEquip(Player player)
        {
            player.magicCrit += 10;
            player.meleeCrit += 10;
            player.rangedCrit += 10;
            player.thrownCrit += 10;
            player.ignoreWater = true;
            player.maxMinions += 1;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("OceanicPlateMail") && legs.type == mod.ItemType("OceanicLeggings");
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawOutlines = true;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Taking more then 25 damage causes toxic water to spray";
            player.GetModPlayer<MyPlayer>().oceanicArmor = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WaterEssence", 8);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
