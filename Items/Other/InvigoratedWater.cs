using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Other
{
    public class InvigoratedWater : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 38;

            item.thrown = true;
            item.noMelee = true;
            item.consumable = true;
            item.noUseGraphic = true;

            item.knockBack = 2f;
            item.maxStack = 999;

            item.useAnimation = 20;
            item.useTime = 20;
            item.useStyle = 1;

            item.UseSound = SoundID.Item1;

            item.value = Item.buyPrice(0, 0, 1, 0);

            item.shoot = ModContent.ProjectileType<Projectiles.Thrown.InvigoratedWaterP>();
            item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Invigorated Water");
            Tooltip.SetDefault("Does not work during the Rifting");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, 1, 0, player.whoAmI);
            return false;
        }
        public override void OnCraft(Recipe recipe)
        {
            Player player = Main.LocalPlayer;
            if (player.CountItem(item.type) > 10 || item.stack > 10 || (player.trashItem.type == item.type && player.trashItem.stack > 10))
            {
                
                item.stack--;
                Main.stackDelay = 20;
                player.QuickSpawnItem(ItemID.Bottle);
                if (item.stack <= 0) item.TurnToAir();
                Main.NewText("The water seemingly evaporates as it enters your bottle...");

                for (int i = 0; i < 20; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, 16)];
                    dust.velocity.Y = Main.rand.NextFloat(-4,-2f);
                    dust.velocity.X *= 0.4f;
                    dust.fadeIn = 2f;
                    dust.noGravity = true;
                }
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bottle, 1);
            recipe.AddTile(ModContent.TileType<Tiles.Objects.InvigorationFountain>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
