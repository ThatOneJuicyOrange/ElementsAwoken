using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Water
{  
    public class BubbleStaff : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 120;
            item.summon = true;
            item.mana = 6;
            item.width = 26;
            item.height = 28;
            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = Item.buyPrice(0, 75, 0, 0);
            item.rare = 8;
            item.UseSound = SoundID.Item44;
            item.shoot = mod.ProjectileType("Bubble");
            item.shootSpeed = 7f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bubble Staff");
            Tooltip.SetDefault("Summons kamikaze bubbles to attack your enemies");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WaterEssence", 8);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
