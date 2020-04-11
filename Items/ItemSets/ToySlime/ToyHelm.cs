using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.ItemSets.ToySlime
{
    [AutoloadEquip(EquipType.Head)]
    public class ToyHelm : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.sellPrice(0, 0, 10, 0);
            item.rare = 3;

            item.defense = 6;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toy Helm");
            Tooltip.SetDefault("4% increased critical strike chance\nReduces damage taken by 1%");
        }

        public override void UpdateEquip(Player player)
        {
            player.magicCrit += 4;
            player.meleeCrit += 4;
            player.rangedCrit += 4;
            player.thrownCrit += 4;
            player.endurance += 0.01f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<ToyBreastplate>() && legs.type == ItemType<ToyLeggings>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "You drop lego bricks when hit";
            player.GetModPlayer<MyPlayer>().toyArmor = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<BrokenToys>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
