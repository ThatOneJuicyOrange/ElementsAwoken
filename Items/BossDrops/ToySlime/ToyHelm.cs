using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.ToySlime
{
    [AutoloadEquip(EquipType.Head)]
    public class ToyHelm : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.buyPrice(0, 0, 10, 0);
            item.rare = 1;

            item.defense = 3;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toy Helm");
            Tooltip.SetDefault("2% increased critical strike chance");
        }

        public override void UpdateEquip(Player player)
        {
            player.magicCrit += 2;
            player.meleeCrit += 2;
            player.rangedCrit += 2;
            player.thrownCrit += 2;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("ToyBreastplate") && legs.type == mod.ItemType("ToyLeggings");
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "You drop a lego brick when hit";
            player.GetModPlayer<MyPlayer>(mod).toyArmor = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "BrokenToys", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
