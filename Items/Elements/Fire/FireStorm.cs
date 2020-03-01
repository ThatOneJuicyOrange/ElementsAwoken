using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Fire
{
    public class FireStorm : ModItem
    {
        private int shotNum = 0;
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 18;

            //item.UseSound = SoundID.Item31;
            item.useStyle = 5;
            item.useAnimation = 15;
            item.useTime = 5;
            item.reuseDelay = 19;

            item.damage = 15;
            item.shootSpeed = 7.75f;

            item.ranged = true;
            item.autoReuse = true;
            item.noMelee = true;

            item.value = Item.buyPrice(0, 7, 0, 0);
            item.rare = 4;

            item.shoot = 10;
            item.useAmmo = AmmoID.Bullet;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Storm");
            Tooltip.SetDefault("Fires a burst of 3 bullets\nThe last bullet deals 50% more damage and inflicts on fire");
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 11);
            if (shotNum >= 3) shotNum = 0;
            shotNum++;
            if (shotNum == 3)
            {
                type = mod.ProjectileType("FirestormShot");
                damage = (int)(damage * 1.5f);
            }
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FireEssence", 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
