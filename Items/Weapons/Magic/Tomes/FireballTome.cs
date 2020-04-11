using ElementsAwoken.Items.Materials;
using ElementsAwoken.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Weapons.Magic.Tomes
{
    public class FireballTome : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;

            item.useTime = 32;
            item.useAnimation = 32;
            item.useStyle = 5;

            item.damage = 18;
            item.mana = 5;

            item.noMelee = true;
            item.magic = true;
            item.autoReuse = false;

            item.knockBack = 2;

            item.value = Item.sellPrice(0, 0, 10, 0);
            item.rare = 1;

            item.UseSound = SoundID.Item42;
            item.shoot = ProjectileType<FireballP>();
            item.shootSpeed = 9f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fireball Tome");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Obsidian, 2);
            recipe.AddIngredient(ItemType<Stardust>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
