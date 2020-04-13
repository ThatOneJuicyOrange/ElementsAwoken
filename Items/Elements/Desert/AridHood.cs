using ElementsAwoken.Items.Essence;
using ElementsAwoken.Tiles.Crafting;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Elements.Desert
{
    [AutoloadEquip(EquipType.Head)]
    public class AridHood : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 3; 
            item.defense = 3;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arid Hood");
            Tooltip.SetDefault("4% increased throwing damage\n3% increased throwing critical strike chance\n15% increased throwing velocity");
        }

        public override void UpdateEquip(Player player)
        {
            player.thrownCrit += 3;
            player.thrownDamage *= 1.04f;
            player.thrownVelocity *= 1.15f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<AridBreastplate>() && legs.type == ItemType<AridLeggings>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Allows weak levitation by holding jump";
            player.GetModPlayer<MyPlayer>().arid = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<DesertEssence>(), 6);
            recipe.AddRecipeGroup("ElementsAwoken:SandGroup", 15);
            recipe.AddRecipeGroup("ElementsAwoken:SandstoneGroup", 5);
            recipe.AddTile(TileType<ElementalForge>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
