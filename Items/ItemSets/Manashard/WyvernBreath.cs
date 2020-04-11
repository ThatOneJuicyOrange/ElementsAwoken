using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Manashard
{
    public class WyvernBreath : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 42;
            item.magic = true;
            item.width = 50;
            item.height = 50;
            item.useTime = 22;
            item.useAnimation = 22;
            Item.staff[item.type] = true;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 5;
            item.mana = 5;
            item.UseSound = SoundID.Item42;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("WyvernBreath1");
            item.shootSpeed = 13f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wyvern Breath");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Manashard", 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
