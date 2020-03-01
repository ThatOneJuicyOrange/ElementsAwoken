using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Magic.Flower
{
    public class BlinkrootWand : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;
            
            item.damage = 13;
            item.knockBack = 2;
            item.mana = 5;

            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.UseSound = SoundID.Item8;

            item.magic = true;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.value = Item.buyPrice(0, 0, 50, 0);
            item.rare = 1;

            item.shoot = mod.ProjectileType("BlinkrootDirt");
            item.shootSpeed = 6f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blinkroot Wand");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Blinkroot, 2);
            recipe.AddIngredient(null, "Stardust", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
