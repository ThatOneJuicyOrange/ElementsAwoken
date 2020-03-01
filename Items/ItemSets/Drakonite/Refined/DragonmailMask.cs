using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Drakonite.Refined
{
    [AutoloadEquip(EquipType.Head)]
    public class DragonmailMask : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.defense = 7;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 7;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragonmail Mask");
            Tooltip.SetDefault("10% increased summon damage\nIncreased minion knockback\n+2 max minions");
        }

        public override void UpdateEquip(Player player)
        {
            player.minionKB += 2.5f;
            player.minionDamage *= 1.10f;
            player.maxMinions += 2;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("DragonmailChestpiece") && legs.type == mod.ItemType("DragonmailLeggings");
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadowSubtle = true;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Minions inflict Dragonfire\nDamage reduced with more minions";
            player.GetModPlayer<MyPlayer>().dragonmailMask = true;
            float minions = 0f;
            for (int i = 0; i < 1000; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == player.whoAmI && Main.projectile[i].minion)
                {
                    minions += Main.projectile[i].minionSlots; // adding how many slots the minion takes
                }
            }
            if (minions >= 1 && minions < 4)
            {
                player.endurance += 0.02f;
            }
            if (minions >= 4 && minions < 7)
            {
                player.endurance += 0.06f;
            }
            if (minions >= 7 && minions < 10)
            {
                player.endurance += 0.08f;
            }
            if (minions >= 10 && minions < 13)
            {
                player.endurance += 0.12f;
            }
            if (minions >= 13 && minions < 16)
            {
                player.endurance += 0.16f;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RefinedDrakonite", 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
