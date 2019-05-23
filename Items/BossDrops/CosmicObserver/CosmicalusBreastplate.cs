using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.CosmicObserver
{
    [AutoloadEquip(EquipType.Body)]
    public class CosmicalusBreastplate : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 4;
            item.defense = 10;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmicalus Breastplate");
            Tooltip.SetDefault("10% increased damage");
        }
        public override void UpdateEquip(Player player)
        {
            player.magicDamage *= 1.1f;
            player.meleeDamage *= 1.1f;
            player.minionDamage *= 1.1f;
            player.rangedDamage *= 1.1f;
            player.thrownDamage *= 1.1f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CosmicShard", 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
