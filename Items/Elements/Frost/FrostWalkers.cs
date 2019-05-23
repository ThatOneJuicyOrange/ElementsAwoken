using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Frost
{
    [AutoloadEquip(EquipType.Wings)]
    public class FrostWalkers : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 32;
            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 7;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost Walkers");
            Tooltip.SetDefault("Awesome speed!\nGreater mobility on ice\nGrants immunity to fire blocks\nTemporary immunity to lava\nAllows flight and slow fall");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.accRunSpeed = 16f;
            player.rocketBoots = 3;
            player.moveSpeed += 10f;
            player.iceSkate = true;
            player.fireWalk = true;
            player.noFallDmg = true;
            player.lavaMax += 420;
            player.wingTimeMax = 180;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FrostEssence", 7);
            recipe.AddRecipeGroup("IceGroup", 5);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddIngredient(ItemID.IceSkates);
            recipe.AddIngredient(null, "SkylineWhirlwind", 1);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
