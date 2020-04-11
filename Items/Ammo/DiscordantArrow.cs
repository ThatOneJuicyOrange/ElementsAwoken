using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Ammo
{
    public class DiscordantArrow : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 8;
            item.height = 8;

            item.damage = 28;
            item.knockBack = 1.5f;

            item.consumable = true;
            item.ranged = true;

            item.maxStack = 999;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;
            item.value = Item.sellPrice(0, 0, 50, 0);

            item.shoot = mod.ProjectileType("DiscordantArrow");
            item.shootSpeed = 34f;
            item.ammo = AmmoID.Arrow;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discordant Arrow");
            Tooltip.SetDefault("Leaves a trail of arrows from the chaos dimension\nSome of these arrows phase through dimensions and shoot at enemies\nAlso inflicts 'Chaotic Necrosis'");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WoodenArrow, 50);
            recipe.AddIngredient(null, "DiscordantBar", 1);
            recipe.AddTile(null, "ChaoticCrucible");
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }
    }
}
