using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.VoidLeviathan
{
    [AutoloadEquip(EquipType.Head)]
    public class VoidWalkersGreatmask : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.value = Item.sellPrice(0, 25, 0, 0);

            item.defense = 45;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Walker's Greatmask");
            Tooltip.SetDefault("25% increased melee damage\n15% increased melee critical strike chance\n20% increased melee speed\nMelee weapons inflict extinction curse");
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage *= 1.25f;
            player.meleeCrit += 15;
            player.meleeSpeed += 0.20f;
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
            player.GetModPlayer<MyPlayer>().voidWalkerArmor = 1;
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
