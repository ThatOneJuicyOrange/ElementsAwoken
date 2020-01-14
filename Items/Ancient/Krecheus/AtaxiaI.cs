using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Ancient.Krecheus
{
    public class AtaxiaI : ModItem
    {
        private int attackNum = 0;
        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;

            item.damage = 19;

            item.melee = true;
            item.useTurn = true;
            item.autoReuse = true;

            item.useTime = 32;
            item.useAnimation = 32;

            item.useStyle = 1;
            item.knockBack = 5;

            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 3;

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("AtaxiaBall");
            item.shootSpeed = 6f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ataxia I");
            Tooltip.SetDefault("Every fifth attack releases lingering crystal blades");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            attackNum++;
            if (attackNum >= 5)
            {
                int numberProjectiles = Main.rand.Next(1, 3);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(7));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("AtaxiaBlade"), damage, knockBack, player.whoAmI);
                }
                attackNum = 0;
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MysticGemstone", 1);
            recipe.AddIngredient(ItemID.Bone, 25);
            recipe.AddRecipeGroup("ElementsAwoken:GoldBar", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
