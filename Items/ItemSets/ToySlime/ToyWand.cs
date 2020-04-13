using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.ToySlime
{
    public class ToyWand : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 32;

            item.damage = 26;
            item.knockBack = 3f;
            item.mana = 4;

            item.magic = true;
            item.useTurn = true;
            item.autoReuse = true;

            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 1;
            item.UseSound = SoundID.Item8;

            item.value = Item.sellPrice(0, 0, 75, 0);
            item.rare = 3;

            item.shoot = mod.ProjectileType("PuzzlePiece");
            item.shootSpeed = 12f;
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
            Tooltip.SetDefault("Shoots a cluster of puzzle pieces");
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
