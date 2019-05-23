using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Frost
{
    public class Icicle : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 52;
            item.melee = true;
            item.width = 60;
            item.height = 60;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 6;
            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 7;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("IcicleProj");
            item.shootSpeed = 8f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Icicle");
            Tooltip.SetDefault("Fires 3 icicles that impale enemies\nRight click to rapidly stab the icicle and fire a volley of icy bolts");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FrostEssence", 7);
            recipe.AddRecipeGroup("IceGroup", 5);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                item.useStyle = 3;
                item.damage = 45;
                item.useTime = 5;
                item.useAnimation = 5;
                int num1 = damage / 2;
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("IcicleShot"), num1, knockBack, player.whoAmI, 0.0f, 0.0f);
                return false;
            }
            else
            {
                item.useStyle = 1;
                item.useTime = 20;
                item.damage = 52;
                item.useAnimation = 20;
                float numberProjectiles = 3;
                float rotation = MathHelper.ToRadians(4);
                position += Vector2.Normalize(new Vector2(speedX, speedY)) * 2;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                }
                return false;
            }
        }
    }
}
