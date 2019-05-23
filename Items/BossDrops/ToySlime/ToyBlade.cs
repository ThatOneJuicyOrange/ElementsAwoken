using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.ToySlime
{
    public class ToyBlade : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 13;     
            item.melee = true;     
            item.width = 60;   
            item.height = 60;    
            item.useTime = 18;   
            item.useAnimation = 18;     
            item.useStyle = 1;          
            item.knockBack = 6;  
            item.value = Item.buyPrice(0, 0, 10, 0);
            item.rare = 1;    
            item.UseSound = SoundID.Item1;  
            item.autoReuse = false; 
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toy Blade");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "BrokenToys", 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
