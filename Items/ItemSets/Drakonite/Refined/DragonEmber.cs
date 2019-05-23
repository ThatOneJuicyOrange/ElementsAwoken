using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Drakonite.Refined
{
    public class DragonEmber : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;

            item.damage = 55;
            item.knockBack = 2;

            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;
            Item.staff[item.type] = true;

            item.mana = 5;
            item.magic = true;
            item.noMelee = true;

            item.value = Item.buyPrice(0, 7, 50, 0);
            item.rare = 7;

            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("DragonEmberBall");
            item.shootSpeed = 15f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Ember");
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
