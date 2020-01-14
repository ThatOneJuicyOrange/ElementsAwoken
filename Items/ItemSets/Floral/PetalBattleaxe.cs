using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Floral
{
    public class PetalBattleaxe : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 60; 
            
            item.damage = 45;
            item.crit = 4;

            item.melee = true;
            item.autoReuse = true;
            item.useTurn = true;
            item.useStyle = 1;

            item.UseSound = SoundID.Item1;
            item.useTime = 30;
            item.useAnimation = 30;
            item.knockBack = 12;

            item.value = Item.buyPrice(0, 1, 0, 0);
            item.rare = 1;

            item.scale *= 1.3f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Petal Battleaxe");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Petal", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
