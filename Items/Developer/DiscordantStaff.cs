using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Developer
{
    public class DiscordantStaff : ModItem
    {

        public override void SetDefaults()
        {
            item.damage = 300;
            item.magic = true;
            item.mana = 18;
            item.width = 54;
            item.height = 52;
            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 5;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.knockBack = 5;
            item.value = Item.buyPrice(1, 50, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("DiscordantBolt");
            item.shootSpeed = 18f;

            item.GetGlobalItem<EATooltip>().developer = true;
            item.GetGlobalItem<EARarity>().rare = 12;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discordant Staff");
            Tooltip.SetDefault("Control the discord\nShoots as fast as you can click\nOinites's developer weapon");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 1 + Main.rand.Next(3); //This defines how many projectiles to shot. 4 + Main.rand.Next(2)= 4 or 5 shots
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(7));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Pyroplasm", 50);
            recipe.AddIngredient(null, "NeutronFragment", 8);
            recipe.AddIngredient(null, "VoidAshes", 8);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 8);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
