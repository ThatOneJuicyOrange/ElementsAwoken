using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;

namespace ElementsAwoken.Items.GemLasers.Tier2
{
    public class AmberRifle : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 54;
            item.height = 28;

            item.ranged = true;
            item.autoReuse = true;

            item.damage = 33;
            item.knockBack = 4;

            item.UseSound = SoundID.Item12;
            item.useTime = 13;
            item.useAnimation = 13;

            item.value = Item.buyPrice(0, 10, 0, 0);
            item.rare = 4;

            item.shoot = mod.ProjectileType("AmberLaser");
            item.shootSpeed = 24f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Amber Rifle");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedBar, 8);
            recipe.AddIngredient(null, "AmberPistol", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
