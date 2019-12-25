using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;

namespace ElementsAwoken.Items.GemLasers.Tier1
{
    public class SapphirePistol : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 24;

            item.ranged = true;
            item.autoReuse = true;

            item.damage = 17;
            item.knockBack = 4;

            item.UseSound = SoundID.Item12;
            item.useTime = 18;
            item.useAnimation = 18;

            item.value = Item.buyPrice(0, 1, 0, 0);
            item.rare = 1;

            item.shoot = mod.ProjectileType("SapphireLaser");
            item.shootSpeed = 24f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sapphire Pistol");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MeteoriteBar, 8);
            recipe.AddIngredient(ItemID.Sapphire, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
