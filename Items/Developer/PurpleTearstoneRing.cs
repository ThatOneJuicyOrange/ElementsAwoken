using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.DataStructures;

namespace ElementsAwoken.Items.Developer
{
    public class PurpleTearstoneRing : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.rare = 4;
            item.value = Item.buyPrice(0, 10, 0, 0);
            item.accessory = true;

            item.GetGlobalItem<EATooltip>().developer = true;
            item.GetGlobalItem<EARarity>().rare = 12;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Purple Tearstone Ring");
            Tooltip.SetDefault("When under 15% life:\nDamage is increased by 20%\nDefence is increased by 50%\nAmadisLFE's developer accessory");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.statLife < player.statLifeMax2 * 0.15f)
            {
                player.thrownDamage *= 1.2f;
                player.meleeDamage *= 1.2f;
                player.magicDamage *= 1.2f;
                player.rangedDamage *= 1.2f;
                player.minionDamage *= 1.2f;

                player.statDefense = (int)(player.statDefense * 1.5f);
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Pyroplasm", 50);
            recipe.AddIngredient(null, "NeutronFragment", 8);
            recipe.AddIngredient(null, "VoiditeBar", 8);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 8);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
