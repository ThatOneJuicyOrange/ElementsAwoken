using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Materials
{
    public class ConcentratedPyroplasm : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 10;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Concentrated Pyroplasm");
            Tooltip.SetDefault("Used to craft Blightfire by throwing it in lava with putrid ore");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(8, 4));
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Pyroplasm>(), 2);
            recipe.AddIngredient(ModContent.ItemType<BossDrops.Volcanox.VolcanicStone>(), 1);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
