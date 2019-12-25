using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.Crow
{
    [AutoloadEquip(EquipType.Head)]
    public class CrowsGreathelm : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 11;

            item.defense = 16;

            item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crow's Greathelm");
            Tooltip.SetDefault("The ultimate sorcerer armor\n15% increased magic damage\nReduces mana usage by 20%\nCrow's donator item");
        }

        public override void UpdateEquip(Player player)
        {
            player.magicDamage *= 1.15f;
            player.statManaMax2 += 30;
            player.manaCost -= 0.20f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("CrowsGreatplate") && legs.type == mod.ItemType("CrowsGreatpants");
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawOutlines = true;
            player.armorEffectDrawShadow = true;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "When an enemy is hit, green lightning strikes down from above them\nThe lightning heals the player";
            player.GetModPlayer<MyPlayer>().crowsArmor = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Pyroplasm", 30);
            recipe.AddIngredient(null, "VolcanicStone", 4);
            recipe.AddIngredient(ItemID.NebulaHelmet, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
