using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Desert
{  
    public class StaffOfTheSandstorm : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 28;
            
            item.damage = 14;
            item.knockBack = 3;
            item.mana = 10;

            item.summon = true;
            item.noMelee = true;

            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 1;
            item.UseSound = SoundID.Item44;

            item.value = Item.buyPrice(0, 5, 0, 0);
            item.rare = 3;

            item.shoot = mod.ProjectileType("MiniatureSandstorm");
            item.shootSpeed = 7f;
            item.rare = 3;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Staff of the Sandstorm");
            Tooltip.SetDefault("Summons a miniature sand storm to fight for you");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DesertEssence", 4);
            recipe.AddRecipeGroup("ElementsAwoken:SandGroup", 25);
            recipe.AddRecipeGroup("ElementsAwoken:SandstoneGroup", 10);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
