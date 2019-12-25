using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Ancient.Krecheus
{
    public class AtaxiaIII : ModItem
    {
        private int attackNum = 0;
        private float altCounter = 0;
        private int altResetTimer = 0;
        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;

            item.damage = 140;

            item.melee = true;
            item.useTurn = true;
            item.autoReuse = true;

            item.useTime = 18;
            item.useAnimation = 18;

            item.useStyle = 1;
            item.knockBack = 5;

            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 10;

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("AtaxiaBall");
            item.shootSpeed = 18f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ataxia III");
            Tooltip.SetDefault("Randomly fires lingering crystal blades\nEvery fourth attack summons crystals to swirl around the player\nHold right click to fire a charged bolt that gets stronger the longer its held");
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override void HoldItem(Player player)
        {
            altResetTimer--;
            if (player.altFunctionUse == 2)
            {
                if (altCounter < 300) altCounter++;
                altResetTimer = 20;
                if (altCounter > 0)
                {
                    item.useTime = (int)MathHelper.Lerp(22, 7, altCounter / 300f);
                    item.useAnimation = (int)MathHelper.Lerp(22, 7, altCounter / 300f);
                }
            }
            else
            {
                if (altResetTimer <= 0)
                {
                    altCounter = 0;
                    item.useTime = 22;
                    item.useAnimation = 22;
                }
            }

        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse != 2)
            {
                altResetTimer = 0;
                int numberProjectiles = Main.rand.Next(1, 3);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(12));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                }

                attackNum++;
                if (attackNum >= 4)
                {
                    for (int l = 0; l < 5; l++)
                    {
                        int distance = Main.rand.Next(360);
                        Projectile orbital = Main.projectile[Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("AtaxiaCrystal"), damage, 0f, 0, l * distance, l >= 3 ? Main.rand.Next(3, 5) : Main.rand.Next(3))];
                        orbital.localAI[0] = l >= 3 ? 75 : 50;
                    }
                    attackNum = 0;
                }
                if (Main.rand.Next(3) == 0)
                {
                    int numberProjectiles2 = Main.rand.Next(1, 4);
                    for (int i = 0; i < numberProjectiles2; i++)
                    {
                        Vector2 speed = new Vector2(speedX, speedY) * 1.5f;
                        Vector2 perturbedSpeed = speed.RotatedByRandom(MathHelper.ToRadians(15));
                        perturbedSpeed *= Main.rand.NextFloat(1f, 1.3f);
                        Projectile proj = Main.projectile[Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("AtaxiaBlade"), damage, knockBack, player.whoAmI)];
                        proj.scale *= 1.4f;
                    }
                }
            }
            else
            {
                Projectile proj = Main.projectile[Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("AtaxiaBolt"), (int)MathHelper.Lerp(damage * 0.3f, damage * 1.7f, altCounter / 300), knockBack, player.whoAmI,altCounter)];
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AtaxiaII", 1);
            recipe.AddIngredient(ItemID.FragmentNebula, 5);
            recipe.AddIngredient(ItemID.FragmentSolar, 5);
            recipe.AddIngredient(ItemID.FragmentStardust, 5);
            recipe.AddIngredient(ItemID.FragmentVortex, 5);
            recipe.AddIngredient(ItemID.LunarBar, 18);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
