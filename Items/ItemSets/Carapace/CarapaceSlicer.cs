using System;
using System.Collections.Generic;
using ElementsAwoken.Projectiles.Thrown;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.ItemSets.Carapace
{
    public class CarapaceSlicer : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 38;  
            item.height = 38;

            item.damage = 11;
            item.knockBack = 1f;

            item.thrown = true;
            item.noMelee = true;
            item.noUseGraphic = true;

            item.useAnimation = 14;
            item.useTime = 14;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;

            item.value = Item.sellPrice(0, 0, 1, 50);
            item.rare = 0;

            item.shoot = ProjectileType<CarapaceSlicerP>();
            item.shootSpeed = 7f;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[ProjectileType<CarapaceSlicerP>()] >= 2)
            {
                return false;
            }
            return true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Carapace Slicer");
            Tooltip.SetDefault("Throws a boomrang that can stick to enemies");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<CarapaceItem>(), 8);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
