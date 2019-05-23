using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Youtuber
{
    public class GnomesBane : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 84;
            item.height = 84;

            item.damage = 170;
            item.knockBack = 5;

            item.melee = true;
            item.useTurn = true;
            item.autoReuse = true;

            item.useTime = 28;
            item.useAnimation = 28;
            item.useStyle = 1;

            item.value = Item.sellPrice(0, 15, 0, 0);
            item.rare = 10;

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("GiantGnome");
            item.shootSpeed = 18f;

            item.GetGlobalItem<EATooltip>().youtuber = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Gnome's Bane");
            Tooltip.SetDefault("The weapon to end it all...\nGameraiders101's Youtuber item");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ClayBlock, 50);
            recipe.AddIngredient(ItemID.LunarBar, 18);
            recipe.AddIngredient(null, "Pyroplasm", 50);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
