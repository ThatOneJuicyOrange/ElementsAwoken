using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Summon
{
    public class Exliture : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 12;
            item.summon = true;
            item.width = 40;
            item.height = 40;
            item.useTime = 28;
            item.useTurn = true;
            item.useAnimation = 28;
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = Item.buyPrice(0, 2, 0, 0);
            item.rare = 1;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("ExlitureEye");
            item.shootSpeed = 4f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Exliture");
            Tooltip.SetDefault("Shoots little eyes. Also serves as a sword");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Lens, 6);
            recipe.AddRecipeGroup("IronBar", 8);
            recipe.AddRecipeGroup("SilverSword");
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
