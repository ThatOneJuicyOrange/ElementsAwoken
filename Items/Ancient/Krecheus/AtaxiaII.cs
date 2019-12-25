using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Ancient.Krecheus
{
    public class AtaxiaII : ModItem
    {
        private int attackNum = 0;
        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;

            item.damage = 51;

            item.melee = true;
            item.useTurn = true;
            item.autoReuse = true;

            item.useTime = 22;
            item.useAnimation = 22;

            item.useStyle = 1;
            item.knockBack = 5;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("AtaxiaBall");
            item.shootSpeed = 9f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ataxia II");
            Tooltip.SetDefault("Randomly fires lingering crystal blades\nEvery fourth attack summons crystals to swirl around the player");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            attackNum++;
            if (attackNum >= 4)
            {
                int orbitalCount = 3;
                for (int l = 0; l < orbitalCount; l++)
                {
                    //int distance = 360 / orbitalCount;
                    int distance = Main.rand.Next(360);
                    Projectile orbital = Main.projectile[Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("AtaxiaCrystal"), damage, 0f, 0, l * distance, Main.rand.Next(3))];
                    orbital.localAI[0] = 50;
                }
                attackNum = 0;
            }
            if (Main.rand.Next(3) == 0)
            {
                int numberProjectiles = Main.rand.Next(1, 4);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                    Projectile proj = Main.projectile[Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("AtaxiaBlade"), damage, knockBack, player.whoAmI)];
                    proj.scale *= 1.3f;
                }
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AtaxiaI", 1);
            recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
            recipe.AddIngredient(ItemID.HallowedBar, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
