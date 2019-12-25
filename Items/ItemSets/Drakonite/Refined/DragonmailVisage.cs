using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Drakonite.Refined
{
    [AutoloadEquip(EquipType.Head)]
    public class DragonmailVisage : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.defense = 12;

            item.value = Item.buyPrice(0, 7, 50, 0);
            item.rare = 7;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragonmail Visage");
            Tooltip.SetDefault("20% chance to not consume ammo\n9% increased ranged damage\n10% increased ranged critical strike chance");
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedCrit += 10;
            player.rangedDamage *= 1.09f;
            player.ammoCost80 = true;
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
            player.setBonus = "Ranged projectiles inflict Dragonfire\nRanged attacks have a chance to explode into Dragonfire";
            player.GetModPlayer<MyPlayer>().dragonmailVisage = true;
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
