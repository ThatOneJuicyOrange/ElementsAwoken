using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;

namespace ElementsAwoken.Items.GemLasers.Tier3
{
    public class RubyBlaster : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 54;
            item.height = 28;

            item.ranged = true;
            item.autoReuse = true;

            item.damage = 55;
            item.knockBack = 4;

            item.UseSound = SoundID.Item12;
            item.useTime = 18;
            item.useAnimation = 18;

            item.value = Item.buyPrice(0, 30, 0, 0);
            item.rare = 7;

            item.shoot = mod.ProjectileType("RubyRay");
            item.shootSpeed = 24f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ruby Blaster");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BeetleHusk, 8);
            recipe.AddIngredient(null, "RubyRifle", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
