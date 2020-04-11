using ElementsAwoken.Items.Materials;
using ElementsAwoken.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Weapons.Magic.Tomes
{
    public class IceSpikeTome : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;

            item.damage = 18;
            item.knockBack = 2;
            item.mana = 6;

            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 5;
            item.UseSound = SoundID.Item42;

            item.noMelee = true;
            item.magic = true;
            item.autoReuse = false;

            item.value = Item.sellPrice(0, 0, 5, 0);
            item.rare = 1;

            item.shoot = ProjectileType<Icicle>();
            item.shootSpeed = 9f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Spike Tome");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IceBlock, 20);
            recipe.AddIngredient(ItemType<Stardust>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
