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
    public class AridFalconHelm : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 3; 
            item.defense = 4;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arid Falcon Helm");
            Tooltip.SetDefault("4% increased ranged damage\n3% increased ranged critical strike chance\n8% not to consume ammo");
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedCrit += 3;
            player.rangedDamage *= 1.04f;
            player.GetModPlayer<MyPlayer>().saveAmmo += 8;
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
        public override bool DrawHead()
        {
            return true;
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
