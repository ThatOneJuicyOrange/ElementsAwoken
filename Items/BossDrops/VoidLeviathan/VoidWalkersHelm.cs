using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.VoidLeviathan
{
    [AutoloadEquip(EquipType.Head)]
    public class VoidWalkersHelm : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.value = Item.sellPrice(0, 25, 0, 0);

            item.defense = 9;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Walker's Helm");
            Tooltip.SetDefault("40% increased minion damage\nMaximum minions increased by 6\nMassively increased minion knockback\nMinions inflict extinction curse");
        }

        public override void UpdateEquip(Player player)
        {
            player.minionDamage *= 1.4f;
            player.maxMinions += 6;
            player.minionKB += 8f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("VoidWalkersBreastplate") && legs.type == mod.ItemType("VoidWalkersLeggings");
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadow = true;
            player.armorEffectDrawOutlines = true;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Double tap down to activate psychosis aura\nThe psychosis aura confuses enemies and inflicts extinction curse";
            player.GetModPlayer<MyPlayer>().voidWalkerArmor = 4;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VoiditeBar", 4);
            recipe.AddIngredient(ItemID.LunarBar, 6);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
