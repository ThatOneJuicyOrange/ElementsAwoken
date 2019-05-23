using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Water
{
    [AutoloadEquip(EquipType.Legs)]
    public class OceanicLeggings : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(0, 75, 0, 0);
            item.rare = 8;
            item.defense = 12;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Oceanic Leggings");
            Tooltip.SetDefault("10% increased movement speed\nGrants defence boosts as health lowers");
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed *= 1.1f;
            if (player.statLife <= (player.statLifeMax2 * 0.75f))
            {
                player.statDefense += 4;
                if (player.statLife <= (player.statLifeMax2 * 0.5f))
                {
                    player.statDefense += 4;
                    if (player.statLife <= (player.statLifeMax2 * 0.25f))
                    {
                        player.statDefense += 4;
                    }
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WaterEssence", 9);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 14);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
