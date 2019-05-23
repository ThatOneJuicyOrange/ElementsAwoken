using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.ToySlime
{
    public class ToyWand : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 32;

            item.damage = 13;
            item.knockBack = 1f;
            item.mana = 4;

            item.magic = true;
            item.useTurn = true;
            item.autoReuse = false;

            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;

            item.value = Item.buyPrice(0, 0, 10, 0);
            item.rare = 1;

            item.shoot = mod.ProjectileType("PuzzlePiece");
            item.shootSpeed = 7f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 1 + Main.rand.Next(2);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toy Wand");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "BrokenToys", 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
