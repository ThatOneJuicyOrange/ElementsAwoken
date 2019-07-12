using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Blightfire
{
    public class BlightLobber : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 32;

            item.damage = 260;
            item.mana = 12;
            item.knockBack = 2;

            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = 5;
            item.UseSound = SoundID.Item20;

            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 7, 50, 0);
            item.rare = 11;

            item.shoot = mod.ProjectileType("BlightRing");
            item.shootSpeed = 14f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blight Lobber");
            Tooltip.SetDefault("Fires a bouncing Blight Ring\nRight click to throw fast returning rings");
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useTime = 5;
                item.useAnimation = 10;
                item.knockBack = 1;
            }
            else
            {
                item.useTime = 16;
                item.useAnimation = 16;
                item.knockBack = 8;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                type = mod.ProjectileType("BlightRang");
                damage = (int)(damage * 0.8f);
            }
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Blightfire", 10);
            recipe.AddIngredient(ItemID.LunarBar, 2);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
