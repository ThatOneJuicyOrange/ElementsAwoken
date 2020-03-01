using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Water
{
    [AutoloadEquip(EquipType.Shield)]
    public class WaterShield : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.value = Item.buyPrice(0, 75, 0, 0);
            item.rare = 8;
            item.accessory = true;
            item.defense = 4;
            item.value = Item.buyPrice(0, 75, 0, 0);

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Great Barrier Reef");
            Tooltip.SetDefault("The creatures of the ocean accept you\nTurns you into a merfolk when in the water\nOcean monsters won't attack you\nGrants immunity to knockback");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.noKnockback = true;
            player.npcTypeNoAggro[65] = true;
            player.npcTypeNoAggro[220] = true;
            player.npcTypeNoAggro[64] = true;
            player.npcTypeNoAggro[67] = true;
            player.npcTypeNoAggro[221] = true;
            player.accMerman = true;
            if (hideVisual)
            {
                player.hideMerman = true;
            }
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WaterEssence", 8);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
