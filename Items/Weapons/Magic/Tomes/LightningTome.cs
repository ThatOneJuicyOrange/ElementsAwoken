using ElementsAwoken.Items.Materials;
using ElementsAwoken.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Weapons.Magic.Tomes
{
    public class LightningTome : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32; 

            item.damage = 22;
            item.knockBack = 2;
            item.mana = 12;

            item.useTime = 32;
            item.useAnimation = 32;
            item.useStyle = 5;

            item.noMelee = true;
            item.magic = true;
            item.autoReuse = false;

            item.value = Item.sellPrice(0, 0, 5, 0);
            item.rare = 1;

            item.UseSound = SoundID.Item8;
            item.shoot = ProjectileType<Lightning>();
            item.shootSpeed = 14f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning Tome");
            Tooltip.SetDefault("Shoots a bolt of lightning that has a chance to chain to a nearby enemy");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Firefly, 8);
            recipe.AddIngredient(ItemType<Stardust>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
