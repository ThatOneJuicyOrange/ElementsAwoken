using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tools
{
    public class MatterManipulator : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 12;

            item.damage = 40;
            item.knockBack = 6;

            item.useTime = 4;
            item.useAnimation = 4;
            item.useStyle = 5;

            item.melee = true;
            item.channel = true;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.pick = 300;
            item.tileBoost += 30;

            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 7;

            item.UseSound = SoundID.Item23;
            item.shoot = mod.ProjectileType("MatterManipulator");
            item.shootSpeed = 40f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Matter Manipulator");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DiscordantBar", 15);
            recipe.AddIngredient(null, "ChaoticFlare", 8);
            recipe.AddIngredient(ItemID.LaserDrill, 1);
            recipe.AddTile(null, "ChaoticCrucible");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}