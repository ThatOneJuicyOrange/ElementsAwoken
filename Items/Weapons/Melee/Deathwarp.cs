using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ElementsAwoken.Items.Weapons.Melee
{
    public class Deathwarp : ModItem
    {
        public override void SetDefaults()
        {
            item.height = 60;
            item.width = 60;

            item.damage = 420;
            item.knockBack = 4.75f;

            item.melee = true;
            item.noMelee = true;
            item.useTurn = true;
            item.noUseGraphic = true;
            item.autoReuse = true;

            item.useAnimation = 12;
            item.useTime = 12;
            item.useStyle = 5;
            item.UseSound = SoundID.Item1;

            item.value = Item.buyPrice(1, 0, 0, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;

            item.maxStack = 1;

            item.shoot = mod.ProjectileType("DeathwarpP");
            item.shootSpeed = 19f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deathwarp");
            Tooltip.SetDefault("Ripple through time and space\nRight click to activate the Deathwarp");
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (player.FindBuffIndex(mod.BuffType("DeathwarpCooldown")) == -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return player.ownedProjectileCounts[item.shoot] < 1;
            }
            //return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("DeathwarpCenter"), 120, 5f, Main.myPlayer, 0f, 0f);
                player.AddBuff(mod.BuffType("DeathwarpCooldown"), 1200);
            }
            else
            {
                int numberProjectiles = 2 + Main.rand.Next(4);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(2));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("DeathwarpLaser"), damage, knockBack, player.whoAmI);
                }
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DiscordantBar", 15);
            recipe.AddTile(null, "ChaoticCrucible");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
