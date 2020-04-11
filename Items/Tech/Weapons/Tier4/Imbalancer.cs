using ElementsAwoken.Items.Tech.Materials;
using ElementsAwoken.Items.Tech.Weapons.Tier1;
using ElementsAwoken.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Tech.Weapons.Tier4
{
    public class Imbalancer : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26;

            item.damage = 43;
            item.knockBack = 1f;
            item.GetGlobalItem<ItemEnergy>().energy = 8;

            item.useAnimation = 32;
            item.useTime = 32;
            item.useStyle = 5;
            item.UseSound = SoundID.Item61;

            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;

            item.shootSpeed = 8f;
            item.shoot = ProjectileType<ImbalancerMine>();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Imbalancer");
            Tooltip.SetDefault("Shoots a charged mine\nIf the mine lands on the ground it will zap nearby enemies\nIf the mine lands on an enemy it will electrify them and explode");
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-7, 0);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<Electrozzitron>(), 1);
            recipe.AddRecipeGroup("ElementsAwoken:AdamantiteBar", 6);
            recipe.AddIngredient(ItemType<GoldWire>(), 10);
            recipe.AddIngredient(ItemType<SiliconBoard>(), 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
