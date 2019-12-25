using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Puff
{
    public class FluffyStaff : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 14;
            item.mana = 10;
            item.width = 26;
            item.height = 28;
            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = Item.buyPrice(0, 0, 2, 0);
            item.rare = 1;
            item.UseSound = SoundID.Item44;
            item.shoot = mod.ProjectileType("BabyPuff");
            item.shootSpeed = 7f;
            item.summon = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fluffy Staff");
            Tooltip.SetDefault("I wonder what it does?");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 25);
            recipe.AddIngredient(null, "Puffball", 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
