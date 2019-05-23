using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Fire
{  
    public class InfurnicesesMinionStaff : ModItem
    {
        public override void SetDefaults()
        {
            item.height = 28;
            item.width = 26;

            item.damage = 22;
            item.mana = 10;
            item.knockBack = 3;

            item.summon = true;
            item.noMelee = true;

            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 1;
            item.UseSound = SoundID.Item44;

            item.value = Item.buyPrice(0, 7, 0, 0);
            item.rare = 4;

            item.shoot = mod.ProjectileType("FireElemental");
            item.shootSpeed = 7f; 
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Spirit Scepter");
            Tooltip.SetDefault("Summons a fire spirit to serve you");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FireEssence", 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
