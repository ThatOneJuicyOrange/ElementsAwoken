using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Projectiles.Spears;
using ElementsAwoken.Projectiles;
using ElementsAwoken.Buffs.Cooldowns;
using ElementsAwoken.Items.BossDrops.Azana;
using ElementsAwoken.Tiles.Crafting;

namespace ElementsAwoken.Items.Weapons.Melee
{
    public class Deathwarp : ModItem
    {
        public override void SetDefaults()
        {
            item.height = 60;
            item.width = 60;

            item.damage = 326;
            item.knockBack = 4.75f;

            item.melee = true;
            item.noMelee = true;
            item.useTurn = true;
            item.noUseGraphic = true;
            item.autoReuse = true;

            item.useAnimation = 18;
            item.useTime = 18;
            item.useStyle = 5;
            item.UseSound = SoundID.Item1;

            item.value = Item.buyPrice(1, 0, 0, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;

            item.maxStack = 1;

            item.shoot = ProjectileType<DeathwarpP>();
            item.shootSpeed = 14f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deathwarp");
            Tooltip.SetDefault("Fires multiple death beams to annihilate your enemies\nRight Click to activate the Deathwarp\n'Ripple through time and space'");
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2) return player.FindBuffIndex(BuffType<DeathwarpCooldown>()) == -1;
            else return player.ownedProjectileCounts[item.shoot] < 1;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                int swirlCount = 2;
                for (int l = 0; l < swirlCount; l++)
                {
                    int distance = 180;

                   Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, ProjectileType<DeathwarpSpinner>(), damage, knockBack, player.whoAmI, l * distance);
                }
                player.AddBuff(BuffType<DeathwarpCooldown>(), 1200);
            }
            else
            {
                int numberProjectiles = Main.rand.Next(2,5);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(2));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<DeathwarpLaser>(), damage, knockBack, player.whoAmI);
                }
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<DiscordantBar>(), 15);
            recipe.AddTile(TileType<ChaoticCrucible>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
