using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.VoidLeviathan
{
    [AutoloadEquip(EquipType.Legs)]
    public class VoidWalkersLeggings : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.value = Item.sellPrice(0, 25, 0, 0);

            item.defense = 18;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Walker's Leggings");
            Tooltip.SetDefault("20% increased movement speed\nEven greater speed when low health");
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed *= 1.1f;
            float speedBoost = MathHelper.Lerp(1.5f, 1f, (float)player.statLife / (float)player.statLifeMax2); // the casts are needed otherwise it returns an int
            player.moveSpeed *= speedBoost;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VoidAshes", 6);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
