using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Sky
{
    [AutoloadEquip(EquipType.Head)]
    public class EmpyreanHeadgear : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 6;
            item.defense = 2;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Empyrean Headgear");
            Tooltip.SetDefault("10% increased minion damage\nIncreases your maximum number of minions by 1\n10% increased minion knockback");
        }

        public override void UpdateEquip(Player player)
        {
            player.maxMinions += 1;
            player.minionKB *= 1.1f;
            player.minionDamage *= 1.1f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("EmpyreanPlate") && legs.type == mod.ItemType("EmpyreanLeggings");
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawOutlines = true;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Storms brew around you";
            player.GetModPlayer<MyPlayer>().empyreanCloudCD--;
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
