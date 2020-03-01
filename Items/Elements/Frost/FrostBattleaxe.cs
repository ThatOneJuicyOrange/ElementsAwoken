using ElementsAwoken.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Elements.Frost
{
    public class FrostBattleaxe : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 60;

            item.damage = 70;
            item.knockBack = 6;

            item.autoReuse = true;
            item.melee = true;  
            
            item.useTime = 40;   
            item.useAnimation = 40;     
            item.useStyle = 1;

            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 7;

            item.UseSound = SoundID.Item1;  
            item.shoot = ProjectileType<IceWaveCheck>();
            item.shootSpeed = 6f;

            item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hoarfrost Crusher");
            Tooltip.SetDefault("Creates a shockwave upon impact with the ground\nInstaFiz's donator item");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        { 
                Projectile.NewProjectile(position.X, position.Y, 0, 0, type, damage, knockBack, player.whoAmI);
            
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FrostEssence", 7);
            recipe.AddRecipeGroup("ElementsAwoken:IceGroup", 5);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
