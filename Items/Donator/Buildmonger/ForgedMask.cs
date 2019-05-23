using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.Buildmonger
{
    [AutoloadEquip(EquipType.Head)]
    public class ForgedMask : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.sellPrice(0, 0, 60, 0);
            item.rare = 3;

            item.defense = 5;

            item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Forged Mask");
            Tooltip.SetDefault("12% increased melee damage\nMelee attacks set enemies on fire\nThe Buildmonger's donator item");
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage *= 1.12f;
            player.magmaStone = true;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("ForgedBreastplate") && legs.type == mod.ItemType("ForgedGreaves");
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Double tap down to shackle yourself to the ground\nTap down again to fling yourself back to the center";
            player.GetModPlayer<MyPlayer>(mod).forgedArmor = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ForgedIronBar", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
