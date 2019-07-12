using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.Eoite
{
    public class EoitesBlade : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 60;

            item.damage = 190;
            item.knockBack = 6;

            item.autoReuse = true;
            item.melee = true;  
            
            item.useTime = 13;   
            item.useAnimation = 13;     
            item.useStyle = 1;

            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 10;

            item.UseSound = SoundID.Item1;  
            item.shoot = mod.ProjectileType("EoiteWave");
            item.shootSpeed = 6f;

            item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("MurrAwAve");
            Tooltip.SetDefault("When in Doubt, go for speed - Weeb Streamer\nEoite's donator item");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float numberProjectiles = 2;
            float rotation = MathHelper.ToRadians(2);
            position += Vector2.Normalize(new Vector2(speedX, speedY)) * 2;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 2f;
                int num1 = Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NeutronFragment", 6);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(ItemID.Amethyst, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
