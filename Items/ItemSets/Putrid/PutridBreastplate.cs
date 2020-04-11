using ElementsAwoken.Items.BossDrops.Volcanox;
using ElementsAwoken.Items.Materials;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.ItemSets.Putrid
{
    [AutoloadEquip(EquipType.Body)]
    public class PutridBreastplate : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 7;

            item.defense = 18;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rotted Reaper Breastplate");
            Tooltip.SetDefault("10% increased minion damage\nIncreases your max number of minions");
        }
        public override void UpdateEquip(Player player)
        {
            player.minionDamage *= 1.1f;
            player.maxMinions++;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<PutridBar>(), 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
