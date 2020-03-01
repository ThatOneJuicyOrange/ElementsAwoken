using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Magic.Flower
{
    public class WaterleafWand : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50; 
            
            item.damage = 13;
            item.knockBack = 2;
            item.mana = 5;

            item.useTime = 28;
            item.useAnimation = 28;
            item.useStyle = 5;
            item.UseSound = SoundID.Item8;

            item.magic = true;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.value = Item.buyPrice(0, 0, 50, 0);
            item.rare = 1;

            item.shoot = mod.ProjectileType("WaterleafBall");
            item.shootSpeed = 6f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Waterleaf Wand");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Waterleaf, 2);
            recipe.AddIngredient(null, "Stardust", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
