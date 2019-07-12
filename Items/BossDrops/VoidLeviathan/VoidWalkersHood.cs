using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.VoidLeviathan
{
    [AutoloadEquip(EquipType.Head)]
    public class VoidWalkersHood : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.value = Item.sellPrice(0, 25, 0, 0);

            item.defense = 15;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Walker's Hood");
            Tooltip.SetDefault("30% increased magic damage\n10% increased magic critical strike chance\n20% decreased mana cost\n100 increased mana\nMagic weapons inflict extinction curse");
        }

        public override void UpdateEquip(Player player)
        {
            player.magicDamage *= 1.3f;
            player.magicCrit += 10;
            player.manaCost *= 0.8f;
            player.statManaMax2 += 100;
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
            player.GetModPlayer<MyPlayer>(mod).voidWalkerArmor = 3;
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
