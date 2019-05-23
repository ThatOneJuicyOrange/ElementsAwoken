using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Drakonite.Refined
{
    [AutoloadEquip(EquipType.Body)]
    public class DragonmailChestpiece : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.defense = 22;

            item.value = Item.buyPrice(0, 7, 50, 0);
            item.rare = 7;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragonmail Chestpiece");
            Tooltip.SetDefault("5% increased damage\nImmunity to On Fire\nYour body glows\nMining speed increased by 5%");
        }
        public override void UpdateEquip(Player player)
        {
            player.thrownDamage *= 1.03f;
            player.meleeDamage *= 1.03f;
            player.magicDamage *= 1.03f;
            player.rangedDamage *= 1.03f;
            player.minionDamage *= 1.03f;

            player.pickSpeed *= 0.95f;

            player.buffImmune[BuffID.OnFire] = true;

            Lighting.AddLight(player.Center, 1f, 0.2f, 0.2f);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RefinedDrakonite", 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
