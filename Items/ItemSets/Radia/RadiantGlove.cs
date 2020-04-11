using System;
using System.Collections.Generic;
using ElementsAwoken.Projectiles;
using ElementsAwoken.Tiles.Crafting;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.ItemSets.Radia
{
    public class RadiantGlove : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 38;

            item.thrown = true;
            item.noMelee = true;
            item.noUseGraphic = true;

            item.damage = 510;
            item.knockBack = 8f;

            item.useAnimation = 12;
            item.useTime = 12;
            item.useStyle = 1;

            item.UseSound = SoundID.Item1;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;

            item.shoot = ProjectileType<RadiantStarThrown>();
            item.shootSpeed = 10f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(SoundID.Item9, position);
            return true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Amaranth Hand");
            Tooltip.SetDefault("Throws Radiant Stars");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<Radia>(), 16);
            recipe.AddTile(TileType<ChaoticCrucible>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
