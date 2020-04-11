using ElementsAwoken.Items.BossDrops.Volcanox;
using ElementsAwoken.Items.Materials;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.ItemSets.Putrid
{
    [AutoloadEquip(EquipType.Head)]
    public class PutridMask : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 7;

            item.defense = 8;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rotted Reaper Mask");
            Tooltip.SetDefault("30% increased minion knockback\n15% increased minion damage\nIncreases your max number of minions");
        }

        public override void UpdateEquip(Player player)
        {
            player.minionDamage *= 1.15f;
            player.minionKB *= 1.3f;
            player.maxMinions++;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<PutridBreastplate>() && legs.type == ItemType<PutridLeggings>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "You create a toxic aura\nNot moving makes more auras spawn\nIncreases your max number of minions\nMinions have a chance to inflict a fast acting poison";
            player.GetModPlayer<MyPlayer>().putridArmour = true;
            player.maxMinions++;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<PutridBar>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
