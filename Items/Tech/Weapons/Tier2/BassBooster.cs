using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tech.Weapons.Tier2
{
    public class BassBooster : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26;

            item.damage = 22;
            item.knockBack = 1f;
            item.GetGlobalItem<ItemEnergy>().energy = 6;

            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 5;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/BassBoost");

            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 1;

            item.shootSpeed = 6f;
            item.shoot = mod.ProjectileType("BassBoost");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bass Booster");
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("GoldBar", 8);
            recipe.AddIngredient(null, "CopperWire", 10);
            recipe.AddIngredient(null, "GoldWire", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
