using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;

namespace ElementsAwoken.Items.ItemSets.Floral
{
    public class PetalPopper : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 28;
            item.ranged = true;
            item.width = 38;
            item.height = 18;
            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = Item.buyPrice(0, 2, 0, 0);
            item.rare = 3;
            item.autoReuse = true;
            item.shoot = 10;
            item.useAmmo = 97;
            item.shootSpeed = 8f;
            item.UseSound = SoundID.Item11;
            item.autoReuse = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Petal Popper");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Petal", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
