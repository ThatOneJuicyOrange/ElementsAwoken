using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tech.Weapons.Tier4
{
    public class PlasmaRifle : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 66;
            item.height = 34;

            item.damage = 30;
            item.knockBack = 3.75f;
            item.GetGlobalItem<ItemEnergy>().energy = 4;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.useTime = 4;
            item.reuseDelay = 12;
            item.useAnimation = 6;
            item.useStyle = 5;

            item.value = Item.buyPrice(0, 15, 0, 0);
            item.rare = 5;

            item.shoot = 10;
            item.shootSpeed = 20f;
            item.useAmmo = 97;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plasma Rifle");
            Tooltip.SetDefault("Fires a burst of 3 plasma bolts");
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(SoundID.Item12, player.position);

            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("CoilRound"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CobaltBar, 8);
            recipe.AddIngredient(null, "Capacitor", 1);
            recipe.AddIngredient(null, "GoldWire", 10);
            recipe.AddIngredient(null, "SiliconBoard", 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PalladiumBar, 8);
            recipe.AddIngredient(null, "Capacitor", 1);
            recipe.AddIngredient(null, "GoldWire", 10);
            recipe.AddIngredient(null, "SiliconBoard", 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
