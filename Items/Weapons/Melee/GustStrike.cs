using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Melee
{
    public class GustStrike : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;

            item.damage = 22;

            item.melee = true;
            item.useTurn = true;
            item.autoReuse = true;

            item.useTime = 32;
            item.useAnimation = 32;

            item.useStyle = 1;
            item.knockBack = 5;

            item.value = Item.sellPrice(0, 1, 50, 0);
            item.rare = 4;

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("LightningBolt");
            item.shootSpeed = 8f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gust Strike");
            Tooltip.SetDefault("Shoots out 2 lightning bolts and a wind blast");
        }


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float numberProjectiles = 2;
            float rotation = MathHelper.ToRadians(5);
            position += Vector2.Normalize(new Vector2(speedX, speedY)) * 1f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("CloudBlast"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FallenStar, 6);
            recipe.AddIngredient(ItemID.Obsidian, 18);
            recipe.AddIngredient(ItemID.Diamond, 3);
            recipe.AddIngredient(ItemID.EnchantedSword, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
