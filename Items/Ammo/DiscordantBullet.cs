using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Ammo
{
    public class DiscordantBullet : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 8;
            item.height = 8;

            item.damage = 32;
            item.knockBack = 1.5f;

            item.consumable = true;
            item.ranged = true;

            item.maxStack = 999;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;
            item.value = Item.sellPrice(0, 0, 50, 0);

            item.shoot = mod.ProjectileType("DiscordantBulletP");
            item.shootSpeed = 24f;
            item.ammo = AmmoID.Bullet;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaotron Bullet");
            Tooltip.SetDefault("The chaos causes them to teleport near living organisms");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MusketBall, 50);
            recipe.AddIngredient(null, "DiscordantBar", 1);
            recipe.AddTile(null, "ChaoticCrucible");
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }
    }
}
