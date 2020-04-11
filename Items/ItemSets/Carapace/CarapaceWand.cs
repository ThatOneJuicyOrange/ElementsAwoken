using System;
using System.Collections.Generic;
using ElementsAwoken.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.ItemSets.Carapace
{
    public class CarapaceWand : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;

            item.damage = 12;
            item.knockBack = 5;
            item.mana = 12;

            item.UseSound = SoundID.Item8;
            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 5;

            Item.staff[item.type] = true;     
            item.noMelee = true;
            item.autoReuse = true;
            item.useTurn = true;
            item.magic = true;

            item.value = Item.sellPrice(0, 0, 1, 50);
            item.rare = 0;

            item.shoot = ProjectileType<CarapaceShard>();
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Carapace Wand");
            Tooltip.SetDefault("Shoots a carapace shard that deals additional damage to enemies it hits");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<CarapaceItem>(), 8);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
