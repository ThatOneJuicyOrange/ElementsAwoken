using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Drakonite.Refined
{
    [AutoloadEquip(EquipType.Head)]
    public class DragonmailGreathelm : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.defense = 17;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 7;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragonmail Greathelm");
            Tooltip.SetDefault("8% increased melee damage\n10% increased melee critical strike chance\nDamage taken reduced by 6%");
        }

        public override void UpdateEquip(Player player)
        {
            player.endurance += 0.06f;
            player.meleeCrit += 10;
            player.meleeDamage *= 1.08f;
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
            player.setBonus = "Melee attacks and projectiles inflict Dragonfire\nMaximum life increased by 50";
            player.statLifeMax2 += 50;
            player.GetModPlayer<MyPlayer>().dragonmailGreathelm = true;
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
