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
    public class AridHeadgear : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 3; 
            item.defense = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arid Headgear");
            Tooltip.SetDefault("4% increased summon damage\n5% increased minion knockback\n+1 maximum minion capacity");
        }

        public override void UpdateEquip(Player player)
        {
            player.minionKB *= 1.05f;
            player.maxMinions += 1;
            player.minionDamage *= 1.04f;
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
        /*public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = true;
        }*/
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
