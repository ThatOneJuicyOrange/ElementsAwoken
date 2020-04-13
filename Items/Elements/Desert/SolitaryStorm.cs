using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Desert
{
    public class SolitaryStorm : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 18;
            item.melee = true;
            item.width = 60;
            item.height = 60;
            item.useTime = 24;
            item.useAnimation = 24;
            item.useStyle = 1;
            item.knockBack = 6;
            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("Sandnado");
            item.shootSpeed = 10f;
            item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Solitary Storm");
            Tooltip.SetDefault("Shoots a sand tornado at your enemies");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DesertEssence", 4);
            recipe.AddRecipeGroup("ElementsAwoken:SandGroup", 25);
            recipe.AddRecipeGroup("ElementsAwoken:SandstoneGroup", 10);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
