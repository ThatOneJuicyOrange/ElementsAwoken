using ElementsAwoken.Items.Tech.Materials;
using ElementsAwoken.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Tech.Weapons.Tier3
{
    public class Zapmaster : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26;

            item.damage = 39;
            item.knockBack = 1f;
            item.GetGlobalItem<ItemEnergy>().energy = 8;

            item.useAnimation = 32;
            item.useTime = 32;
            item.useStyle = 5;
            item.UseSound = SoundID.Item91;

            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 3;

            item.shootSpeed = 8f;
            item.shoot = ProjectileType<ZapMasterLightning>();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zapmaster 9000");
            Tooltip.SetDefault("Shoots an arcing electric bolt");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("IronBar", 8);
            recipe.AddIngredient(ItemType<GoldWire>(), 10);
            recipe.AddIngredient(ItemType<Capacitor>(), 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
