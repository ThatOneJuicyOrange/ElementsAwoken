using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Magic.Tomes
{
    public class LightningTome : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 22;
            item.magic = true;
            item.width = 50;
            item.height = 50;
            item.useTime = 32;
            item.useAnimation = 32;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.buyPrice(0, 3, 0, 0);
            item.rare = 2;
            item.mana = 12;
            item.UseSound = SoundID.Item8;
            item.autoReuse = false;
            item.shoot = mod.ProjectileType("Lightning");
            item.shootSpeed = 14f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning Tome");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Firefly, 8);
            recipe.AddIngredient(null, "Stardust", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
