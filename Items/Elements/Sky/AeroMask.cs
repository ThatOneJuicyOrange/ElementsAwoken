using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Sky
{
    [AutoloadEquip(EquipType.Head)]
    public class AeroMask : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(0, 25, 0, 0);
            item.rare = 6;
            item.defense = 27;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aero Mask");
            Tooltip.SetDefault("10% increased ranged damage\n7% increased critical strike chance");
        }

        public override void UpdateEquip(Player player)
        {
            player.magicCrit += 7;
            player.meleeCrit += 7;
            player.rangedCrit += 7;
            player.thrownCrit += 7;
            player.rangedDamage *= 1.1f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("AeroPlate") && legs.type == mod.ItemType("AeroLeggings");
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawOutlines = true;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Storms brew around you";
            player.GetModPlayer<MyPlayer>().aeroArmor = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SkyEssence", 6);
            recipe.AddIngredient(ItemID.Cloud, 25);
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
