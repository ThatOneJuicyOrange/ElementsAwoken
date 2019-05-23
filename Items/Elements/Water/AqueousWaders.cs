using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Water
{
    [AutoloadEquip(EquipType.Wings)]
    public class AqueousWaders : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 32;
            item.value = Item.buyPrice(0, 75, 0, 0);
            item.rare = 8;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquatic Waders");
            Tooltip.SetDefault("Ridiculous speed!\nGreater mobility on ice\nWater and lava walking\nTemporary immunity to lava\nAllows flight and slow fall");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.accRunSpeed = 18f;
            player.rocketBoots = 3;
            player.moveSpeed += 15f;
            player.iceSkate = true;
            player.waterWalk = true;
            player.noFallDmg = true;
            player.fireWalk = true;
            player.lavaMax += 420;
            player.wingTimeMax = 180;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WaterEssence", 8);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
            recipe.AddIngredient(null, "FrostWalkers", 1);
            recipe.AddIngredient(ItemID.FishronWings);
            recipe.AddIngredient(ItemID.WaterWalkingBoots);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
