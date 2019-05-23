using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Drakonite.Refined
{
    [AutoloadEquip(EquipType.Legs)]
    public class DragonmailLeggings : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.defense = 16;

            item.value = Item.buyPrice(0, 7, 50, 0);
            item.rare = 7;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragonmail Leggings");
            Tooltip.SetDefault("9% increased movement speed\n12% increased melee speed\nLife regen increased by 1");
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed *= 1.09f;
            player.meleeSpeed *= 1.12f;
            player.lifeRegen += 1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RefinedDrakonite", 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
