using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.VoidLeviathan
{
    [AutoloadEquip(EquipType.Head)]
    public class VoidWalkersVisage : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.value = Item.sellPrice(0, 25, 0, 0);

            item.defense = 30;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Walker's Visage");
            Tooltip.SetDefault("35% increased ranged damage\n30% increased ranged critical strike chance\n25% chance not to consume ammo\nRanged weapons inflict extinction curse");
        }
        public override bool DrawHead()
        {
            return false;
        }
        public override void UpdateEquip(Player player)
        {
            player.rangedDamage *= 1.35f;
            player.rangedCrit += 30;
            player.ammoCost75 = true;
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
            player.setBonus = "Press the armour ability key to activate psychosis aura\nThe psychosis aura confuses enemies and inflicts extinction curse";
            player.GetModPlayer<MyPlayer>(mod).voidWalkerArmor = 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VoidAshes", 4);
            recipe.AddIngredient(ItemID.LunarBar, 6);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
