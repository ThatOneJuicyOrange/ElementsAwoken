using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.Superbaseball101
{
    [AutoloadEquip(EquipType.Head)]
    public class FireDemonsHelm : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.buyPrice(0, 3, 0, 0);
            item.rare = 3;

            item.defense = 5;

            item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Demon's Helm");
            Tooltip.SetDefault("5% increased minion damage and knockback\nSuperbaseball101's donator armor");
        }

        public override void UpdateEquip(Player player)
        {
            player.minionDamage *= 1.05f;
            player.minionKB *= 1.05f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("FireDemonsBreastplate") && legs.type == mod.ItemType("FireDemonsLeggings");
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawOutlines = true;
            player.armorEffectDrawShadow = true;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Summons a demon to destroy your foes";
            player.GetModPlayer<MyPlayer>().superbaseballDemon = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FireEssence", 2);
            recipe.AddIngredient(ItemID.HellstoneBar, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
