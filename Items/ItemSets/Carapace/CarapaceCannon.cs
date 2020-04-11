using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;
using System;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Projectiles;

namespace ElementsAwoken.Items.ItemSets.Carapace
{
    public class CarapaceCannon : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 18;

            item.damage = 10;
            item.knockBack = 7;

            item.UseSound = SoundID.Item61;
            item.useTime = 38;
            item.useAnimation = 38;
            item.useStyle = 5;

            item.noMelee = true;
            item.ranged = true;

            item.value = Item.sellPrice(0, 0, 1, 50);
            item.rare = 0;

            item.shoot = ProjectileType<CarapaceBall>();
            item.shootSpeed = 9f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Carapace Cannon");
            Tooltip.SetDefault("Shoots a rolling carapace ball");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<CarapaceItem>(), 8);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
