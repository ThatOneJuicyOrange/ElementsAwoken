using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.Eoite
{
    [AutoloadEquip(EquipType.Head)]
    public class EoitesHood : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 10;
            item.defense = 10;

            item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eoite's Hood");
            Tooltip.SetDefault("The hood of a weeb streamer\n10% increased magic damage");
        }

        public override void UpdateEquip(Player player)
        {
            player.magicDamage *= 1.1f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("EoitesRobes") && legs.type == mod.ItemType("EoitesLeggings");
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawOutlines = true;
            player.armorEffectDrawShadow = true;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "15% increased magic damage\nDamage reduced by a further 10%";
            player.endurance *= 1.1f;
            player.magicDamage *= 1.15f;
            //player.GetModPlayer<MyPlayer>(mod).voidArmor = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NeutronFragment", 8);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(ItemID.Silk, 16);
            recipe.AddIngredient(ItemID.Amethyst, 4);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
