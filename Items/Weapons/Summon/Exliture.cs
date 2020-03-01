using ElementsAwoken.Projectiles;
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
            item.width = 40;
            item.height = 40; 
            
            item.damage = 12;
            item.knockBack = 5;

            item.UseSound = SoundID.Item1;
            item.useTime = 28;
            item.useAnimation = 28;
            item.useStyle = 1;

            item.useTurn = true;
            item.summon = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 0, 20, 0);
            item.rare = 1;

            item.shoot = ModContent.ProjectileType<ExlitureEye>();
            item.shootSpeed = 4f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Exliture");
            Tooltip.SetDefault("Shoots eyes that attack enemies\nAlso serves as a sword");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Lens, 6);
            recipe.AddRecipeGroup("IronBar", 8);
            recipe.AddRecipeGroup("ElementsAwoken:SilverSword");
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
