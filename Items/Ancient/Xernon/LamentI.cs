using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Ancient.Xernon
{
    public class LamentI : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;

            item.damage = 38;
            item.mana = 9;
            item.knockBack = 2;

            item.useStyle = 5;
            item.useTime = 18;
            item.useAnimation = 18;

            Item.staff[item.type] = true;
            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 3;

            item.UseSound = SoundID.Item20;
            item.shoot = mod.ProjectileType("LamentBall");
            item.shootSpeed = 9f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lament I");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MysticGemstone", 1);
            recipe.AddIngredient(ItemID.Bone, 25);
            recipe.AddRecipeGroup("ElementsAwoken:GoldBar", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
