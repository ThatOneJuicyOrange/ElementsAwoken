using ElementsAwoken.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.ItemSets.Putrid
{
    public class AcridSabre : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 70;
            item.height = 70;
            
            item.damage = 68;

            item.useTime = 21;

            item.melee = true;
            item.useTurn = true;
            item.autoReuse = true;

            item.useAnimation = 12;
            item.useStyle = 1;
            item.knockBack = 5;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 7;

            item.UseSound = SoundID.Item1;
            item.shoot = ProjectileType<PutridSkull>();
            item.shootSpeed = 12f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acrid Sabre");
            Tooltip.SetDefault("Shoots acrid skulls in all directions");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float rotation = MathHelper.TwoPi;
            float numProj = Main.rand.Next(3, 7);
            float speed = 4.5f;
            for (int i = 0; i < numProj; i++)
            {
                Vector2 perturbedSpeed = (rotation / numProj * i).ToRotationVector2() * speed;
                Projectile.NewProjectile(position, perturbedSpeed, type, damage, knockBack, player.whoAmI);
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<PutridBar>(), 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
