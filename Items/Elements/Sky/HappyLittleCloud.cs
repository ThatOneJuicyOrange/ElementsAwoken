using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Sky
{  
    public class HappyLittleCloud : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 28;

            item.damage = 30;
            item.mana = 10;
            item.knockBack = 3;

            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 1;
            item.UseSound = SoundID.Item44;

            item.noMelee = true;
            item.summon = true;

            item.value = Item.buyPrice(0, 25, 0, 0);
            item.rare = 6;

            item.shoot = mod.ProjectileType("HappyCloud");
            item.shootSpeed = 7f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Happy Little Cloud Staff");
            Tooltip.SetDefault("Summons a Happy Little Cloud to protect you from the happy little mistakes");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SkyEssence", 6);
            recipe.AddIngredient(ItemID.Cloud, 25);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
