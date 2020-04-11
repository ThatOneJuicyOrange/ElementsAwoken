using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Tiles.Crafting;
using ElementsAwoken.Projectiles;

namespace ElementsAwoken.Items.ItemSets.Radia
{
    public class RadiantMortar : ModItem
    {
        public int shootTimer = 120;
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;

            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiant Mortar");
            Tooltip.SetDefault("Lights up the skies with high damaging mortars\nDisable visuals to turn off this effect\n2% increased movement speed");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            shootTimer--;
            if (player.whoAmI == Main.myPlayer && !hideVisual && shootTimer <= 0)
            {
                Projectile.NewProjectile(player.Center, new Vector2(Main.rand.NextFloat(-6, 6), Main.rand.NextFloat(-14, -10)), ProjectileType<RadiantStarMortar>(), 750, 0f, player.whoAmI);
                shootTimer = Main.rand.Next(10, 120);
            }
            player.accRunSpeed *= 1.02f;
            player.moveSpeed *= 1.02f;
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
