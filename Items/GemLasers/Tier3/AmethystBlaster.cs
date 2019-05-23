using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;

namespace ElementsAwoken.Items.GemLasers.Tier3
{
    public class AmethystBlaster : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.SpaceGun);
            item.ranged = true;
            item.damage = 55;
            item.useTime = 18;
            item.useAnimation = 18;
            item.knockBack = 4;
            item.scale *= 1.2f;
            item.value = Item.buyPrice(0, 30, 0, 0);
            item.rare = 7;
            item.UseSound = SoundID.Item12;
            item.shoot = mod.ProjectileType("AmethystRay");
            item.shootSpeed = 24f;
            item.autoReuse = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Amethyst Blaster");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BeetleHusk, 8);
            recipe.AddIngredient(null, "AmethystRifle", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
