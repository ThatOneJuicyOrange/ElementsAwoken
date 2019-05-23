using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Magic.Flower
{
    public class DeathweedWand : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 6;
            item.magic = true;
            item.width = 50;
            item.height = 50;
            item.useTime = 12;
            item.useAnimation = 12;
            Item.staff[item.type] = true;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.buyPrice(0, 0, 50, 0);
            item.rare = 1;
            item.mana = 5;
            item.UseSound = SoundID.Item8;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("DeathweedBall");
            item.shootSpeed = 6f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deathweed Wand");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Deathweed, 6);
            recipe.AddIngredient(null, "Stardust", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
