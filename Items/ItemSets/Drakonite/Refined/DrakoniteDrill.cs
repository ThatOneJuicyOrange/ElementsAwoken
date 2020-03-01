using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Drakonite.Refined
{
    public class DrakoniteDrill : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 12;

            item.damage = 40;
            item.knockBack = 6;

            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 5;

            item.melee = true;
            item.channel = true;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.pick = 200;
            item.tileBoost++;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 7;

            item.UseSound = SoundID.Item23;
            item.shoot = mod.ProjectileType("DrakoniteDrill");
            item.shootSpeed = 40f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Drakonite Drill");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RefinedDrakonite", 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}