using ElementsAwoken.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Tools
{
    public class AcidFlask : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;

            item.maxStack = 30;

            item.useAnimation = 30;
            item.useTime = 30; 
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;

            item.noUseGraphic = true;
            item.noMelee = true;
            item.consumable = true;

            item.value = Item.buyPrice(0, 0, 20, 0);
            item.rare = 1;

            item.shootSpeed = 5f;
            item.shoot = ProjectileType<Projectiles.Explosives.AcidFlaskP>();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acid Flask");
            Tooltip.SetDefault("A small explosion that creates tile destroying acid");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<AcidDrop>());
            recipe.AddIngredient(ItemID.Bottle);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
